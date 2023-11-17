using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Define os métodos para a obtenção de informações sobre traduções de Opções de Escolha Única.
    /// </summary>
    public interface ISingleChoiceOptionTranslationsGetterService
    {
        /// <summary>
        /// Obtém todas as traduções de Opções de Escolha Única.
        /// </summary>
        /// <returns>Uma lista de objetos do tipo <see cref="SingleChoiceOptionTranslationDTOResponse"/>.</returns>
        Task<ApiResponse<List<SingleChoiceOptionTranslationDTOResponse>>> GetAllSingleChoiceOptionTranslations();

        /// <summary>
        /// Obtém uma tradução de Opção de Escolha Única com base no seu identificador único.
        /// </summary>
        /// <param name="singleChoiceOptionTranslationId">O identificador único da tradução a ser obtida.</param>
        /// <returns>O objeto <see cref="SingleChoiceOptionTranslationDTOResponse"/> correspondente ao identificador fornecido.</returns>
        Task<ApiResponse<SingleChoiceOptionTranslationDTOResponse?>> GetSingleChoiceOptionTranslationById(Guid? singleChoiceOptionTranslationId);

        /// <summary>
        /// Obtém todas as traduções associadas a uma Opção de Escolha Única com base no seu identificador.
        /// </summary>
        /// <param name="singleChoiceOptionId">O identificador único da Opção de Escolha Única.</param>
        /// <returns>Uma lista de objetos do tipo <see cref="SingleChoiceOptionTranslationDTOResponse"/>, ou null se não forem encontradas traduções.</returns>
        Task<ApiResponse<List<SingleChoiceOptionTranslationDTOResponse>?>> GetTranslationsBySingleChoiceOptionId(Guid? singleChoiceOptionId);
    }


}
