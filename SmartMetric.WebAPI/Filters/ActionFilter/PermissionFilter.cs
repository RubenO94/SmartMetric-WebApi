using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.ServicesContracts;
using System;
using System.Linq;

namespace SmartMetric.WebAPI.Filters.ActionFilter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SkipPermissionAuthorizationAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionRequiredAttribute : Attribute
    {
        public int PermissionId {  get; set; } 
        public PermissionRequiredAttribute(int permissionId) 
        {
            PermissionId = permissionId;
        }
    }

    public class PermissionFilter : ActionFilterAttribute
    {
        private readonly ISmartTimeRepository _smartTimeRepository;
        private readonly IJwtService _jwtService;

        public PermissionFilter(ISmartTimeRepository smartTimeRepository, IJwtService jwtService)
        {
            _smartTimeRepository = smartTimeRepository;
            _jwtService = jwtService;
        }

        public override async void OnActionExecuting(ActionExecutingContext context)
        {
            // Lógica de autorização aqui

            bool skipAuthorization = context.ActionDescriptor.EndpointMetadata
                .Any(em => em is SkipPermissionAuthorizationAttribute);

            if (skipAuthorization)
            {
                return;
            }

            var permissionAttribute = (PermissionRequiredAttribute)context.ActionDescriptor
                .EndpointMetadata.FirstOrDefault(em => em is PermissionRequiredAttribute);

            if (permissionAttribute == null)
            {
                return;
            }

            var requiredPermission = permissionAttribute.PermissionId;

            var token = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var principal = _jwtService.GetPrincipalFromJwtToken(token);
            if (principal == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userIdClaim = principal.FindFirst("UserId");
            var userProfileIdClaim = principal.FindFirst("UserProfileId");

            if (userIdClaim == null || userProfileIdClaim == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userProfileId = userProfileIdClaim.Value;

            if (!int.TryParse(userProfileId, out int profileId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userWindowPermissions = await _smartTimeRepository.GetProfileWindowsByProfileId(profileId);

            var hasPermission = userWindowPermissions.Contains(requiredPermission);

            if (!hasPermission)
            {
                context.Result = new ForbidResult(); // Resposta HTTP 403 (Forbidden)
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
