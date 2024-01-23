using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SmartMetric.Core.Domain.Entities;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class ReviewResponseDTOUpdate {
        public Guid QuestionId { get; set; }
        [StringLength(500)]
        public string? TextResponse { get; set; }
        public int? RatingValueResponse { get; set; }

        public ReviewResponse ToReviewResponse()
        {
            return new ReviewResponse()
            {
                QuestionId = QuestionId,
                TextResponse = TextResponse,
                RatingValueResponse = RatingValueResponse
            };
        }
    }
}