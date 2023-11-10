using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Domain.Entities
{
    /// <summary>
    /// Representa uma questão que pode ser incluída em modelos de formulários e revisões.
    /// </summary>
    public class Question
    {
        /// <summary>
        /// Obtém ou define o identificador único da questão.
        /// </summary>
        [Key]
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define as associações com os relacionamentos de modelo de formulário para esta questão.
        /// </summary>
        public virtual ICollection<FormTemplateQuestion>? FormTemplateQuestions { get; set; }

        /// <summary>
        /// Obtém ou define as associações com os relacionamentos de revisão para esta questão.
        /// </summary>
        public virtual ICollection<ReviewQuestion>? ReviewQuestions { get; set; }

        /// <summary>
        /// Obtém ou define as traduções associadas a esta questão para diferentes idiomas.
        /// </summary>
        public virtual ICollection<QuestionTranslation>? Translations { get; set; }

        /// <summary>
        /// Obtém ou define as opções de classificação associadas a esta questão.
        /// </summary>
        public virtual ICollection<RatingOption>? RatingOptions { get; set; }

        /// <summary>
        /// Obtém ou define as opções de escolha única associadas a esta questão.
        /// </summary>
        public virtual ICollection<SingleChoiceOption>? SingleChoiceOptions { get; set; }

        /// <summary>
        /// Obtém ou define se a resposta a esta questão é obrigatória.
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Obtém ou define o tipo de resposta esperado para esta questão.
        /// </summary>
        [StringLength(20)]
        public string? ResponseType { get; set; }

        /// <summary>
        /// Obtém ou define a posição desta questão em um formulário ou revisão.
        /// </summary>
        public int? Position { get; set; }
    }

}
