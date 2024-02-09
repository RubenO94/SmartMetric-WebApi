using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.ServicesContracts.Reviews;
using SmartMetric.Core.ServicesContracts.Submission;
using SmartMetric.Core.ServicesContracts.Submissions;
using SmartMetric.WebAPI.Filters.ActionFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações relacionadas as submissões de avaliações.
    /// </summary>
    [ApiVersion("1.0")]
    public class SubmissionsController : CustomBaseController
    {
        private readonly ISubmissionAdderService _submissionAdderService;
        private readonly ISubmissionGetterService _submissionGetterSerive;
        private readonly ISubmissionDeleterService _submissionDeleterService;
        private readonly ISubmissionUpdaterService _submissionUpdaterService;
        private readonly IReviewGetterService _reviewGetterService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionAdderService"></param>
        /// <param name="submissionGetterService"></param>
        /// <param name="submissionDeleterService"></param>
        /// <param name="submissionUpdaterService"></param>
        /// <param name="reviewGetterService"></param>
        public SubmissionsController(
            ISubmissionAdderService submissionAdderService,
            ISubmissionGetterService submissionGetterService,
            ISubmissionDeleterService submissionDeleterService,
            ISubmissionUpdaterService submissionUpdaterService,
            IReviewGetterService reviewGetterService)
        {
            _submissionAdderService = submissionAdderService;
            _submissionGetterSerive = submissionGetterService;
            _submissionDeleterService = submissionDeleterService;
            _submissionUpdaterService = submissionUpdaterService;
            _reviewGetterService = reviewGetterService;
        }

        /// <summary>
        /// Método para receber uma submissão através do seu id único
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        [HttpGet("{submissionId}")]
        public async Task<ActionResult> GetSubmissionById(Guid submissionId)
        {
            var response = await _submissionGetterSerive.GetSubmissionById(submissionId);
            return Ok(response);
        }

        /// <summary>
        /// Método para eliminar uma submissão
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns>Retorna um boolean correspondente ao sucesso do request</returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete("{submissionId}")]
        public async Task<ApiResponse<bool>> DeleteSubmission(Guid submissionId)
        {
            var response = await _submissionDeleterService.DeleteSubmission(submissionId);
            return response;
        }

        /// <summary>
        /// Método para alterar submissão, adicionando data da submissão e respostas ao formulário
        /// </summary>
        /// <param name="submissionId"></param>
        /// <param name="submission"></param>
        /// <returns></returns>
        [HttpPatch("{submissionId}")]
        public async Task<IActionResult> UpdateSubmission(Guid submissionId, [FromBody] SubmissionFormDTOUpdate submission)
        {
            var response = await _submissionUpdaterService.UpdateSubmission(submissionId, submission);
            return Ok(response);
        }

        /// <summary>
        /// Método para receber todas as submissions de um funcionário específico
        /// </summary>
        /// <param name="evaluatorEmployeeId"></param>
        /// <returns></returns>
        [HttpGet("EvaluatorEmployee/{evaluatorEmployeeId}")]
        public async Task<IActionResult> GetSubmissionsByEvaluatorEmployeeId(int evaluatorEmployeeId)
        {
            var response = await _submissionGetterSerive.GetSubmissionsByEvaluatorEmployeeId(evaluatorEmployeeId);
            return Ok(response);
        }

        /// <summary>
        /// Método para receber todas as submissões sobre um funcionário específico
        /// </summary>
        /// <param name="evaluatedEmployeeId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("EvaluatedEmployee/{evaluatedEmployeeId}")]
        public async Task<IActionResult> GetSubmissionsByEvaluatedEmployeeId(int evaluatedEmployeeId)
        {
            var response = await _submissionGetterSerive.GetSubmissionsByEvaluatedEmployeeId(evaluatedEmployeeId);
            return Ok(response);
        }

        #region Not implemented yet

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submission"></param>
        /// <returns></returns>
        // [HttpPost]
        // public async Task<IActionResult> CreateSubmission([FromBody] SubmissionDTOAddRequest submission)
        // {
        //     var response = await _submissionAdderService.AddSubmission(submission);
        //     return Ok(response);
        // }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        // [HttpGet]
        // public async Task<IActionResult> GetSubmissionsFromAuthenticatedUser(Guid? reviewId)
        // {
        //     var response = _submissionGetterSerive.GetSubmissionsByReviewId(reviewId);
        //     throw new NotImplementedException();
        // }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionId"></param>
        /// <param name="submission"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        // [HttpPut("{submissionId}")]
        // public async Task<IActionResult> UpdateSubmissionFromAuthenticatedUser(Guid submissionId, [FromBody] SubmissionDTOAddRequest submission) // TODO: Criar e mudar para DTOUpdate
        // {
        //     throw new NotImplementedException();
        // }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        // [HttpDelete("{submissionId}")]
        // [PermissionRequired(WindowType.Submissions, PermissionType.Delete)]
        // public async Task<IActionResult> DeleteSubmission(Guid submissionId)
        // {
        //     throw new NotImplementedException();
        // }
        #endregion
    }
}
