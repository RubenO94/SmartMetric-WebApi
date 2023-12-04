using Microsoft.Extensions.Logging;
using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Domain.RepositoryContracts;
using SmartMetric.Core.DTO;
using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.Enums;
using SmartMetric.Core.Exceptions;
using SmartMetric.Core.ServicesContracts;
using SmartMetric.Infrastructure.Models;

namespace SmartMetric.Core.Services
{
    public class SmartTimeService : ISmartTimeService
    {
        private readonly ISmartTimeRepository _smartTimeRepository;
        private readonly ILogger<SmartTimeService> _logger;

        public SmartTimeService(ISmartTimeRepository smartTimeRepository, ILogger<SmartTimeService> logger)
        {
            _smartTimeRepository = smartTimeRepository;
            _logger = logger;
        }

        #region Departamentos

        public async Task<List<DepartmentDTOResponse>> GetDepartmentsByPerfilId(int? perfilId)
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(GetDepartmentsByPerfilId)} foi iniciado");

            if (perfilId == null) throw new ArgumentNullException(nameof(perfilId));

            var perfilResult = await _smartTimeRepository.GetProfileById(perfilId.Value);

            if (perfilResult == null) throw new ArgumentNullException("Perfil does not exist");

            var departments = await _smartTimeRepository.GetDepartmentsByPerfilId(perfilId.Value);

            return departments.Select(temp =>
            {
                return new DepartmentDTOResponse()
                {
                    DepartmentId = temp.Iddepartamento,
                    DepartmentFatherId = temp.IddepartamentoPai,
                    DepartmentDescription = temp.Descricao
                };

            }).ToList();
        }

        public async Task<List<Departamento>> GetDepartmentsByListIds(List<int> departmentIds)
        {
            if (!departmentIds.Any()) throw new ArgumentException("List of Departments can't be empty");

            var departmentsResult = await _smartTimeRepository.GetDepartmentsByListIds(departmentIds);

            var departmentsNotExisting = departmentIds.Except(departmentsResult.Select(temp => temp.Iddepartamento).ToList()).ToList();

            if (departmentsNotExisting.Any()) throw new ArgumentException("Some of the departments ids does not exist");

            return departmentsResult;
        }


        #endregion

        #region Perfis

        public async Task<UserProfileDTOResponse> GetProfileByUserId(ApplicationUserType applicationUserType, int userId)
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(GetProfileByUserId)} foi iniciado");

            if (applicationUserType == ApplicationUserType.User)
            {
                var user = await _smartTimeRepository.GetUserById(userId);

                if (user == null) throw new ArgumentException("Unidentified user", "User");

                if (user.Idperfil == null) throw new ArgumentException("The user does not have an associated profile", "User");

                var profile = await _smartTimeRepository.GetProfileById(user.Idperfil.Value);

                var departments = await _smartTimeRepository.GetDepartmentsByPerfilId(user.Idperfil.Value, 1, int.MaxValue);

                //TODO: Recever as janelas do prefil associado ao utlizador

                return new UserProfileDTOResponse()
                {
                    UserId = user.Idutilizador,
                    UserName = user.Nome,
                    UserEmail = user.Email,
                    EmployeeId = user.Idfuncionario,
                    ProfileType = profile!.PortalColaborador == null || profile.PortalColaborador == 0 ? ProfileType.Backoffice : ProfileType.Frontoffice,
                    ProfileDescription = profile.Nome,
                    Permissions = new List<WindowPermissionDTO>() //TODO: Alterar de estático para dinamico
                    {
                        new WindowPermissionDTO()
                        {
                            WindowId = 2312100,
                            WindowName = "Forms",
                            CanRead = true,
                            CanCreate = true,
                            CanDelete = true,
                            CanUpdate = true,
                            CanChangeSettings = true,
                            CanAccessAdvancedSettings = true,
                        },
                        new WindowPermissionDTO()
                        {
                            WindowId = 2312101,
                            WindowName = "Reviews",
                            CanRead = true,
                            CanCreate = true,
                            CanDelete = true,
                            CanUpdate = true,
                            CanChangeSettings = true,
                            CanAccessAdvancedSettings = true,
                        },
                        new WindowPermissionDTO()
                        {
                            WindowId = 2312102,
                            WindowName = "Statistics",
                            CanRead = true,
                            CanCreate = true,
                            CanDelete = true,
                            CanUpdate = true,
                            CanChangeSettings = true,
                            CanAccessAdvancedSettings = true,
                        },
                        new WindowPermissionDTO()
                        {
                            WindowId = 2312103,
                            WindowName = "Admin Settings",
                            CanRead = true,
                            CanCreate = true,
                            CanDelete = true,
                            CanUpdate = true,
                            CanChangeSettings = true,
                            CanAccessAdvancedSettings = true,
                        },
                    },
                };

            }

            if (applicationUserType == ApplicationUserType.Employee)
            {
                var user = await _smartTimeRepository.GetEmployeeById(userId);

                if (user == null) throw new ArgumentException("Unidentified employee", "Employee");

                if (user.Idperfil == null) throw new ArgumentException("The employee does not have an associated profile", "Employee");

                var profile = await _smartTimeRepository.GetProfileById(user.Idperfil.Value);

                var departments = await _smartTimeRepository.GetDepartmentsByPerfilId(user.Idperfil.Value);

                //TODO: Recever as janelas do prefil associado ao utlizador

                return new UserProfileDTOResponse()
                {
                    UserName = user.Nome,
                    UserEmail = user.Email,
                    EmployeeId = user.Idfuncionario,
                    ProfileType = profile!.PortalColaborador == null || profile.PortalColaborador == 0 ? ProfileType.Backoffice : ProfileType.Frontoffice,
                    ProfileDescription = profile.Nome,
                    Permissions = new List<WindowPermissionDTO>()
                    {
                        new WindowPermissionDTO()
                        {
                            WindowId = 2312100,
                            WindowName = "Forms",
                            CanRead = true,
                            CanCreate = true,
                            CanDelete = true,
                            CanUpdate = true,
                            CanChangeSettings = true,
                            CanAccessAdvancedSettings = true,
                        },
                    },

                };
            }

            throw new ArgumentException("Unidentified application user", "Application User");
        }

        #endregion

        #region Funcionarios

        public Task<List<UserDTO>> GetAllEmployeesBySelectedDepartments()
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(GetAllEmployeesBySelectedDepartments)} foi iniciado");

            throw new NotImplementedException();
        }

        public async Task<UserDTO?> GetEmployeeByEmail(string? email)
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(GetEmployeeByEmail)} foi iniciado");

            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            Funcionario? funcionario = await _smartTimeRepository.GetEmployeeByEmail(email);

            if (funcionario == null)
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User doesn't exist.");
            }

            return funcionario.ToUserDTO();
        }

        public async Task<UserDTO?> GetEmployeeById(int employeeId)
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(GetEmployeeById)} foi iniciado");
            if (employeeId == default)
            {
                throw new ArgumentNullException(nameof(employeeId));
            }

            Funcionario? funcionario = await _smartTimeRepository.GetEmployeeById(employeeId);

            if (funcionario == null)
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User doesn't exist.");
            }

            return funcionario.ToUserDTO();
        }

        #endregion

        #region Utilizadores

        public async Task<UserDTO?> GetUserByEmail(string? email)
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(GetUserByEmail)} foi iniciado");
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            Utilizador? utilizador = await _smartTimeRepository.GetUserByEmail(email);

            if (utilizador == null)
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User doesn't exist.");
            }

            return utilizador.ToUserDTO();
        }

        public async Task<UserDTO?> GetUserById(int userId)
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(GetUserById)} foi iniciado");
            if (userId == default)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            Utilizador? utilizador = await _smartTimeRepository.GetUserById(userId);
            if (utilizador == null)
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User doesn't exist.");
            }

            return utilizador.ToUserDTO();
        }

        public async Task<UserDTO?> GetUserByName(string? name)
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(GetUserByName)} foi iniciado");
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            Utilizador? utilizador = await _smartTimeRepository.GetUserByName(name);
            if (utilizador == null)
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User doesn't exist.");
            }

            return utilizador.ToUserDTO();
        }

        public async Task<UserDTO?> UpdateApplicationUser(UserDTO user)
        {
            _logger.LogInformation($"{nameof(SmartTimeService)}.{nameof(UpdateApplicationUser)} foi iniciado");

            if (user == null)
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "Request can't be null.");
            }

            if (user.ApplicationUserType == Enums.ApplicationUserType.User)
            {
                Utilizador? utilizador = await _smartTimeRepository.GetUserById(user.UserId);
                if (utilizador == null)
                {
                    throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User doesn't exist.");
                }

                utilizador.RefreshTokenExpiration = user.RefreshTokenExpiration;
                utilizador.RefreshToken = user.RefreshToken;

                await _smartTimeRepository.UpdateUser(utilizador);

                return utilizador.ToUserDTO();
            }
            else if (user.ApplicationUserType == Enums.ApplicationUserType.Employee)
            {
                Funcionario? funcionario = await _smartTimeRepository.GetEmployeeById(user.UserId);
                if (funcionario == null)
                {
                    throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User doesn't exist.");
                }

                funcionario.RefreshTokenExpiration = user.RefreshTokenExpiration;
                funcionario.RefreshToken = user.RefreshToken;

                await _smartTimeRepository.UpdateEmployee(funcionario);

                return funcionario.ToUserDTO();
            }
            else
            {
                throw new HttpStatusException(System.Net.HttpStatusCode.BadRequest, "User profile not recongnize");
            }


        }

        #endregion




    }
}
