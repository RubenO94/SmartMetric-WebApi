using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;

namespace SmartMetric.Core.ServicesContracts.RatingOptionTranslations
{
    public interface IRatingOptionTranslationDeleterService
    {
        /// <summary>
        /// Exclui a tradução de uma opção de resposta de classificação com base no ID da opção de resposta de classificação e no idioma fornecidos como parâmetros.
        /// </summary>
        /// <param name="ratingOptionId">O GUID da opção de resposta de classificação a ser pesquisada.</param>
        /// /// <param name="language">O idioma da tradução a ser excluída.</param>
        /// <returns>Retorna true se a exclusão for bem-sucedida; caso contrário, retorna false.</returns>
        Task<ApiResponse<bool>> DeleteRatingOptionTranslationById(Guid? ratingOptionId, Language? language);

    }
}
