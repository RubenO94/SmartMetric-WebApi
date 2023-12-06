using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.Entities.Common
{
    public class BaseTranslation
    {
        [StringLength(10)]
        public string? Language { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
    }
}
