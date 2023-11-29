using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    /// <summary>
    /// DTO de solicitação para adicionar uma nova tradução.
    /// </summary>
    public class TranslationDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o idioma para a tradução. Este campo é obrigatório.
        /// </summary>
        [EnumDataType(typeof(Language), ErrorMessage = "Language inserted is a invalid option")]
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
        /// Converte o DTO de solicitação para a entidade correspondente de tradução de uma revisão.
        /// </summary>
        /// <returns>A entidade de tradução de uma revisão.</returns>
        public ReviewTranslation ToReviewTranslation()
        {
            return new ReviewTranslation()
            {
                Language = Language.ToString(),
                Title = Title,
                Description = Description
            };
        }

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

        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de tradução da questão.
        /// </summary>
        /// <returns>A entidade de tradução da questão.</returns>
        public QuestionTranslation ToQuestionTranslation()
        {
            return new QuestionTranslation()
            {
                Language = Language?.ToString(),
                Title = Title,
                Description = Description
            };
        }

        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de tradução da resposta por classificação.
        /// </summary>
        /// <returns>A entidade de tradução da resposta por classificação.</returns>
        public RatingOptionTranslation ToRatingOptionTranslation()
        {
            return new RatingOptionTranslation()
            {
                Language = Language.ToString(),
                Description = Description
            };

        }

        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de tradução de opção de escolha única.
        /// </summary>
        /// <returns>A entidade de tradução de opção de escolha única.</returns>
        public SingleChoiceOptionTranslation ToSingleChoiceOptionTranslation()
        {
            return new SingleChoiceOptionTranslation()
            {
                Language = Language.ToString(),
                Description = Description
            };
        }
    }
}
