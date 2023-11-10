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
    /// Representa a resposta de um objeto SingleChoiceOptionTranslation apos ser inserido ou modificado na base dados
    /// </summary>
    public class SingleChoiceOptionTranslationDTOResponse
    {
        public Guid SingleChoiceOptionTranslationId { get; set; }
        public Guid SingleChoiceOptionId { get; set; }
        public Language? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        /// <summary>
        /// Compara os dados atuais deste objeto com o parametro
        /// </summary>
        /// <param name="obj">Objeto parametro para ser comparado</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem com o objeto especificado no parametro</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SingleChoiceOptionTranslationDTOResponse)) return false;

            SingleChoiceOptionTranslationDTOResponse translation = (SingleChoiceOptionTranslationDTOResponse)obj;
            return this.SingleChoiceOptionTranslationId == translation.SingleChoiceOptionTranslationId && this.SingleChoiceOptionId == translation.SingleChoiceOptionId && this.Language == translation.Language && this.Title == translation.Title && this.Description == translation.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"FormTemplateTranslationdId: {this.SingleChoiceOptionTranslationId}\nFormTemplateId: {this.SingleChoiceOptionId}\nTitle: {this.Title}\nDescription:{this.Description}\n";
        }
    }
}
