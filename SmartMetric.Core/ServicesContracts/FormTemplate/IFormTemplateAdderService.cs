using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.FormTemplates
{
    /// <summary>
    /// Define o serviço para adição de novos modelos de formulário.
    /// </summary>
    public interface IFormTemplateAdderService
    {
        /// <summary>
        /// Adiciona um novo modelo de formulário à base de dados.
        /// </summary>
        /// <param name="addFormTemplateRequest">Os detalhes do modelo de formulário a ser adicionado.</param>
        /// <returns>Uma ApiResponse contendo o FormTemplateDTOResponse correspondente ao modelo de formulário adicionado em caso de sucesso, ou uma mensagem de erro em caso de falha.</returns>
        Task<ApiResponse<FormTemplateDTOResponse?>> AddFormTemplate(FormTemplateDTOAddRequest? addFormTemplateRequest);
    }


}
