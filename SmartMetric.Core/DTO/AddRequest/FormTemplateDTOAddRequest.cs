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
    /// DTO para adicionar um novo modelo de formulário.
    /// </summary>
    public class FormTemplateDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define a data de criação.
        /// </summary>
        [DataType(DataType.Date)]
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Obtém ou define o ID do usuário que criou o formulário. Este campo é obrigatório.
        /// </summary>
        [Required(ErrorMessage = "The field CreatedByUserId is required.")]
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Obtém ou define as traduções do modelo de formulário. Deve conter pelo menos uma tradução.
        /// </summary>
        [MinLength(1, ErrorMessage = "Need at least one language.")]
        [Required(ErrorMessage = "The field Translations is required and must contain at least one language.")]
        public List<FormTemplateTranslationDTOAddRequest>? Translations { get; set; }

        /// <summary>
        /// Converte o DTO para um objeto FormTemplate.
        /// </summary>
        /// <returns>Um objeto FormTemplate populado com os dados do DTO.</returns>
        public FormTemplate ToFormTemplate()
        {
            return new FormTemplate()
            {
                CreatedDate = CreatedDate,
                CreatedByUserId = CreatedByUserId,
                Translations = Translations?.Select(temp => temp.ToFormTemplateTranslation()).ToList()
            };
        }
    }

}
