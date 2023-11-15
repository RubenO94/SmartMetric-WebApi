using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Deleters
{
    public interface IRatingOptionDeleterService
    {
        /// <summary>
        /// Elimina a opção de resposta de classificação através do Id passado por parâmetro
        /// </summary>
        /// <param name="ratingOptionId"></param>
        /// <returns>Retorna True em caso de sucesso, em caso contrário retorna False</returns>
        Task<bool> DeleteRatingOptionById(Guid? ratingOptionId);
    }
}
