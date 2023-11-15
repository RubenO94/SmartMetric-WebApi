using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class FormTemplateQuestionDTOResponse
    {
        public Guid FormTemplateQuestionId { get; set; }
        public Guid? FormTemplateId { get; set; }
        public Guid? QuestionId { get; set; }

        /// <summary>
        /// Compara os dados atuais deste objeto com o parâmetro.
        /// </summary>
        /// <param name="obj">O objeto parâmetro a ser comparado.</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem ao objeto especificado no parâmetro.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(FormTemplateQuestionDTOResponse)) return false;

            FormTemplateQuestionDTOResponse fq = (FormTemplateQuestionDTOResponse)obj;
            return FormTemplateQuestionId == fq.FormTemplateQuestionId && FormTemplateId == fq.FormTemplateId && fq.QuestionId == QuestionId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"FormTemplateQuestionId: {FormTemplateQuestionId}\nFormTemplateId: {FormTemplateId}\nQuestionId: {QuestionId}\n";
        }
    }

    public static class FormTemplateQuestionExtensions
    {
        public static FormTemplateQuestionDTOResponse ToFormTemplateQuestionDTOResponse(this FormTemplateQuestion question)
        {
            return new FormTemplateQuestionDTOResponse() {
                FormTemplateQuestionId = question.FormTemplateQuestionId,
                FormTemplateId = question.FormTemplateId,
                QuestionId = question.QuestionId
            };
        }
    }
}
