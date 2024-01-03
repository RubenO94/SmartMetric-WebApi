using Microsoft.EntityFrameworkCore;
using SmartMetric.Core.Domain.RepositoryContracts.Common;
using SmartMetric.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories.Common
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountRecords<TEntity>(Expression<Func<TEntity, bool>>? filter = null) where TEntity : class
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.CountAsync();
        }

    }
}
