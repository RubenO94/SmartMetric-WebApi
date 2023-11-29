using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class QuestionDTOResponse
    {
        public Guid QuestionId { get; set; }
        public bool? IsRequired { get; set; }
        public int? Position { get; set; }
        public string? ResponseType { get; set; }
        public List<TranslationDTOResponse>? Translations { get; set; }
        public List<SingleChoiceOptionDTOResponse>? SingleChoiceOptions { get; set; }
        public List<RatingOptionDTOResponse>? RatingOptions { get; set; }
    }

    public static class QuestionExtensions
    {
        public static QuestionDTOResponse ToQuestionDTOResponse(this Question question)
        {
            return new QuestionDTOResponse()
            {
                QuestionId = question.QuestionId,
                IsRequired = question.IsRequired,
                Position = question.Position,
                ResponseType = question.ResponseType,
                Translations = question.Translations?.Select(translation => translation.ToTranslationDTOResponse()).ToList(),
                SingleChoiceOptions = question.SingleChoiceOptions?.Select(sco => sco.ToSingleChoiceOptionDTOResponse()).ToList(),
                RatingOptions = question.RatingOptions?.Select(rto => rto.ToRatingOptionDTOResponse()).ToList(),
            };
        }
    }
}
