using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class ReviewResponseDTOResponse
    {
        public Guid ReviewResponseId { get; set; }
        public Guid? QuestionId { get; set; }
        public string? TextResponse { get; set; }
        public int? RatingValueResponse { get; set; }
    }

    public static class ReviewResponseExtensions
    {
        public static ReviewResponseDTOResponse ToReviewResponseDTOResponse(this ReviewResponse reviewResponse)
        {
            return new ReviewResponseDTOResponse()
            {
                RatingValueResponse = reviewResponse.RatingValueResponse,
                ReviewResponseId = reviewResponse.ReviewResponseId,
                TextResponse = reviewResponse.TextResponse,
                QuestionId = reviewResponse.QuestionId
            };
        }
    }
}
