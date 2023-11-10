using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma questão associada a uma revisão.
    /// </summary>
    public class ReviewQuestion
    {
        /// <summary>
        /// Obtém ou define o identificador único da questão associada à revisão.
        /// </summary>
        public Guid ReviewQuestionId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da revisão à qual a questão está associada.
        /// </summary>
        public Guid? ReviewId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da questão associada.
        /// </summary>
        public Guid? QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a revisão à qual a questão está vinculada.
        /// </summary>
        [ForeignKey(nameof(ReviewId))]
        [Required]
        public virtual Review? Review { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a questão relacionada.
        /// </summary>
        [ForeignKey(nameof(QuestionId))]
        [Required]
        public virtual Question? Question { get; set; }
    }


}
