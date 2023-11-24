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
        public Guid? SubmissionId { get; set; }
        public string? TextResponse { get; set; }
        public int? RatingValueResponse { get; set; }
    }
}
