using SmartMetric.Core.Domain.Entities.Common;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma tradução associada a uma opção de classificação.
    /// </summary>
    public class RatingOptionTranslation : BaseTranslation
    {
        /// <summary>
        /// Obtém ou define o identificador único da tradução da opção de classificação.
        /// </summary>
        public Guid RatingOptionTranslationId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da opção de classificação à qual esta tradução está associada.
        /// </summary>
        public Guid? RatingOptionId { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a opção de classificação relacionada a esta tradução.
        /// </summary>
        [ForeignKey(nameof(RatingOptionId))]
        public virtual RatingOption? RatingOption { get; set; }
    }

}
