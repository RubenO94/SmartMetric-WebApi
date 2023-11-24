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
        public int NumericValue { get; set; }
<<<<<<< HEAD
        [Required(ErrorMessage = "Please select a response type for this rating option")]
        [MinLength(1, ErrorMessage = "Please enter data in at least one language.")]
=======
        [Required(ErrorMessage = "Please ensure that the rating option is inserted in at least one language.")]
        [MinLength(1, ErrorMessage = "Please ensure that the rating option is inserted in at least one language.")]
>>>>>>> 3efbc32826497b6845c45329a5c68902f50dfa33
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
