using Microsoft.EntityFrameworkCore;
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
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ReviewRepository> _logger;

        public ReviewRepository(ApplicationDbContext context, ILogger<ReviewRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddReview(Review review)
        {
            _logger.LogInformation($"{nameof(ReviewRepository)}.{nameof(AddReview)} foi iniciado.");

            _context.Reviews.Add(review);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteReview(Guid reviewId)
        {
            _logger.LogInformation($"{nameof(ReviewRepository)}.{nameof(DeleteReview)} foi iniciado.");

            _context.Reviews.RemoveRange(_context.Reviews.Where(temp => temp.ReviewId == reviewId));
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<List<Review>> GetAllReviews(int page = 1, int pageSize = 20)
        {
            _logger.LogInformation($"{nameof(ReviewRepository)}.{nameof(GetAllReviews)} foi iniciado.");

            return await _context.Reviews
                .Include(temp => temp.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q!.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q.RatingOptions).ThenInclude(rt => rt.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q.SingleChoiceOptions).ThenInclude(sco => sco.Translations)
                .Include(temp => temp.Departments)!.ThenInclude(d => d.Department)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Review?> GetReviewById(Guid reviewId)
        {
            _logger.LogInformation($"{nameof(ReviewRepository)}.{nameof(GetReviewById)} foi iniciado.");
            return await _context.Reviews
                .Include(temp => temp.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q!.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q.RatingOptions).ThenInclude(rt => rt.Translations)
                .Include(temp => temp.Questions)!.ThenInclude(q => q.SingleChoiceOptions).ThenInclude(sco => sco.Translations)
                .Include(temp => temp.Departments)!.ThenInclude(d => d.Department)
                .FirstOrDefaultAsync(temp => temp.ReviewId == reviewId);
        }

        public async Task<int> GetTotalRecords()
        {
            return await base.CountRecords<Review>();
        }

        public async Task<bool> UpdateReview(Review review)
        {
            _logger.LogInformation($"{nameof(ReviewRepository)}.{nameof(UpdateReview)} foi iniciado.");

            Review? matchingReview = await _context.Reviews.FirstOrDefaultAsync(temp => temp.ReviewId == review.ReviewId);

            if (matchingReview == null)
            {
                return false;
            }

            matchingReview.StartDate = review.StartDate;
            matchingReview.EndDate = review.EndDate;
            matchingReview.Departments = review.Departments;
            matchingReview.Employees = review.Employees;
            matchingReview.Translations = review.Translations;
            matchingReview.ReviewStatus = review.ReviewStatus;
            matchingReview.ReviewType = review.ReviewType;
            matchingReview.Questions = review.Questions;

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
    }
}
