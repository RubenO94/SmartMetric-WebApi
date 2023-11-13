using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    /// <summary>
    /// DTO de solicitação para adicionar uma nova tradução de questão.
    /// </summary>
    public class QuestionTranslationDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o identificador da questão a ser traduzida.
        /// </summary>
        [Required(ErrorMessage = "Please select a question to translate")]
        public Guid? QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define o idioma para a tradução.
        /// </summary>
        [Required(ErrorMessage = "Please select a language")]
        public Language? Language { get; set; }

        /// <summary>
        /// Obtém ou define o título da tradução.
        /// </summary>
        [Required(ErrorMessage = "Title can't be blank")]
        public string? Title { get; set; }

        /// <summary>
        /// Obtém ou define a descrição da tradução.
        /// </summary>
        public string? Description { get; set; }


        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de tradução da questão.
        /// </summary>
        /// <returns>A entidade de tradução da questão.</returns>
        public QuestionTranslation ToQuestionTranslation()
        {
            return new QuestionTranslation()
            {
                QuestionId = this.QuestionId,
                Language = this.Language.ToString(),
                Title = this.Title,
                Description = this.Description
            };
        }
    }
}
