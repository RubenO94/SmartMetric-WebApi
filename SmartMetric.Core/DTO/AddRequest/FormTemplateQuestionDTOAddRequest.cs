using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    public class FormTemplateQuestionDTOAddRequest
    {
        [Required(ErrorMessage ="FormTemplateId is required.")]
        public Guid? FormTemplateId { get; set; }
        [Required(ErrorMessage = "QuestionId is required.")]
        public Guid? QuestionId { get; set; }


        public FormTemplateQuestion ToFormTemplateQuestion()
        {
            return new FormTemplateQuestion
            {
                FormTemplateId = FormTemplateId,
                QuestionId = QuestionId
            };
        }
    }
}
