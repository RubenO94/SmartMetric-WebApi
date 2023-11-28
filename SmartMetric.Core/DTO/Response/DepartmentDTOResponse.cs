using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.Response
{
    public class DepartmentDTOResponse
    {
        public int DepartmentId { get; set; }
        public int? DepartmentFatherId { get; set; }
        public string? DepartmentDescription { get; set; }
    }


    public static class DepartmentExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto Departamento em um objeto DepartmentDTOResponse.
        /// </summary>
        /// <param name="departamento">O objeto Departamento a ser convertido.</param>
        /// <returns>Retorna o DepartmentDTOResponse convertido.</returns>
        public static DepartmentDTOResponse ToDepartamentDTOResponse(Departamento departamento)
        {
            return new DepartmentDTOResponse()
            {
                DepartmentId = departamento.Iddepartamento,
                DepartmentFatherId = departamento.IddepartamentoPai,
                DepartmentDescription = departamento.Descricao
            };
        }
    }
}
