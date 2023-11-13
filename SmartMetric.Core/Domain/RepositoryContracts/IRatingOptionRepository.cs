using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface IRatingOptionRepository
    {
        #region Adders

        /// <summary>
        /// Adiciona um novo objeto do tipo RatingOption na base de dados
        /// </summary>
        /// <param name="ratingOption"></param>
        /// <returns>Retorna o objeto RatingOption após ser inserido na base de dados</returns>
        Task<RatingOption> AddRatingOption(RatingOption ratingOption);

        #endregion

        #region Getters

        /// <summary>
        /// Retorna todas as respostas de classificação criadas
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo RatingOption</returns>
        Task<List<RatingOption>> GetAllRatingOption();

        /// <summary>
        /// Procura uma resposta de classificação através do seu Id passado no parâmetro
        /// </summary>
        /// <param name="ratingOptionId"></param>
        /// <returns>Retorna um objeto do tipo RatingOption</returns>
        Task<RatingOption?> GetRatingOptionById(Guid ratingOptionId);

        /// <summary>
        /// Procura todas as opções de respostas de classificação associadas à questão passada no parâmetro através do seu Id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna uma lista de objetos do tipo RatingOption</returns>
        Task<List<RatingOption>?> GetRatingOptionByQuestionId(Guid questionId);

        #endregion
    }
}
