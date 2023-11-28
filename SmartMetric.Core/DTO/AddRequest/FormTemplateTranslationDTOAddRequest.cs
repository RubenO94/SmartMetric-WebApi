using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartMetric.Core.DTO.AddRequest
{
    /// <summary>
    /// DTO de solicitação para adicionar uma nova tradução de modelo de formulário.
    /// </summary>
    public class FormTemplateTranslationDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o idioma para a tradução. Este campo é obrigatório.
        /// </summary>
        [EnumDataType(typeof(Language), ErrorMessage ="Language inserted is a invalid option")]
        [Required(ErrorMessage = "The field Language is required.")]
        public Language? Language { get; set; }

        /// <summary>
        /// Obtém ou define o título da tradução. Deve ter pelo menos 10 caracteres.
        /// </summary>
        [MinLength(10, ErrorMessage = "The title must have a minimum length of 10 characters.")]
        [Required(ErrorMessage = "The field Title is required.")]
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
                Language = Language?.ToString(),
                Title = Title,
                Description = Description
            };
        }
    }


}
