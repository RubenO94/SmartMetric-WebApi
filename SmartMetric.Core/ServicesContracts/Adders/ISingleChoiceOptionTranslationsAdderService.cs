using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    /// <summary>
    /// Define o serviço para adição de traduções de opções de resposta única.
    /// </summary>
    public interface ISingleChoiceOptionTranslationsAdderService
    {
        /// <summary>
        /// Adiciona uma tradução de um modelo de resposta única à sua lista de traduções.
        /// </summary>
        /// <param name="request">A Tradução a ser adicionada.</param>
        /// <returns>Retorna o objeto SingleChoiceOptionTranslationDTOResponse em caso de sucesso, ou null em caso de falha.</returns>
        Task<ApiResponse<SingleChoiceOptionTranslationDTOResponse?>> AddSingleChoiceOptionTranslation(Guid? singleCHoiceOption, SingleChoiceOptionTranslationDTOAddRequest? request);
    }
}
