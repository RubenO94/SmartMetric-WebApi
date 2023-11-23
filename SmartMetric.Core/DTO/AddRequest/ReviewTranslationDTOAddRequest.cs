using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    /// <summary>
    /// DTO de solicitação para adicionar uma nova tradução de uma revisão.
    /// </summary>
    public class ReviewTranslationDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o identificador da revisão a ser traduzido.
        /// </summary>
        [JsonIgnore]
        public Guid? ReviewId { get; set; }

        /// <summary>
        /// Obtém ou define o idioma para a tradução.
        /// </summary>
        public Language? Language { get; set; }

        /// <summary>
        /// Obtém ou define o título da tradução.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da tradução.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de tradução de uma revisão.
        /// </summary>
        /// <returns>A entidade de tradução de uma revisão.</returns>
        public ReviewTranslation ToReviewTranslation()
        {
            return new ReviewTranslation()
            {
                ReviewId = ReviewId,
                Language = Language.ToString(),
                Title = Title,
                Description = Description
            };
        }
    }
}
