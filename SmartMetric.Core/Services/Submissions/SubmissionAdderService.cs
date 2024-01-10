using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
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
        private readonly IReviewRepository _reviewRepository;
        private readonly ISmartTimeRepository _smartTimeRepository;
        private ILogger<SubmissionAdderService> _logger;
        public SubmissionAdderService(ISubmissionRepository submissionRepository, IReviewRepository reviewRepository, ISmartTimeRepository smartTimeRepository, ILogger<SubmissionAdderService> logger)
        {
            _submissionRepository = submissionRepository;
            _reviewRepository = reviewRepository;
            _smartTimeRepository = smartTimeRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<SubmissionDTOResponse>> AddSubmission(SubmissionDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(SubmissionAdderService)}.{nameof(AddSubmission)} foi iniciado");

            if( request == null ) throw new ArgumentNullException(nameof(request), "Submission Request can't be null");


            // verificar se review existe e está ativa

            var review = await _reviewRepository.GetReviewById(request.ReviewId);
            if( review == null ) throw new ArgumentException($"{nameof(request.ReviewId)} is invalid", nameof(request.ReviewId));

            if (review.ReviewStatus != ReviewStatus.Active.ToString()) throw new ArgumentException("This review is not active", nameof(Review));

            // Verificar se os utilizadores da submissão existem em BD

            var evaluatedEmployee = await _smartTimeRepository.GetEmployeeById(request.EvaluatedEmployeeId);
            if (evaluatedEmployee == null) throw new ArgumentException($"{nameof(request.EvaluatedEmployeeId)} is invalid", nameof(request.EvaluatedEmployeeId));

            var evaluatorEmployee = await _smartTimeRepository.GetEmployeeById(request.EvaluatorEmployeeId);
            if (evaluatorEmployee == null) throw new ArgumentException($"{nameof(request.EvaluatorEmployeeId)} is invalid", nameof(request.EvaluatorEmployeeId));

            // Verificar se ambos employees pertence á lista de emplooyes da avaliação:

            var reviewEmployeeIds = review.Employees?.Select(temp => temp.EmployeeId).ToList();

            if (reviewEmployeeIds != null && !reviewEmployeeIds.Any()) throw new ArgumentException("This survey does not yet have any employees added to its list");

            if (!reviewEmployeeIds.Contains(request.EvaluatedEmployeeId)) throw new ArgumentException($"{nameof(request.EvaluatedEmployeeId)} {request.EvaluatedEmployeeId} does not belong to the list of employees in this survey", nameof(request.EvaluatedEmployeeId));
            if (!reviewEmployeeIds.Contains(request.EvaluatorEmployeeId)) throw new ArgumentException($"{nameof(request.EvaluatorEmployeeId)} {request.EvaluatorEmployeeId} does not belong to the list of employees in this survey", nameof(request.EvaluatorEmployeeId));


            // Converter ResponsesAddRequest para ReviewResponses Entity

            var reviewResponses = request.ReviewResponses?.Select( temp => temp.ToReviewResponse());

            var submissionToAdd = request.ToSubmission();
            submissionToAdd.SubmissionDate = DateTime.UtcNow;
            submissionToAdd.ReviewResponses = reviewResponses?.ToList();

            var result = await _submissionRepository.AddSubmission(submissionToAdd);

            if (!result) throw new Exception();

            return new ApiResponse<SubmissionDTOResponse>()
            {
                StatusCode = 200,
                Message = "Submisson added with success",
                Data = submissionToAdd.ToSubmissionDTOResponse()
            };


            throw new NotImplementedException();
        }
    }
}
