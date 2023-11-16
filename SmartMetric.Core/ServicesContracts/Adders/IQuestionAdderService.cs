using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    public interface IQuestionAdderService
    {
        /// <summary>
        /// Adiciona uma questão com associação a um modelo de formulário
        /// </summary>
        /// <param name="request">A questão a ser adicionada</param>
        /// <returns>Retorna um objeto do tipo QuestionDTOResponse</returns>
        Task<QuestionDTOResponse?> AddQuestionToFormTemplate(QuestionDTOAddRequest? request);

        /// <summary>
        /// Adiciona uma questão com associação a uma revisão
        /// </summary>
        /// <param name="request">A questão a ser adicionada</param>
        /// <returns>Retorna um objeto do tipo QuestionDTOResponse</returns>
        Task<QuestionDTOResponse?> AddQuestionToReview(QuestionDTOAddRequest? request);
    }
}

