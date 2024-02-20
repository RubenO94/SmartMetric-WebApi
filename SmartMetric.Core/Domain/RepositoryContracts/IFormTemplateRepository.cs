using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        Task<List<FormTemplate>> GetAllFormTemplates(int page = 1, int pageSize = 20, string? language = null, string name = "");

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
        #endregion

        /// <summary>
        /// Elimina um FormTemplate através do GUID fornecido
        /// </summary>
        /// <param name="formTemplateId">GUID para pesquisar</param>
        /// <returns>Retorna True em caso de sucesso, caso contrário retorna False</returns>
        Task<bool> DeleteFormTemplateById (Guid formTemplateId);

        Task<FormTemplate> UpdateFormTemplate(FormTemplate formTemplate);

        Task<int> GetTotalRecords(Expression<Func<FormTemplate, bool>>? filter = null);

        Task<int> GetTotalForms(string? language, string name);
    }


}
