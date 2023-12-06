using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.Entities.Common;
using SmartMetric.Core.DTO.UpdateRequest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Helpers
{
    public static class UpdateHelper
    {
        public static void UpdateTranslations<TTranslation, TDTO>(
        ICollection<TTranslation>? existingTranslations,
        IEnumerable<TDTO>? newTranslations)
        where TTranslation : BaseTranslation
        where TDTO : TranslationDTOUpdate
        {
            if (existingTranslations == null || newTranslations == null) return;

            foreach (var translationUpdate in newTranslations)
            {
                var existingTranslation = existingTranslations
                    .FirstOrDefault(temp => temp.Language!.ToUpper() == translationUpdate.Language.ToString());

                if (existingTranslation != null)
                {

                    existingTranslation.Description = translationUpdate.Description;

                    if (existingTranslation is FormTemplateTranslation formTemplateTranslation)
                    {
                        formTemplateTranslation.Title = translationUpdate.Title;
                    }
                    else if (existingTranslation is QuestionTranslation questionTranslation)
                    {
                        questionTranslation.Title = translationUpdate.Title;
                    }
                    else if (existingTranslation is ReviewTranslation reviewTranslation)
                    {
                        reviewTranslation.Title = translationUpdate.Title;
                    }
                }
                else
                {

                    if (existingTranslations is ICollection<FormTemplateTranslation> formTemplateTranslations)
                    {
                        formTemplateTranslations.Add(translationUpdate.ToFormTemplateTranslation());
                    }
                    else if (existingTranslations is ICollection<QuestionTranslation> questionTranslations)
                    {
                        questionTranslations.Add(translationUpdate.ToQuestionTranslation());
                    }
                    else if (existingTranslations is ICollection<RatingOptionTranslation> ratingOptionTranslations)
                    {
                        ratingOptionTranslations.Add(translationUpdate.ToRatingOptionTranslation());
                    }
                    else if (existingTranslations is ICollection<SingleChoiceOptionTranslation> singleChoiceOptionTranslations)
                    {
                        singleChoiceOptionTranslations.Add(translationUpdate.ToSingleChoiceOptionTranslation());
                    }
                    else if (existingTranslations is ICollection<ReviewTranslation> reviewTranslations)
                    {
                        reviewTranslations.Add(translationUpdate.ToReviewTranslation());
                    }
                }
            }
        }

    }
}
