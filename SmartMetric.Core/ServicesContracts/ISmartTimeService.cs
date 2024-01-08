using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
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
        #region Departamento
        /// <summary>
        /// Obtém a lista de departamentos associados a um perfil.
        /// </summary>
        /// <param name="profileId">O ID do perfil.</param>
        /// <returns>Uma lista de objetos DepartmentDTOResponse representando os departamentos.</returns>
        Task<ApiResponse<List<DepartmentDTOResponse>>> GetDepartmentsByProfileId(int? profileId, int page = 1, int pageSize = 20);

        Task<List<Departamento>> GetDepartmentsByListIds(List<int> departmentIds);

        Task<ApiResponse<List<DepartmentDTOResponse>>> GetAllDepartments(int page = 1, int pageSize = 20);
        #endregion

        #region Perfil
        /// <summary>
        /// Obtém as informações de perfil pelo userId.
        /// </summary>
        /// <param name="userId">O ID do user.</param>
        /// <returns>Um objeto UserProfileDTOResponse representando as informações do perfil.</returns>
        Task<ApiResponse<UserProfileDTOResponse>> GetProfileByUserId(ApplicationUserType applicationUserType, int userId);

        Task<ApiResponse<List<PermissionDTO>>> UpdateWindowPermissionsToProfile(int profileId, List<int> permissionsIds);

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
        /// Obtém uma lista de todos os funcionários associados aos departamentos selecionados.
        /// </summary>
        /// <returns>Uma lista de objetos Funcionario representando os funcionários associados aos departamentos selecionados.</returns>
        Task<List<UserDTO>> GetAllEmployeesBySelectedDepartments();

        /// <summary>
        /// Obtém uma lista de todos os funcionários associados ao id departamento no parametro.
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        Task<ApiResponse<List<EmployeeDTOResponse>>> GetEmployeesByDepartmentId(int departmentId);

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
