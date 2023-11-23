using SmartMetric.Core.Enums;
using SmartMetric.Infrastructure.Models;
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
        /// Obtém ou define o identificador único do utilizador que criou a revisão.
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
        /// Obtém ou define as traduções associadas à revisão
        /// </summary>
        public virtual ICollection<ReviewTranslation>? Translations { get; set; }

        /// <summary>
        /// Obtém ou define as questões relacionadas à revisão.
        /// </summary>
        public virtual ICollection<Question>? Questions { get; set; }

        /// <summary>
        /// Obtem ou define os funcionários a quem esta revisão se distina
        /// </summary>
        public virtual ICollection<ReviewEmployee>? Employees { get; set; }

        /// <summary>
        /// Obtem ou define os departamentos a quem esta revisão se distina
        /// </summary>
        public virtual ICollection<ReviewDepartment>? Departments { get; set; }

        /// <summary>
        /// Obtém ou define o utilizador que criou a revisão
        /// </summary>
        [ForeignKey(nameof(CreatedByUserId))]
        public virtual Utilizador? Utilizador { get; set; }
    }


}
