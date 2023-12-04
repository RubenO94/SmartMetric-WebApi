﻿using SmartMetric.Core.DTO;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Helpers
{
    public static class WindowPermissionHelper
    {
        private static readonly List<WindowPermissionDTO> StaticPermissions = new List<WindowPermissionDTO>()
        {
                new WindowPermissionDTO
                {
                    WindowId = 23121000,
                    WindowType = WindowType.Forms,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23121001, PermissionType = "canCreate"},
                        new PermissionDTO {PermissionId = 23121002, PermissionType = "canRead"},
                        new PermissionDTO {PermissionId = 23121003, PermissionType = "canUpdate"},
                        new PermissionDTO {PermissionId = 23121004, PermissionType = "canDelete"}
                    }
                },
                new WindowPermissionDTO
                {
                    WindowId = 23122000,
                    WindowType = WindowType.Reviews,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23122001, PermissionType = "canCreate"},
                        new PermissionDTO {PermissionId = 23122002, PermissionType = "canRead"},
                        new PermissionDTO {PermissionId = 23122003, PermissionType = "canUpdate" },
                        new PermissionDTO {PermissionId = 23122004, PermissionType = "canDelete"}
                    }
                },
                new WindowPermissionDTO
                {
                    WindowId = 23123000,
                    WindowType = WindowType.Statistics,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23123001, PermissionType = "canCreate"},
                        new PermissionDTO {PermissionId = 23123002, PermissionType = "canRead"},
                        new PermissionDTO {PermissionId = 23123003, PermissionType = "canUpdate"},
                        new PermissionDTO {PermissionId = 23123004, PermissionType = "canDelete"}
                    }
                },
                new WindowPermissionDTO
                {
                    WindowId = 23124000,
                    WindowType = WindowType.AdminSettings,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23124001, PermissionType = "canCreate"},
                        new PermissionDTO {PermissionId = 23124002, PermissionType = "canRead" },
                        new PermissionDTO {PermissionId = 23124003, PermissionType = "canUpdate"},
                        new PermissionDTO {PermissionId = 23124004, PermissionType = "canDelete"}
                    }
                }
            };
        public static List<WindowPermissionDTO> CheckProfilePermissions(List<int?> profilePermissionIds)
        {


            foreach (var windowPermission in StaticPermissions)
            {
                foreach (var permission in windowPermission.Permissions!)
                {
                    // Define HasPermission como true se o PermissionId estiver presente na lista de permissões do perfil
                    permission.HasPermission = profilePermissionIds.Contains(permission.PermissionId);
                }
            }

            return StaticPermissions;
        }

        public static bool PermissionIdExists(int permissionId)
        {
            return StaticPermissions.Any(wp => wp.Permissions!.Any(p => p.PermissionId == permissionId));
        }
    }
}
