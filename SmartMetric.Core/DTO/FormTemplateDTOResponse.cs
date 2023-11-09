using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class FormTemplateDTOResponse
    {
        public Guid FormTemplateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public List<FormTemplateTranslation>? Translations { get; set; }
        public List<Question>? Questions { get; set; }

    }
}
