using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface IRatingOptionTranslationsRepository
    {
        #region Adders

        /// <summary>
        /// Adiciona um novo objeto de RatingOptionTranslation na base de dados.
        /// </summary>
        /// <param name="ratingOptionTranslation">O objeto RatingOptionTranslation a ser adicionado </param>
        /// <returns>Retorna o objeto RatingOptionTranslation após ser adicionado á tabela</returns>
        Task<RatingOptionTranslation> AddRatingOptionTranslation(RatingOptionTranslation ratingOptionTranslation);
        
        #endregion

        #region Getters
        //TODO: Getters
        #endregion
    }
}
