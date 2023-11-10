using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma submissão no contexto de revisões.
    /// </summary>
    public class Submission
    {
        /// <summary>
        /// Obtém ou define o identificador único da submissão.
        /// </summary>
        public Guid SubmissionId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da revisão associada à submissão.
        /// </summary>
        public Guid? ReviewId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único do funcionário avaliado na submissão.
        /// </summary>
        public int? EvaluatedEmployeeId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único do funcionário avaliador na submissão.
        /// </summary>
        public int? EvaluatorEmployeeId { get; set; }

        /// <summary>
        /// Obtém ou define a data da submissão.
        /// </summary>
        public DateTime? SubmissionDate { get; set; }

        /// <summary>
        /// Obtém ou define as respostas associadas à submissão.
        /// </summary>
        public virtual ICollection<ReviewResponse>? ReviewResponses { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a revisão relacionada.
        /// </summary>
        [ForeignKey(nameof(ReviewId))]
        public virtual Review? Review { get; set; }
    }

}
