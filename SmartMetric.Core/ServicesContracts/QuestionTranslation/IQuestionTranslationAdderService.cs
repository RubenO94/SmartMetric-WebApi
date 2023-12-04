using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.QuestionTranslations
{

    /// <summary>
    /// Define o serviço para adição de traduções de questões.
    /// </summary>
    public interface IQuestionTranslationAdderService
    {
        /// <summary>
        /// Adiciona uma tradução de um modelo de classificação à sua lista de traduções.
        /// </summary>
        /// <param name="request">A Tradução a ser adicionada.</param>
        /// <returns>Uma ApiResponse contendo o objeto QuestionTranslationDTOResponse em caso de sucesso, ou uma mensagem de erro em caso de falha.</returns>
        Task<ApiResponse<TranslationDTOResponse?>> AddQuestionTranslation(Guid? questionId, TranslationDTOAddRequest? request);
    }

}
