using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class WindowPermissionDTO
    {
        public int WindowId { get; set; }
        public WindowType WindowType { get; set; }
        public List<PermissionDTO>? Permissions { get; set; }

    }
}
