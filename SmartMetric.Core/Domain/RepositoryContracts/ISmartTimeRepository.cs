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
        #region Utilizadores
        Task<Utilizador?> GetUserById(int userId);
        Task<Utilizador?> GetUserByEmail(string email);
        Task<Utilizador?> GetUserByName(string name);
        Task<Utilizador?> UpdateUser(Utilizador utilizador);
        #endregion

        #region Funcionarios
        Task<Funcionario?> GetEmployeeByEmail(string email);
        Task<Funcionario?> GetEmployeeById(int userId);
        Task<Funcionario?> GetEmployeeByName(string name);
        Task<Funcionario?> UpdateEmployee(Funcionario funcionario);
        Task<List<Funcionario>> GetAllEmployees();
        #endregion
    }
}
