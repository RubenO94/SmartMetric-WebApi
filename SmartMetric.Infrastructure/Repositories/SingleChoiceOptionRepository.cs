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
    public class SingleChoiceOptionRepository : ISingleChoiceOptionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<SingleChoiceOptionRepository> _logger;

        public SingleChoiceOptionRepository(ApplicationDbContext dbContext, ILogger<SingleChoiceOptionRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        #region Adders

        public async Task<SingleChoiceOption> AddSingleChoiceOption(SingleChoiceOption singleChoiceOption)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionRepository)}.{nameof(AddSingleChoiceOption)} foi iniciado");

            _dbContext.SingleChoiceOptions.Add(singleChoiceOption);
            await _dbContext.SaveChangesAsync();
            return singleChoiceOption;
        }

        #endregion

        #region Getters

        public async Task<List<SingleChoiceOption>> GetAllSingleChoiceOptions()
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionRepository)}.{nameof(GetAllSingleChoiceOptions)} foi iniciado");
            return await _dbContext.SingleChoiceOptions.ToListAsync();
        }

        public async Task<SingleChoiceOption?> GetSingleChoiceOptionById(Guid? singleChoiceOptionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionRepository)}.{nameof(GetSingleChoiceOptionById)} foi iniciado");

            var response = await _dbContext.SingleChoiceOptions.Include(temp => temp.Translations).FirstOrDefaultAsync(sco => sco.SingleChoiceOptionId == singleChoiceOptionId);
            return response;
        }

        public async Task<List<SingleChoiceOption>?> GetSingleChoiceOptionsByQuestionId(Guid questionId)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionRepository)}.{nameof(GetSingleChoiceOptionsByQuestionId)} foi iniciado");
            return await _dbContext.SingleChoiceOptions.Where(temp => temp.QuestionId.Equals(questionId)).ToListAsync();
        }

        #endregion

        #region Deleters

        public async Task<bool> DeleteSingleChoiceOptionById(Guid singleChoiceOptionId)
        {
            _dbContext.SingleChoiceOptions.RemoveRange(_dbContext.SingleChoiceOptions.Where(temp => temp.SingleChoiceOptionId == singleChoiceOptionId));
            int rowsDeleted = await _dbContext.SaveChangesAsync();

            return rowsDeleted > 0;
        }

        #endregion
    }
}
