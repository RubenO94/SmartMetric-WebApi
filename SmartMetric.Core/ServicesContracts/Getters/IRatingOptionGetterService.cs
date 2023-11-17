using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Define os métodos para a obtenção de informações sobre opções de resposta de classificação.
    /// </summary>
    public interface IRatingOptionGetterService
    {
        /// <summary>
        /// Obtém todas as opções de resposta de classificação de todas as questões.
        /// </summary>
        /// <returns>Uma lista de objetos do tipo <see cref="RatingOptionDTOResponse"/>.</returns>
        Task<ApiResponse<List<RatingOptionDTOResponse>>> GetAllRatingOptions();

        /// <summary>
        /// Obtém uma opção de resposta de classificação com base no ID fornecido.
        /// </summary>
        /// <param name="ratingOptionId">O ID da opção de resposta de classificação a ser pesquisada.</param>
        /// <returns>O objeto <see cref="RatingOptionDTOResponse"/> correspondente ao ID fornecido.</returns>
        Task<ApiResponse<RatingOptionDTOResponse?>> GetRatingOptionById(Guid? ratingOptionId);

        /// <summary>
        /// Obtém todas as opções de resposta de classificação associadas à questão pretendida.
        /// </summary>
        /// <param name="questionId">O ID da questão para a qual as opções de resposta de classificação serão recuperadas.</param>
        /// <returns>Uma lista de objetos do tipo <see cref="RatingOptionDTOResponse"/>.</returns>
        Task<ApiResponse<List<RatingOptionDTOResponse>?>> GetRatingOptionsByQuestionId(Guid? questionId);
    }

}
