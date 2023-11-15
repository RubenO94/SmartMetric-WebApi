using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Deleters
{
    public interface ISingleChoiceOptionDeleterService
    {
        /// <summary>
        /// Elimina uma opção de resposta de escolha única através do Id passado por parâmetro
        /// </summary>
        /// <param name="singleChoiceOptionId"></param>
        /// <returns>Retorna True em caso de sucesso, em caso contrário retorna False</returns>
        Task<bool> DeleteSingleChoiceOptionById(Guid? singleChoiceOptionId);
    }
}
