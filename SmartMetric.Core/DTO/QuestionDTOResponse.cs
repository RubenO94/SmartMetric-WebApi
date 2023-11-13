using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class QuestionDTOResponse
    {
        public Guid QuestionId { get; set; }
        public bool IsRequired { get; set; }
        public string? ResponseType { get; set; }
        public List<QuestionTranslationDTOResponse>? Translations { get; set; }
        public List<SingleChoiceOptionDTOResponse>? SingleChoiceOptions { get; set; }
        public List<RatingOptionDTOResponse>? RatingOptions { get; set; }
    }

    public static class QuestionExtensions
    {
        public static QuestionDTOResponse ToQuestionDTOResponse(this Question question)
        {
            return new QuestionDTOResponse()
            {

            };
        }
    }
}
