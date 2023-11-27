﻿using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartMetric.Core.DTO.AddRequest
{
    /// <summary>
    /// DTO de solicitação para adicionar uma nova revisão.
    /// </summary>
    public class ReviewDTOAddRequest
    {
        /// <summary>
        /// Obtém ou define o identificador do usuário que criou a revisão. Este campo é obrigatório.
        /// </summary>
        [Required(ErrorMessage = "CreatedByUserId is required.")]
        public int? CreatedByUserId { get; set; }

        /// <summary>
        /// Obtém ou define a data de criação da revisão.
        /// </summary>
        [JsonIgnore]
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Obtém ou define a data de início da revisão.
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Obtém ou define a data de término da revisão.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de revisão. Este campo é obrigatório.
        /// </summary>
        [Required(ErrorMessage = "ReviewType is required.")]
        public ReviewType? ReviewType { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de assunto da revisão. Este campo é obrigatório.
        /// </summary>
        [Required(ErrorMessage = "SubjectType is required.")]
        public SubjectType? SubjectType { get; set; }

        /// <summary>
        /// Obtém ou define o status da revisão. Este campo é obrigatório e tem um valor padrão de "NotStarted".
        /// </summary>
        [Required(ErrorMessage = "ReviewStatus is required.")]
        public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.NotStarted;

        /// <summary>
        /// Obtém ou define as traduções associadas à revisão. Deve haver pelo menos uma tradução.
        /// </summary>
        [Required(ErrorMessage = "At least one translation is required.")]
        [MinLength(1, ErrorMessage = "At least one translation is required.")]
        public List<ReviewTranslationDTOAddRequest>? Translations { get; set; }

        /// <summary>
        /// Obtém ou define as perguntas associadas à revisão.
        /// </summary>
        public List<QuestionDTOAddRequest>? Questions { get; set; }

        /// <summary>
        /// Obtém ou define os IDs dos funcionários associados à revisão. Este campo é obrigatório.
        /// </summary>
        [Required(ErrorMessage = "ReviewEmployeeIds is required.")]
        public List<int>? ReviewEmployeeIds { get; set; }

        /// <summary>
        /// Obtém ou define os IDs dos departamentos associados à revisão. Este campo é obrigatório.
        /// </summary>
        [Required(ErrorMessage = "ReviewDepartmentsIds is required.")]
        public List<int>? ReviewDepartmentsIds { get; set; }

        /// <summary>
        /// Converte o DTO de solicitação para a entidade correspondente de revisão.
        /// </summary>
        /// <returns>A entidade de revisão.</returns>
        public Review ToReview()
        {
            return new Review()
            {
                CreatedByUserId = CreatedByUserId,
                CreatedDate = CreatedDate,
                StartDate = StartDate,
                EndDate = EndDate,
                ReviewStatus = ReviewStatus.ToString(),
                ReviewType = ReviewType.ToString(),
                SubjectType = SubjectType.ToString(),
                Translations = Translations?.Select(temp => temp.ToReviewTranslation()).ToList() ?? null
            };
        }
    }

}
