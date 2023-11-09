using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.Entities
{
    public class ReviewQuestion
    {
        public Guid ReviewQuestionId { get; set; }
        public Guid? ReviewId { get; set; }
        public Guid? QuestionId { get; set; }

        [ForeignKey(nameof(ReviewId))]
        [Required]
        public virtual Review? Review { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [Required]
        public virtual Question? Question { get; set; }
    }

}
