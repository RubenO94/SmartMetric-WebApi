using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories
{
    public class RatingOptionRepository : IRatingOptionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RatingOptionRepository> _logger;

        public RatingOptionRepository(ApplicationDbContext dbContext, ILogger<RatingOptionRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<RatingOption> AddRatingOption(RatingOption ratingOption)
        {
                _logger.LogInformation($"{nameof(RatingOptionRepository)}.{nameof(AddRatingOption)} foi iniciado");

                _dbContext.RatingOptions.Add(ratingOption);
                await _dbContext.SaveChangesAsync();
                return ratingOption;
        }

        #endregion

        #region Getters

        public async Task<List<RatingOption>> GetAllRatingOption()
        {
            _logger.LogInformation($"{nameof(RatingOptionRepository)}.{nameof(GetAllRatingOption)} foi iniciado.");
            return await _dbContext.RatingOptions.ToListAsync();
        }

        public async Task<RatingOption?> GetRatingOptionById(Guid ratingOptionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionRepository)}.{nameof(GetRatingOptionById)} foi iniciado.");

            var response = await _dbContext.RatingOptions.Include(temp => temp.Translations).FirstOrDefaultAsync(rto => rto.RatingOptionId == ratingOptionId);
            return response;
        }

        public async Task<List<RatingOption>?> GetRatingOptionByQuestionId(Guid questionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionRepository)}.{nameof(GetRatingOptionByQuestionId)} foi iniciado.");
            return await _dbContext.RatingOptions.Where(temp => temp.QuestionId == questionId).ToListAsync();
        }

        #endregion

        #region Deleters

        public async Task<bool> DeleteRatingOptionById(Guid ratingOptionId)
        {
            _dbContext.RatingOptions.RemoveRange(_dbContext.RatingOptions.Where(temp => temp.RatingOptionId == ratingOptionId));
            int rowsDeleted = await _dbContext.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        #endregion
    }
}
