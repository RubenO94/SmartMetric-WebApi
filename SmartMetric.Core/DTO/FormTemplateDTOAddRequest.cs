using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class FormTemplateDTOAddRequest
    {
        public DateTime CreatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public Language Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }


        public FormTemplate ToFormTemplate()
        {
            var translation = new FormTemplateTranslation()
            {
                Language = Language.ToString(),
                Title = Title,
                Description = Description,
            };

            return new FormTemplate()
            {
                CreatedDate = CreatedDate,
                CreatedByUserId = CreatedByUserId,
                Translations = new List<FormTemplateTranslation>() { translation }

            };
        }
    }
}
