using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa a resposta a uma questão de revisão submetida por um utilizador.
    /// </summary>
    public class ReviewResponse
    {
        /// <summary>
        /// Obtém ou define o identificador único da resposta à revisão.
        /// </summary>
        public Guid ReviewResponseId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da questão de revisão à qual a resposta está associada.
        /// </summary>
        public Guid? QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da submissão à qual a resposta está vinculada.
        /// </summary>
        public Guid? SubmissionId { get; set; }
        [ForeignKey(nameof(SubmissionId))]
        public virtual Submission? Submission { get; set; }

        /// <summary>
        /// Obtém ou define o texto da resposta em caso de resposta textual.
        /// </summary>
        [StringLength(500)]
        public string? TextResponse { get; set; }

        /// <summary>
        /// Obtém ou define o valor da classificação em caso de resposta de classificação.
        /// </summary>
        public int? RatingValueResponse { get; set; }
    }


}


