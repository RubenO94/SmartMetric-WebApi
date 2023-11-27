using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO.Response;
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
    public class RatingOptionDTOAddRequest
    {
        [JsonIgnore]
        public Guid? QuestionId { get; set; }

        [Required(ErrorMessage = "Please select a value for this rating option")]
        [Range(1, int.MaxValue, ErrorMessage = "NumericValue must be equal or higher than 1")]
        public int NumericValue { get; set; }

        [Required(ErrorMessage = "Please select a response type for this rating option")]
        [MinLength(1, ErrorMessage = "Please enter data in at least one language.")]
        public List<RatingOptionTranslationDTOAddRequest>? Translations { get; set; }


        public RatingOption ToRatingOption()
        
        {
            return new RatingOption()
            {
                QuestionId = QuestionId,
                NumericValue = NumericValue,
                Translations = Translations?.Select(temp => temp.ToRatingOptionTranslation()).ToList() ?? new List<RatingOptionTranslation>()
            };
        }

    }
}
