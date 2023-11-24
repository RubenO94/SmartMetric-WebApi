using SmartMetric.Core.Domain.Entities;
using SmartMetric.Core.Enums;

namespace SmartMetric.Core.DTO.AddRequest
{
    public class ReviewDTOAddRequest
    {
        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public ReviewType? ReviewType { get; set; }
        public SubjectType? SubjectType { get; set; }
        public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.NotStarted;
        public List<ReviewTranslationDTOAddRequest>? Translations { get; set; }
        public List<QuestionDTOAddRequest>? Questions { get; set; }
        public List<int>? ReviewEmployeeIds { get; set; }
        public List<int>? ReviewDepartmentsIds { get; set; }

        public Review ToReview()
        {
            return new Review()
            {
                CreatedByUserId = CreatedByUserId,
                CreatedDate = CreatedDate,
                StartDate = StartDate,
                EndDate = EndDate,
                ReviewStatus = ReviewStatus.ToString(),
                ReviewType = ReviewType.ToString(),
                SubjectType = SubjectType.ToString(),
                Translations = Translations?.Select(temp => temp.ToReviewTranslation()).ToList() ?? null

            };
        }
    }
}
