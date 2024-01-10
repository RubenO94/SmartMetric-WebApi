using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Helpers;
using SmartMetric.Core.ServicesContracts.Reviews;

namespace SmartMetric.Core.Services.Reviews
{
    public class ReviewAdderService : IReviewAdderService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ISmartTimeRepository _smartTimeRepository;
        private ILogger<ReviewAdderService> _logger;

        public ReviewAdderService(IReviewRepository reviewRepository, ISmartTimeRepository smartTimeRepository, ILogger<ReviewAdderService> logger)
        {
            _reviewRepository = reviewRepository;
            _smartTimeRepository = smartTimeRepository;
            _logger = logger;
        }

        public async Task<ApiResponse<ReviewDTOResponse?>> AddReview(ReviewDTOAddRequest? request)
        {
            _logger.LogInformation($"{nameof(ReviewAdderService)}.{nameof(AddReview)} foi iniciado");

            if (request == null) throw new ArgumentNullException(nameof(request));

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


            var result = await _reviewRepository.AddReview(review);

            if (result)
            {
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
