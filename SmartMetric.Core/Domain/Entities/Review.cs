using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa o modelo que inicia uma revisão de desempenho.
    /// </summary>
    public class Review
    {
        /// <summary>
        /// Obtém ou define o identificador único da revisão.
        /// </summary>
        public Guid ReviewId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único do modelo de formulário associado à revisão.
        /// </summary>
        public Guid? FormTemplateId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único do usuário que criou a revisão.
        /// </summary>
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Obtém ou define a data de criação da revisão.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Obtém ou define a data de início da revisão.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Obtém ou define a data de término da revisão.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de revisão.
        /// </summary>
        [StringLength(20)]
        public string? ReviewType { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de sujeito de avaliação associado à revisão.
        /// </summary>
        [StringLength(20)]
        public string? SubjectType { get; set; }

        /// <summary>
        /// Obtém ou define o status da revisão.
        /// </summary>
        [StringLength(20)]
        public string? ReviewStatus { get; set; }

        /// <summary>
        /// Obtém ou define as submissões associadas à revisão.
        /// </summary>
        public virtual ICollection<Submission>? Submissions { get; set; }

        /// <summary>
        /// Obtém ou define a associação com o modelo de formulário relacionado à revisão.
        /// </summary>
        [ForeignKey(nameof(FormTemplateId))]
        public virtual FormTemplate? FormTemplate { get; set; }
    }


}
