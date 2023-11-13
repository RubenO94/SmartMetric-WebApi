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
    public class QuestionDTOAddRequest
    {
        [Required(ErrorMessage ="Please select a FormTemplate")]
        public Guid FormTemplateId { get; set; }
        public Guid? RatingTemplateId { get; set; }
        public Guid? SingleChoiceTemplateId { get; set; }
        public bool IsRequired { get; set; }
        [Required(ErrorMessage ="Please select a response type for this question")]
        public ResponseType ResponseType { get; set; }
        public List<QuestionTranslationDTOAddRequest>? Translations { get; set; }
        public List<SingleChoiceOptionDTOAddRequest>? SingleChoiceOptions { get; set; }
        public List<RatingOptionDTOAddRequest>? RatingOptions { get; set; }

        //TODO: Adicionar metodo para conversão do objecto Request em objeto Entity
        public Question ToQuestion()
        {
            List<QuestionTranslation> translations;
            if(Translations != null)
            foreach(QuestionTranslationDTOAddRequest translationRequest in Translations)
            {
                    //var translation = translationRequest.t
            } 

            return new Question()
            {
                //RatingOptions 
            };
        }
    }
}
