using SmartMetric.Core.Domain.Entities;

namespace SmartMetric.Core.DTO.Response
{
    public class DepartmentDTOResponse
    {
        public int DepartmentId { get; set; }
        public string? DepartmentDescription { get; set; }
        public List<EmployeeDTOResponse>? Employees { get; set; }

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
            return DepartmentId == department.DepartmentId && DepartmentDescription == department.DepartmentDescription;
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
                DepartmentDescription = departamento.Descricao,
            };
        }
    }
}
