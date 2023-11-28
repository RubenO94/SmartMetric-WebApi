using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    /// <summary>
    /// Define o serviço para adição de opções de resposta única a uma questão.
    /// </summary>
    public interface ISingleChoiceOptionsAdderService
    {
        /// <summary>
        /// Adiciona uma opção de resposta única a uma questão e cria uma nova linha na tabela SingleChoiceOption.
        /// </summary>
        /// <param name="request">Os detalhes da opção de resposta única a ser adicionada.</param>
        /// <returns>Retorna o objeto SingleChoiceOptionDTOResponse em caso de sucesso, ou null em caso de falha.</returns>
        Task<ApiResponse<SingleChoiceOptionDTOResponse?>> AddSingleChoiceOption(Guid? questionId, SingleChoiceOptionDTOAddRequest? request);
    }

}
