using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Enums
{
    /// <summary>
    /// Enumeração que define os tipos de perfil no módulo de desempenho, como Backoffice e Frontoffice.
    /// </summary>
    public enum ProfileType
    {
        /// <summary>
        /// Representa o tipo de perfil Backoffice, que concede acesso ao dashboard de administração do módulo.
        /// Os utilizadores com este perfil geralmente têm privilégios administrativos.
        /// </summary>
        Backoffice,

        /// <summary>
        /// Representa o tipo de perfil Frontoffice, concedendo privilégios para responder aos inquéritos de avaliação de desempenho.
        /// Os utilizadores com este perfil têm acesso voltado especificamente para interações relacionadas à avaliação de desempenho.
        /// </summary>
        Frontoffice
    }

}
