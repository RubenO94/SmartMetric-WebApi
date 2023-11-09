using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class QuestionDTOAddRequest
    {
        [Required]
        public Guid FormTemplateId { get; set; }
        public Guid? RatingTemplateId { get; set; }
        public Guid? SingleChoiceTemplateId { get; set; }
        public bool IsRequired { get; set; }
        [Required]
        public ResponseType ResponseType { get; set; }
        public Language Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        //TODO: Adicionar metodo para conversão do objecto Request em objeto Entity
    }
}
