using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface ITranslationsRepository
    {
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
    }
}
