using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    public interface IQuestionTranslationGetterService
    {
        /// <summary>
        /// Retorna todas as traduções das questões
        /// </summary>
        /// <returns>Retorna uma lista de todas as linhas da tabela de traduções de questões (QuestionTranslationDTOResponse)</returns>
        Task<List<QuestionTranslationDTOResponse>> GetAllQuestionTranslations();

        /// <summary>
        /// Procura por uma tradução de uma questão baseado no id da tradução fornecida.
        /// </summary>
        /// <param name="questionTranslationId"></param>
        /// <returns>Retorna um objeto do tipo QuestionTranslationDTOResponse, que corresponde à tradução pedida através do Id</returns>
        Task<QuestionTranslationDTOResponse> GetQuestionTranslationById(Guid? questionTranslationId);

        /// <summary>
        /// Procura por todas as traduções correspondentes ao id da questão fornecido nos parâmetros.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna uma lista de objectos do tipo QuestionTranslationDTOResponse (todas as traduções da questão)</returns>
        Task<List<QuestionTranslationDTOResponse>?> GetQuestionTranslationByQuestionId(Guid? questionId);
    }
}
