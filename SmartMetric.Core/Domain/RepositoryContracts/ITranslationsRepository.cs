using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface ITranslationsRepository
    {
        #region Adders
        /// <summary>
        /// Adiciona um novo objeto de FormTemplateTranslation na base de dados.
        /// </summary>
        /// <param name="FormTemplateTranslation">O objeto FormTemplateTranslation a ser adicionado </param>
        /// <returns>Retorna o objeto FormTemplateTranslation após ser adicionado á tabela</returns>
        Task<FormTemplateTranslation> AddFormTemplateTranslation(FormTemplateTranslation formTemplateTranslation);

        /// <summary>
        /// Adiciona um novo objeto de QuestionTranslation na base de dados.
        /// </summary>
        /// <param name="questionTranslation">O objeto QuestionTranslation a ser adicionado </param>
        /// <returns>Retorna o objeto QuestionTranslation após ser adicionado á tabela</returns>
        Task<QuestionTranslation> AddQuestionTranslation(QuestionTranslation questionTranslation);

        /// <summary>
        /// Adiciona um novo objeto de RatingOptionTranslation na base de dados.
        /// </summary>
        /// <param name="ratingOptionTranslation">O objeto RatingOptionTranslation a ser adicionado </param>
        /// <returns>Retorna o objeto RatingOptionTranslation após ser adicionado á tabela</returns>
        Task<RatingOptionTranslation> AddRatingOptionTranslation(RatingOptionTranslation ratingOptionTranslation);

        /// <summary>
        /// Adiciona um novo objeto de SingleChoiceOptionTranslation na base de dados.
        /// </summary>
        /// <param name="singleChoiceOptionTranslation">O objeto SingleChoiceOptionTranslation a ser adicionado </param>
        /// <returns>Retorna o objeto SingleChoiceOptionTranslation após ser adicionado á tabela</returns>
        Task<SingleChoiceOptionTranslation> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslation singleChoiceOptionTranslation);
        #endregion

        #region Getters
        /// <summary>
        /// Retorna todas as traduções dos modelos de formulário
        /// </summary>
        /// <returns>Retorna uma lista de ForTemplateTranslation</returns>
        Task<List<FormTemplateTranslation>> GetAllFormTemplateTranslations();

        /// <summary>
        /// Procura por uma tradução de um modelo de formulário baseado no id da tradução fornecida.
        /// </summary>
        /// <param name="formTemplateTranslationId">Id da tradução para pesquisa</param>
        /// <returns>Retorna a tradução correspondente</returns>
        Task<FormTemplateTranslation?> GetFormTemplateTranslationById(Guid formTemplateTranslationId);

        /// <summary>
        /// Procura por todas as traduções corresnpondentes ao id do modelo de formulário fornecido nos paremetros 
        /// </summary>
        /// <param name="formTemplateId">Id do modelo de forumário a ser pesquisado</param>
        /// <returns>Retorna uma lista de objetos FormTemplateTranslations</returns>
        Task<List<FormTemplateTranslation>> GetFilteredTranslationsByFormTemplateId(Guid formTemplateId);

        //TODO: QuestionsTranslation Getters
        //TODO: RatingOptionsTranslation Getters
        //TODO: SingleChoiceOptionsTranslation Getters
        #endregion
    }
}
