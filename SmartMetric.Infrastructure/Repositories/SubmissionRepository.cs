using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using SmartMetric.Infrastructure.DatabaseContext;
using SmartMetric.Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<List<Submission>> GetAllSubmissionsByEmployeeId(int employeeId)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(GetAllSubmissionsByEmployeeId)} foi iniciado.");

            return await _context.Submissions.Where(temp => temp.EvaluatorEmployeeId == employeeId).ToListAsync();
        }

        public Task<List<Submission>> GetAllSubmissionsByReviewId(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public async Task<Submission?> GetSubmissionById(Guid submissionId)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(GetSubmissionById)} foi iniciado.");
            return await _context.Submissions.Include(temp => temp.ReviewResponses).FirstOrDefaultAsync(temp => temp.SubmissionId == submissionId);
        }

        public async Task<bool> UpdateSubmission(Guid submissionId, SubmissionFormDTOUpdate submissionUpdate)
        {
            _logger.LogInformation($"{nameof(SubmissionRepository)}.{nameof(UpdateSubmission)} foi iniciado");

            //Search in db matching submission
            Submission? matchingSubmission = await _context.Submissions.FirstOrDefaultAsync(temp => temp.SubmissionId == submissionId);
            if (matchingSubmission == null) return false;
            
            Submission? submission = submissionUpdate.ToSubmission();

            //insert patch values to the submission
            matchingSubmission.SubmissionDate = submission.SubmissionDate;
            matchingSubmission.ReviewResponses = submission.ReviewResponses;

            //save changes and return
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
