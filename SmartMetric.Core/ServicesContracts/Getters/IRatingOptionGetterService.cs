using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    public interface IRatingOptionGetterService
    {
        /// <summary>
        /// Procura todas as opções de resposta de classificação de todas as questões
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo RatingOptionDTOResponse</returns>
        Task<List<RatingOptionDTOResponse>> GetAllRatingOption();

        /// <summary>
        /// Procura por uma opção de resposta de classificação através do seu Id passado por parâmetro
        /// </summary>
        /// <param name="ratingOptionId"></param>
        /// <returns>Retorna um objeto do tipo RatingOptionDTOResponse</returns>
        Task<RatingOptionDTOResponse?> GetRatingOptionById(Guid? ratingOptionId);

        /// <summary>
        /// Procura por todas as opções de resposta declassificação associadas à questão pretendida
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna uma lista de objetos do tipo RatingOptionDTOResponse</returns>
        Task<List<RatingOptionDTOResponse>?> GetRatingOptionByQuestionId(Guid? questionId);
    }
}
