using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa a relação entre um modelo de formulário e uma pergunta associada.
    /// </summary>
    public class FormTemplateQuestion
    {
        /// <summary>
        /// Obtém ou define o identificador único da relação entre modelo de formulário e pergunta.
        /// </summary>
        public Guid FormTemplateQuestionId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único do modelo de formulário.
        /// </summary>
        public Guid? FormTemplateId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da pergunta associada.
        /// </summary>
        public Guid? QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define o modelo de formulário associado à relação.
        /// </summary>
        [ForeignKey(nameof(FormTemplateId))]
        [Required]
        public virtual FormTemplate? FormTemplate { get; set; }

        /// <summary>
        /// Obtém ou define a pergunta associada à relação.
        /// </summary>
        [ForeignKey(nameof(QuestionId))]
        [Required]
        public virtual Question? Question { get; set; }
    }

}
