using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts
{
    public interface ISmartTimeService
    {
        Task<UserDTO?> GetUserById(int userId);
        Task<UserDTO?> GetUserByName(string name);
        Task<UserDTO?> GetUserByEmail(string? email);

        Task<UserDTO?> GetEmployeeByEmail(string? email);
        Task<UserDTO?> GetEmployeeById(int employeeId);
        Task<List<Funcionario>> GetAllEmployees();

        Task<UserDTO?> UpdateApplicationUser(UserDTO userDTO);
    }
}
