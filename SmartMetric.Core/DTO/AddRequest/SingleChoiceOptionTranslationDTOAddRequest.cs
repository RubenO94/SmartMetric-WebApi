using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    /// <summary>
    /// DTO de solicitação para adicionar uma tradução de opção de escolha única.
    /// </summary>
    public class SingleChoiceOptionTranslationDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o identificador da opção de escolha única a ser traduzida.
        /// </summary>
        [JsonIgnore]
        public Guid? SingleChoiceOptionId { get; set; }

        /// <summary>
        /// Obtém ou define o idioma para a tradução.
        /// </summary>
        [Required(ErrorMessage = "Please select a language")]
        public Language? Language { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da tradução.
        /// </summary>
        [Required(ErrorMessage = "Please insert a description for this single choice option")]
        public string? Description { get; set; }

        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de tradução de opção de escolha única.
        /// </summary>
        /// <returns>A entidade de tradução de opção de escolha única.</returns>
        public SingleChoiceOptionTranslation ToSingleChoiceOptionTranslation()
        {
            return new SingleChoiceOptionTranslation()
            {
                SingleChoiceOptionId = SingleChoiceOptionId,
                Language = Language.ToString(),
                Description = Description
            };
        }
    }

}
