using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.Entities
{
    public class ReviewDepartment
    {
        [Key]
        public Guid ReviewDepartmentId {  get; set; } 
        public Guid ReviewId { get; set; }
        public int DepartmentId { get; set; }


        [ForeignKey(nameof(ReviewId))]
        public virtual Review? Review { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public virtual Departamento? Department { get; set; }
    }
}
