using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Deleters
{
    /// <summary>
    /// Define o serviço para a exclusão de modelos de formulário.
    /// </summary>
    public interface IFormTemplatesDeleterService
    {
        /// <summary>
        /// Exclui um modelo de formulário com base no ID.
        /// </summary>
        /// <param name="formTemplateId">O ID do modelo de formulário a ser excluído.</param>
        /// <returns>Retorna true em caso de sucesso, caso contrário, false.</returns>
        Task<ApiResponse<bool>> DeleteFormTemplateById(Guid? formTemplateId);
    }

}
