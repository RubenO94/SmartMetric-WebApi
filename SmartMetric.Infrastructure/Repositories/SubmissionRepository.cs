using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Core.Enums;
using SmartMetric.Infrastructure.DatabaseContext;
using SmartMetric.Infrastructure.Repositories.Common;

namespace SmartMetric.Infrastructure.Repositories
{
    public class SubmissionRepository : BaseRepository, ISubmissionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SmartTimeRepository> _logger;

        public SubmissionRepository(ApplicationDbContext context, ILogger<SmartTimeRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddSubmission(Submission submission)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(AddSubmission)} foi iniciado.");

            _context.Submissions.Add(submission);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteSubmission(Guid submissionId)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(DeleteSubmission)} foi iniciado.");

            var submissionToDelete = await _context.Submissions.FindAsync(submissionId);
            if (submissionToDelete != null)
            {
                _context.Submissions.Remove(submissionToDelete);
                var deleted = await _context.SaveChangesAsync();
                return deleted > 0;
            }

            return false;
        }

        public Task<List<Submission>> GetAllSubmissions(int page = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Submission>> GetAllSubmissionsByEvaluatorEmployeeId(int employeeId)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(GetAllSubmissionsByEvaluatorEmployeeId)} foi iniciado.");

            return await _context.Submissions
                .Where(temp => temp.Review!.ReviewStatus == "Active")
                .Where(temp => temp.EvaluatorEmployeeId == employeeId)
                .Include(temp => temp.EvaluatorDepartment)
                .Include(temp => temp.EvaluatedDepartment)
                .Include(temp => temp.EvaluatedEmployee)
                .ToListAsync();
        }

        public async Task<List<Submission>> GetAllSubmissionsByEvaluatedEmployeeId(int employeeId)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(GetAllSubmissionsByEvaluatedEmployeeId)} foi iniciado.");

            return await _context.Submissions
                .Where(temp => temp.Review!.ReviewStatus == "Completed")
                .Where(temp => temp.EvaluatedEmployeeId == employeeId)
                .Include(temp => temp.EvaluatorDepartment)
                .Include(temp => temp.EvaluatedDepartment)
                .Include(temp => temp.EvaluatorEmployee)
                .Include(temp => temp.ReviewResponses)
                .ToListAsync();
        }

        public async Task<List<Submission>> GetAllSubmissionsByReviewId(Guid reviewId, int page = 1, int pageSize = 20, string name = "", int statusSubmission = 0)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(GetAllSubmissionsByReviewId)} foi iniciado.");

            IQueryable<Submission> query = _context.Submissions
                .Where(temp => temp.ReviewId == reviewId)
                .Include(temp => temp.EvaluatedDepartment)
                .Include(temp => temp.EvaluatorDepartment)
                .Include(temp => temp.EvaluatedEmployee)
                .Include(temp => temp.EvaluatorEmployee)
                .Include(temp => temp.ReviewResponses);

            switch (statusSubmission)
            {
                case 1: 
                    query = query.Where(temp => temp.SubmissionDate != null);
                    break;
                case 2:
                    query = query.Where(temp => temp.SubmissionDate == null);
                    break;
                default:
                    break;
            }

            if (!string.IsNullOrEmpty(name)) query = query.Where(temp => temp.EvaluatedEmployee.Nome.Contains(name) || temp.EvaluatorEmployee.Nome.Contains(name));

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Submission?> GetSubmissionById(Guid submissionId)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(GetSubmissionById)} foi iniciado.");
            return await _context.Submissions
                .Include(temp => temp.ReviewResponses)
                .Include(temp => temp.EvaluatedDepartment)
                .Include(temp => temp.EvaluatedEmployee)
                .Include(temp => temp.EvaluatorDepartment)
                .Include(temp => temp.EvaluatorEmployee)
                .FirstOrDefaultAsync(temp => temp.SubmissionId == submissionId);
        }

        public async Task<bool> UpdateSubmission(Guid submissionId, SubmissionFormDTOUpdate submissionUpdate)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(UpdateSubmission)} foi iniciado");

            //Search in db matching submission
            Submission? matchingSubmission = await _context.Submissions.Include(temp => temp.ReviewResponses).FirstOrDefaultAsync(temp => temp.SubmissionId == submissionId);
            if (matchingSubmission == null) return false;
            
            Submission? submission = submissionUpdate.ToSubmission();

            //insert patch values to the submission
            matchingSubmission.SubmissionDate = submission.SubmissionDate;
            matchingSubmission.ReviewResponses = submission.ReviewResponses;

            //save changes and return
            var result = await _context.SaveChangesAsync();

            //Check if all submissions of that review are completed
            CheckAndUpdateReviewStatus(matchingSubmission.ReviewId!.Value);

            return result > 0;
        }

        private void CheckAndUpdateReviewStatus(Guid reviewId)
        {
            var totalSubmissions = _context.Submissions.Count(temp => temp.ReviewId == reviewId);
            var completedSubmissions = _context.Submissions.Count(temp => temp.ReviewId == reviewId && temp.SubmissionDate != null);

            if (totalSubmissions == completedSubmissions)
            {
                var review = _context.Reviews.FirstOrDefault(temp => temp.ReviewId == reviewId);
                if (review != null)
                {
                    review.ReviewStatus = ReviewStatus.Completed.ToString();
                    _context.SaveChanges();
                }
            }
        }
    }
}
