using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    /// <summary>
    /// DTO (Data Transfer Object) para adicionar uma nova pergunta a um modelo de formulário.
    /// </summary>
    public class QuestionDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o identificador único do modelo de formulário ao qual a pergunta será associada.
        /// </summary>
        [Required(ErrorMessage = "Please select a FormTemplate")]
        public Guid? FormTemplateId { get; set; }

        /// <summary>
        /// Obtém ou define se a resposta a esta pergunta é obrigatória.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de resposta esperado para esta pergunta.
        /// </summary>
        [Required(ErrorMessage = "Please select a response type for this question")]
        public ResponseType ResponseType { get; set; }

        /// <summary>
        /// Obtém ou define as traduções associadas a esta pergunta.
        /// </summary>
        [Required(ErrorMessage = "At least one title and description translation is required.")]
        public List<QuestionTranslationDTOAddRequest>? Translations { get; set; }

        /// <summary>
        /// Obtém ou define as opções de escolha única associadas a esta pergunta.
        /// </summary>
        public List<SingleChoiceOptionDTOAddRequest>? SingleChoiceOptions { get; set; }

        /// <summary>
        /// Obtém ou define as opções de classificação associadas a esta pergunta.
        /// </summary>
        public List<RatingOptionDTOAddRequest>? RatingOptions { get; set; }

        /// <summary>
        /// Converte a instância atual em uma entidade Question correspondente.
        /// </summary>
        /// <returns>Entidade Question correspondente.</returns>
        public Question ToQuestion()
        {
            return new Question()
            {
                IsRequired = this.IsRequired,
                ResponseType = this.ResponseType.ToString(),
                Translations = this.Translations?.Select(temp => temp.ToQuestionTranslation()).ToList() ?? new List<QuestionTranslation>(),
                SingleChoiceOptions = this.SingleChoiceOptions?.Select(temp => temp.ToSingleChoiceOption()).ToList() ?? new List<SingleChoiceOption>(),
                RatingOptions = this.RatingOptions?.Select(temp => temp.ToRatingOption()).ToList() ?? new List<RatingOption>(),
            };
        }
    }

}
