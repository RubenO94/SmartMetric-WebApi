using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace SmartMetric.WebAPI.Filters.ActionFilter
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SkipPermissionAuthorizationAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionRequiredAttribute : Attribute
    {
        public WindowType Window { get; set; }
        public PermissionType Permission { get; set; }
        public PermissionRequiredAttribute(WindowType window, PermissionType permission)
        {
            Window = window;
            Permission = permission;
        }

        public int GetPermissionId()
        {
            return (int)Window + (int)Permission;
        }
    }

    public class PermissionFilter : IAsyncActionFilter
    {
        private readonly ISmartTimeRepository _smartTimeRepository;
        private readonly IJwtService _jwtService;

        public PermissionFilter(ISmartTimeRepository smartTimeRepository, IJwtService jwtService)
        {
            _smartTimeRepository = smartTimeRepository;
            _jwtService = jwtService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Lógica de autorização aqui

            bool skipAuthorization = context.ActionDescriptor.EndpointMetadata
                .Any(em => em is SkipPermissionAuthorizationAttribute);

            if (skipAuthorization)
            {
                await next();
                return;
            }

            var permissionAttribute = (PermissionRequiredAttribute)context.ActionDescriptor
                .EndpointMetadata.FirstOrDefault(em => em is PermissionRequiredAttribute);

            if (permissionAttribute == null)
            {
                await next();
                return;
            }

            var requiredPermission = permissionAttribute.GetPermissionId();

            var token = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult(new { Error = "Access Token is missing" });
                return;
            }

            // remove "Bearer " prefix
            token = token.ToString().Replace("Bearer ", "");
            var principal = _jwtService.GetPrincipalFromJwtToken(token);
            if (principal == null)
            {
                context.Result = new UnauthorizedObjectResult(new { Error = "Invalid Access Token" });
                return;
            }

            var userName = principal.FindFirst("name");
            var userIdClaim = principal.FindFirst("UserId");
            var userProfileIdClaim = principal.FindFirst("UserProfileId");

            if(userName?.Value == "Administrador")
            {
                await next();
                return;
            }

            if (userIdClaim == null || userProfileIdClaim == null)
            {
                context.Result = new UnauthorizedObjectResult(new { Error = "Invalid Access Token" });
                return;
            }

            var userProfileId = userProfileIdClaim.Value;

            if (!int.TryParse(userProfileId, out int profileId))
            {
                context.Result = new UnauthorizedObjectResult(new { Error = "Invalid Access Token" });
                return;
            }

            var userWindowPermissions = await _smartTimeRepository.GetProfileWindowsByProfileId(profileId);

            var hasPermission = userWindowPermissions.Contains(requiredPermission);

            if (!hasPermission)
            {
                context.Result = new ObjectResult(new { Error = "Authenticated user is not authorized." }) { StatusCode = 403 }; // Resposta HTTP 403 (Forbidden)
                return;
            }

            await next();
        }
    }
}
