using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.FormTemplates
{
    /// <summary>
    /// Define o serviço para a exclusão de modelos de formulário.
    /// </summary>
    public interface IFormTemplatesDeleterService
    {
        /// <summary>
        /// Exclui um modelo de formulário com base no ID.
        /// </summary>
        /// <param name="formTemplateId">O ID do modelo de formulário a ser excluído.</param>
        /// <returns>Retorna true em caso de sucesso, caso contrário, false.</returns>
        Task<ApiResponse<bool>> DeleteFormTemplateById(Guid? formTemplateId);
    }

}
