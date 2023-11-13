using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    /// <summary>
    /// Representa a DTO usada na maioria dos retornos para os métodos dos serviços de tradução da questão.
    /// </summary>
    public class QuestionTranslationDTOResponse
    {
        public Guid QuestionTranslationId { get; set; }
        public Guid? QuestionId { get; set; }
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
            if (obj.GetType() != typeof(QuestionTranslationDTOResponse)) return false;

            QuestionTranslationDTOResponse translation = (QuestionTranslationDTOResponse)obj;
            return this.QuestionTranslationId == translation.QuestionTranslationId && this.QuestionId == translation.QuestionId && this.Language == translation.Language && this.Title == translation.Title && this.Description == translation.Description;
        }

        public override int GetHashCode() 
        { 
            return base.GetHashCode(); 
        }

        public override string ToString()
        {
            return $"QuestionTranslationId: {this.QuestionTranslationId}\nQuestionId: {this.QuestionId}\nTitle: {this.Title}\nDescription: {this.Description}\n";
        }
    }

    public static class QuestionTranslationExtensions
    {
        public static QuestionTranslationDTOResponse ToQuestionTranslationDTOResponse(this QuestionTranslation questionTranslation)
        {
            return new QuestionTranslationDTOResponse()
            {
                QuestionTranslationId = questionTranslation.QuestionTranslationId,
                QuestionId = questionTranslation.QuestionId,
                Language = questionTranslation.Language,
                Title = questionTranslation.Title,
                Description = questionTranslation.Description,
            };
        }
    }
}
