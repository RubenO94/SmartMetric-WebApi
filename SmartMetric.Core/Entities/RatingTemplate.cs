using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Entities
{
    public class RatingTemplate
    {
        public Guid Id { get; set; }
        public ICollection<ScaleOption> ScaleOptions { get; set; }
    }
}
