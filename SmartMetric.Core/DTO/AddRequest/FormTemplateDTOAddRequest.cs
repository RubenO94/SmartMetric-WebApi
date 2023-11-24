using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    public class FormTemplateDTOAddRequest
    {
        [DataType(DataType.Date)]
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "CreatedByUserId is Required")]
        public int? CreatedByUserId { get; set; }

        [MinLength(1, ErrorMessage = "Need atleast one language")]
        [Required(ErrorMessage ="Need atleast one language")]
        public List<FormTemplateTranslationDTOAddRequest>? Translations { get; set; }


        public FormTemplate ToFormTemplate()
        {
            return new FormTemplate()
            {
                CreatedDate = CreatedDate,
                CreatedByUserId = CreatedByUserId,
                Translations = Translations?.Select(temp => temp.ToFormTemplateTranslation()).ToList()
            };
        }
    }
}
