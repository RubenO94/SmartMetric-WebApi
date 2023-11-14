using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    public interface ISingleChoiceOptionsAdderService
    {
        /// <summary>
        /// Adiciona uma opção de resposta única a uma questão e numa nova linha na tabela SingleChoiceOption
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retorna o objeto do tipo SingleChoiceOptionDTOResponse</returns>
        Task<SingleChoiceOptionDTOResponse?> AddSingleChoiceOption(SingleChoiceOptionDTOAddRequest? request);
    }
}
