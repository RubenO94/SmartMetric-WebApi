using SmartMetric.Core.Domain.Entities.Common;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa a tradução de uma revisão para um idioma específico.
    /// </summary>
    public class ReviewTranslation : BaseTranslation
    {
        /// <summary>
        /// Obtém ou define o identificador único da tradução do revisão.
        /// </summary>
        [Key]
        public Guid ReviewTranslationId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da revisão associado à tradução.
        /// </summary>
        public Guid? ReviewId { get; set; }


        /// <summary>
        /// Obtém ou define o título traduzido da revisão.
        /// </summary>
        [StringLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Obtém ou define a revisão associado à tradução.
        /// </summary>
        [ForeignKey(nameof(ReviewId))]
        public virtual Review? Review { get; set; }
    }

}
