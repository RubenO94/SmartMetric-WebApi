using SmartMetric.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [MinLength(1, ErrorMessage = "Review Translations can't be less than 1")]
        public ICollection<TranslationDTOUpdate>? Translations { get; set; }
        [MinLength(1, ErrorMessage = "Review Questions can't be less than 1")]
        public ICollection<QuestionDTOUpdate>? Questions { get; set; }
        public ICollection<int>? ReviewEmployeesIds { get; set; }
        public ICollection<int>? ReviewDepartmentsIds { get; set; }
    }
}
