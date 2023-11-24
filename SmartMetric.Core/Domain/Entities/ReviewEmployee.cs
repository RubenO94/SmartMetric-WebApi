using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Domain.Entities
{
    public class ReviewEmployee
    {
        [Key]
        public Guid ReviewEmployeeId { get; set; }
        public Guid ReviewId { get; set; }
        public int EmployeeId { get; set; }


        [ForeignKey(nameof(ReviewId))]
        public virtual Review? Review { get; set; }
        
        [ForeignKey(nameof(EmployeeId))]
        public virtual Funcionario? Employee { get; set; }
    }
}
