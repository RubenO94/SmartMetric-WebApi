using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    /// <summary>
    /// Define o serviço para adição de questões.
    /// </summary>
    public interface IQuestionAdderService
    {
        /// <summary>
        /// Adiciona uma questão com associação a um modelo de formulário.
        /// </summary>
        /// <param name="request">A questão a ser adicionada.</param>
        /// <returns>Uma ApiResponse contendo o objeto QuestionDTOResponse em caso de sucesso, ou uma mensagem de erro em caso de falha.</returns>
        Task<ApiResponse<QuestionDTOResponse?>> AddQuestionToFormTemplate(Guid? formTemplateId, QuestionDTOAddRequest? request);

        /// <summary>
        /// Adiciona uma questão com associação a uma revisão.
        /// </summary>
        /// <param name="request">A questão a ser adicionada.</param>
        /// <returns>Uma ApiResponse contendo o objeto QuestionDTOResponse em caso de sucesso, ou uma mensagem de erro em caso de falha.</returns>
        Task<ApiResponse<QuestionDTOResponse?>> AddQuestionToReview(Guid? reviewId, QuestionDTOAddRequest? request);
    }

}

