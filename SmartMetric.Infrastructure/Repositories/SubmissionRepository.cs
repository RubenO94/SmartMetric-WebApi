using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories
{
    public class SubmissionRepository : ISubmissionRepository
    {
        public Task<bool> AddSubmission(Submission submission)
        {
            throw new NotImplementedException();
        }

        public Task<List<Submission>> GetAllSubmissions(int page = 1, int pageSize = 20)
        {
            throw new NotImplementedException();
        }

        public Task<List<Submission>> GetAllSubmissionsByReviewId(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<Submission?> GetSubmissionById(Guid submissionId)
        {
            throw new NotImplementedException();
        }
    }
}
