using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class QuestionDTOResponse
    {
        public Guid QuestionId { get; set; }
        public bool IsRequired { get; set; }
        public string? ResponseType { get; set; }
    }
}
