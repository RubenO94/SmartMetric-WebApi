using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class ReviewDTOUpdate
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ReviewType { get; set; }
        public string? ReviewStatus { get; set; }

        public ICollection<TranslationDTOUpdate>? Translations { get; set; }
        public ICollection<QuestionDTOUpdate>? Questions { get; set; }
        public ICollection<int>? EmployeeIds { get; set; }
        public ICollection<int>? DepartmentIds { get; set; }
    }
}
