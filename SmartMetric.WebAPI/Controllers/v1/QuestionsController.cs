using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Questions;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações relacionadas a questões.
    /// </summary>
    [ApiVersion("1.0")]
    public class QuestionsController : CustomBaseController
    {
        private readonly IQuestionGetterService _questionGetterService;

        /// <summary>
        /// Contructor of the Questions Controller
        /// </summary>
        /// <param name="questionGetterService"></param>
        public QuestionsController(IQuestionGetterService questionGetterService)
        {
            _questionGetterService = questionGetterService;
        }

        /// <summary>
        /// Método que recebe todas as questões através do Id da revisão passado por parâmetro
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> GetQuestionByReviewId(Guid? reviewId)
        {
            var response = await _questionGetterService.GetQuestionsByReviewId(reviewId);
            return Ok(response);
        }
    }
}