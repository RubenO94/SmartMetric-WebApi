using SmartMetric.Core.Domain.Entities;

namespace SmartMetric.Core.DTO
{
    /// <summary>
    /// Representa a resposta de uma tradução de opção de escolha única após ser inserida ou modificada na base de dados.
    /// </summary>
    public class SingleChoiceOptionTranslationDTOResponse
    {
        public Guid SingleChoiceOptionTranslationId { get; set; }
        public Guid? SingleChoiceOptionId { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }

        /// <summary>
        /// Compara os dados atuais desta tradução com outro objeto.
        /// </summary>
        /// <param name="obj">Objeto a ser comparado.</param>
        /// <returns>True se todos os detalhes da tradução correspondem ao objeto especificado, False caso contrário.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(SingleChoiceOptionTranslationDTOResponse)) return false;

            SingleChoiceOptionTranslationDTOResponse translation = (SingleChoiceOptionTranslationDTOResponse)obj;
            return this.SingleChoiceOptionTranslationId == translation.SingleChoiceOptionTranslationId &&
                   this.SingleChoiceOptionId == translation.SingleChoiceOptionId &&
                   this.Language == translation.Language &&
                   this.Description == translation.Description;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"SingleChoiceOptionTranslationId: {this.SingleChoiceOptionTranslationId}\nSingleChoiceOptionId: {this.SingleChoiceOptionId}\nLanguage: {this.Language}\nDescription:{this.Description}\n";
        }
    }

    /// <summary>
    /// Métodos de extensão para traduções de opções de escolha única.
    /// </summary>
    public static class SingleChoiceOptionTranslationsExtensions
    {
        /// <summary>
        /// Converte uma entidade de tradução de opção de escolha única para a resposta DTO correspondente.
        /// </summary>
        /// <param name="translation">Entidade de tradução de opção de escolha única.</param>
        /// <returns>A resposta DTO correspondente.</returns>
        public static SingleChoiceOptionTranslationDTOResponse ToSingleChoiceOptionTranslationDTOResponse(this SingleChoiceOptionTranslation translation)
        {
            return new SingleChoiceOptionTranslationDTOResponse()
            {
                SingleChoiceOptionTranslationId = translation.SingleChoiceOptionTranslationId,
                SingleChoiceOptionId = translation.SingleChoiceOptionId,
                Language = translation.Language,
                Description = translation.Description
            };
        }
    }

}
