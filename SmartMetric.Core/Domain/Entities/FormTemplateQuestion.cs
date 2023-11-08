using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.Entities
{
    public class FormTemplateQuestion
    {
        public Guid FormTemplateQuestionId { get; set; }
        public Guid FormTemplateId { get; set; }
        public Guid QuestionId { get; set; }

        [ForeignKey(nameof(FormTemplateId))]
        [Required]
        public FormTemplate? FormTemplate { get; set; }

        [ForeignKey(nameof(QuestionId))]
        [Required]
        public Question? Question { get; set; }

    }
}
