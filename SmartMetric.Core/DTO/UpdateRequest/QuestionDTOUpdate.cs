using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class QuestionDTOUpdate
    {
        [Required(ErrorMessage ="Question Id is required")]
        public Guid QuestionId { get; set; }

        public bool IsRequired { get; set; }
        public int? Position { get; set; }
        public List<TranslationDTOUpdate>? Translations { get; set; }
        public List<RatingOptionDTOUpdate>? RatingOptions { get; set; }
        public List<SingleChoiceDTOUpdate>? SingleChoiceOptions { get; set; }
    }
}
