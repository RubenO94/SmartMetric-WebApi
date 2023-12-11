using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts.Common
{
    public interface IBaseRepository
    {
        /// <summary>
        /// Conta o número total de registos na tabela associada à entidade especificada.
        /// </summary>
        /// <typeparam name="TEntity">O tipo da entidade cuja tabela será contada.</typeparam>
        /// <returns>O número total de registros na tabela.</returns>
        Task<int> CountRecords<TEntity>(Expression<Func<TEntity, bool>>? filter = null) where TEntity : class;
    }
}
