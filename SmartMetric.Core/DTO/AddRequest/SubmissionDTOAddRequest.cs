using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    public class SubmissionDTOAddRequest
    {
        [Required(ErrorMessage ="ReviewId is Required")]
        public Guid ReviewId { get; set; }
        
        [Required(ErrorMessage = "EvaluatedEmployeeId is Required")]
        public int EvaluatedEmployeeId { get; set; }
        
        [Required(ErrorMessage = "EvaluatorEmployeeId is Required")]
        public int EvaluatorEmployeeId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Submission needs at least one response")]
        public ICollection<ReviewResponseDTOAddRequest>? ReviewResponses { get; set; }


        public Submission ToSubmission()
        {
            return new Submission()
            {
                ReviewId = ReviewId,
                EvaluatedEmployeeId = EvaluatedEmployeeId,
                EvaluatorEmployeeId = EvaluatorEmployeeId,
            };
        }

    }
}
