using SmartMetric.Core.DTO;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa um modelo de formulário que define a estrutura de perguntas e traduções associadas.
    /// </summary>
    public class FormTemplate
    {
        /// <summary>
        /// Obtém ou define o identificador único do modelo de formulário.
        /// </summary>
        [Key]
        public Guid FormTemplateId { get; set; }

        /// <summary>
        /// Obtém ou define a data de criação do modelo de formulário.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Obtém ou define a data de modificação do modelo de formulário.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Obtém ou define o identificador do utilizador que criou o modelo de formulário.
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Obtém ou define as traduções associadas ao modelo de formulário.
        /// </summary>
        public virtual ICollection<FormTemplateTranslation>? Translations { get; set; }

        /// <summary>
        /// Obtém ou define as perguntas associadas ao modelo de formulário.
        /// </summary>
        public virtual ICollection<FormTemplateQuestion>? FormTemplateQuestions { get; set; }
    }


}
