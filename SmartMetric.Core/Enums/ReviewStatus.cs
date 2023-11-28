using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Enums
{
    /// <summary>
    /// Enumeração que representa os diferentes estados possíveis de uma revisão.
    /// </summary>
    public enum ReviewStatus
    {
        /// <summary>
        /// Revisão ainda não iniciada.
        /// </summary>
        NotStarted,

        /// <summary>
        /// Revisão em andamento.
        /// </summary>
        Active,

        /// <summary>
        /// Revisão cancelada.
        /// </summary>
        Canceled,

        /// <summary>
        /// Revisão concluída.
        /// </summary>
        Completed
    }

}
