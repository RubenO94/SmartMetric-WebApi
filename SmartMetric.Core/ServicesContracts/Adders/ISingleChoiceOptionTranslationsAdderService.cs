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
    /// Representa a lógica de negócio para inserção traduções de Opções de Escolha Única.
    /// </summary>
    public interface ISingleChoiceOptionTranslationsAdderService
    {
        /// <summary>
        /// Adiciona uma tradução de um modelo de resposta unica à sua lista de traduções
        /// </summary>
        /// <param name="request">A Tradução a ser adicionada</param>
        /// <returns>Retorna o objeto Tradução (incluindo o novo FormTemplateTranslationId gerado)</returns>
        Task<SingleChoiceOptionTranslationDTOResponse> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslationDTOAddRequest? request);
    }
}
