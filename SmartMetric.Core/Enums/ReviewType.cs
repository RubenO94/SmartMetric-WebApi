using System;
using System.Collections.Generic;
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
        TopToBottom,

        /// <summary>
        /// Revisão de baixo para cima, onde os subordinados avaliam os supervisores.
        /// </summary>
        BottomToTop,

        /// <summary>
        /// Autoavaliação, onde os colaboradores avaliam a si mesmos.
        /// </summary>
        SelfEvaluation,

        /// <summary>
        /// Revisão entre departamentos, envolvendo colaboradores de diferentes setores.
        /// </summary>
        InterDepartment,

        /// <summary>
        /// Revisão entre pares, onde os colaboradores avaliam uns aos outros.
        /// </summary>
        PeerReview
    }

}
