using SmartMetric.Core.Domain.Entities;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    public interface ISmartTimeRepository
    {
        Task<Utilizador?> GetUser(int userId);
        Task<Utilizador?> UpdateUser(Utilizador utilizador);
        Task<List<Funcionario>> GetAllEmployees();
    }
}
