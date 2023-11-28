using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Enums
{
    /// <summary>
    /// Enumeração que representa os diferentes tipos de sujeitos à revisão.
    /// </summary>
    public enum SubjectType
    {
        /// <summary>
        /// Revisão por departamento, focada em colaboradores de um departamento específico.
        /// </summary>
        [Description("Intradepartamento: Avaliação interna entre os colaboradores de um mesmo departamento.")]
        Department,

        /// <summary>
        /// Revisão entre departamentos, envolvendo colaboradores de diferentes setores.
        /// </summary>
        [Description("Interdepartamento: O supervisor avalia o supervisor de outro departamento e vice-versa.")]
        InterDepartment
    }


}
