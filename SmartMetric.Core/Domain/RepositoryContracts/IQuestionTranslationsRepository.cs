using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface IQuestionTranslationsRepository
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
        //TODO: Getters
        #endregion
    }
}
