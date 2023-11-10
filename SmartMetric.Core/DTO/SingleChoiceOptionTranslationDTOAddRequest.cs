using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    /// <summary>
    /// DTO de solicitação para adicionar uma tradução de opção de escolha única.
    /// </summary>
    public class SingleChoiceOptionTranslationDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o identificador da opção de escolha única a ser traduzida.
        /// </summary>
        [Required(ErrorMessage = "Por favor, selecione uma opção de escolha única para traduzir.")]
        public Guid? SingleChoiceOptionId { get; set; }

        /// <summary>
        /// Obtém ou define o idioma para a tradução.
        /// </summary>
        [Required(ErrorMessage = "Por favor, selecione um idioma para a tradução.")]
        public Language? Language { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da tradução.
        /// </summary>
        [Required(ErrorMessage = "A descrição não pode estar em branco.")]
        public string? Description { get; set; }

        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de tradução de opção de escolha única.
        /// </summary>
        /// <returns>A entidade de tradução de opção de escolha única.</returns>
        public SingleChoiceOptionTranslation ToSingleChoiceOptionTranslation()
        {
            return new SingleChoiceOptionTranslation()
            {
                SingleChoiceOptionId = this.SingleChoiceOptionId,
                Language = this.Language.ToString(),
                Description = this.Description
            };
        }
    }

}
