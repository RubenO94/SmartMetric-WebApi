using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    /// <summary>
    /// Representa a logica de negócio para a inserção de traduções
    /// </summary>
    public interface IFormTemplateTranslationsAdderService
    {
        /// <summary>
        /// Adiciona uma tradução de um modelo de formulário à sua lista de traduções
        /// </summary>
        /// <param name="request">A Tradução a ser adicionada</param>
        /// <returns>Retorna o objeto Tradução (incluindo o novo FormTemplateTranslationId gerado)</returns>
        Task<FormTemplateTranslationDTOResponse> AddFormTemplateTranslation(FormTemplateTranslationDTOAddRequest? request);

    }
}
