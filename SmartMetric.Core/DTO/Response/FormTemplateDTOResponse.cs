using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class FormTemplateDTOResponse
    {
        public Guid FormTemplateId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? CreatedByUserId { get; set; }
        public List<FormTemplateTranslationDTOResponse>? Translations { get; set; }
        public List<QuestionDTOResponse>? Questions { get; set; }


        /// <summary>
        /// Compara os dados atuais deste objeto com o parâmetro.
        /// </summary>
        /// <param name="obj">O objeto parâmetro a ser comparado.</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem ao objeto especificado no parâmetro.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(FormTemplateDTOResponse)) return false;

            FormTemplateDTOResponse formTemplate = (FormTemplateDTOResponse)obj;
            return FormTemplateId == formTemplate.FormTemplateId && CreatedDate == formTemplate.CreatedDate && ModifiedDate == formTemplate.ModifiedDate && CreatedByUserId == formTemplate.CreatedByUserId && Translations == formTemplate.Translations && Questions == formTemplate.Questions;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"FormTemplateId: {FormTemplateId}\nCreatedDate: {CreatedDate?.ToString("dd-MM-yyyy")}\nModifiedDate: {ModifiedDate?.ToString("dd-MM-yyyy")}\nTranslations count: {Translations?.Count()}\nQuestions count: {Questions?.Count()}";
        }
    }


    public static class FormTemplateExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto FormTemplateExtensions em um objeto FormTemplateDTOResponse.
        /// </summary>
        /// <param name="formTemplate">O objeto FormTemplate a ser convertido.</param>
        /// <returns>Retorna o FormTemplateDTOResponse convertido.</returns>
        public static FormTemplateDTOResponse ToFormTemplateDTOResponse(this FormTemplate formTemplate)
        {

            return new FormTemplateDTOResponse()
            {
                FormTemplateId = formTemplate.FormTemplateId,
                CreatedByUserId = formTemplate.CreatedByUserId,
                CreatedDate = formTemplate.CreatedDate,
                ModifiedDate = formTemplate.ModifiedDate,
                Translations = formTemplate.Translations?.Select(temp => temp.ToFormTemplateTranslationDTOResponse()).ToList() ?? new List<FormTemplateTranslationDTOResponse>(),
                Questions = formTemplate.Questions?.Select(q => q.ToQuestionDTOResponse()).ToList() ?? new List<QuestionDTOResponse>(),//Convertendo ICollection para List
            };
        }
    }
}
