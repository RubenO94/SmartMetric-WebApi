using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Representa a logica de negócio receber uma lista de Traduções
    /// </summary>
    public interface IFormTemplateTranslationsGetterService
    {
        /// <summary>
        /// Retorna todas as traduções dos modelos de formulário
        /// </summary>
        /// <returns>Retorna uma lista de ForTemplateTranslationDTOResponse</returns>
        Task<List<FormTemplateTranslationDTOResponse>> GetAllFormTemplateTranslations();

        /// <summary>
        /// Procura por uma tradução de um modelo de formulário baseado no id da tradução fornecida.
        /// </summary>
        /// <param name="formTemplateTranslationId">Id da tradução para pesquisa</param>
        /// <returns>Retorna a tradução correspondente</returns>
        Task<FormTemplateTranslationDTOResponse?> GetFormTemplateTranslationById(Guid? formTemplateTranslationId);

        /// <summary>
        /// Procura por todas as traduções corresnpondentes ao id do modelo de formulário fornecido nos paremetros 
        /// </summary>
        /// <param name="formTemplateId">Id do modelo de forumário a ser pesquisado</param>
        /// <returns>Retorna uma lista de objetos FormTemplateTranslationsDTOResponse</returns>
        Task<List<FormTemplateTranslationDTOResponse>?> GetTranslationsByFormTemplateId(Guid? formTemplateId);

    }
}
