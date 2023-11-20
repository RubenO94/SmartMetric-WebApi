using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
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
        [JsonIgnore]
        public Guid? FormTemplateId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da revisão ao qual a pergunta será associada.
        /// </summary>
        [JsonIgnore]
        public Guid? ReviewId { get; set; }

        /// <summary>
        /// Obtém ou define se a resposta a esta pergunta é obrigatória.
        /// </summary>
        [Required(ErrorMessage ="Please select a option for IsRequired")]
        public bool IsRequired { get; set; }

        /// <summary>
        /// Obtém ou define o posicionamento desta pergunta na sua lista.
        /// </summary>
        [Required(ErrorMessage ="Please select a position number for this  question")]
        public int Position { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de resposta esperado para esta pergunta.
        /// </summary>
        [Required(ErrorMessage = "Please select a response type for this question")]
        public ResponseType ResponseType { get; set; }

        /// <summary>
        /// Obtém ou define as traduções associadas a esta pergunta.
        /// </summary>
        [Required(ErrorMessage = "Please ensure that the question is inserted in at least one language.")]
        [MinLength(1, ErrorMessage = "Please ensure that the question is inserted in at least one language.")]
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
                FormTemplateId = FormTemplateId,
                ReviewId = ReviewId,
                Position = Position,
                IsRequired = this.IsRequired,
                ResponseType = this.ResponseType.ToString(),
                Translations = this.Translations?.Select(temp => temp.ToQuestionTranslation()).ToList() ?? new List<QuestionTranslation>(),
                SingleChoiceOptions = this.SingleChoiceOptions?.Select(temp => temp.ToSingleChoiceOption()).ToList() ?? new List<SingleChoiceOption>(),
                RatingOptions = this.RatingOptions?.Select(temp => temp.ToRatingOption()).ToList() ?? new List<RatingOption>(),
            };
        }
    }

}
