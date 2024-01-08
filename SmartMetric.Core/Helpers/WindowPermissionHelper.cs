using SmartMetric.Core.DTO;
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
                        new PermissionDTO {PermissionId = 23121001, PermissionType = PermissionType.Create},
                        new PermissionDTO {PermissionId = 23121002, PermissionType = PermissionType.Read},
                        new PermissionDTO {PermissionId = 23121003, PermissionType = PermissionType.Update},
                        new PermissionDTO {PermissionId = 23121004, PermissionType = PermissionType.Delete},
                        new PermissionDTO {PermissionId = 23121005, PermissionType = PermissionType.Patch}
                    }
                },
                new WindowPermissionDTO
                {
                    WindowId = 23122000,
                    WindowType = WindowType.Reviews,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23122001, PermissionType = PermissionType.Create},
                        new PermissionDTO {PermissionId = 23122002, PermissionType = PermissionType.Read},
                        new PermissionDTO {PermissionId = 23121003, PermissionType = PermissionType.Update},
                        new PermissionDTO {PermissionId = 23122004, PermissionType = PermissionType.Delete},
                        new PermissionDTO {PermissionId = 23122005, PermissionType = PermissionType.Patch}
                    }
                },
                new WindowPermissionDTO
                {
                    WindowId = 23123000,
                    WindowType = WindowType.Statistics,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23123001, PermissionType = PermissionType.Create},
                        new PermissionDTO {PermissionId = 23123002, PermissionType = PermissionType.Read},
                        new PermissionDTO {PermissionId = 23123003, PermissionType = PermissionType.Update},
                        new PermissionDTO {PermissionId = 23123004, PermissionType = PermissionType.Delete},
                        new PermissionDTO {PermissionId = 23123005, PermissionType = PermissionType.Patch}
                    }
                },
                new WindowPermissionDTO
                {
                    WindowId = 23124000,
                    WindowType = WindowType.AdminSettings,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23124001, PermissionType = PermissionType.Create},
                        new PermissionDTO {PermissionId = 23124002, PermissionType = PermissionType.Read},
                        new PermissionDTO {PermissionId = 23124003, PermissionType = PermissionType.Update},
                        new PermissionDTO {PermissionId = 23124004, PermissionType = PermissionType.Delete},
                        new PermissionDTO {PermissionId = 23124005, PermissionType = PermissionType.Patch}
                    }
                },
                 new WindowPermissionDTO
                {
                    WindowId = 23125000,
                    WindowType = WindowType.Profiles,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23125001, PermissionType = PermissionType.Create},
                        new PermissionDTO {PermissionId = 23125002, PermissionType = PermissionType.Read},
                        new PermissionDTO {PermissionId = 23125003, PermissionType = PermissionType.Update},
                        new PermissionDTO {PermissionId = 23125004, PermissionType = PermissionType.Delete},
                        new PermissionDTO {PermissionId = 23125005, PermissionType = PermissionType.Patch}
                    }
                },
                  new WindowPermissionDTO
                {
                    WindowId = 23126000,
                    WindowType = WindowType.Departments,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23126001, PermissionType = PermissionType.Create},
                        new PermissionDTO {PermissionId = 23126002, PermissionType = PermissionType.Read},
                        new PermissionDTO {PermissionId = 23126003, PermissionType = PermissionType.Update},
                        new PermissionDTO {PermissionId = 23126004, PermissionType = PermissionType.Delete},
                        new PermissionDTO {PermissionId = 23126005, PermissionType = PermissionType.Patch}
                    }
                },
                   new WindowPermissionDTO
                {
                    WindowId = 23127000,
                    WindowType = WindowType.Accounts,
                    Permissions = new List<PermissionDTO>
                    {
                        new PermissionDTO {PermissionId = 23127001, PermissionType = PermissionType.Create},
                        new PermissionDTO {PermissionId = 23127002, PermissionType = PermissionType.Read},
                        new PermissionDTO {PermissionId = 23127003, PermissionType = PermissionType.Update},
                        new PermissionDTO {PermissionId = 23127004, PermissionType = PermissionType.Delete},
                        new PermissionDTO {PermissionId = 23127005, PermissionType = PermissionType.Patch}
                    }
                },
            };
        public static List<WindowPermissionDTO> CheckProfilePermissions(List<int> profilePermissionIds)
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

        public static List<WindowPermissionDTO> GiveAllPermissions()
        {


            foreach (var windowPermission in StaticPermissions)
            {
                foreach (var permission in windowPermission.Permissions!)
                {
                    permission.HasPermission = true;
                }
            }

            return StaticPermissions;
        }

        public static bool PermissionIdExists(int permissionId)
        {
            return StaticPermissions.Any(wp => wp.Permissions!.Any(p => p.PermissionId == permissionId));
        }

        public static List<WindowPermissionDTO> GetAllWindows()
        {
            return StaticPermissions;
        }
    }
}
