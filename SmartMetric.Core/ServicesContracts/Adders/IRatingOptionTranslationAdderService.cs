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
    /// Define o serviço para adição de traduções de opções de resposta de classificação.
    /// </summary>
    public interface IRatingOptionTranslationAdderService
    {
        /// <summary>
        /// Adiciona uma tradução de um modelo de classificação à sua lista de traduções.
        /// </summary>
        /// <param name="request">Os detalhes da tradução a ser adicionada.</param>
        /// <returns>Uma ApiResponse contendo o objeto RatingOptionTranslationDTOResponse em caso de sucesso, ou uma mensagem de erro em caso de falha.</returns>
        Task<ApiResponse<RatingOptionTranslationDTOResponse?>> AddRatingOptionTranslation(Guid? ratingOptionId, RatingOptionTranslationDTOAddRequest? request);
    }

}
