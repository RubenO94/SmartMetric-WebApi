using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    public interface IRatingOptionAdderService
    {
        /// <summary>
        /// Adiciona uma opção de resposta de classificação a uma questão e numa nova linha na tabela RatingOption
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retorna o objeto do tipo RatingOptionDTOResponse</returns>
        Task<RatingOptionDTOResponse> AddRatingOption(RatingOptionDTOAddRequest? request);
    }
}
