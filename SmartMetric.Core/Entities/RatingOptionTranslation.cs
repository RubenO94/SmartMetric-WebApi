﻿using SmartMetric.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMetric.Core.Entities
{
    public class RatingOptionTranslation
    {
        public Guid RatingOptionTranslationId { get; set; }
        public Guid RatingTemplateId { get; set; }
        public string? Language { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }

        [ForeignKey(nameof(RatingTemplateId))]
        public RatingTemplate? RatingTemplate { get; set; }
    }
}
