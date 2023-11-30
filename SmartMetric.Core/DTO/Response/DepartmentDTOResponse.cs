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

        /// <summary>
        /// Compara os dados atuais deste objeto com o parâmetro.
        /// </summary>
        /// <param name="obj">O objeto parâmetro a ser comparado.</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem ao objeto especificado no parâmetro.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(DepartmentDTOResponse)) return false;

            DepartmentDTOResponse department = (DepartmentDTOResponse)obj;
            return DepartmentId == department.DepartmentId && DepartmentFatherId == department.DepartmentFatherId && DepartmentDescription == department.DepartmentDescription;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    public static class DepartmentExtensions
    {
        /// <summary>
        /// Um método de extensão que converte um objeto Departamento em um objeto DepartmentDTOResponse.
        /// </summary>
        /// <param name="departamento">O objeto Departamento a ser convertido.</param>
        /// <returns>Retorna o DepartmentDTOResponse convertido.</returns>
        public static DepartmentDTOResponse ToDepartamentDTOResponse(this Departamento departamento)
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
