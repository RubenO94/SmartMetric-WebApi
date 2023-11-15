using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Deleters
{
    public interface IFormTemplatesDeleterService
    {
        /// <summary>
        /// Elimina um FormTemplate por id
        /// </summary>
        /// <param name="formTemplateId">O Id para pesquisa</param>
        /// <returns>Retorna True em caso de sucesso, caso contrário False</returns>
        Task<bool> DeleteFormTemplateById(Guid? formTemplateId);
    }
}
