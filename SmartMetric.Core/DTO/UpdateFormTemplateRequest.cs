using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class UpdateFormTemplateRequest
    {
        public Guid FormTemplateId { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public ICollection<FormTemplateTranslationDTO>? Translations { get; set; }
        public ICollection<UpdateQuestionRequest>? Questions { get; set; }
    }
}
