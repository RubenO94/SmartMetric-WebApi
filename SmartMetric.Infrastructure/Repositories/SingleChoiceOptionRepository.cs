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

        public async Task<SingleChoiceOption> AddSingleChoiceOption(SingleChoiceOption singleChoiceOption)
        {
            _logger.LogInformation($"{nameof(SingleChoiceOptionRepository)}.{nameof(AddSingleChoiceOption)} foi iniciado");

            _dbContext.SingleChoiceOptions.Add(singleChoiceOption);
            await _dbContext.SaveChangesAsync();
            return singleChoiceOption;
        }

        public Task<List<SingleChoiceOption>> GetAllSingleChoiceOptions()
        {
            throw new NotImplementedException();
        }

        public Task<SingleChoiceOption?> GetSingleChoiceOptionById(Guid singleChoiceOptionId)
        {
            throw new NotImplementedException();
        }

        public Task<List<SingleChoiceOption>?> GetSingleChoiceOptionsByQuestionId(Guid questionId)
        {
            throw new NotImplementedException();
        }
    }
}
