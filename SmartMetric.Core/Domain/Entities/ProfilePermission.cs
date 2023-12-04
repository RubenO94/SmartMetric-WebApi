using SmartMetric.Infrastructure.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    public class ProfilePermission
    {
        public int ProfileId { get; set;}
        public int PermissionId { get; set; }

        [ForeignKey(nameof(ProfileId))]
        public virtual Perfil? Profile { get; set; }
    }
}
