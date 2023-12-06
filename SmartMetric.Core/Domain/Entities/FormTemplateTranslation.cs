using SmartMetric.Core.Domain.Entities.Common;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa a tradução de um modelo de formulário para um idioma específico.
    /// </summary>
    public class FormTemplateTranslation : BaseTranslation
    {
        /// <summary>
        /// Obtém ou define o identificador único da tradução do modelo de formulário.
        /// </summary>
        [Key]
        public Guid FormTemplateTranslationId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único do modelo de formulário associado à tradução.
        /// </summary>
        public Guid? FormTemplateId { get; set; }

        /// <summary>
        /// Obtém ou define o título traduzido do modelo de formulário.
        /// </summary>
        [StringLength(100)]
        public string? Title { get; set; }

        /// <summary>
        /// Obtém ou define o modelo de formulário associado à tradução.
        /// </summary>
        [ForeignKey(nameof(FormTemplateId))]
        public virtual FormTemplate? FormTemplate { get; set; }
    }

}
