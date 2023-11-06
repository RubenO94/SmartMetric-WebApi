using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Entities
{
    public class FormTemplate
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedByUserId { get; set; }
        public ICollection<FormTemplateTranslation>? Translations { get; set; }
        public List<Question>? Questions { get; set; }
    }

}
