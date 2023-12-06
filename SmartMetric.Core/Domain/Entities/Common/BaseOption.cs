using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.Entities.Common
{
    public class BaseOption
    {
        /// <summary>
        /// Obtém ou define o identificador único da questão à qual esta opção de classificação está associada.
        /// </summary>
        public Guid? QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define a associação com a questão relacionada a esta opção de classificação.
        /// </summary>
        [ForeignKey(nameof(QuestionId))]
        public virtual Question? Question { get; set; }
    }
}
