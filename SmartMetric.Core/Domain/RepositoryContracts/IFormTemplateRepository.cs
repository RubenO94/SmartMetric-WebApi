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
    public interface IFormTemplateRepository
    {
        #region Getters
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
        Task<FormTemplate?> GetFormTemplateById(Guid? formTemplateId);
        #endregion

        #region Adders
        /// <summary>
        /// Adiciona um novo modelo de formulário à base de dados.
        /// </summary>
        /// <param name="formTemplate">O modelo de formulário a ser adicionado.</param>
        /// <returns>O modelo de formulário inserido.</returns>
        Task<FormTemplate> AddFormTemplate(FormTemplate formTemplate);

        /// <summary>
        /// Elimina um FormTemplate através do GUID fornecido
        /// </summary>
        /// <param name="formTemplateId">GUID para pesquisar</param>
        /// <returns>Retorna True em caso de sucesso, caso contrário retorna False</returns>
        Task<bool> DeleteFormTemplateById (Guid formTemplateId);
        #endregion

    }


}
