using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class SingleChoiceDTOUpdate
    {
        public Guid SingleChoiceOptionId { get; set; }
        [MinLength(1, ErrorMessage ="SingleChoiceOption Translations can't be less than 1")]
        public List<TranslationDTOUpdate>? Translations { get; set; }
    }
}
