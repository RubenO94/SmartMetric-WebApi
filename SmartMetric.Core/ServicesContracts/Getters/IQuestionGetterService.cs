using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Define os métodos para a obtenção de informações sobre questões.
    /// </summary>
    public interface IQuestionGetterService
    {
        /// <summary>
        /// Obtém todas as questões disponíveis.
        /// </summary>
        /// <returns>Uma lista de objetos do tipo <see cref="QuestionDTOResponse"/>.</returns>
        Task<ApiResponse<List<QuestionDTOResponse>>> GetAllQuestions();

        /// <summary>
        /// Obtém uma questão com base no ID fornecido.
        /// </summary>
        /// <param name="questionId">O ID da questão a ser pesquisada.</param>
        /// <returns>O objeto <see cref="QuestionDTOResponse"/> correspondente ao ID fornecido, ou null se não encontrado.</returns>
        Task<ApiResponse<QuestionDTOResponse?>> GetQuestionById(Guid? questionId);

        /// <summary>
        /// Obtém todas as questões associadas a um modelo de formulário específico.
        /// </summary>
        /// <param name="formTemplateId">O ID do modelo de formulário para o qual as questões estão associadas.</param>
        /// <returns>Uma lista de objetos <see cref="QuestionDTOResponse"/> associados ao modelo de formulário fornecido, ou null se não houver questões.</returns>
        Task<ApiResponse<List<QuestionDTOResponse>?>> GetQuestionsByFormTemplateId(Guid? formTemplateId);
    }

}
