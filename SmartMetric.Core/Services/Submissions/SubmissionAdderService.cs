using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Services.SingleChoiceOptionTranslations;
using SmartMetric.Core.ServicesContracts.Submissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Services.Submissions
{
    public class SubmissionAdderService : ISubmissionAdderService
    {
        private readonly ISubmissionRepository _submissionRepository;
        private ILogger<SubmissionAdderService> _logger;
        public SubmissionAdderService(ISubmissionRepository submissionRepository, ILogger<SubmissionAdderService> logger)
        {
            _submissionRepository = submissionRepository;
            _logger = logger;
        }

        public Task<ApiResponse<SubmissionDTOResponse>> AddSubmission(SubmissionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(SubmissionAdderService)}.{nameof(AddSubmission)} foi iniciado");

            if( request == null ) throw new ArgumentNullException(nameof(request), "Submission Request can't be null");



            throw new NotImplementedException();
        }
    }
}
