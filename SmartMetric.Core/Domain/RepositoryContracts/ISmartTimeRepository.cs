using SmartMetric.Core.Domain.Entities;
using SmartMetric.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.RepositoryContracts
{
    /// <summary>
    /// Interface para o repositório relacionado ao SmartTime.
    /// </summary>
    public interface ISmartTimeRepository
    {

        Task<int> GetTotalRecords();

        #region Perfis
        Task<Perfil?> GetProfileById(int profileId);
        Task<List<int>> GetProfileWindowsByProfileId(int profileId);

        Task<List<ProfilePermission>?> AddProfilePermissions(List<ProfilePermission> profilePermissions);
        #endregion

        #region Utilizadores

        /// <summary>
        /// Obtém um utilizador pelo ID.
        /// </summary>
        /// <param name="userId">O ID do utilizador.</param>
        /// <returns>Um objeto Utilizador ou null se não encontrado.</returns>
        Task<Utilizador?> GetUserById(int userId);

        /// <summary>
        /// Obtém um utilizador pelo endereço de e-mail.
        /// </summary>
        /// <param name="email">O endereço de e-mail do utilizador.</param>
        /// <returns>Um objeto Utilizador ou null se não encontrado.</returns>
        Task<Utilizador?> GetUserByEmail(string email);

        /// <summary>
        /// Obtém um utilizador pelo nome.
        /// </summary>
        /// <param name="name">O nome do utilizador.</param>
        /// <returns>Um objeto Utilizador ou null se não encontrado.</returns>
        Task<Utilizador?> GetUserByName(string name);

        /// <summary>
        /// Atualiza as informações de um utilizador.
        /// </summary>
        /// <param name="utilizador">O objeto Utilizador atualizado.</param>
        /// <returns>Um objeto Utilizador atualizado ou null se não encontrado.</returns>
        Task<Utilizador?> UpdateUser(Utilizador utilizador);

        #endregion

        #region Funcionarios

        /// <summary>
        /// Obtém um funcionário pelo endereço de e-mail.
        /// </summary>
        /// <param name="email">O endereço de e-mail do funcionário.</param>
        /// <returns>Um objeto Funcionario ou null se não encontrado.</returns>
        Task<Funcionario?> GetEmployeeByEmail(string email);

        /// <summary>
        /// Obtém um funcionário pelo ID.
        /// </summary>
        /// <param name="userId">O ID do funcionário.</param>
        /// <returns>Um objeto Funcionario ou null se não encontrado.</returns>
        Task<Funcionario?> GetEmployeeById(int userId);

        /// <summary>
        /// Obtém um funcionário pelo nome.
        /// </summary>
        /// <param name="name">O nome do funcionário.</param>
        /// <returns>Um objeto Funcionario ou null se não encontrado.</returns>
        Task<Funcionario?> GetEmployeeByName(string name);

        /// <summary>
        /// Atualiza as informações de um funcionário.
        /// </summary>
        /// <param name="funcionario">O objeto Funcionario atualizado.</param>
        /// <returns>Um objeto Funcionario atualizado ou null se não encontrado.</returns>
        Task<Funcionario?> UpdateEmployee(Funcionario funcionario);

        /// <summary>
        /// Obtém uma lista de todos os funcionários associados ao departamentos selecionados.
        /// </summary>
        /// <returns>Uma lista de objetos Funcionario representando todos os funcionários.</returns>
        Task<List<Funcionario>> GetEmployeesByDepartmentsSelected(List<int?> departmentIds);

        /// <summary>
        /// Obtém uma lista de todos os funcionários associados ao id do departamento no parametro
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        Task<List<Funcionario>> GetEmployeesByDepartmentId(int departmentId);

        #endregion

        #region Departamentos
        Task<List<Departamento>> GetAllDepartaments(int page = 1, int pageSize = 20);
        Task<List<Departamento>> GetDepartmentsByPerfilId(int perfilId, int page = 1, int pageSize = 20);
        Task<List<Departamento>> GetDepartmentsByListIds(List<int> departmentIds);
        #endregion

        #region FuncionariosChefia
        Task<List<Funcionario>> GetEmployeesByChiefId(int chiefId, int page = 1, int pageSize = 20);
        Task<List<Departamento>> GetDepartmentsByChiefId(int chiefId, int page = 1, int pageSize = 20);
        Task<List<FuncionariosChefia>> GetAllChiefsEmployee(int page = 1, int pageSize = 20);
        #endregion
    }

}
