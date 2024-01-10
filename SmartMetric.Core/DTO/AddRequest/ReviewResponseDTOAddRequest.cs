using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    public class ReviewResponseDTOAddRequest
    {
        [Required]
        public Guid QuestionId { get; set; }
        public string? TextResponse { get; set; }
        public int? RatingValue { get; set; }

        public ReviewResponse ToReviewResponse()
        {
            return new ReviewResponse()
            {
                QuestionId = QuestionId,
                TextResponse = TextResponse,
                RatingValueResponse = RatingValue
            };
        }
    }

    //TODO: Adicionar metodo para conversão do objecto Request em objeto Entity

}
