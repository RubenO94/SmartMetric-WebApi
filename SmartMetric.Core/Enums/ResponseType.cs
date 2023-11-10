using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Enums
{
    /// <summary>
    /// Enumeração que define os diferentes tipos de resposta possíveis para uma questão.
    /// </summary>
    public enum ResponseType
    {
        /// <summary>
        /// Resposta em texto livre.
        /// </summary>
        Text,

        /// <summary>
        /// Resposta de classificação.
        /// </summary>
        Rating,

        /// <summary>
        /// Resposta única (escolha única de uma seleção).
        /// </summary>
        SingleChoice
    }

}
