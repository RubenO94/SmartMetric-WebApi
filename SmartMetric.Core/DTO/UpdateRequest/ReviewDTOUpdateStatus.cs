using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class ReviewDTOUpdateStatus
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required ReviewStatus ReviewStatus { get; set; }
    }
}
