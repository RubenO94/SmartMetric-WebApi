using SmartMetric.Core.Domain.Entities;
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
    /// Define o serviço para adição de novos modelos de formulário.
    /// </summary>
    public interface IFormTemplatesAdderService
    {
        /// <summary>
        /// Adiciona um novo modelo de formulário à base de dados.
        /// </summary>
        /// <param name="addFormTemplateRequest">Os detalhes do modelo de formulário a ser adicionado.</param>
        /// <returns>O objeto FormTemplateDTOResponse correspondente ao modelo de formulário adicionado em caso de sucesso, ou null em caso de falha.</returns>
        Task<FormTemplateDTOResponse?> AddFormTemplate(FormTemplateDTOAddRequest? addFormTemplateRequest);
    }

}
