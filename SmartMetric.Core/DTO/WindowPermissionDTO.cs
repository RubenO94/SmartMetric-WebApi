using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class WindowPermissionDTO
    {
        public int WindowId { get; set; }
        public string? WindowName { get; set; }

        // Permissões básicas
        public bool CanRead { get; set; } = false;
        public bool CanCreate { get; set; } = false;
        public bool CanUpdate { get; set; } = false;
        public bool CanDelete { get; set; } = false;

        // Permissões específicas para a janela "Settings"
        public bool CanChangeSettings { get; set; } = false;
        public bool CanAccessAdvancedSettings { get; set; } = false;

    }
}
