using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class RatingOptionDTOResponse
    {
        public Guid RatingOptionId { get; set; }

        [JsonIgnore]
        public Guid? QuestionId { get; set; }
        public int? NumericValue { get; set; }

        public List<TranslationDTOResponse>? Translations { get; set; }


        /// <summary>
        /// Compara os dados atuais deste objeto com o parametro
        /// </summary>
        /// <param name="obj">Objeto parametro para ser comparado</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem com o objeto especificado no parametro</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(RatingOptionDTOResponse)) return false;

            RatingOptionDTOResponse translation = (RatingOptionDTOResponse)obj;
            return this.RatingOptionId == translation.RatingOptionId && this.QuestionId == translation.QuestionId && this.NumericValue == translation.NumericValue && this.Translations == translation.Translations;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(RatingOptionId)}: {this.RatingOptionId}\n{nameof(QuestionId)}: {this.QuestionId}\n{nameof(NumericValue)}: {this.NumericValue}\nTranslations count: {Translations?.Count()}";
        }
    }

    public static class RatingOptionExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto RatingOption em um objeto RatingOptionDTOResponse
        /// </summary>
        /// <param name="ratingOption"></param>
        /// <returns>Retorna o convertido RatingOptionDTOResponse</returns>
        public static RatingOptionDTOResponse ToRatingOptionDTOResponse(this RatingOption ratingOption)
        {
            return new RatingOptionDTOResponse()
            {
                RatingOptionId = ratingOption.RatingOptionId,
                QuestionId = ratingOption.QuestionId,
                NumericValue = ratingOption.NumericValue,
                Translations = ratingOption.Translations?.Select(temp => temp.ToTranslationDTOResponse()).ToList(),
            };
        }
    }
}
