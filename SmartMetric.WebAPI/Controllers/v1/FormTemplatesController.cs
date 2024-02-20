using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.FormTemplates;
using SmartMetric.Core.ServicesContracts.FormTemplateTranslations;
using SmartMetric.Core.ServicesContracts.Questions;
using SmartMetric.WebAPI.Filters.ActionFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações relacionadas a modelos de formulários.
    /// </summary>
    [ApiVersion("1.0")]
    public class FormTemplatesController : CustomBaseController
    {
        private readonly IFormTemplateGetterService _formTemplateGetterService;
        private readonly IFormTemplateAdderService _formTemplateAdderService;
        private readonly IFormTemplateDeleterService _formTemplatesDeleterService;
        private readonly IFormTemplateUpdaterService _formTemplatesUpdaterService;
        private readonly IQuestionAdderService _questionAdderService;
        private readonly IFormTemplateTranslationsAdderService _formTemplateTranslationsAdderService;
        private readonly IFormTemplateTranslationsDeleterService _formTemplateTranslationsDeleterService;
        private readonly IFormTemplateRepository _formTemplateRepository;

        /// <summary>
        /// Construtor do controlador FormTemplates.
        /// </summary>
        /// <param name="formTemplateGetterService">Serviço para obter modelos de formulários.</param>
        /// <param name="formTemplatesAdderService">Serviço para adicionar modelos de formulários.</param>
        /// <param name="formTemplatesDeleterService">Serviço para excluir modelos de formulários.</param>
        /// <param name="formTemplatesUpdaterService">Serviço para atualizar modelos de formulários.</param>
        /// <param name="questionAdderService">Serviço para adicionar perguntas a modelos de formulários.</param>
        /// <param name="formTemplateTranslationsAdderService">Serviço para adicionar traduções a modelos de formulários.</param>
        /// <param name="formTemplateTranslationsDeleterService">Serviço para excluir traduções de modelos de formulários.</param>
        /// <param name="formTemplateRepository"></param>
        public FormTemplatesController(
            IFormTemplateGetterService formTemplateGetterService,
            IFormTemplateAdderService formTemplatesAdderService,
            IFormTemplateDeleterService formTemplatesDeleterService,
            IFormTemplateUpdaterService formTemplatesUpdaterService,
            IQuestionAdderService questionAdderService,
            IFormTemplateTranslationsAdderService formTemplateTranslationsAdderService,
            IFormTemplateTranslationsDeleterService formTemplateTranslationsDeleterService,
            IFormTemplateRepository formTemplateRepository
        )
        {
            _formTemplateGetterService = formTemplateGetterService;
            _formTemplateAdderService = formTemplatesAdderService;
            _formTemplatesDeleterService = formTemplatesDeleterService;
            _formTemplatesUpdaterService = formTemplatesUpdaterService;

            _questionAdderService = questionAdderService;

            _formTemplateTranslationsAdderService = formTemplateTranslationsAdderService;
            _formTemplateTranslationsDeleterService = formTemplateTranslationsDeleterService;

            _formTemplateRepository = formTemplateRepository;
        }


        /// <summary>
        /// Retorna todos os modelos de formulários.
        /// </summary>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <param name="language">Idioma das traduções (opcional).</param>
        /// <param name="name">Search por titulo</param>
        /// <returns>Um ActionResult representando os modelos de formulários obtidos.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormTemplateDTOResponse>>> GetAllFormTemplates(int page = 1, int pageSize = 20, Language? language = null, string name = "")
        {
            var formTemplates = await _formTemplateGetterService.GetAllFormTemplates(page, pageSize, language, name);

            return Ok(formTemplates);
        }

        /// <summary>
        /// Obtém um modelo de formulário pelo ID.
        /// </summary>
        /// <param name="formTemplateId">ID do modelo de formulário.</param>
        /// <returns>Um ActionResult representando o modelo de formulário obtido.</returns>
        [HttpGet("{formTemplateId}")]
        public async Task<ActionResult<FormTemplateDTOResponse>> GetFormTemplateById(Guid? formTemplateId)
        {
            var formTemplate = await _formTemplateGetterService.GetFormTemplateById(formTemplateId);
            return Ok(formTemplate);
        }

        /// <summary>
        /// Adiciona um novo modelo de formulário.
        /// </summary>
        /// <param name="formTemplateDTOAddRequest">Dados do novo modelo de formulário.</param>
        /// <returns>Um IActionResult representando o resultado da adição do modelo de formulário.</returns>
        [HttpPost]
        public async Task<IActionResult> AddFormTemplate([FromBody] FormTemplateDTOAddRequest? formTemplateDTOAddRequest)
        {
            var response = await _formTemplateAdderService.AddFormTemplate(formTemplateDTOAddRequest);
            return CreatedAtAction(nameof(AddFormTemplate), response);
        }

        /// <summary>
        /// Adiciona uma tradução a um modelo de formulário.
        /// </summary>
        /// <param name="formTemplateId">ID do modelo de formulário.</param>
        /// <param name="formTemplateTranslationDTOAddRequest">Dados da tradução a ser adicionada.</param>
        /// <returns>Um IActionResult representando o resultado da adição da tradução.</returns>
        [HttpPost("{formTemplateId}/Translations")]
        public async Task<IActionResult> AddFormTemplateTranslation(Guid? formTemplateId, [FromBody] TranslationDTOAddRequest? formTemplateTranslationDTOAddRequest)
        {
            var translation = await _formTemplateTranslationsAdderService.AddFormTemplateTranslation(formTemplateId, formTemplateTranslationDTOAddRequest);

            return CreatedAtAction(nameof(AddFormTemplateTranslation), translation);
        }

        /// <summary>
        /// Adiciona uma pergunta a um modelo de formulário.
        /// </summary>
        /// <param name="formTemplateId">ID do modelo de formulário.</param>
        /// <param name="questionDTOAddRequest">Dados da pergunta a ser adicionada.</param>
        /// <returns>Um IActionResult representando o resultado da adição da pergunta.</returns>
        [HttpPost("{formTemplateId}/Questions")]
        public async Task<IActionResult> AddQuestionToFormTemplate(Guid? formTemplateId, [FromBody] QuestionDTOAddRequest questionDTOAddRequest)
        {

            var response = await _questionAdderService.AddQuestionToFormTemplate(formTemplateId, questionDTOAddRequest);
            return Ok(response);
        }

        /// <summary>
        /// Exclui um modelo de formulário pelo ID.
        /// </summary>
        /// <param name="formTemplateId">ID do modelo de formulário a ser excluído.</param>
        /// <returns>Um ActionResult representando o resultado da exclusão do modelo de formulário.</returns>
        [HttpDelete("{formTemplateId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormTemplateById(Guid? formTemplateId)
        {
            var response = await _formTemplatesDeleterService.DeleteFormTemplateById(formTemplateId);
            return response;
        }

        /// <summary>
        /// Exclui uma tradução de um modelo de formulário.
        /// </summary>
        /// <param name="formTemplateId">ID do modelo de formulário.</param>
        /// <param name="language">Idioma da tradução a ser excluída.</param>
        /// <returns>Um ActionResult representando o resultado da exclusão da tradução.</returns>
        [HttpDelete("{formTemplateId}/Translations")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteFormTemplateTranslation(Guid? formTemplateId, [FromQuery] Language language)
        {
            var response = await _formTemplateTranslationsDeleterService.DeleteFormTemplateTranslationById(formTemplateId, language);
            return response;
        }

        /// <summary>
        /// Atualiza um modelo de formulário pelo ID.
        /// </summary>
        /// <param name="formTemplateId">ID do modelo de formulário a ser atualizado.</param>
        /// <param name="formTemplate">Dados atualizados do modelo de formulário.</param>
        /// <returns>Um IActionResult representando o resultado da atualização do modelo de formulário.</returns>
        [HttpPut("{formTemplateId}")]
        public async Task<IActionResult> UpdateFormTemplate(Guid? formTemplateId, [FromBody] FormTemplateDTOUpdate formTemplate)
        {
            var response = await _formTemplatesUpdaterService.UpdateFormTemplate(formTemplateId, formTemplate);
            return Ok(response);
        }

    }
}
