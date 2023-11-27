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
    /// <summary>
    /// Interface para serviços relacionados ao SmartTime.
    /// </summary>
    public interface ISmartTimeService
    {
        #region FuncionarioChefia
        //TODO: Serviço para FuncionarioChefia
        #endregion

        #region Departamento
        //TODO: Serviço para Departamento
        #endregion

        #region PerfilDepartamento
        //TODO: Serviço para PerfilDepartamento
        #endregion

        #region Perfil
        //TODO: Serviço para Perfil
        #endregion

        #region Utilizador

        /// <summary>
        /// Obtém um utilizador pelo ID.
        /// </summary>
        /// <param name="userId">O ID do utilizador.</param>
        /// <returns>Um objeto UserDTO ou null se não encontrado.</returns>
        Task<UserDTO?> GetUserById(int userId);

        /// <summary>
        /// Obtém um utilizador pelo nome.
        /// </summary>
        /// <param name="name">O nome do utilizador.</param>
        /// <returns>Um objeto UserDTO ou null se não encontrado.</returns>
        Task<UserDTO?> GetUserByName(string name);

        /// <summary>
        /// Obtém um utilizador pelo endereço de e-mail.
        /// </summary>
        /// <param name="email">O endereço de e-mail do utilizador.</param>
        /// <returns>Um objeto UserDTO ou null se não encontrado.</returns>
        Task<UserDTO?> GetUserByEmail(string? email);

        #endregion

        #region Funcionário

        /// <summary>
        /// Obtém um funcionário pelo endereço de e-mail.
        /// </summary>
        /// <param name="email">O endereço de e-mail do funcionário.</param>
        /// <returns>Um objeto UserDTO representando um funcionário ou null se não encontrado.</returns>
        Task<UserDTO?> GetEmployeeByEmail(string? email);

        /// <summary>
        /// Obtém um funcionário pelo ID.
        /// </summary>
        /// <param name="employeeId">O ID do funcionário.</param>
        /// <returns>Um objeto UserDTO representando um funcionário ou null se não encontrado.</returns>
        Task<UserDTO?> GetEmployeeById(int employeeId);

        /// <summary>
        /// Obtém uma lista de todos os funcionários.
        /// </summary>
        /// <returns>Uma lista de objetos Funcionario representando todos os funcionários.</returns>
        Task<List<Funcionario>> GetAllEmployees();

        #endregion

        #region Geral

        /// <summary>
        /// Atualiza as informações do utilizador na aplicação, verificando pelo o ApplicationUserType, (Utilizador ou Funcionário).
        /// </summary>
        /// <param name="userDTO">As informações atualizadas do utilizador.</param>
        /// <returns>Um objeto UserDTO atualizado ou null se não encontrado.</returns>
        Task<UserDTO?> UpdateApplicationUser(UserDTO userDTO);

        #endregion
    }

}
