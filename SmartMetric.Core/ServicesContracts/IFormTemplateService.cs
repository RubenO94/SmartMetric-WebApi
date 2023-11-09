using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts
{
    public interface IFormTemplateService
    {
        /// <summary>
        /// Metodo para retornar todos os modelos de formulário criados.
        /// </summary>
        /// <returns>Retorna todos os modelos criados  caso haja, senão retorna NULL</returns>
        Task<List<FormTemplateDTOResponse?>> GetAllFormTemplates();

        /// <summary>
        /// Metodo para retornar o modelo de formulário por GUID
        /// </summary>
        /// <param name="formTemplateId">GUID do modelo de formulário pertentido</param>
        /// <returns>Retorna o modelo de formulário que seja igual ao GUID do paremetro ou se não houver correspondência retorna NULL</returns>
        Task<FormTemplateDTOResponse?> GetFormTemplateById(Guid formTemplateId);

        /// <summary>
        /// Metodo para inserir um novo modelo de formulário e guardar na base dados. 
        /// </summary>
        /// <param name="addFormTemplateRequest">Modelo de formulário a ser adicionado</param>
        /// <returns>Retorna o modelo de formulário inserido em caso de sucesso, senão NULL </returns>
        Task<FormTemplateDTOResponse?> AddFormTemplate(FormTemplateDTOAddRequest? addFormTemplateRequest);
    }
}
