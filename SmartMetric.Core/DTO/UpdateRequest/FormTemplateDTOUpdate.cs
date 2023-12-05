using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class FormTemplateDTOUpdate
    {
        public List<TranslationDTOUpdate>? Translations { get; set; }
        public List<QuestionDTOUpdate>? Questions { get; set; }
    }
}
