using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    /// <summary>
    /// Representa a DTO que é usada na maioria dos returns para os métodos dos serviços RatingOptionTranslation
    /// </summary>
    public class RatingOptionTranslationDTOResponse
    {
        public Guid RatingOptionTranslationId { get; set; }
        public Guid? RatingOptionId { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }


        /// <summary>
        /// Compara os dados atuais deste objeto com o parametro
        /// </summary>
        /// <param name="obj">Objeto parametro para ser comparado</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem com o objeto especificado no parametro</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(RatingOptionTranslationDTOResponse)) return false;

            RatingOptionTranslationDTOResponse translation = (RatingOptionTranslationDTOResponse)obj;
            return this.RatingOptionTranslationId == translation.RatingOptionTranslationId && this.RatingOptionId == translation.RatingOptionId && this.Language == translation.Language && this.Description == translation.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"{nameof(RatingOptionTranslationId)}: {this.RatingOptionTranslationId}\n{nameof(RatingOptionId)}: {this.RatingOptionId}\n{nameof(Description)}:{this.Description}\n";
        }
    }

    public static class RatingOptionTranslationExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto RatingOptionTranslation em um objeto RatingOptionTranslationDTOResponse
        /// </summary>
        /// <param name="ratingOptionTranslation">O objeto ratingOptionTranslation a ser convertido</param>
        /// <returns>Retorna o convertido RatingOptionTranslationDTOResponse</returns>
        public static RatingOptionTranslationDTOResponse ToRatingOptionTranslationDTOResponse(this RatingOptionTranslation ratingOptionTranslation)
        {
            return new RatingOptionTranslationDTOResponse()
            {
                RatingOptionTranslationId = ratingOptionTranslation.RatingOptionTranslationId,
                RatingOptionId = ratingOptionTranslation.RatingOptionId,
                Language = ratingOptionTranslation.Language,
                Description = ratingOptionTranslation.Description,
            };
        }
    }
}
