using Microsoft.EntityFrameworkCore;
using SmartMetric.Core.Domain.RepositoryContracts.Common;
using SmartMetric.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories.Common
{
    public class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountRecords<TEntity>() where TEntity : class
        {
            return await _dbContext.Set<TEntity>().CountAsync();
        }

    }
}
