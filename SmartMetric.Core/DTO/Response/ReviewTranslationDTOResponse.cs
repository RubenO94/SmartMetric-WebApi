using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    /// <summary>
    /// Representa a DTO usada na maioria dos retornos para os métodos dos serviços de tradução de uma revisão.
    /// </summary>
    public class ReviewTranslationDTOResponse
    {
        public Guid ReviewTranslationId { get; set; }
        private Guid? ReviewId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }

    public static class ReviewTranslationExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto ReviewTranslation em um objeto ReviewTranslationDTOResponse.
        /// </summary>
        /// <param name="reviewTranslation">O objeto ReviewTranslation a ser convertido.</param>
        /// <returns>Retorna o ReviewTranslationDTOResponse convertido.</returns>
        public static ReviewTranslationDTOResponse ToReviewTranslationDTOResponse(this ReviewTranslation reviewTranslation)
        {
            return new ReviewTranslationDTOResponse()
            {
                ReviewTranslationId = reviewTranslation.ReviewTranslationId,
                Language = reviewTranslation.Language,
                Title = reviewTranslation.Title,
                Description = reviewTranslation.Description,
            };
        }
    }
}
