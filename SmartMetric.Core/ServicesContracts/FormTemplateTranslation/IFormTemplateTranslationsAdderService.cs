using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.FormTemplateTranslations
{
    /// <summary>
    /// Representa a lógica de negócio para a inserção de traduções.
    /// </summary>
    public interface IFormTemplateTranslationsAdderService
    {
        /// <summary>
        /// Adiciona uma tradução de um modelo de formulário à sua lista de traduções.
        /// </summary>
        /// <param name="request">A tradução a ser adicionada.</param>
        /// <returns>Uma ApiResponse contendo o objeto FormTemplateTranslationDTOResponse (incluindo o novo FormTemplateTranslationId gerado) em caso de sucesso, ou uma mensagem de erro em caso de falha.</returns>
        Task<ApiResponse<TranslationDTOResponse?>> AddFormTemplateTranslation(Guid? formTemplateId, TranslationDTOAddRequest? request);
    }

}
