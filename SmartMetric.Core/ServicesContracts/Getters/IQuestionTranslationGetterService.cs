using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Define os métodos para a obtenção de informações sobre traduções de questões.
    /// </summary>
    public interface IQuestionTranslationGetterService
    {
        /// <summary>
        /// Obtém todas as traduções das questões.
        /// </summary>
        /// <returns>Uma lista de objetos do tipo <see cref="QuestionTranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<QuestionTranslationDTOResponse>>> GetAllQuestionTranslations();

        /// <summary>
        /// Obtém uma tradução de uma questão com base no ID fornecido.
        /// </summary>
        /// <param name="questionTranslationId">O ID da tradução da questão a ser pesquisada.</param>
        /// <returns>O objeto <see cref="QuestionTranslationDTOResponse"/> correspondente ao ID fornecido.</returns>
        Task<ApiResponse<QuestionTranslationDTOResponse?>> GetQuestionTranslationById(Guid? questionTranslationId);

        /// <summary>
        /// Obtém todas as traduções correspondentes ao ID da questão fornecido nos parâmetros.
        /// </summary>
        /// <param name="questionId">O ID da questão para a qual as traduções serão recuperadas.</param>
        /// <returns>Uma lista de objetos do tipo <see cref="QuestionTranslationDTOResponse"/> (todas as traduções da questão).</returns>
        Task<ApiResponse<List<QuestionTranslationDTOResponse>?>> GetQuestionTranslationsByQuestionId(Guid? questionId);
    }

}
