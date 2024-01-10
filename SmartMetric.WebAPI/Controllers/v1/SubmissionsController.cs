using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.Enums;
using SmartMetric.WebAPI.Filters.ActionFilter;

namespace SmartMetric.WebAPI.Controllers.v1
{
    /// <summary>
    /// Controlador responsável por operações relacionadas as submissões de avaliações.
    /// </summary>
    [ApiVersion("1.0")]
    public class SubmissionsController : CustomBaseController
    {
        [HttpPost]
        Task<IActionResult> CreateSubmission([FromBody] SubmissionDTOAddRequest submission)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        Task<IActionResult> GetSubmissionsFromAuthenticatedUser([FromBody] SubmissionDTOAddRequest submission)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("{submissionId}")]
        Task<IActionResult> UpdateSubmissionFromAuthenticatedUser(Guid submissionId, [FromBody] SubmissionDTOAddRequest submission) // TODO: Criar e mudar para DTOUpdate
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{submissionId}")]
        [PermissionRequired(WindowType.Submissions, PermissionType.Delete)]
        Task<IActionResult> DeleteSubmission(Guid submissionId)
        {
            throw new NotImplementedException();
        }

    }
}
