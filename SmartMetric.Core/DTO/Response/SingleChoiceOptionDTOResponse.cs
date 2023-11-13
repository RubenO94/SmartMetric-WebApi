using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    /// <summary>
    /// DTO (Data Transfer Object) para a resposta de uma opção de escolha única.
    /// </summary>
    public class SingleChoiceOptionDTOResponse
    {
        /// <summary>
        /// Obtém ou define o identificador único da opção de escolha única.
        /// </summary>
        public Guid SingleChoiceOptionId { get; set; }

        /// <summary>
        /// Obtém ou define o identificador único da pergunta associada.
        /// </summary>
        private Guid QuestionId { get; set; }

        /// <summary>
        /// Obtém ou define as traduções associadas a esta opção de escolha única.
        /// </summary>
        public List<SingleChoiceOptionTranslationDTOResponse>? Translations { get; set; }

        /// <summary>
        /// Compara os dados atuais desta opção de escolha única com outro objeto.
        /// </summary>
        /// <param name="obj">Objeto a ser comparado.</param>
        /// <returns>True se todos os detalhes da opção de escolha única correspondem ao objeto especificado, False caso contrário.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SingleChoiceOptionDTOResponse)) return false;

            SingleChoiceOptionDTOResponse singleChoiceOption = (SingleChoiceOptionDTOResponse)obj;
            return SingleChoiceOptionId == singleChoiceOption.SingleChoiceOptionId && Translations == singleChoiceOption.Translations;
        }

        /// <summary>
        /// Obtém o código de hash para esta instância.
        /// </summary>
        /// <returns>Código de hash.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Obtém uma representação de string desta instância.
        /// </summary>
        /// <returns>String que representa esta instância.</returns>
        public override string ToString()
        {
            return $"SingleChoiceOptionId: {SingleChoiceOptionId}\nTranslations count: {Translations?.Count()}";
        }
    }

    /// <summary>
    /// Classe de extensão para converter uma entidade SingleChoiceOption em SingleChoiceOptionDTOResponse.
    /// </summary>
    public static class SingleChoiceOptionsExtensions
    {
        /// <summary>
        /// Converte uma instância de SingleChoiceOption em SingleChoiceOptionDTOResponse.
        /// </summary>
        /// <param name="singleChoiceOption">Instância de SingleChoiceOption.</param>
        /// <returns>Instância de SingleChoiceOptionDTOResponse correspondente.</returns>
        public static SingleChoiceOptionDTOResponse ToSingleChoiceOptionDTOResponse(this SingleChoiceOption singleChoiceOption)
        {
            return new SingleChoiceOptionDTOResponse()
            {
                SingleChoiceOptionId = singleChoiceOption.SingleChoiceOptionId,
                Translations = singleChoiceOption.Translations?.Select(temp => temp.ToSingleChoiceOptionTranslationDTOResponse()).ToList(),
            };
        }
    }

}
