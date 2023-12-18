using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.Entities.Common;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Helpers
{
    /// <summary>
    /// Classe auxiliar que fornece métodos para atualização de entidades relacionadas a formulários e revisões, como traduções, perguntas e opções.
    /// </summary>
    public static class UpdateHelper
    {
        /// <summary>
        /// Atualiza as perguntas existentes com base nas perguntas atualizadas.
        /// </summary>
        public static void UpdateQuestions(ICollection<Question>? existingQuestions, IEnumerable<QuestionDTOUpdate>? newQuestions)
        {
            if (existingQuestions == null || newQuestions == null) return;

            // Remover perguntas que não estão presentes nas perguntas atualizadas
            var questionsToRemove = existingQuestions
                .Where(existingQuestion =>
                    newQuestions.All(questionUpdate => questionUpdate.QuestionId != existingQuestion.QuestionId))
                .ToList();

            foreach (var questionUpdate in newQuestions)
            {
                var existingQuestion = existingQuestions
                    .FirstOrDefault(q => q.QuestionId == questionUpdate.QuestionId);

                if (existingQuestion != null)
                {
                    existingQuestion.Position = questionUpdate.Position;
                    existingQuestion.IsRequired = questionUpdate.IsRequired;
                    existingQuestion.ResponseType = questionUpdate.ResponseType.ToString();

                    // Question Translations
                    UpdateTranslations(existingQuestion.Translations, questionUpdate.Translations);

                    // Question Rating Options
                    if (existingQuestion.ResponseType == ResponseType.Rating.ToString() && questionUpdate.RatingOptions != null && questionUpdate.RatingOptions.Any())
                    {
                        UpdateRatingOptions(existingQuestion.RatingOptions, questionUpdate.RatingOptions);
                    }
                    // Questions SingleChoice Options
                    else if (existingQuestion.ResponseType == ResponseType.SingleChoice.ToString() && questionUpdate.SingleChoiceOptions != null && questionUpdate.SingleChoiceOptions.Any())
                    {
                        UpdateSingleChoiceOptions(existingQuestion.SingleChoiceOptions, questionUpdate.SingleChoiceOptions);
                    }
                }
                else
                {
                    // Adicionar nova pergunta
                    existingQuestions.Add(new Question
                    {
                        // Preencher propriedades da nova pergunta com base na DTO
                        QuestionId = questionUpdate.QuestionId,
                        ResponseType = questionUpdate.ResponseType.ToString(),
                        Position = questionUpdate.Position,
                        IsRequired = questionUpdate.IsRequired,

                        // Question Translations
                        Translations = questionUpdate.Translations?.Select(t =>
                            new QuestionTranslation
                            {
                                Language = t.Language.ToString(),
                                Description = t.Description,
                                Title = t.Title
                            }).ToList(),

                        // Question Rating Options
                        RatingOptions = questionUpdate.RatingOptions?.Select(o =>
                            new RatingOption
                            {
                                NumericValue = o.NumericValue,
                                Translations = o.Translations?.Select(t =>
                                    new RatingOptionTranslation
                                    {
                                        Language = t.Language.ToString(),
                                        Description = t.Description
                                    }).ToList()
                            }).ToList(),

                        // Questions SingleChoice Options
                        SingleChoiceOptions = questionUpdate.SingleChoiceOptions?.Select(o =>
                            new SingleChoiceOption
                            {
                                Translations = o.Translations?.Select(t =>
                                    new SingleChoiceOptionTranslation
                                    {
                                        Language = t.Language.ToString(),
                                        Description = t.Description
                                    }).ToList()
                            }).ToList()
                    });
                }

            }

            // Remover perguntas que não estão presentes nas perguntas atualizadas
            foreach (var questionToRemove in questionsToRemove)
            {
                existingQuestions.Remove(questionToRemove);
            }
        }

        /// <summary>
        /// Atualiza as traduções existentes com base nas traduções atualizadas.
        /// </summary>
        public static void UpdateTranslations<TTranslation, TDTO>(
        ICollection<TTranslation>? existingTranslations,
        IEnumerable<TDTO>? newTranslations)
        where TTranslation : BaseTranslation
        where TDTO : TranslationDTOUpdate
        {
            if (existingTranslations == null || newTranslations == null) return;

            var existingTranslationsList = existingTranslations.ToList();

            // Encontrar traduções que não estão presentes nas traduções atualizadas e removê-las
            var translationsToRemove = existingTranslationsList
                .Where(existingTranslation =>
                    newTranslations.All(translationUpdate => translationUpdate.Language.ToString() != existingTranslation.Language!.ToUpper()))
                .ToList();

            foreach (var translationUpdate in newTranslations)
            {
                var existingTranslation = existingTranslationsList
                    .FirstOrDefault(temp => temp.Language!.ToUpper() == translationUpdate.Language.ToString());

                if (existingTranslation != null)
                {
                    // Atualizar tradução existente
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

            // Remover traduções que não estão presentes nas traduções atualizadas
            foreach (var translationToRemove in translationsToRemove)
            {
                existingTranslations.Remove(translationToRemove);
            }

            //// Converter de volta para ICollection
            //existingTranslations.Clear();
            //foreach (var item in existingTranslationsList)
            //{
            //    existingTranslations.Add(item);
            //}
        }

        /// <summary>
        /// Atualiza as opções de classificação existentes com base nas opções de classificação atualizadas.
        /// </summary>
        public static void UpdateRatingOptions(ICollection<RatingOption>? existingOptions, IEnumerable<RatingOptionDTOUpdate>? newOptions)
        {
            if (existingOptions == null || newOptions == null) return;

            var existingOptionsList = existingOptions.ToList();

            var optionsToRemove = existingOptionsList
                .Where(existingOption => newOptions.All(optionUpdate => optionUpdate.RatingOptionId != existingOption.RatingOptionId))
                .ToList();

            foreach (var optionUpdate in newOptions)
            {
                var existingOption = existingOptionsList.FirstOrDefault(o => o.RatingOptionId == optionUpdate.RatingOptionId);

                if (existingOption != null)
                {
                    // Update translations for RatingOption
                    UpdateTranslations(existingOption.Translations, optionUpdate.Translations);
                    existingOption.NumericValue = optionUpdate.NumericValue;
                }
                else
                {
                    existingOptionsList.Add(new RatingOption
                    {
                        NumericValue = optionUpdate.NumericValue,
                        Translations = optionUpdate.Translations?.Select(t =>
                            new RatingOptionTranslation
                            {
                                Language = t.Language.ToString(),
                                Description = t.Description
                            }).ToList(),
                    });
                }
            }

            // Remover opções que não estão presentes nas opções atualizadas
            foreach (var optionToRemove in optionsToRemove)
            {
                existingOptionsList.Remove(optionToRemove);
            }

            // Converter de volta para ICollection
            existingOptions.Clear();
            foreach (var item in existingOptionsList)
            {
                existingOptions.Add(item);
            }
        }

        /// <summary>
        /// Atualiza as opções de escolha única existentes com base nas opções de escolha única atualizadas.
        /// </summary>
        public static void UpdateSingleChoiceOptions(ICollection<SingleChoiceOption>? existingOptions, IEnumerable<SingleChoiceDTOUpdate>? newOptions)
        {
            if (existingOptions == null || newOptions == null) return;

            var existingOptionsList = existingOptions.ToList();

            var optionsToRemove = existingOptionsList
                .Where(existingOption => newOptions.All(optionUpdate => optionUpdate.SingleChoiceOptionId != existingOption.SingleChoiceOptionId))
                .ToList();

            foreach (var optionUpdate in newOptions)
            {
                var existingOption = existingOptionsList.FirstOrDefault(o => o.SingleChoiceOptionId == optionUpdate.SingleChoiceOptionId);

                if (existingOption != null)
                {
                    // Update translations for SingleChoiceOption
                    UpdateTranslations(existingOption.Translations, optionUpdate.Translations);
                }
                else
                {
                    existingOptionsList.Add(new SingleChoiceOption
                    {
                        Translations = optionUpdate.Translations?.Select(t =>
                            new SingleChoiceOptionTranslation
                            {
                                Language = t.Language.ToString(),
                                Description = t.Description
                            }).ToList(),
                    });
                }
            }

            // Remover opções que não estão presentes nas opções atualizadas
            foreach (var optionToRemove in optionsToRemove)
            {
                existingOptionsList.Remove(optionToRemove);
            }

            // Converter de volta para ICollection
            existingOptions.Clear();
            foreach (var item in existingOptionsList)
            {
                existingOptions.Add(item);
            }
        }


        public static void UpdateReview(Review existingReview, ReviewDTOUpdate newReview)
        {
            if (existingReview == null || newReview == null) return;

            existingReview.StartDate = newReview.StartDate;
            existingReview.EndDate = newReview.EndDate;

            // Atualizar traduções da revisão
            UpdateTranslations(existingReview.Translations, newReview.Translations);

            // Atualizar perguntas da revisão
            UpdateQuestions(existingReview.Questions, newReview.Questions);

            //Atualizar os departamentos da revisão
            UpdateReviewDepartments(existingReview.Departments, newReview.DepartmentIds);
        }

        public static void UpdateReviewDepartments(ICollection<ReviewDepartment>? existingDepartments, IEnumerable<int>? newDepartmentIds)
        {
            if (existingDepartments == null || newDepartmentIds == null) return;

            var existingDepartmentsList = existingDepartments.ToList();

            // Encontrar departamentos que não estão presentes na lista atualizada e removê-los
            var departmentsToRemove = existingDepartmentsList
                .Where(existingDepartment => !newDepartmentIds.Contains(existingDepartment.DepartmentId))
                .ToList();

            // Adicionar novos departamentos que não estão presentes na lista existente
            var departmentsToAdd = newDepartmentIds
                .Where(newDepartmentId => existingDepartmentsList.All(existingDepartment => existingDepartment.DepartmentId != newDepartmentId))
                .Select(newDepartmentId => new ReviewDepartment { DepartmentId = newDepartmentId })
                .ToList();

            // Remover departamentos que não estão presentes na lista atualizada
            foreach (var departmentToRemove in departmentsToRemove)
            {
                existingDepartmentsList.Remove(departmentToRemove);
            }

            // Adicionar novos departamentos
            existingDepartmentsList.AddRange(departmentsToAdd);

            // Converter de volta para ICollection
            existingDepartments.Clear();
            foreach (var item in existingDepartmentsList)
            {
                existingDepartments.Add(item);
            }
        }


    }
}
