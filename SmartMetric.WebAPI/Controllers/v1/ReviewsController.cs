using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.ServicesContracts.Reviews;
using SmartMetric.WebAPI.Filters.ActionFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações relacionadas a avaliações (Reviews).
    /// </summary>
    [ApiVersion("1.0")]
    public class ReviewsController : CustomBaseController
    {
        private readonly IReviewGetterService _reviewGetterService;
        private readonly IReviewAdderService _reviewAdderService;
        private readonly IReviewDeleterService _reviewDeleterService;
        private readonly IReviewUpdaterService _reviewUpdaterService;

        // <summary>
        /// Construtor do controlador Reviews.
        /// </summary>
        /// <param name="reviewGetterService">Serviço utilizado para obter avaliações.</param>
        /// <param name="reviewAdderService">Serviço utilizado para adicionar avaliações.</param>
        /// <param name="reviewDeleterService">Serviço utilizado para excluir avaliações.</param>
        /// <param name="reviewUpdaterService">Serviço utilizado para atualizar avaliações.</param>
        public ReviewsController(IReviewGetterService reviewGetterService, IReviewAdderService reviewAdderService, IReviewDeleterService reviewDeleterService, IReviewUpdaterService reviewUpdaterService)
        {
            _reviewGetterService = reviewGetterService;
            _reviewAdderService = reviewAdderService;
            _reviewDeleterService = reviewDeleterService;
            _reviewUpdaterService = reviewUpdaterService;
        }

        /// <summary>
        /// Obtém todas as avaliações paginadas.
        /// </summary>
        /// <param name="page">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <param name="language">Idioma pertendido na procura.</param>
        /// <returns>Um IActionResult representando as avaliações obtidas.</returns>
        [PermissionRequired(WindowType.Reviews, PermissionType.Read)]
        [HttpGet]
        public async Task<IActionResult> GetAllReviews(int page = 1, int pageSize = 20, Language? language = null) 
        {
            var response = await _reviewGetterService.GetReviews(page, pageSize, language);
            return Ok(response);
        }

        /// <summary>
        /// Obtém uma avaliação pelo ID.
        /// </summary>
        /// <param name="reviewId">ID da avaliação.</param>
        /// <returns>Um IActionResult representando a avaliação obtida pelo ID.</returns>
        [PermissionRequired(WindowType.Reviews, PermissionType.Read)]
        [HttpGet("{reviewId}")]
        public async Task<IActionResult> GetReviewById(Guid? reviewId)
        {
            var review = await _reviewGetterService.GetReviewById(reviewId);
            return Ok(review);
        }

        /// <summary>
        /// Adiciona uma nova avaliação.
        /// </summary>
        /// <param name="request">Dados da avaliação a ser adicionada.</param>
        /// <returns>Um IActionResult representando o resultado da adição da avaliação.</returns>
        [PermissionRequired(WindowType.Reviews, PermissionType.Create)]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTOAddRequest? request)
        {
            var response = await _reviewAdderService.AddReview(request);
            if (response.Data != null)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Exclui uma avaliação pelo ID.
        /// </summary>
        /// <param name="reviewId">ID da avaliação a ser excluída.</param>
        /// <returns>Um ActionResult representando o resultado da exclusão da avaliação.</returns>
        [PermissionRequired(WindowType.Reviews, PermissionType.Delete)]
        [HttpDelete("{reviewId}")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteReview(Guid? reviewId)
        {
            var response = await _reviewDeleterService.DeleteReviewById(reviewId);
            return response;
        }

        /// <summary>
        /// Atualiza uma avaliação pelo ID.
        /// </summary>
        /// <param name="reviewId">ID da avaliação a ser atualizada.</param>
        /// <param name="reviewDTOUpdate">Dados atualizados da avaliação.</param>
        /// <returns>Um IActionResult representando o resultado da atualização da avaliação.</returns>
        [PermissionRequired(WindowType.Reviews, PermissionType.Update)]
        [HttpPut("{reviewId}")]
        public async Task<IActionResult> UpdateReview(Guid? reviewId, [FromBody] ReviewDTOUpdate? reviewDTOUpdate)
        {
            //TODO: Review Update Endpoint
            var response = await _reviewUpdaterService.UpdateReview(reviewId, reviewDTOUpdate);

            return Ok(response);
        }

        /// <summary>
        /// Atualiza o status de uma avaliação pelo ID.
        /// </summary>
        /// <param name="reviewId">ID da avaliação a ter o status atualizado.</param>
        /// <param name="review">Dados de atualização do status da avaliação.</param>
        /// <returns>Um IActionResult representando o resultado da atualização do status da avaliação.</returns>
        [PermissionRequired(WindowType.Reviews, PermissionType.Patch)]
        [HttpPatch("{reviewId}")]
        public async Task<IActionResult> UpdateReviewStatus(Guid? reviewId, [FromBody] ReviewDTOUpdateStatus review)
        {
            var response = await _reviewUpdaterService.UpdateReviewStatus(reviewId, review);
            return Ok(response);
            
        }
    }
}
