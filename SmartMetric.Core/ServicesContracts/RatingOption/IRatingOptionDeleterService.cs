using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.RatingOptions
{
    public interface IRatingOptionDeleterService
    {
        /// <summary>
        /// Exclui a opção de resposta de classificação com base no ID fornecido como parâmetro.
        /// </summary>
        /// <param name="ratingOptionId">O GUID da opção de resposta de classificação a ser excluída.</param>
        /// <returns>Retorna true se a exclusão for bem-sucedida; caso contrário, retorna false.</returns>
        Task<ApiResponse<bool>> DeleteRatingOptionById(Guid? ratingOptionId);

    }
}
