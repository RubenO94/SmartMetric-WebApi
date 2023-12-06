using SmartMetric.Core.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma opção de escolha única associada a uma pergunta.
    /// </summary>
    public class SingleChoiceOption : BaseOption
    {
        /// <summary>
        /// Obtém ou define o identificador único da opção de escolha única.
        /// </summary>
        public Guid SingleChoiceOptionId { get; set; }

        /// <summary>
        /// Obtém ou define as traduções associadas a esta opção de escolha única.
        /// </summary>
        public virtual ICollection<SingleChoiceOptionTranslation>? Translations { get; set; }

    }

}
