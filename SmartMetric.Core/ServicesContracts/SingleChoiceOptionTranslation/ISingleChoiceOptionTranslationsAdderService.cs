using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;

namespace SmartMetric.Core.ServicesContracts.SingleChoiceOptionTranslations
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
        Task<ApiResponse<TranslationDTOResponse?>> AddSingleChoiceOptionTranslation(Guid? singleCHoiceOption, TranslationDTOAddRequest? request);
    }
}
