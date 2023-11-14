using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    public interface IQuestionAdderService
    {
        /// <summary>
        /// Adiciona uma questão
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Retorna um objeto do tipo QuestionDTOResponse</returns>
        Task<QuestionDTOResponse> AddQuestion(QuestionDTOAddRequest? request);
    }
}
