using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Services.FormTemplates;
using SmartMetric.Core.ServicesContracts.FormTemplate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.FormTemplate
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

        public async Task<FormTemplateDTOResponse> UpdateFormTemplate(Guid? formtemplateId, FormTemplateDTOUpdate? formTemplateUpdate)
        {
            _logger.LogInformation($"{nameof(FormTemplateUpdaterService)}.{nameof(UpdateFormTemplate)} foi iniciado");

            if (formTemplateUpdate == null) throw new ArgumentNullException(nameof(formTemplateUpdate));

            if (formtemplateId == null) throw new ArgumentNullException(nameof(formtemplateId));

            var matchingFormTemplate = await _formTemplateRepository.GetFormTemplateById(formtemplateId);

            if (matchingFormTemplate == null) throw new ArgumentException("The given formTemplateId does not exist", nameof(formtemplateId));

            matchingFormTemplate.ModifiedDate = DateTime.UtcNow;

            //FormTemplate Translations
            if (formTemplateUpdate.Translations != null && formTemplateUpdate.Translations.Any())
                foreach (var translationUpdate in matchingFormTemplate.Translations!)
                {
                    var existingTranslation = matchingFormTemplate.Translations.FirstOrDefault(temp => temp.Language == translationUpdate.Language);

                    if (existingTranslation != null)
                    {
                        existingTranslation.Title = translationUpdate.Title;
                        existingTranslation.Description = translationUpdate.Description;

                    }
                    else
                    {
                        matchingFormTemplate.Translations.Add(new FormTemplateTranslation()
                        {
                            Language = translationUpdate.Language,
                            Title = translationUpdate.Title,
                            Description = translationUpdate.Description,
                        });
                    }
                }

            //FormTemplate Questions
            if (formTemplateUpdate.Questions != null && formTemplateUpdate.Questions.Any())
                foreach (var questionUpdate in formTemplateUpdate.Questions!)
                {
                    var existingQuestion = matchingFormTemplate.Questions!
                    .FirstOrDefault(q => q.QuestionId == questionUpdate.QuestionId);

                    if (existingQuestion != null)
                    {
                        existingQuestion.Position = questionUpdate.Position;
                        existingQuestion.IsRequired = questionUpdate.IsRequired;

                        //Question Translations
                        if (questionUpdate.Translations != null && questionUpdate.Translations.Any())
                        {
                            foreach (var questionTranslationUpdate in questionUpdate.Translations)
                            {
                                var existingQuestionTranslation = existingQuestion.Translations!
                                    .FirstOrDefault(t => t.Language == questionTranslationUpdate.Language.ToString());

                                if (existingQuestionTranslation != null)
                                {
                                    existingQuestionTranslation.Title = questionTranslationUpdate.Title;
                                    existingQuestionTranslation.Description = questionTranslationUpdate.Description;
                                }
                                else
                                {
                                    existingQuestion.Translations!.Add(new QuestionTranslation
                                    {
                                        Language = questionTranslationUpdate.Language.ToString(),
                                        Title = questionTranslationUpdate.Title,
                                        Description = questionTranslationUpdate.Description,
                                    });
                                }
                            }
                        }

                        //Question Rating Options
                        if (questionUpdate.RatingOptions != null && questionUpdate.RatingOptions.Any())
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
                                    // Se a opção de classificação não existe, é criado uma nova
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
                                if (ratingOptionUpdate.Translations != null && ratingOptionUpdate.Translations.Any())
                                {
                                    foreach (var optionTranslationUpdate in ratingOptionUpdate.Translations)
                                    {
                                        var existingOptionTranslation = existingRatingOption!.Translations!
                                            .FirstOrDefault(t => t.Language == optionTranslationUpdate.Language.ToString());

                                        if (existingOptionTranslation != null)
                                        {
                                            existingOptionTranslation.Description = optionTranslationUpdate.Description;
                                        }
                                        else
                                        {
                                            // Se a tradução não existe, criamos uma nova
                                            existingRatingOption.Translations!.Add(new RatingOptionTranslation
                                            {
                                                Language = optionTranslationUpdate.Language.ToString(),
                                                Description = optionTranslationUpdate.Description
                                            });
                                        }
                                    }
                                }
                            }
                        }


                        //TODO: Questions SingleChoice Options

                    }
                }

            throw new NotImplementedException();
        }
    }
}
