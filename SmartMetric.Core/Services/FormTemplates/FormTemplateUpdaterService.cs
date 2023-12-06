using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.Services.FormTemplates;
using SmartMetric.Core.ServicesContracts.FormTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.FormTemplates
{
    public class FormTemplateUpdaterService : IFormTemplateUpdaterService
    {
        private readonly IFormTemplateRepository _formTemplateRepository;
        private readonly ILogger<FormTemplateUpdaterService> _logger;
        public FormTemplateUpdaterService(IFormTemplateRepository formTemplateRepository, ILogger<FormTemplateUpdaterService> logger)
        {
            _formTemplateRepository = formTemplateRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<FormTemplateDTOResponse>> UpdateFormTemplate(Guid? formtemplateId, FormTemplateDTOUpdate? formTemplateUpdate)
        {
            _logger.LogInformation($"{nameof(FormTemplateUpdaterService)}.{nameof(UpdateFormTemplate)} foi iniciado");

            if (formTemplateUpdate == null) throw new ArgumentNullException(nameof(formTemplateUpdate));

            if (formtemplateId == null) throw new ArgumentNullException(nameof(formtemplateId));

            var matchingFormTemplate = await _formTemplateRepository.GetFormTemplateById(formtemplateId);

            if (matchingFormTemplate == null) throw new ArgumentException("The given formTemplateId does not exist", nameof(formtemplateId));

            matchingFormTemplate.ModifiedDate = DateTime.UtcNow;

            // FormTemplate Translations
            UpdateHelper.UpdateTranslations(matchingFormTemplate.Translations, formTemplateUpdate.Translations);

            // FormTemplate Questions
            if (formTemplateUpdate.Questions != null && formTemplateUpdate.Questions.Any())
            {
                foreach (var questionUpdate in formTemplateUpdate.Questions)
                {
                    var existingQuestion = matchingFormTemplate.Questions!
                        .FirstOrDefault(q => q.QuestionId == questionUpdate.QuestionId);

                    if (existingQuestion != null)
                    {
                        existingQuestion.Position = questionUpdate.Position;
                        existingQuestion.IsRequired = questionUpdate.IsRequired;

                        // Question Translations
                        UpdateHelper.UpdateTranslations(existingQuestion.Translations, questionUpdate.Translations);

                        // Question Rating Options
                        if (existingQuestion.ResponseType == ResponseType.Rating.ToString() && questionUpdate.RatingOptions != null && questionUpdate.RatingOptions.Any())
                        {
                            foreach (var ratingOptionUpdate in questionUpdate.RatingOptions)
                            {
                                var existingRatingOption = existingQuestion.RatingOptions!
                                    .FirstOrDefault(ro => ro.RatingOptionId == ratingOptionUpdate.RatingOptionId);

                                if (existingRatingOption != null)
                                {
                                    existingRatingOption.NumericValue = ratingOptionUpdate.NumericValue;
                                }
                                else
                                {
                                    // Se a opção de classificação não existe, é criada uma nova
                                    existingQuestion.RatingOptions!.Add(new RatingOption
                                    {
                                        NumericValue = ratingOptionUpdate.NumericValue,
                                        Translations = ratingOptionUpdate.Translations!.Select(t =>
                                            new RatingOptionTranslation()
                                            {
                                                Language = t.Language.ToString(),
                                                Description = t.Description
                                            }).ToList(),
                                    });
                                }

                                // Atualizar traduções da opção de classificação se houver atualizações
                                UpdateHelper.UpdateTranslations(existingRatingOption!.Translations, ratingOptionUpdate.Translations);
                            }
                        }
                        // Questions SingleChoice Options
                        else if (existingQuestion.ResponseType == ResponseType.SingleChoice.ToString() && questionUpdate.SingleChoiceOptions != null && questionUpdate.SingleChoiceOptions.Any())
                        {
                            foreach (var singleChoiceOptionUpdate in questionUpdate.SingleChoiceOptions)
                            {
                                var existingSingleChoiceOption = existingQuestion.SingleChoiceOptions!
                                    .FirstOrDefault(sc => sc.SingleChoiceOptionId == singleChoiceOptionUpdate.SingleChoiceOptionId);

                                if (existingSingleChoiceOption == null)
                                {
                                    existingQuestion.SingleChoiceOptions!.Add(new SingleChoiceOption
                                    {
                                        Translations = singleChoiceOptionUpdate.Translations!.Select(temp =>
                                            new SingleChoiceOptionTranslation()
                                            {
                                                Language = temp.Language.ToString(),
                                                Description = temp.Description
                                            }).ToList(),
                                    });
                                }
                                else
                                {
                                    // Atualizar traduções da opção de escolha única se houver atualizações
                                    UpdateHelper.UpdateTranslations(existingSingleChoiceOption.Translations, singleChoiceOptionUpdate.Translations);
                                }
                            }
                        }
                    }
                }
            }

            var result = await _formTemplateRepository.UpdateFormTemplate(matchingFormTemplate);

            return new ApiResponse<FormTemplateDTOResponse>()
            {
                StatusCode = 200,
                Message = "FormTemplate updated with success!",
                Data = result.ToFormTemplateDTOResponse(),
            };
        }

    }
}
