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

        #region Perfis

        public async Task<Perfil?> GetProfileById(int perfilId)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetProfileById)} foi iniciado");

            return await _context.Perfis.FirstOrDefaultAsync(temp => temp.Idperfil == perfilId);
        }

        public async Task<List<int>> GetProfileWindowsByProfileId(int profileId)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetProfileWindowsByProfileId)} foi iniciado");

           var result =await _context.ProfilePermissions.Where(temp => temp.ProfileId == profileId).ToListAsync();

            return result.Select(temp => temp.PermissionId).ToList();
        }

        public async Task<ProfilePermission?> AddProfilePermission(ProfilePermission profilePermission)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(AddProfilePermission)} foi iniciado");
            
            if (profilePermission == null) return null;

            _context.ProfilePermissions.Add(profilePermission);
            await _context.SaveChangesAsync();

            return profilePermission;
        }

        #endregion

        #region Funcionarios

        public async Task<List<Funcionario>> GetAllEmployeesByDepartmentsSelected(List<int?> departmentIds, int page = 1, int pageSize = 20)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetAllEmployeesByDepartmentsSelected)} foi iniciado");

            // Filtrar os funcionários pelos IDs dos departamentos selecionados
            var employees = await _context.Funcionarios
                .Where(funcionario => departmentIds.Contains(funcionario.Iddepartamento))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return employees;
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


        #endregion

        #region Departamentos

        public async Task<List<Departamento>> GetAllDepartaments(int page = 1, int pageSize = 20)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetAllDepartaments)} foi iniciado");

            // Aplica a funcionalidade de paginação
            var departments = await _context.Departamentos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return departments;
        }

        public async Task<List<Departamento>> GetDepartmentsByPerfilId(int perfilId, int page = 1, int pageSize = 20)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetDepartmentsByPerfilId)} foi iniciado");

            // Obtém os IDs dos departamentos associados ao perfil
            var departamentoIds = await _context.PerfisDepartamentos
                .Where(temp => temp.Idperfil == perfilId)
                .Select(pd => pd.Iddepartamento)
                .ToListAsync();

            // Filtra os departamentos com base nos IDs obtidos e aplica a funcionalidade de paginação
            var departamentosAssociados = await _context.Departamentos
                .Where(departamento => departamentoIds.Contains(departamento.Iddepartamento))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return departamentosAssociados;
        }

        public async Task<List<Departamento>> GetDepartmentsByListIds(List<int> departmentIds)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetDepartmentsByPerfilId)} foi iniciado");

            // Filtra os departamentos com base nos IDs fornecidos
            var departments = await _context.Departamentos
                .Where(d => departmentIds.Contains(d.Iddepartamento))
                .ToListAsync();

            return departments;

        }


        #endregion

        #region FuncionariosCHefias

        public async Task<List<FuncionariosChefia>> GetAllChiefsEmployee(int page = 1, int pageSize = 20)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetAllChiefsEmployee)} foi iniciado");

            return await _context.FuncionariosChefias
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }



        public async Task<List<Funcionario>> GetEmployeesByChiefId(int chiefId, int page = 1, int pageSize = 20)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetEmployeesByChiefId)} foi iniciado");

            // Obtém os IDs dos funcionários associados ao supervisor
            var funcionarioIds = await _context.FuncionariosChefias
                .Where(temp => temp.IdfuncionarioSuperior == chiefId)
                .Select(fc => fc.Idfuncionario)
                .ToListAsync();

            // Filtra os funcionários com base nos IDs obtidos e aplica a funcionalidade de paginação
            var funcionariosAssociados = await _context.Funcionarios
                .Where(temp => funcionarioIds.Contains(temp.Idfuncionario))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return funcionariosAssociados;
        }


        public async Task<List<Departamento>> GetDepartmentsByChiefId(int chiefId, int page = 1, int pageSize = 20)
        {
            _logger.LogInformation($"{nameof(SmartTimeRepository)}.{nameof(GetDepartmentsByChiefId)} foi iniciado");

            // Obtém os IDs dos departamentos associados ao supervisor
            var departamentoIds = await _context.FuncionariosChefias
                .Where(temp => temp.IdfuncionarioSuperior == chiefId)
                .Select(fc => fc.Iddepartamento)
                .ToListAsync();

            // Filtra os departamentos com base nos IDs obtidos e aplica a funcionalidade de paginação
            var departamentosAssociados = await _context.Departamentos
                .Where(temp => departamentoIds.Contains(temp.Iddepartamento))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return departamentosAssociados;
        }

        #endregion

    }
}
