using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Reviews;
using System.Linq.Expressions;
using System.Net;
using System.Net.WebSockets;

namespace SmartMetric.Core.Services.Reviews
{
    public class ReviewUpdaterService : IReviewUpdaterService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ISmartTimeRepository _smartTimeRepository;
        private readonly ISubmissionRepository _submissionRepository;
        private readonly ILogger<ReviewUpdaterService> _logger;

        public ReviewUpdaterService(IReviewRepository reviewRepository, ISmartTimeRepository smartTimeRepository, ISubmissionRepository submissionRepository, ILogger<ReviewUpdaterService> logger)
        {
            _reviewRepository = reviewRepository;
            _smartTimeRepository = smartTimeRepository;
            _submissionRepository = submissionRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<ReviewDTOResponse>> UpdateReview(Guid? reviewId, ReviewDTOUpdate? reviewDTOUpdate)
        {
            _logger.LogInformation($"{nameof(ReviewUpdaterService)}.{nameof(UpdateReview)} foi iniciado");

            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            if (reviewDTOUpdate == null) throw new ArgumentNullException(nameof(reviewDTOUpdate));

            if (reviewDTOUpdate.ReviewStatus != ReviewStatus.NotStarted.ToString()) throw new ArgumentException("Review can't be edited when reviewStatus different than NotStarted", nameof(reviewDTOUpdate.ReviewStatus));

            if (reviewDTOUpdate.StartDate >= reviewDTOUpdate.EndDate) throw new ArgumentException("Start date must be before the end date.", nameof(reviewDTOUpdate.StartDate));

            var departments = await _smartTimeRepository.GetDepartmentsByListIds(reviewDTOUpdate.ReviewDepartmentsIds!.ToList());
            var departmentsNotExisting = reviewDTOUpdate.ReviewDepartmentsIds!.Except(departments.Select(temp => temp.Iddepartamento).ToList()).ToList();
            if (departmentsNotExisting.Any()) throw new ArgumentException("Some of the departments ids does not exist", nameof(reviewDTOUpdate.ReviewDepartmentsIds));

            var employees = await _smartTimeRepository.GetEmployeesByListIds(reviewDTOUpdate.ReviewEmployeesIds!.ToList());
            var employeesNotExisting = reviewDTOUpdate.ReviewEmployeesIds!.Except(employees.Select(temp => temp.Idfuncionario).ToList()).ToList();
            if (employeesNotExisting.Any()) throw new ArgumentException("Some of the employees ids doesn't exist", nameof(reviewDTOUpdate.ReviewEmployeesIds));

            var matchingReview = await _reviewRepository.GetReviewById(reviewId.Value);

            if (matchingReview == null) throw new ArgumentException("Review does not exist.", nameof(Review));

            matchingReview.ModifiedDate = DateTime.UtcNow;

            // Atualizar revisão usando o helper
            UpdateHelper.UpdateReview(matchingReview, reviewDTOUpdate);

            var response = await _reviewRepository.UpdateReview(matchingReview);

            return new ApiResponse<ReviewDTOResponse>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Review updated successfully",
                Data = matchingReview.ToReviewDTOResponse()
            };
        }

        public async Task<ApiResponse<bool>> UpdateReviewStatus(Guid? reviewId, ReviewDTOUpdateStatus review)
        {
            _logger.LogInformation($"{nameof(ReviewUpdaterService)}.{nameof(UpdateReviewStatus)} foi iniciado");

            if (reviewId == null) throw new ArgumentNullException(nameof(reviewId));

            var enumNames = Enum.GetNames(typeof(ReviewStatus));
            if (!enumNames.Any(name => name == review.ReviewStatus.ToString())) throw new ArgumentException("Review Status doesn't exist");

            if (review.ReviewStatus == ReviewStatus.Active && (review.EndDate == null || review.EndDate.ToString() == "")) throw new ArgumentNullException("To Change reviewStatus to Active need an End Date for review.");

            review.StartDate = DateTime.UtcNow;

            if (review.StartDate >= review.EndDate) throw new ArgumentException("Start date must be before the end date.", nameof(review.StartDate));

            var matchingReview = await _reviewRepository.GetReviewById(reviewId.Value);

            if (matchingReview == null) throw new ArgumentNullException("Review doesn't exist", nameof(reviewId));

            switch (matchingReview.ReviewStatus)
            {
                case "NotStarted":
                    if (review.ReviewStatus != ReviewStatus.Active && review.ReviewStatus != ReviewStatus.Canceled) throw new ArgumentException($"Review Status can´t change to {review.ReviewStatus}");
                    break;
                case "Active":
                    if (review.ReviewStatus != ReviewStatus.Canceled && review.ReviewStatus != ReviewStatus.Completed) throw new ArgumentException($"Review Status can´t change to {review.ReviewStatus}");
                    break;
                default:
                    throw new ArgumentException($"Review Status can't change to {review.ReviewStatus}");
            }

            var result = await _reviewRepository.UpdateReviewStatus(reviewId.Value, review);

            if (review.ReviewStatus == ReviewStatus.Active)
            {
                switch(matchingReview.ReviewType)
                {
                    case "SelfEvaluation":
                        foreach (var employee in matchingReview.Employees!)
                        {
                            var submission = new Submission()
                            {
                                ReviewId = matchingReview.ReviewId,
                                EvaluatedEmployeeId = employee.EmployeeId,
                                EvaluatorEmployeeId = employee.EmployeeId
                            };
                            await _submissionRepository.AddSubmission(submission);
                        }
                        break;
                    case "TopDown":
                        foreach (var employee in matchingReview.Employees!)
                        {
                            var chefias = await _smartTimeRepository.GetChefiasByEmployee(employee.EmployeeId);
                            foreach (var chefia in chefias)
                            {
                                var chefiaEmployee = await _smartTimeRepository.GetEmployeeById(chefia.IdfuncionarioSuperior!.Value);
                                var submission = new Submission()
                                {
                                    ReviewId = matchingReview.ReviewId,
                                    EvaluatedEmployeeId = employee.EmployeeId,
                                    EvaluatorEmployeeId = chefiaEmployee!.Idfuncionario,
                                };
                                await _submissionRepository.AddSubmission(submission);
                            }
                        }
                        break;
                    case "BottomUp":
                        foreach (var employee in matchingReview.Employees!)
                        {
                            var chefias = await _smartTimeRepository.GetChefiasByEmployee(employee.EmployeeId);
                            foreach (var chefia in chefias)
                            {
                                var chefiaEmployee = await _smartTimeRepository.GetEmployeeById(chefia.IdfuncionarioSuperior!.Value);
                                var submission = new Submission()
                                {
                                    ReviewId = matchingReview.ReviewId,
                                    EvaluatedEmployeeId = chefiaEmployee!.Idfuncionario,
                                    EvaluatorEmployeeId = employee.EmployeeId,
                                };
                                await _submissionRepository.AddSubmission(submission);
                            }
                        }
                        break;
                    case "Interdepartamental":
                        foreach (var department in matchingReview.Departments!)
                        {
                            var chefias = await _smartTimeRepository.GetChefiasByDepartment(department.DepartmentId);
                            var otherDepartments = matchingReview.Departments.Where(d => d.DepartmentId != department.DepartmentId);
                            foreach (var chefia in chefias)
                            {
                                foreach (var otherDepartment in otherDepartments)
                                {
                                    var chefiaEmployeeResponse = await _smartTimeRepository.GetEmployeeById(chefia.IdfuncionarioSuperior!.Value);
                                    var submission = new Submission()
                                    {
                                        ReviewId = matchingReview.ReviewId,
                                        EvaluatedDepartmentId = otherDepartment.DepartmentId,
                                        EvaluatorDepartmentId = department.DepartmentId,
                                        EvaluatorEmployeeId = chefiaEmployeeResponse?.Idfuncionario
                                    };
                                    await _submissionRepository.AddSubmission(submission);
                                }
                            }
                        }
                        break;
                    default: break;
                }
            }

            return new ApiResponse<bool>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Review Status updated successfully",
                Data = result
            };
        }
    }
}
