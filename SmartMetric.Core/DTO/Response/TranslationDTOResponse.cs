using SmartMetric.Core.Domain.Entities;

namespace SmartMetric.Core.DTO.Response
{
    public class TranslationDTOResponse
    {
        public Guid TranslationId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(TranslationDTOResponse)) return false;

            TranslationDTOResponse translation = (TranslationDTOResponse)obj;
            return TranslationId == translation.TranslationId &&  Language == translation.Language && Title == translation.Title && Description == translation.Description;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"TranslationId: {TranslationId}\nLanguage: {Language}\nTitle: {Title}\nDescription: {Description}\n";
        }
    }

    public static class TranslationExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto FormTemplateTranslation em um objeto TranslationDTOResponse.
        /// </summary>
        /// <param name="formTemplateTranslation">O objeto FormTemplateTranslation a ser convertido.</param>
        /// <returns>Retorna o TranslationDTOResponse convertido.</returns>
        public static TranslationDTOResponse ToTranslationDTOResponse(this FormTemplateTranslation formTemplateTranslation)
        {
            return new TranslationDTOResponse()
            {
                TranslationId = formTemplateTranslation.FormTemplateTranslationId,
                Language = formTemplateTranslation.Language,
                Title = formTemplateTranslation.Title,
                Description = formTemplateTranslation.Description,
            };
        }

        /// <summary>
        /// Um método de extensão que converte um objeto ReviewTranslation em um objeto TranslationDTOResponse.
        /// </summary>
        /// <param name="reviewTranslation">O objeto ReviewTranslation a ser convertido.</param>
        /// <returns>Retorna o TranslationDTOResponse convertido.</returns>
        public static TranslationDTOResponse ToTranslationDTOResponse(this ReviewTranslation reviewTranslation)
        {
            return new TranslationDTOResponse()
            {
                TranslationId = reviewTranslation.ReviewTranslationId,
                Language = reviewTranslation.Language,
                Title = reviewTranslation.Title,
                Description = reviewTranslation.Description,
            };
        }

        /// <summary>
        /// Um método de extensão que converte um objeto QuestionTranslation em um objeto TranslationDTOResponse.
        /// </summary>
        /// <param name="questionTranslation">O objeto QuestionTranslation a ser convertido.</param>
        /// <returns>Retorna o TranslationDTOResponse convertido.</returns>
        public static TranslationDTOResponse ToTranslationDTOResponse(this QuestionTranslation questionTranslation)
        {
            return new TranslationDTOResponse()
            {
                TranslationId = questionTranslation.QuestionTranslationId,
                Language = questionTranslation.Language,
                Title = questionTranslation.Title,
                Description = questionTranslation.Description,
            };
        }

        /// <summary>
        /// Um método de extensão que converte um objeto RatingOptionTranslation em um objeto TranslationDTOResponse.
        /// </summary>
        /// <param name="ratingOptionTranslation">O objeto RatingOptionTranslation a ser convertido.</param>
        /// <returns>Retorna o TranslationDTOResponse convertido.</returns>
        public static TranslationDTOResponse ToTranslationDTOResponse(this RatingOptionTranslation ratingOptionTranslation)
        {
            return new TranslationDTOResponse()
            {
                TranslationId = ratingOptionTranslation.RatingOptionTranslationId,
                Language = ratingOptionTranslation.Language,
                Description = ratingOptionTranslation.Description,
            };
        }

        /// <summary>
        /// Um método de extensão que converte um objeto SingleChoiceOptionTranslation em um objeto TranslationDTOResponse.
        /// </summary>
        /// <param name="singleChoiceOptionTranslation">O objeto SingleChoiceOptionTranslation a ser convertido.</param>
        /// <returns>Retorna o TranslationDTOResponse convertido.</returns>
        public static TranslationDTOResponse ToTranslationDTOResponse(this SingleChoiceOptionTranslation singleChoiceOptionTranslation)
        {
            return new TranslationDTOResponse()
            {
                TranslationId = singleChoiceOptionTranslation.SingleChoiceOptionTranslationId,
                Language = singleChoiceOptionTranslation.Language,
                Description = singleChoiceOptionTranslation.Description,
            };
        }
    }
}
