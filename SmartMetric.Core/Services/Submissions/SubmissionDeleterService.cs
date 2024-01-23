using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.ServicesContracts.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Submissions
{
    public class SubmissionDeleterService : ISubmissionDeleterService
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ILogger<SubmissionDeleterService> _logger;

        public SubmissionDeleterService(ISubmissionRepository submissionRepository, ILogger<SubmissionDeleterService> logger)
        {
            _submissionRepository = submissionRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<bool>> DeleteSubmission(Guid? submissionId)
        {
            _logger.LogInformation($"{nameof(SubmissionDeleterService)}.{nameof(DeleteSubmission)} foi iniciado.");

            if (submissionId == null) throw new ArgumentException("SubmissionId can't be null");

            var matchingSubmission = await _submissionRepository.GetSubmissionById(submissionId.Value);
            if (matchingSubmission == null) throw new ArgumentException("Submission doesn't exist with this Id");

            var result = await _submissionRepository.DeleteSubmission(submissionId.Value);

            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.NoContent,
                Message = "Submission removed successfully",
                Data = result
            };
        }
    }
}
