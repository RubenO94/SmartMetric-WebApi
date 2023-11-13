using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace SmartMetric.Core.DTO.AddRequest
{
    /// <summary>
    /// DTO de solicitação para adicionar uma nova tradução de modelo de formulário.
    /// </summary>
    public class FormTemplateTranslationDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o identificador do modelo de formulário a ser traduzido.
        /// </summary>
        [Required(ErrorMessage = "Por favor, selecione um modelo de formulário.")]
        public Guid? FormTemplateId { get; set; }

        /// <summary>
        /// Obtém ou define o idioma para a tradução.
        /// </summary>
        [Required(ErrorMessage = "Por favor, selecione um idioma para a tradução.")]
        public Language? Language { get; set; }

        /// <summary>
        /// Obtém ou define o título da tradução.
        /// </summary>
        [Required(ErrorMessage = "O título não pode estar em branco.")]
        public string? Title { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da tradução.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de tradução de modelo de formulário.
        /// </summary>
        /// <returns>A entidade de tradução de modelo de formulário.</returns>
        public FormTemplateTranslation ToFormTemplateTranslation()
        {
            return new FormTemplateTranslation()
            {
                FormTemplateId = FormTemplateId,
                Language = Language.ToString(),
                Title = Title,
                Description = Description
            };
        }
    }

}
