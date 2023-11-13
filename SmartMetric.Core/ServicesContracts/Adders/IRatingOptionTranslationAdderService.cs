using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    public interface IRatingOptionTranslationAdderService
    {
        /// <summary>
        /// Adiciona uma tradução de um modelo de classificação à sua lista de traduções
        /// </summary>
        /// <param name="request">A Tradução a ser adicionada</param>
        /// <returns>Retorna o objeto Tradução (incluindo o novo RatingOptionTranslationId gerado)</returns>
        Task<RatingOptionTranslationDTOResponse> AddRatingOptionTranslation(RatingOptionTranslationDTOAddRequest? request);
    }
}
