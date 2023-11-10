using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma tradução associada a uma questão para um determinado idioma.
    /// </summary>
    public class QuestionTranslation
    {
        /// <summary>
        /// Obtém ou define o identificador único da tradução da questão.
        /// </summary>
        public Guid QuestionTranslationId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da questão associada a esta tradução.
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define o código do idioma para o qual esta tradução está associada.
        /// </summary>
        [StringLength(10)]
        public string? Language { get; set; }

        /// <summary>
        /// Obtém ou define o título da questão traduzido para o idioma especificado.
        /// </summary>
        [StringLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da questão traduzida para o idioma especificado.
        /// </summary>
        [StringLength(300)]
        public string? Description { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a questão relacionada a esta tradução.
        /// </summary>
        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }
    }

}
