using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.UpdateRequest
{
    public class FormTemplateDTOUpdate
    {
        [MinLength(1, ErrorMessage = "FormTemplate Translations can't be less than 1")]
        public List<TranslationDTOUpdate>? Translations { get; set; }
        [MinLength(1, ErrorMessage = "FormTemplate Questions can't be less than 1")]
        public List<QuestionDTOUpdate>? Questions { get; set; }
    }
}
