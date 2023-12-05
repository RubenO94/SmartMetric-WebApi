using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class RatingOptionDTOUpdate
    {
        public Guid RatingOptionId { get; set; }
        public int NumericValue { get; set; }
        public List<TranslationDTOUpdate>? Translations { get; set; }
    }
}
