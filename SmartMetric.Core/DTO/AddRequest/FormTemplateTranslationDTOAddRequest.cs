﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
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
        /// Obtém ou define o identificador do modelo de formulário a ser traduzido.
        /// </summary>
        [JsonIgnore]
        public Guid? FormTemplateId { get; set; }

        /// <summary>
        /// Obtém ou define o idioma para a tradução.
        /// </summary>
        [Required(ErrorMessage ="Language is required")]
        [EnumDataType(typeof(Language), ErrorMessage ="Language inserted is a invalid option")]
        public Language? Language { get; set; }

        /// <summary>
        /// Obtém ou define o título da tradução.
        /// </summary>
        [MinLength(10, ErrorMessage ="Minimum length is 10 caracters")]
        [Required(ErrorMessage = "Title is required")]
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
