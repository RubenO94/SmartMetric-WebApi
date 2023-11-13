﻿using SmartMetric.Core.Domain.Entities;

namespace SmartMetric.Core.DTO.Response
{
    /// <summary>
    /// Representa a DTO usada na maioria dos retornos para os métodos dos serviços de tradução de modelo de formulário.
    /// </summary>
    public class FormTemplateTranslationDTOResponse
    {
        public Guid FormTemplateTranslationId { get; set; }
        private Guid? FormTemplateId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }


        /// <summary>
        /// Compara os dados atuais deste objeto com o parâmetro.
        /// </summary>
        /// <param name="obj">O objeto parâmetro a ser comparado.</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem ao objeto especificado no parâmetro.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(FormTemplateTranslationDTOResponse)) return false;

            FormTemplateTranslationDTOResponse translation = (FormTemplateTranslationDTOResponse)obj;
            return FormTemplateTranslationId == translation.FormTemplateTranslationId && FormTemplateId == translation.FormTemplateId && Language == translation.Language && Title == translation.Title && Description == translation.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"FormTemplateTranslationId: {FormTemplateTranslationId}\nFormTemplateId: {FormTemplateId}\nTitle: {Title}\nDescription: {Description}\n";
        }
    }

    public static class FormTemplateTranslationExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto FormTemplateTranslation em um objeto FormTemplateTranslationDTOResponse.
        /// </summary>
        /// <param name="formTemplateTranslation">O objeto FormTemplateTranslation a ser convertido.</param>
        /// <returns>Retorna o FormTemplateTranslationDTOResponse convertido.</returns>
        public static FormTemplateTranslationDTOResponse ToFormTemplateTranslationDTOResponse(this FormTemplateTranslation formTemplateTranslation)
        {
            return new FormTemplateTranslationDTOResponse()
            {
                FormTemplateTranslationId = formTemplateTranslation.FormTemplateTranslationId,
                Language = formTemplateTranslation.Language,
                Title = formTemplateTranslation.Title,
                Description = formTemplateTranslation.Description,
            };
        }
    }

}
