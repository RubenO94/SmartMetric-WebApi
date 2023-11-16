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
        /// Obtém ou define o identificador único da pergunta à qual a opção de escolha única será associada.
        /// </summary>
        [JsonIgnore]
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define as traduções para a opção de escolha única, pelo menos uma tradução é necessária.
        /// </summary>
        [Required(ErrorMessage = "At least one title and description translation is required.")]
        public List<SingleChoiceOptionTranslationDTOAddRequest>? Translations { get; set; }

        /// <summary>
        /// Converte o DTO em uma entidade SingleChoiceOption.
        /// </summary>
        /// <returns>Instância de SingleChoiceOption.</returns>
        public SingleChoiceOption ToSingleChoiceOption()
        {
            return new SingleChoiceOption()
            {
                QuestionId = QuestionId,
                Translations = Translations?.Select(temp => temp.ToSingleChoiceOptionTranslation()).ToList() ?? new List<SingleChoiceOptionTranslation>(),
            };
        }
    }

}
