using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Interface que define operações de acesso a dados para modelos de formulários.
    /// </summary>
    public interface IFormTemplatesRepository
    {
        /// <summary>
        /// Obtém todos os modelos de formulário existentes.
        /// </summary>
        /// <returns>Uma lista de modelos de formulário.</returns>
        Task<List<FormTemplate>> GetAllFormTemplates();

        /// <summary>
        /// Obtém um modelo de formulário pelo seu identificador único (GUID).
        /// </summary>
        /// <param name="formTemplateId">O identificador único (GUID) do modelo de formulário.</param>
        /// <returns>O modelo de formulário correspondente ao identificador ou null em caso de não haver correspondência.</returns>
        Task<FormTemplate?> GetFormTemplateById(Guid formTemplateId);

        /// <summary>
        /// Adiciona um novo modelo de formulário à base de dados.
        /// </summary>
        /// <param name="formTemplate">O modelo de formulário a ser adicionado.</param>
        /// <returns>O modelo de formulário inserido.</returns>
        Task<FormTemplate> AddFormTemplate(FormTemplate formTemplate);
    }


}
