﻿using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.RatingOptionTranslations
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
        Task<ApiResponse<TranslationDTOResponse?>> AddRatingOptionTranslation(Guid? ratingOptionId, TranslationDTOAddRequest? request);
    }

}
