﻿using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;

namespace SmartMetric.Core.ServicesContracts.SingleChoiceOptionTranslations
{
    public interface ISingleChoiceOptionTranslationDeleterService
    {
        /// <summary>
        /// Exclui a tradução de uma opção de escolha única com base no ID da opção de escolha única e no idioma fornecidos como parâmetros.
        /// </summary>
        /// <param name="singleChoiceOptionId">>O GUID da escolha única a ser pesquisada</param>
        /// /// <param name="language">O idioma da tradução a ser excluída.</param>
        /// <returns>Retorna true se a exclusão for bem-sucedida; caso contrário, retorna false.</returns>
        Task<ApiResponse<bool>> DeleteSingleChoiceOptionTranslationById(Guid? singleChoiceOptionId, Language? language);
    }
}
