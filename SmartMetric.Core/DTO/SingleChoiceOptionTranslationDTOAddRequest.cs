using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class SingleChoiceOptionTranslationDTOAddRequest
    {
        [Required(ErrorMessage ="Please select a SingleChoiceOption to translate")]
        public Guid? SingleChoiceOptionId { get; set; }
        
        [Required(ErrorMessage = "Please select a Language to translate")]
        public Language? Language { get; set; }
        
        [Required(ErrorMessage = "Description can't be blank")]
        public string? Description { get; set; }


        public SingleChoiceOptionTranslation ToSingleChoiceOptionTranslation()
        {
            return new SingleChoiceOptionTranslation()
            {
                SingleChoiceOptionId = SingleChoiceOptionId,
                Language = this.Language.ToString(),
                Description = this.Description
            };
        }
    }
}
