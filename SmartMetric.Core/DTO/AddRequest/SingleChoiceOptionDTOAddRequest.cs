using SmartMetric.Core.Domain.Entities;
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
    /// DTO (Data Transfer Object) para adicionar uma opção de escolha única a uma pergunta.
    /// </summary>
    public class SingleChoiceOptionDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define as traduções para a opção de escolha única, pelo menos uma tradução é necessária.
        /// </summary>
        [Required(ErrorMessage = "At least one title and description translation is required.")]
        [MinLength(1, ErrorMessage = "Please enter data in at least one language.")]
        public List<TranslationDTOAddRequest>? Translations { get; set; }

        /// <summary>
        /// Converte o DTO em uma entidade SingleChoiceOption.
        /// </summary>
        /// <returns>Instância de SingleChoiceOption.</returns>
        public SingleChoiceOption ToSingleChoiceOption()
        {
            return new SingleChoiceOption()
            {
                Translations = Translations?.Select(temp => temp.ToSingleChoiceOptionTranslation()).ToList() ?? new List<SingleChoiceOptionTranslation>(),
            };
        }
    }

}
