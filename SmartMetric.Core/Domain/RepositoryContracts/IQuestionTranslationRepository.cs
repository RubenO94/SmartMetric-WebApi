using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface IQuestionTranslationRepository
    {
        #region Adders

        /// <summary>
        /// Adiciona um novo objeto de QuestionTranslation na base de dados.
        /// </summary>
        /// <param name="questionTranslation">O objeto QuestionTranslation a ser adicionado </param>
        /// <returns>Retorna o objeto QuestionTranslation após ser adicionado á tabela</returns>
        Task<QuestionTranslation> AddQuestionTranslation(QuestionTranslation questionTranslation);

        #endregion

        #region Getters

        /// <summary>
        /// Retorna uma todas as traduções de todas as questões
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo QuestionTranslation</returns>
        Task<List<QuestionTranslation>> GetAllQuestionTranslations();

        /// <summary>
        /// Retorna uma tradução específica (passado o Id da tradução por parâmetro)
        /// </summary>
        /// <param name="questionTranslationId"></param>
        /// <returns>Retorna um objeto do tipo QuestionTranslation</returns>
        Task<QuestionTranslation?> GetQuestionTranslationsById(Guid questionTranslationId);

        /// <summary>
        /// Retorna todas as traduções de uma questão específica (passado o Id da questão por parâmetro)
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna uma lista de objetos do tipo QuestionTranslation</returns>
        Task<List<QuestionTranslation>> GetQuestionTranslationsByQuestionId(Guid questionId);

        #endregion

        #region Deleters

        /// <summary>
        /// Remove um objeto de QuestionTranslation na base de dados
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Retorna um boolean, caso True foi removido com sucesso, caso contrário retorna False</returns>
        Task<bool> DeleteQuestionTranslationById(Guid questionTranslationId);

        #endregion
    }
}
