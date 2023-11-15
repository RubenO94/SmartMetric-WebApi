using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface ISingleChoiceOptionRepository
    {
        #region Adders

        /// <summary>
        /// Adiciona um novo objeto do tipo SingleChoiceOption na base de dados.
        /// </summary>
        /// <param name="singleChoiceOption">O objeto SingleChoiceOption a ser adicionado.</param>
        /// <returns>Retorna o objeto SingleChoiceOption após ser inserido na base de dados.</returns>
        Task<SingleChoiceOption> AddSingleChoiceOption(SingleChoiceOption singleChoiceOption);

        #endregion

        #region Getters

        /// <summary>
        /// Retorna todas as opções de escolha única criadas.
        /// </summary>
        /// <returns>Retorna uma lista de objetos do tipo SingleChoiceOption.</returns>
        Task<List<SingleChoiceOption>> GetAllSingleChoiceOptions();

        /// <summary>
        /// Procura uma opção de escolha única através do seu Id passado no parâmetro.
        /// </summary>
        /// <param name="singleChoiceOptionId">O Id da opção de escolha única a ser procurada.</param>
        /// <returns>Retorna um objeto do tipo SingleChoiceOption.</returns>
        Task<SingleChoiceOption?> GetSingleChoiceOptionById(Guid singleChoiceOptionId);

        /// <summary>
        /// Procura todas as opções de escolha única associadas à questão passada no parâmetro através do seu Id.
        /// </summary>
        /// <param name="questionId">O Id da questão associada às opções de escolha única.</param>
        /// <returns>Retorna uma lista de objetos do tipo SingleChoiceOption.</returns>
        Task<List<SingleChoiceOption>?> GetSingleChoiceOptionsByQuestionId(Guid questionId);

        #endregion
    }

}
