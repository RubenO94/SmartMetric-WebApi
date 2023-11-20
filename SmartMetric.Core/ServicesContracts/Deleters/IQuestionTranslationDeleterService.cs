using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Deleters
{
    public interface IQuestionTranslationDeleterService
    {
        /// <summary>
        /// Exclui a tradução de uma questão com base no ID da questão e o idioma fornecidos como parâmetro
        /// </summary>
        /// <param name="questionTranslationId"></param>
        /// <param name="language"></param>
        /// <returns>Retorna true se a remoção for bem sucedida, caso contrário, retorna false</returns>
        Task<ApiResponse<bool>> DeleteQuestionTranslationById(Guid? questionId, Language? language);
    }
}
