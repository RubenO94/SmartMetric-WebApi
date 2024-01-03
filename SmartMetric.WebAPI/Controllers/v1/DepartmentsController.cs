using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.WebAPI.Filters.ActionFilter;
using SmartMetric.WebAPI.Filters.AutorizationFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class DepartmentsController : CustomBaseController
    {
        private readonly ISmartTimeService _smartTimeService;

        public DepartmentsController(ISmartTimeService smartTimeService)
        {
            _smartTimeService = smartTimeService;
        }

        [HttpGet]
        //[PermissionRequired(23124001)]
        public async Task<IActionResult> GetAllDepartmentsByProfileId(int page = 1, int pageSize = 20)
        {
            if (HttpContext.Items.TryGetValue("UserProfileId", out var userProfileIdObj) && userProfileIdObj is int userProfileId)
            {
                var departments = await _smartTimeService.GetDepartmentsByProfileId(userProfileId, page, pageSize);

                return Ok(departments);
            }

            throw new ArgumentException("User does not have an associated profile");
        }

        [HttpGet("{departmentId}/Employees")]
        //[PermissionRequired(23124001)]
        public async Task<IActionResult> GetDepartmentEmployees(int departmentId)
        {
            if (HttpContext.Items.TryGetValue("UserProfileId", out var userProfileIdObj) && userProfileIdObj is int userProfileId)
            {
                var profileDepartments = await _smartTimeService.GetDepartmentsByProfileId(userProfileId, pageSize: int.MaxValue);

                if (!profileDepartments.Data!.Any(temp => temp.DepartmentId == departmentId)) throw new ArgumentException("This user does not have access to the department given in the parameter or the department id does not exist", nameof(departmentId));

                var response = await _smartTimeService.GetEmployeesByDepartmentId(departmentId);
                return Ok(response);
            }

            throw new ArgumentException("User does not have an associated profile");
        }
    }
}
