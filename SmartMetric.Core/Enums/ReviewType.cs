using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Enums
{
    /// <summary>
    /// Enumeração que representa os diferentes tipos de revisão disponíveis.
    /// </summary>
    public enum ReviewType
    {
        /// <summary>
        /// Revisão de cima para baixo, onde os supervisores avaliam os subordinados.
        /// </summary>
        [Description("Top to Bottom: Avaliação de cima para baixo. Os supervisores avaliam os subordinados.")]
        TopToBottom,

        /// <summary>
        /// Revisão de baixo para cima, onde os subordinados avaliam os supervisores.
        /// </summary>
        [Description("Bottom to Top: Avaliação de baixo para cima. Os subordinados avaliam os supervisores.")]
        BottomToTop,

        /// <summary>
        /// Autoavaliação, onde os colaboradores avaliam a si mesmos.
        /// </summary>
        [Description("Self-Evaluation: Avaliação do próprio indivíduo.")]
        SelfEvaluation,
    }


}
