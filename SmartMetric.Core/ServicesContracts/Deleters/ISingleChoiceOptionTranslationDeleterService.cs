using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Deleters
{
    public interface ISingleChoiceOptionTranslationDeleterService
    {
        /// <summary>
        /// Exclui a tradução de uma opção de escolha única com base no ID da opção de escolha única e no idioma fornecidos como parâmetros.
        /// </summary>
        /// <param name="singleChoiceOptionId">>O GUID da escolha única a ser pesquisada</param>
        /// /// <param name="language">O idioma da tradução a ser excluída.</param>
        /// <returns>Retorna true se a exclusão for bem-sucedida; caso contrário, retorna false.</returns>
        Task<bool> DeleteSingleChoiceOptionTranslationById(Guid? singleChoiceOptionId, Language? language);
    }
}
