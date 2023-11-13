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
            return await _dbContext.RatingOptions.FirstOrDefaultAsync(temp => temp.RatingOptionId == ratingOptionId);
        }

        public async Task<List<RatingOption>?> GetRatingOptionByQuestionId(Guid questionId)
        {
            _logger.LogInformation($"{nameof(RatingOptionRepository)}.{nameof(GetRatingOptionByQuestionId)} foi iniciado.");
            return await _dbContext.RatingOptions.Where(temp => temp.QuestionId == questionId).ToListAsync();
        }

        #endregion
    }
}
