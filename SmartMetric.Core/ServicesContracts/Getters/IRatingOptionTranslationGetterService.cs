using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Define os métodos para a obtenção de informações sobre traduções de respostas de classificação.
    /// </summary>
    public interface IRatingOptionTranslationGetterService
    {
        /// <summary>
        /// Obtém todas as traduções das respostas de classificação.
        /// </summary>
        /// <returns>Uma lista de objetos do tipo <see cref="RatingOptionTranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<RatingOptionTranslationDTOResponse>>> GetAllRatingOptionTranslations();

        /// <summary>
        /// Obtém uma tradução de uma resposta de classificação com base no ID fornecido.
        /// </summary>
        /// <param name="ratingOptionTranslationId">O ID da tradução da resposta de classificação a ser pesquisada.</param>
        /// <returns>O objeto <see cref="RatingOptionTranslationDTOResponse"/> correspondente ao ID fornecido.</returns>
        Task<ApiResponse<RatingOptionTranslationDTOResponse?>> GetRatingOptionTranslationById(Guid? ratingOptionTranslationId);

        /// <summary>
        /// Obtém todas as traduções correspondentes ao ID da resposta de classificação fornecido nos parâmetros.
        /// </summary>
        /// <param name="ratingOptionId">O ID da resposta de classificação para a qual as traduções serão recuperadas.</param>
        /// <returns>Uma lista de objetos do tipo <see cref="RatingOptionTranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<RatingOptionTranslationDTOResponse>?>> GetRatingOptionTranslationsByRatingOptionId(Guid? ratingOptionId);
    }

}
