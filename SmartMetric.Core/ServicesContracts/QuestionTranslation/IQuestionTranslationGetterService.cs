using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.QuestionTranslations
{
    /// <summary>
    /// Define os métodos para a obtenção de informações sobre traduções de questões.
    /// </summary>
    public interface IQuestionTranslationGetterService
    {
        /// <summary>
        /// Obtém todas as traduções das questões.
        /// </summary>
        /// <returns>Uma lista de objetos do tipo <see cref="TranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<TranslationDTOResponse>>> GetAllQuestionTranslations();

        /// <summary>
        /// Obtém uma tradução de uma questão com base no ID fornecido.
        /// </summary>
        /// <param name="questionTranslationId">O ID da tradução da questão a ser pesquisada.</param>
        /// <returns>O objeto <see cref="TranslationDTOResponse"/> correspondente ao ID fornecido.</returns>
        Task<ApiResponse<TranslationDTOResponse?>> GetQuestionTranslationById(Guid? questionTranslationId);

        /// <summary>
        /// Obtém todas as traduções correspondentes ao ID da questão fornecido nos parâmetros.
        /// </summary>
        /// <param name="questionId">O ID da questão para a qual as traduções serão recuperadas.</param>
        /// <returns>Uma lista de objetos do tipo <see cref="TranslationDTOResponse"/> (todas as traduções da questão).</returns>
        Task<ApiResponse<List<TranslationDTOResponse>?>> GetQuestionTranslationsByQuestionId(Guid? questionId);
    }

}
