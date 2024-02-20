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
    public class SubmissionGetterService : ISubmissionGetterService
    {
        //variables
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ISmartTimeRepository _smartTimeRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogger<SubmissionGetterService> _logger;

        //Constructor
        public SubmissionGetterService(ISubmissionRepository submissionRepository, ISmartTimeRepository smartTimeRepository, IReviewRepository reviewRepository, ILogger<SubmissionGetterService> logger)
        {
            _submissionRepository = submissionRepository;
            _smartTimeRepository = smartTimeRepository;
            _reviewRepository = reviewRepository;
            _logger = logger;
        }

        public Task<ApiResponse<List<SubmissionDTOResponse>>> GetAllSubmissions()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsByEvaluatorEmployeeId(int employeeId)
        {
            _logger.LogInformation($"{nameof(SubmissionGetterService)}.{nameof(GetSubmissionsByEvaluatorEmployeeId)} foi iniciado");

            var employeeExist = await _smartTimeRepository.GetEmployeeById(employeeId);
            if (employeeExist == null) throw new ArgumentException("Employee doesn't exist");

            var submissions = await _submissionRepository.GetAllSubmissionsByEvaluatorEmployeeId(employeeId);

            return new ApiResponse<List<SubmissionDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "List of submissions of Employee",
                Data = submissions.Select(temp => temp.ToSubmissionDTOResponse()).ToList(),
                TotalCount = submissions.Count
            };
        }

        public async Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsByEvaluatedEmployeeId(int employeeId)
        {
            _logger.LogInformation($"{nameof(SubmissionGetterService)}.{nameof(GetSubmissionsByEvaluatedEmployeeId)} foi iniciado.");

            var employeeExist = await _smartTimeRepository.GetEmployeeById(employeeId);
            if (employeeExist == null) throw new ArgumentException("Employee doesn't exist");

            var submissions = await _submissionRepository.GetAllSubmissionsByEvaluatedEmployeeId(employeeId);

            return new ApiResponse<List<SubmissionDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "List of submissions of Employee",
                Data = submissions.Select(temp => temp.ToSubmissionDTOResponse()).ToList(),
                TotalCount = submissions.Count
            };
        }

        public async Task<ApiResponse<SubmissionDTOResponse>> GetSubmissionById(Guid? submissionId)
        {
            _logger.LogInformation($"{nameof(SubmissionGetterService)}.{nameof(GetSubmissionById)} foi iniciado.");

            if (submissionId == null) throw new ArgumentException("SubmissionId can't be null.");

            var submission = await _submissionRepository.GetSubmissionById(submissionId.Value);
            if (submission == null) throw new ArgumentException("Submission doesn't exist.");

            return new ApiResponse<SubmissionDTOResponse>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "",
                Data = submission.ToSubmissionDTOResponse()
            };
        }

        public async Task<ApiResponse<List<SubmissionDTOResponse>>> GetSubmissionsByReviewId(Guid? reviewId, int page = 1, int pageSize = 20, string name = "", int statusSubmission = 0)
        {
            _logger.LogInformation($"{nameof(SubmissionGetterService)}.{nameof(GetSubmissionsByReviewId)} foi iniciado.");

            if (reviewId == null) throw new ArgumentException("ReviewId can't be null.");

            var submissions = await _submissionRepository.GetAllSubmissionsByReviewId(reviewId.Value, page, pageSize, name, statusSubmission);
            if (submissions == null) throw new ArgumentException("This review doesn't exist");

            var totalCount = await _reviewRepository.GetTotalSubmissions(reviewId.Value, name, statusSubmission);

            return new ApiResponse<List<SubmissionDTOResponse>>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Data retrieved successfully.",
                Data = submissions.Select(temp => temp.ToSubmissionDTOResponse()).ToList(),
                TotalCount = totalCount
            };
        }
    }
}
