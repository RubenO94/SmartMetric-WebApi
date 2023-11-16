using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Deleters
{
    public interface IFormTemplateTranslationsDeleterService
    {
        /// <summary>
        /// Elimina uma tradução de um template de formulário através do Id passado por parâmetro
        /// </summary>
        /// <param name="formTemplateTranslationId"></param>
        /// <returns>Retorna True se for executado com sucesso, caso contrário retorna False</returns>
        Task<bool> DeleteFormTemplateTranslationById(Guid? formTemplateTranslationId);
    }
}
