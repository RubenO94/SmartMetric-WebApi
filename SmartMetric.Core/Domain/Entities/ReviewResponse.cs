using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa a resposta a uma questão de revisão submetida por um usuário.
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
        public Guid? ReviewQuestionId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da submissão à qual a resposta está vinculada.
        /// </summary>
        public Guid? SubmissionId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da opção de escolha única à qual a resposta está associada.
        /// </summary>
        public Guid? SingleChoiceOptionId { get; set; }

        /// <summary>
        /// Obtém ou define o texto da resposta em caso de resposta textual.
        /// </summary>
        [StringLength(500)]
        public string? TextResponse { get; set; }

        /// <summary>
        /// Obtém ou define o valor da classificação em caso de resposta de classificação.
        /// </summary>
        public int? RatingValue { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a opção de escolha única à qual a resposta está vinculada em caso de resposta de escolha única.
        /// </summary>
        [ForeignKey(nameof(SingleChoiceOptionId))]
        public virtual SingleChoiceOption? SingleChoiceOption { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a questão de revisão relacionada.
        /// </summary>
        [ForeignKey(nameof(ReviewQuestionId))]
        [Required]
        public virtual ReviewQuestion? ReviewQuestion { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a submissão à qual a resposta está vinculada.
        /// </summary>
        [ForeignKey(nameof(SubmissionId))]
        [Required]
        public virtual Submission? Submission { get; set; }
    }


}


