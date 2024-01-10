using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
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
