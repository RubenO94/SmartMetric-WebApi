﻿using SmartMetric.Core.Domain.Entities.Common;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma tradução associada a uma opção de escolha única.
    /// </summary>
    public class SingleChoiceOptionTranslation : BaseTranslation
    {
        /// <summary>
        /// Obtém ou define o identificador único da tradução da opção de escolha única.
        /// </summary>
        public Guid SingleChoiceOptionTranslationId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da opção de escolha única à qual a tradução está associada.
        /// </summary>
        public Guid? SingleChoiceOptionId { get; set; } 

        /// <summary>
        /// Obtém ou define a associação com a opção de escolha única relacionada.
        /// </summary>
        [ForeignKey(nameof(SingleChoiceOptionId))]
        public virtual SingleChoiceOption? SingleChoiceOption { get; set; }
    }

}
