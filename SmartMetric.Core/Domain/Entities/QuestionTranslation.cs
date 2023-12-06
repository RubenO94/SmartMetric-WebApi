using SmartMetric.Core.Domain.Entities.Common;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma tradução associada a uma questão para um determinado idioma.
    /// </summary>
    public class QuestionTranslation : BaseTranslation
    {
        /// <summary>
        /// Obtém ou define o identificador único da tradução da questão.
        /// </summary>
        public Guid QuestionTranslationId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da questão associada a esta tradução.
        /// </summary>
        public Guid? QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define o título da questão traduzido para o idioma especificado.
        /// </summary>
        [StringLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a questão relacionada a esta tradução.
        /// </summary>
        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }
    }

}
