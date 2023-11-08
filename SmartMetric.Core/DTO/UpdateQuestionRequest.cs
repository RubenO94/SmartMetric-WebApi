﻿using SmartMetric.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO
{
    public class UpdateQuestionRequest
    {
        public Guid QuestionId { get; set; }
        public Guid FormTemplateId { get; set; }
        public Guid? RatingTemplateId { get; set; }
        public Guid? SingleChoiceTemplateId { get; set; }
        public bool IsRequired { get; set; }
        public ResponseType ResponseType { get; set; }
        public ICollection<QuestionTranslationDTO>? Translations { get; set; }
    }
}
