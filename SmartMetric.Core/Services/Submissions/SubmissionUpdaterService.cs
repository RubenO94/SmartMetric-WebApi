using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.ServicesContracts.Submission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Submissions
{
    public class SubmissionUpdaterService : ISubmissionUpdaterService
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ILogger<SubmissionUpdaterService> _logger;

        public SubmissionUpdaterService(ISubmissionRepository submissionRepository, ILogger<SubmissionUpdaterService> logger)
        {
            _submissionRepository = submissionRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> UpdateSubmission(Guid submissionId, SubmissionFormDTOUpdate submission)
        {
            _logger.LogInformation($"{nameof(SubmissionUpdaterService)}.{nameof(UpdateSubmission)} foi iniciado");

            submission.SubmissionDate = DateTime.UtcNow;

            var result = await _submissionRepository.UpdateSubmission(submissionId, submission);

            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Submission updated successfully.",
                Data = result
            };
        }
    }
}
