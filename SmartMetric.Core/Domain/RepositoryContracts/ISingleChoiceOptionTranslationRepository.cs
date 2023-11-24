using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface ISingleChoiceOptionTranslationRepository
    {
        #region Adders

        /// <summary>
        /// Adiciona um novo objeto de SingleChoiceOptionTranslation na base de dados.
        /// </summary>
        /// <param name="singleChoiceOptionTranslation">O objeto SingleChoiceOptionTranslation a ser adicionado </param>
        /// <returns>Retorna o objeto SingleChoiceOptionTranslation após ser adicionado á tabela</returns>
        Task<SingleChoiceOptionTranslation> AddSingleChoiceOptionTranslation(SingleChoiceOptionTranslation singleChoiceOptionTranslation);

        #endregion

        #region Getters

        /// <summary>
        /// Obtém todas as traduções de Opções de Escolha Única.
        /// </summary>
        /// <returns>Uma lista de SingleChoiceOptionTranslationDTOResponse.</returns>
        Task<List<SingleChoiceOptionTranslation>> GetAllSingleChoiceOptionTranslations();

        /// <summary>
        /// Obtém uma tradução de Opção de Escolha Única pelo seu identificador.
        /// </summary>
        /// <param name="singleChoiceOptionTranslationId">O identificador da tradução a obter.</param>
        /// <returns>A tradução correspondente, ou null se não for encontrada.</returns>
        Task<SingleChoiceOptionTranslation?> GetSingleChoiceOptionTranslationById(Guid singleChoiceOptionTranslationId);

        /// <summary>
        /// Obtém todas as traduções associadas a uma Opção de Escolha Única.
        /// </summary>
        /// <param name="singleChoiceOptionId">O identificador da Opção de Escolha Única.</param>
        /// <returns>Uma lista de SingleChoiceOptionTranslationDTOResponse.</returns>
        Task<List<SingleChoiceOptionTranslation>> GetTranslationsBySingleChoiceOptionId(Guid singleChoiceOptionId);


        #endregion

        #region Deleters

        /// <summary>
        /// Elimina uma tradução de uma resposta escolha única através do seu Guid passado por parâmetro
        /// </summary>
        /// <param name="singleChoiceOptionTranslationId"></param>
        /// <returns>Retorna um boolean, True quando remove com sucesso, False se não foi possível remover</returns>
        Task<bool> DeleteSingleChoiceOptionTranslationById (Guid singleChoiceOptionTranslationId);

        #endregion
    }
}
