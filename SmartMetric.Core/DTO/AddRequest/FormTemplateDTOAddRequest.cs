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
<<<<<<< HEAD
        [JsonIgnore]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }

        public int? CreatedByUserId { get; set; }

=======
        [Required(ErrorMessage ="Created Date is required")]
        [DataType(DataType.Date)]
        public DateTime? CreatedDate { get; set; }
        [Required(ErrorMessage = "Please select a user")]
        public int? CreatedByUserId { get; set; }
        [Required(ErrorMessage = "Please ensure that the form is inserted in at least one language.")]
        [MinLength(1, ErrorMessage = "Please ensure that the form is inserted in at least one language.")]
>>>>>>> 3efbc32826497b6845c45329a5c68902f50dfa33
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
