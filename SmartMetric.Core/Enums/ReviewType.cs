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
        [Description("Top-Down: Avaliação de cima para baixo. Os supervisores avaliam os subordinados.")]
        TopDown,

        /// <summary>
        /// Revisão de baixo para cima, onde os subordinados avaliam os supervisores.
        /// </summary>
        [Description("Bottom-Up: Avaliação de baixo para cima. Os subordinados avaliam os supervisores.")]
        BottomUp,

        /// <summary>
        /// Revisão entre departamentos, envolvendo os supervisores de diferentes setores.
        /// </summary>
        [Description("Interdepartamental: O supervisor avalia o supervisor de outro departamento e vice-versa.")]
        Interdepartamental,

        /// <summary>
        /// Autoavaliação, onde os colaboradores avaliam a si mesmos.
        /// </summary>
        [Description("Self-Evaluation: Avaliação do próprio indivíduo.")]
        SelfEvaluation,

        /// <summary>
        /// Revisão por equipa, onde os membros da equipa se avaliam mutuamente.
        /// </summary>
        [Description("Team Evaluation: Avaliação por equipa. Membros da equipa se avaliam mutuamente.")]
        TeamEvaluation,
    }



}
