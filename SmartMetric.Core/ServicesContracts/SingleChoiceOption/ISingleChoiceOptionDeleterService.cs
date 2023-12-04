using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.SingleChoiceOptions
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
