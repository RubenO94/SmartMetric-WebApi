using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class UserProfileDTOResponse
    {
        public int? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public int? EmployeeId { get; set; }
        public string? ProfileDescription { get; set;}
        public ProfileType ProfileType { get; set; }
        public List<DepartmentDTOResponse>? Departments { get; set; }
        public List<WindowPermissionDTO>? Permissions { get; set; }
    }
}
