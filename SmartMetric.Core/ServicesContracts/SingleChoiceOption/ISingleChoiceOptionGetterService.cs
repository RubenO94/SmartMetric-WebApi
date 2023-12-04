using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.SingleChoiceOptions
{
    public interface ISingleChoiceOptionGetterService
    {
        /// <summary>
        /// Procura todas as opções de resposta de escolhaúnica de todas as questões
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo SingleChoiceOptionDTOResponse</returns>
        Task<ApiResponse<List<SingleChoiceOptionDTOResponse>>> GetAllSingleChoiceOption();

        /// <summary>
        /// Procura por uma opção de resposta de escolha única através do seu Id passado por parâmetro
        /// </summary>
        /// <param name="singleChoiceOptionId"></param>
        /// <returns>Retorna um objeto do tipo SingleChoiceOptionDTOResponse</returns>
        Task<ApiResponse<SingleChoiceOptionDTOResponse?>> GetSingleChoiceOptionById(Guid? singleChoiceOptionId);

        /// <summary>
        /// Procura por todas as opções de resposta de escolha unica associadas à questão pretendida
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna uma lista de objetos do tipo SingleChoiceOptionDTOResponse</returns>
        Task<ApiResponse<List<SingleChoiceOptionDTOResponse>?>> GetSingleChoiceOptionByQuestionId (Guid? questionId);
    }
}
