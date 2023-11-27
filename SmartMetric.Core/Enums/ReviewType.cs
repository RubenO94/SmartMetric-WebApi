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
        [Description("Top to Bottom: Avaliação de cima para baixo.")]
        TopToBottom,

        /// <summary>
        /// Revisão de baixo para cima, onde os subordinados avaliam os supervisores.
        /// </summary>
        [Description("Bottom to Top: Avaliação de baixo para cima.")]
        BottomToTop,

        /// <summary>
        /// Autoavaliação, onde os colaboradores avaliam a si mesmos.
        /// </summary>
        [Description("Autoavaliação: Avaliação do próprio indivíduo.")]
        SelfEvaluation,

        /// <summary>
        /// Revisão entre departamentos, envolvendo colaboradores de diferentes setores.
        /// </summary>
        [Description("Interdepartamento: O supervisor avalia o supervisor de outro departamento e vice-versa.")]
        InterDepartment,

        /// <summary>
        /// Revisão entre pares, onde os colaboradores avaliam uns aos outros.
        /// </summary>
        [Description("Peer Reviews: Avaliação entre pares (top-to-bottom e bottom-to-top).")]
        PeerReview
    }

}
