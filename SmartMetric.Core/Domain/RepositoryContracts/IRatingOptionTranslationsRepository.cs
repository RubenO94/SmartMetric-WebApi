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

        /// <summary>
        /// Retorna todas as traduções de todas as RatingOptions
        /// </summary>
        /// <returns>Retorna uma lista de RatingOptionTranslation</returns>
        Task<List<RatingOptionTranslation>> GetAllRatingOptionTranslations();

        /// <summary>
        /// Procura uma linha na tabela RatingOptionTranslation recebendo como parametro um Id
        /// </summary>
        /// <returns>Retorna um objecto RatingOptionTranslation com o Id correspondente ao enviado como parâmetro.</returns>
        Task<RatingOptionTranslation?> GetRatingOptionTranslationById(Guid ratingOptionTranslationId);

        /// <summary>
        /// Procura linhas na tabela RatingOptionTranslation recebendo como parametro um RatingOptionId
        /// </summary>
        /// <returns>Retorna uma lista de objectos do tipo RatingOptionTranslation onde RatingOptionId é igual ao recebido como parâmetro.</returns>
        Task<List<RatingOptionTranslation>> GetRatingOptionTranslationByRatingOptionId(Guid ratingOptionId);

        #endregion

        #region Deleters

        /// <summary>
        /// Elimina uma tradução de uma opção de resposta de classificação através do seu Id passado por parâmetro
        /// </summary>
        /// <param name="ratingOptionTranslationId"></param>
        /// <returns>Retorna um boolean, True quando remove com sucesso, False se não foi possível remover</returns>
        Task<bool> DeleteRatingOptionTranslationById (Guid ratingOptionTranslationId);

        #endregion
    }
}
