using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface ISingleChoiceOptionTranslationsRepository
    {
        #region

        /// <summary>
        /// Adiciona um novo objeto de SingleChoiceOptionTranslation na base de dados.
        /// </summary>
        /// <param name="singleChoiceOptionTranslation">O objeto SingleChoiceOptionTranslation a ser adicionado </param>
        /// <returns>Retorna o objeto SingleChoiceOptionTranslation após ser adicionado á tabela</returns>
        Task<SingleChoiceOptionTranslation> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslation singleChoiceOptionTranslation);

        #endregion

        #region Getters
        //TODO: Getters
        #endregion
    }
}
