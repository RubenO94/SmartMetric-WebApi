using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class RatingOptionDTOAddRequest
    {
        public Guid QuestionId { get; set; }
        public int NumericValue { get; set; }
        public Language Language { get; set; }
        public string? Description { get; set; }


        public RatingOption ToRatingOption()
        {
            RatingOptionTranslation translation = new RatingOptionTranslation() { Language = Language.ToString(), Description = Description, };
            return new RatingOption()
            {

            };
        }

    }
}
