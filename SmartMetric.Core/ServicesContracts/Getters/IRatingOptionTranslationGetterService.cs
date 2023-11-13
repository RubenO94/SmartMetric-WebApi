using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Getters
{
    /// <summary>
    /// Representa a logica de negócio receber uma lista de Traduções
    /// </summary>
    public interface IRatingOptionTranslationGetterService
    {
        /// <summary>
        /// Retorna todas as traduções das respostas de classificação
        /// </summary>
        /// <returns>Retorna uma lista de todas as linhas da tabela de traduções de resposta de classificação (RatingOptiontranslationsDTOResponse)</returns>
        Task<List<RatingOptionTranslationDTOResponse>> GetAllRatingOptionTranslations();

        /// <summary>
        /// Procura por uma tradução de uma resposta de classificação baseado no id da tradução fornecida.
        /// </summary>
        /// <param name="ratingOptionTranslationId"></param>
        /// <returns>Retorna um objeto do tipo RatingOptionTranslationDTOResponse, que corresponde à tradução pedida através do Id</returns>
        Task<RatingOptionTranslationDTOResponse?> GetRatingOptionTranslationById(Guid? ratingOptionTranslationId);

        /// <summary>
        /// Procura por todas as traduções correspondentes ao id da resposta de classificação fornecido nos parâmetros.
        /// </summary>
        /// <param name="ratingOptionId"></param>
        /// <returns>Retorna uma lista de objectos do tipo RatingOptionTranslationDTOResponse (todas as traduções da resposta de classificação)</returns>
        Task<List<RatingOptionTranslationDTOResponse>?> GetRatingOptiontranslationsByRatingOptionId(Guid? ratingOptionId);
    }
}
