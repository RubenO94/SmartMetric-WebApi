using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;

namespace SmartMetric.Core.ServicesContracts.FormTemplates
{
    /// <summary>
    /// Define os métodos para a obtenção de modelos de formulário.
    /// </summary>
    public interface IFormTemplateGetterService
    {
        /// <summary>
        /// Obtém todos os modelos de formulário existentes.
        /// </summary>
        /// <returns>
        /// Uma lista de objetos <see cref="FormTemplateDTOResponse"/>, ou null se nenhum modelo de formulário estiver disponível.
        /// </returns>
        Task<ApiResponse<List<FormTemplateDTOResponse?>>> GetAllFormTemplates(int page = 1, int pageSize = 20, Language?language = null);

        /// <summary>
        /// Obtém um modelo de formulário com base no seu identificador único (GUID).
        /// </summary>
        /// <param name="formTemplateId">O identificador único (GUID) do modelo de formulário desejado.</param>
        /// <returns>
        /// O objeto <see cref="FormTemplateDTOResponse"/> correspondente ao identificador fornecido, 
        /// ou null se nenhum modelo for encontrado.
        /// </returns>
        Task<ApiResponse<FormTemplateDTOResponse?>> GetFormTemplateById(Guid? formTemplateId);
    }


}
