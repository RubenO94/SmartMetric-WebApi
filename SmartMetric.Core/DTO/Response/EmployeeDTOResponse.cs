using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class EmployeeDTOResponse
    {
        public int EmployeeId { get; set; }
        public int? DepartmentId { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeeEmail { get; set; }
        public byte[]? EmployeePhoto { get; set; }


        /// <summary>
        /// Compara os dados atuais deste objeto com o parâmetro.
        /// </summary>
        /// <param name="obj">O objeto parâmetro a ser comparado.</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem ao objeto especificado no parâmetro.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(EmployeeDTOResponse)) return false;

            EmployeeDTOResponse employee = (EmployeeDTOResponse)obj;
            return EmployeeId == employee.EmployeeId && EmployeeName == employee.EmployeeName && EmployeeEmail == employee.EmployeeEmail && EmployeePhoto == employee.EmployeePhoto;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"EmployeeId: {EmployeeId}\nEmployeeName: {EmployeeName}\nEmployeeEmail: {EmployeeEmail}\nHavePhoto: {EmployeePhoto?.Any() ?? false}";
        }
    }

    public static class EmployeeExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto Funcionario em um objeto EmployeeDTOResponse.
        /// </summary>
        /// <param name="formTemplate">O objeto Funcionario a ser convertido.</param>
        /// <returns>Retorna o EmployeeDTOResponse convertido.</returns>
        public static EmployeeDTOResponse ToFormTemplateDTOResponse(this Funcionario funcionario)
        {

            return new EmployeeDTOResponse()
            {
                EmployeeId = funcionario.Idfuncionario,
                DepartmentId = funcionario.Iddepartamento,
                EmployeeName = funcionario.Nome,
                EmployeeEmail = funcionario.Email,
                EmployeePhoto = funcionario.Fotografia,
            };
        }
    }
}
