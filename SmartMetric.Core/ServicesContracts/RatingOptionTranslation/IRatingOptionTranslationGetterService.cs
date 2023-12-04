using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.RatingOptionTranslations
{
    /// <summary>
    /// Define os métodos para a obtenção de informações sobre traduções de respostas de classificação.
    /// </summary>
    public interface IRatingOptionTranslationGetterService
    {
        /// <summary>
        /// Obtém todas as traduções das respostas de classificação.
        /// </summary>
        /// <returns>Uma lista de objetos do tipo <see cref="TranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<TranslationDTOResponse>>> GetAllRatingOptionTranslations();

        /// <summary>
        /// Obtém uma tradução de uma resposta de classificação com base no ID fornecido.
        /// </summary>
        /// <param name="ratingOptionTranslationId">O ID da tradução da resposta de classificação a ser pesquisada.</param>
        /// <returns>O objeto <see cref="TranslationDTOResponse"/> correspondente ao ID fornecido.</returns>
        Task<ApiResponse<TranslationDTOResponse?>> GetRatingOptionTranslationById(Guid? ratingOptionTranslationId);

        /// <summary>
        /// Obtém todas as traduções correspondentes ao ID da resposta de classificação fornecido nos parâmetros.
        /// </summary>
        /// <param name="ratingOptionId">O ID da resposta de classificação para a qual as traduções serão recuperadas.</param>
        /// <returns>Uma lista de objetos do tipo <see cref="TranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<TranslationDTOResponse>?>> GetRatingOptionTranslationsByRatingOptionId(Guid? ratingOptionId);
    }

}
