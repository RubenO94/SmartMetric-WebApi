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
    public class RatingOptionTranslationDTOAddRequest
    {
        [JsonIgnore]
        public Guid? RatingOptionId { get; set; }

        [Required(ErrorMessage = "Please select a Language")]
        [EnumDataType(typeof(Language), ErrorMessage = "Language inserted is a invalid option")]
        public Language? Language { get; set; }

        [Required(ErrorMessage = "Description can't be blank")]
        public string? Description { get; set; }


        public RatingOptionTranslation ToRatingOptionTranslation()
        {
            return new RatingOptionTranslation()
            {
                RatingOptionId = RatingOptionId,
                Language = Language.ToString(),
                Description = Description
            };

        }
    }
}
