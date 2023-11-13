using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    public class FormTemplateQuestionDTOAddRequest
    {
        public Guid FormTemplateId { get; set; }
        public Guid QuestionId { get; set;}
    }
}
