using SmartMetric.Core.DTO.Response;
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
        /// Exclui uma opção de resposta de escolha única com base no ID fornecido como parâmetro.
        /// </summary>
        /// <param name="singleChoiceOptionId">O GUID da opção de resposta de escolha única a ser excluída.</param>
        /// <returns>Retorna true se a exclusão for bem-sucedida; caso contrário, retorna false.</returns>
        Task<ApiResponse<bool>> DeleteSingleChoiceOptionById(Guid? singleChoiceOptionId);

    }
}
