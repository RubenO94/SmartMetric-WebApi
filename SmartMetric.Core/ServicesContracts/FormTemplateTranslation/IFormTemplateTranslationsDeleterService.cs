using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;

namespace SmartMetric.Core.ServicesContracts.FormTemplateTranslations
{
    public interface IFormTemplateTranslationsDeleterService
    {
        /// <summary>
        /// Exclui uma tradução de um modelo de formulário com base no ID do modelo de formulário e no idioma fornecidos como parâmetros.
        /// </summary>
        /// <param name="formTemplateId">O GUID do modelo de formulário a ser pesquisado.</param>
        /// <param name="language">O idioma da tradução a ser excluída.</param>
        /// <returns>Retorna true se a exclusão for bem-sucedida; caso contrário, retorna false.</returns>
        Task<ApiResponse<bool>> DeleteFormTemplateTranslationById(Guid? formTemplateId, Language? language);

    }
}
