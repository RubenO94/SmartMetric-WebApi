using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    public interface IQuestionGetterService
    {
        /// <summary>
        /// Procura todas as questões
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo QuestionDTOResponse</returns>
        Task<List<QuestionDTOResponse>> GetAllQuestion();

        /// <summary>
        /// Procura por uma questão através do seu Id passado por parâmetro
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna um objeto do tipo QuestionDTOResponse</returns>
        Task<QuestionDTOResponse?> GetQuestionById(Guid? questionId);

        /// <summary>
        /// Procura por todas as questões associadas ao formTemplate pretendido
        /// </summary>
        /// <param name="formTemplateId"></param>
        /// <returns>Retorna uma lista de objetos do tipo QuestionDTOResponse</returns>
        Task<List<QuestionDTOResponse>?> GetQuestionByFormTemplateId(Guid? formTemplateId);
    }
}
