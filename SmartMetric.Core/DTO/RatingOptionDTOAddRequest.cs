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
    public class RatingOptionDTOAddRequest
    {
        [Required(ErrorMessage = "Please select a QuestionId")]
        public Guid? QuestionId { get; set; }

        [Required(ErrorMessage = "NumericValue can't be blank")]
        public int? NumericValue { get; set; }


        public RatingOption ToRatingOption()
        {
            return new RatingOption()
            {
                QuestionId = this.QuestionId,
                NumericValue = this.NumericValue
            };
        }

    }
}
