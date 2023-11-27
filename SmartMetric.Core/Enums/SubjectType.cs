using System;
using System.Collections.Generic;
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
        /// Revisão de toda a empresa, envolvendo todos os departamentos.
        /// </summary>
        WholeCompany,

        /// <summary>
        /// Revisão por departamento, focada em colaboradores de um departamento específico.
        /// </summary>
        Department,

        /// <summary>
        /// Revisão por equipa, envolvendo colaboradores de uma equipa específica.
        /// </summary>
        Team,

        /// <summary>
        /// Revisão individual, onde cada colaborador é avaliado separadamente.
        /// </summary>
        Employee
    }

}
