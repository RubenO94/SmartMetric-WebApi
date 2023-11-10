using SmartMetric.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts
{
    /// <summary>
    /// Representa a lógica de negócio para obter traduções de Opções de Escolha Única.
    /// </summary>
    public interface ISingleChoiceOptionTranslationsGetterService
    {
        /// <summary>
        /// Obtém todas as traduções de Opções de Escolha Única.
        /// </summary>
        /// <returns>Uma lista de SingleChoiceOptionTranslationDTOResponse.</returns>
        Task<List<SingleChoiceOptionTranslationDTOResponse>> GetAllSingleChoiceOptionTranslations();

        /// <summary>
        /// Obtém uma tradução de Opção de Escolha Única pelo seu identificador.
        /// </summary>
        /// <param name="singleChoiceOptionTranslationId">O identificador da tradução a obter.</param>
        /// <returns>A tradução correspondente, ou null se não for encontrada.</returns>
        Task<SingleChoiceOptionTranslationDTOResponse?> GetSingleChoiceOptionTranslationById(Guid? singleChoiceOptionTranslationId);

        /// <summary>
        /// Obtém todas as traduções associadas a uma Opção de Escolha Única.
        /// </summary>
        /// <param name="singleChoiceOptionId">O identificador da Opção de Escolha Única.</param>
        /// <returns>Uma lista de SingleChoiceOptionTranslationDTOResponse, ou null se não for encontrada.</returns>
        Task<List<SingleChoiceOptionTranslationDTOResponse>?> GetTranslationsBySingleChoiceOptionId(Guid? singleChoiceOptionId);

    }

}
