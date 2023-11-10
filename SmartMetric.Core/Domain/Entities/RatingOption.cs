using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma opção de classificação associada a uma determinada questão.
    /// </summary>
    public class RatingOption
    {
        /// <summary>
        /// Obtém ou define o identificador único da opção de classificação.
        /// </summary>
        public Guid RatingOptionId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da questão à qual esta opção de classificação está associada.
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define a coleção de traduções associadas a esta opção de classificação.
        /// </summary>
        public virtual ICollection<RatingOptionTranslation>? Translations { get; set; }

        /// <summary>
        /// Obtém ou define o valor numérico associado a esta opção de classificação.
        /// </summary>
        public int NumericValue { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a questão relacionada a esta opção de classificação.
        /// </summary>
        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }
    }

}
