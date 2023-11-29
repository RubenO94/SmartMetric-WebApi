using SmartMetric.Core.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        /// Obtém ou define o ID do utilizador que criou o formulário. Este campo é obrigatório.
        /// </summary>
        [Required(ErrorMessage = "The field CreatedByUserId is required.")]
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Obtém ou define as traduções do modelo de formulário. Deve conter pelo menos uma tradução.
        /// </summary>
        [MinLength(1, ErrorMessage = "Need at least one language.")]
        [Required(ErrorMessage = "The field Translations is required and must contain at least one language.")]
        public List<TranslationDTOAddRequest>? Translations { get; set; }

        /// <summary>
        /// Obtém ou define as questões do modelo de formulário. Deve conter pelo menos uma questão.
        /// </summary>
        [MinLength(1, ErrorMessage = "Need at least one question.")]
        [Required(ErrorMessage = "The field Questions is required and must contain at least one question.")]
        public List<QuestionDTOAddRequest>? Questions { get; set; }

        /// <summary>
        /// Converte o DTO para um objeto FormTemplate.
        /// </summary>
        /// <returns>Um objeto FormTemplate populado com os dados do DTO.</returns>
        public FormTemplate ToFormTemplate()
        {
            return new FormTemplate()
            {
                CreatedDate = DateTime.UtcNow,
                CreatedByUserId = CreatedByUserId,
                Translations = Translations?.Select(temp => temp.ToFormTemplateTranslation()).ToList(),
                Questions = Questions?.Select(temp => temp.ToQuestion()).ToList(),
            };
        }
    }

}
