using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma opção de escolha única associada a uma pergunta.
    /// </summary>
    public class SingleChoiceOption
    {
        /// <summary>
        /// Obtém ou define o identificador único da opção de escolha única.
        /// </summary>
        public Guid SingleChoiceOptionId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da pergunta à qual a opção de escolha única está associada.
        /// </summary>
        public Guid? QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define as traduções associadas a esta opção de escolha única.
        /// </summary>
        public virtual ICollection<SingleChoiceOptionTranslation>? Translations { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a pergunta relacionada.
        /// </summary>
        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }
    }

}
