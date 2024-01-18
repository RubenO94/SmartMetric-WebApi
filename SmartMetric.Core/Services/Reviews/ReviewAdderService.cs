using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Reviews;

namespace SmartMetric.Core.Services.Reviews
{
    public class ReviewAdderService : IReviewAdderService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ISmartTimeRepository _smartTimeRepository;
        private readonly ISubmissionRepository _submissionRepository;
        private ILogger<ReviewAdderService> _logger;

        public ReviewAdderService(IReviewRepository reviewRepository, ISmartTimeRepository smartTimeRepository, ISubmissionRepository submissionRepository, ILogger<ReviewAdderService> logger)
        {
            _reviewRepository = reviewRepository;
            _smartTimeRepository = smartTimeRepository;
            _submissionRepository = submissionRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<ReviewDTOResponse?>> AddReview(ReviewDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(ReviewAdderService)}.{nameof(AddReview)} foi iniciado");

            if (request == null) throw new ArgumentNullException(nameof(request));

            request.CreatedDate = DateTime.UtcNow;
            if (request.ReviewStatus == ReviewStatus.Active) request.StartDate = request.CreatedDate;

            ValidationHelper.ModelValidation(request);

            var userResult = await _smartTimeRepository.GetUserById(request.CreatedByUserId!.Value);

            if (userResult == null) throw new ArgumentException("User does not exist.", nameof(request.CreatedByUserId));

            if (request.StartDate >= request.EndDate) throw new ArgumentException("Start date must be before the end date.", nameof(request.StartDate));

            var departments = await _smartTimeRepository.GetDepartmentsByListIds(request.ReviewDepartmentsIds!);

            var departmentsNotExisting = request.ReviewDepartmentsIds!.Except(departments.Select(temp => temp.Iddepartamento).ToList()).ToList();

            if (departmentsNotExisting.Any()) throw new ArgumentException("Some of the departments ids does not exist", nameof(request.ReviewDepartmentsIds));

            // TODO: Adicionar Employees a uma Review;

            var employees = await _smartTimeRepository.GetEmployeesByListIds(request.ReviewEmployeesIds!);

            var employeesNotExisting = request.ReviewEmployeesIds!.Except(employees.Select(temp => temp.Idfuncionario).ToList()).ToList();
            if (employeesNotExisting.Any()) throw new ArgumentException("Some of the employee ids does not exist", nameof(request.ReviewEmployeesIds));

            var employeesNotBelongToDepartments = request.ReviewEmployeesIds!.Except(employees.Where(temp => request.ReviewDepartmentsIds!.Contains(temp.Iddepartamento!.Value)).Select(temp => temp.Idfuncionario)).ToList().ToList();

            if (employeesNotBelongToDepartments.Any()) throw new ArgumentException("Some of the employee ids does not belong to the departments selected", nameof(request.ReviewEmployeesIds));

            Review review = request.ToReview();

            foreach (var department in departments)
            {
                review.Departments?.Add(new ReviewDepartment() { Department = department, Review = review });
            }

            foreach (var employee in employees)
            {
                review.Employees?.Add(new ReviewEmployee() { Employee = employee, Review = review });
            }

            //Save review in db
            var result = await _reviewRepository.AddReview(review);

            if (result)
            {
                //Creating submissions
                if (review.ReviewStatus == ReviewStatus.Active.ToString())
                {
                    switch (request.ReviewType)
                    {
                        case ReviewType.SelfEvaluation:
                            foreach (var employee in employees)
                            {
                                var submission = new Submission()
                                {
                                    ReviewId = review.ReviewId,
                                    EvaluatedEmployeeId = employee.Idfuncionario,
                                    EvaluatorEmployeeId = employee.Idfuncionario,
                                };
                                await _submissionRepository.AddSubmission(submission);
                            }
                            break;
                        case ReviewType.TopDown:
                            foreach (var employee in employees)
                            {
                                var chefias = await _smartTimeRepository.GetChefiasByEmployee(employee.Idfuncionario);
                                foreach (var chefia in chefias)
                                {
                                    var submission = new Submission()
                                    {
                                        ReviewId = review.ReviewId,
                                        EvaluatedEmployeeId = employee.Idfuncionario,
                                        EvaluatorEmployeeId = chefia.IdfuncionarioSuperior
                                    };
                                    await _submissionRepository.AddSubmission(submission);
                                }
                            }
                            break;
                        case ReviewType.BottomUp:
                            foreach (var employee in employees)
                            {
                                var chefias = await _smartTimeRepository.GetChefiasByEmployee(employee.Idfuncionario);
                                foreach (var chefia in chefias)
                                {
                                    var submission = new Submission()
                                    {
                                        ReviewId = review.ReviewId,
                                        EvaluatedEmployeeId = chefia.IdfuncionarioSuperior,
                                        EvaluatorEmployeeId = employee.Idfuncionario
                                    };
                                    await _submissionRepository.AddSubmission(submission);
                                }
                            }
                            break;
                        case ReviewType.Interdepartamental:
                            foreach (var department in departments)
                            {
                                var chefias = await _smartTimeRepository.GetChefiasByDepartment(department.Iddepartamento);
                                var otherDepartments = departments.Where(d => d.Iddepartamento != department.Iddepartamento);
                                foreach (var chefia in chefias)
                                {
                                    foreach (var otherDepartment in otherDepartments)
                                    {
                                        var submission = new Submission()
                                        {
                                            ReviewId = review.ReviewId,
                                            EvaluatedDepartmentId = otherDepartment.Iddepartamento,
                                            EvaluatorDepartmentId = department.Iddepartamento,
                                            EvaluatorEmployeeId = chefia.IdfuncionarioSuperior
                                        };
                                        await _submissionRepository.AddSubmission(submission);
                                    }
                                }
                            }
                            break;
                        default: break;
                    }
                }

                return new ApiResponse<ReviewDTOResponse?>()
                {
                    StatusCode = (int)System.Net.HttpStatusCode.Created,
                    Message = "Review create with success!",
                    Data = review.ToReviewDTOResponse()
                };
            }

            return new ApiResponse<ReviewDTOResponse?>()
            {
                StatusCode = (int)System.Net.HttpStatusCode.InternalServerError,
                Message = "Something went wrong!",
            };
        }
    }
}
