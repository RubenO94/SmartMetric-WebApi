using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Infrastructure.DatabaseContext;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Infrastructure.Repositories
{
    public class SmartTimeRepository : ISmartTimeRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SmartTimeRepository> _logger;

        public SmartTimeRepository(ApplicationDbContext context, ILogger<SmartTimeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Funcionarios

        public async Task<List<Funcionario>> GetAllEmployees()
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetAllEmployees)} foi iniciado");

            return await _context.Funcionarios.ToListAsync();
        }

        public async Task<Funcionario?> GetEmployeeByEmail(string email)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetEmployeeByEmail)} foi iniciado");

            return await _context.Funcionarios.FirstOrDefaultAsync(temp => temp.Email == email);
        }

        public async Task<Funcionario?> GetEmployeeById(int userId)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetEmployeeById)} foi iniciado");

            return await _context.Funcionarios.FirstOrDefaultAsync(temp => temp.Idfuncionario == userId);
        }

        public async Task<Funcionario?> GetEmployeeByName(string name)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetEmployeeByName)} foi iniciado");

            return await _context.Funcionarios.FirstOrDefaultAsync(temp => temp.Nome == name);
        }

        public async Task<Funcionario?> UpdateEmployee(Funcionario funcionario)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(UpdateEmployee)} foi iniciado");

            var user = await _context.Funcionarios.FindAsync(funcionario.Idfuncionario);

            if (user == null)
            {
                return funcionario;
            }

            user.RefreshTokenExpiration = funcionario.RefreshTokenExpiration;
            user.RefreshToken = funcionario.RefreshToken;

            await _context.SaveChangesAsync();

            return user;
        }

        #endregion

        #region Utilizadores

        public async Task<Utilizador?> GetUserByEmail(string email)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetUserByEmail)} foi iniciado");

            return await _context.Utilizadores.FirstOrDefaultAsync(temp => temp.Email == email);
        }

        public async Task<Utilizador?> GetUserById(int userId)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetUserById)} foi iniciado");

            return await _context.Utilizadores.FirstOrDefaultAsync(temp => temp.Idutilizador == userId);
        }

        public async Task<Utilizador?> GetUserByName(string name)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetUserByName)} foi iniciado");

            return await _context.Utilizadores.FirstOrDefaultAsync(temp => temp.Nome == name);
        }

        public async Task<Utilizador?> UpdateUser(Utilizador utilizador)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(UpdateUser)} foi iniciado");

            var user = await _context.Utilizadores.FindAsync(utilizador.Idutilizador);

            if (user == null)
            {
                return utilizador;
            }

            user.RefreshTokenExpiration = utilizador.RefreshTokenExpiration;
            user.RefreshToken = utilizador.RefreshToken;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<Departamento>> GetAllDepartaments()
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetAllDepartaments)} foi iniciado");

            return await _context.Departamentos.ToListAsync();

        }

        public async Task<List<FuncionariosChefia>> GetAllFuncionariosChefia(int page = 1, int pageSize = 20)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetAllFuncionariosChefia)} foi iniciado");

            return await _context.FuncionariosChefias
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<List<Departamento>> GetSelectedDepartments(List<int> departmentIds)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
