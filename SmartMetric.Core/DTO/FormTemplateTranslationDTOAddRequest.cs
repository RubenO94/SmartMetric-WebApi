using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartMetric.Core.DTO
{
    /// <summary>
    /// Faz a transferencia de dados para inserir uma nova tradução do tipo FormTemplateTranslation
    /// </summary>
    public class FormTemplateTranslationDTOAddRequest
    {
        [Required(ErrorMessage = "Please select a FormTemplate")]
        public Guid? FormTemplateId { get; set; }
        [Required(ErrorMessage = "Please select a Language")]
        public Language? Language { get; set; }
        [Required(ErrorMessage = "Title can't be blank")]
        public string? Title { get; set; }
        public string? Description { get; set; }


        /// <summary>
        /// Convert o atual obejto Request em um novo objeto do tipo FormTemplateTranslation
        /// </summary>
        /// <returns></returns>
        public FormTemplateTranslation ToFormTemplateTranslation()
        {
            return new FormTemplateTranslation() { FormTemplateId = FormTemplateId, Language = Language.ToString(), Title = Title, Description = Description };
        }

    }
}
