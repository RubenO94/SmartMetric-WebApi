using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    public class FormTemplateDTOAddRequest
    {
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Please select a User")]
        public int? CreatedByUserId { get; set; }
        public List<FormTemplateTranslationDTOAddRequest>? Translations { get; set; }
        public List<QuestionDTOAddRequest>? Questions { get; set; }


        public FormTemplate ToFormTemplate()
        {


            return new FormTemplate()
            {
                CreatedDate = CreatedDate,
                CreatedByUserId = CreatedByUserId,
                Translations = Translations?.Select(temp => temp.ToFormTemplateTranslation()).ToList(),

            };
        }
    }
}
