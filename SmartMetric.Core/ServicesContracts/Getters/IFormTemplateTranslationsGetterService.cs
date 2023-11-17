using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Representa a lógica de negócio para a obtenção de traduções de modelos de formulário.
    /// </summary>
    public interface IFormTemplateTranslationsGetterService
    {
        /// <summary>
        /// Obtém todas as traduções dos modelos de formulário.
        /// </summary>
        /// <returns>Uma lista de objetos <see cref="FormTemplateTranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<FormTemplateTranslationDTOResponse>>> GetAllFormTemplateTranslations();

        /// <summary>
        /// Obtém uma tradução de um modelo de formulário com base no ID da tradução fornecido.
        /// </summary>
        /// <param name="formTemplateTranslationId">ID da tradução para pesquisa.</param>
        /// <returns>A tradução correspondente, ou null se não encontrada.</returns>
        Task<ApiResponse<FormTemplateTranslationDTOResponse?>> GetFormTemplateTranslationById(Guid? formTemplateTranslationId);

        /// <summary>
        /// Obtém todas as traduções correspondentes ao ID do modelo de formulário fornecido.
        /// </summary>
        /// <param name="formTemplateId">ID do modelo de formulário a ser pesquisado.</param>
        /// <returns>Uma lista de objetos <see cref="FormTemplateTranslationDTOResponse"/> ou null se não houver traduções.</returns>
        Task<ApiResponse<List<FormTemplateTranslationDTOResponse>?>> GetTranslationsByFormTemplateId(Guid? formTemplateId);
    }


}
}
