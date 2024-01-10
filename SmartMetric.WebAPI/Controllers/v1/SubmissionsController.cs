using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.Enums;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionAdderService"></param>
        /// <param name="submissionGetterService"></param>
        /// <param name="submissionDeleterService"></param>
        /// <param name="submissionUpdaterService"></param>
        public SubmissionsController(ISubmissionAdderService submissionAdderService, ISubmissionGetterService submissionGetterService, ISubmissionDeleterService submissionDeleterService, ISubmissionUpdaterService submissionUpdaterService)
        {
            _submissionAdderService = submissionAdderService;
            _submissionGetterSerive = submissionGetterService;
            _submissionDeleterService = submissionDeleterService;
            _submissionUpdaterService = submissionUpdaterService;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="submission"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSubmission([FromBody] SubmissionDTOAddRequest submission)
        {
            var response = await _submissionAdderService.AddSubmission(submission);
            return Ok(response);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<IActionResult> GetSubmissionsFromAuthenticatedUser(Guid? reviewId)
        {
            var response = _submissionGetterSerive.GetSubmissionsByReviewId(reviewId);
            throw new NotImplementedException();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionId"></param>
        /// <param name="submission"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut("{submissionId}")]
        public async Task<IActionResult> UpdateSubmissionFromAuthenticatedUser(Guid submissionId, [FromBody] SubmissionDTOAddRequest submission) // TODO: Criar e mudar para DTOUpdate
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submissionId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete("{submissionId}")]
        [PermissionRequired(WindowType.Submissions, PermissionType.Delete)]
        public async Task<IActionResult> DeleteSubmission(Guid submissionId)
        {
            throw new NotImplementedException();
        }

    }
}
