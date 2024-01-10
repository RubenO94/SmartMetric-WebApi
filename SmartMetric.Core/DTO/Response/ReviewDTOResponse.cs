using SmartMetric.Core.Domain.Entities;

namespace SmartMetric.Core.DTO.Response
{
    public class ReviewDTOResponse
    {
        public Guid ReviewId { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ReviewType { get; set; }
        public string? SubjectType { get; set; }
        public string? ReviewStatus { get; set; }
        public List<TranslationDTOResponse>? Translations { get; set; }
        public List<QuestionDTOResponse>? Questions { get; set; }
        public List<DepartmentDTOResponse>? Departments { get; set; }
        public List<EmployeeDTOResponse>? Employees { get; set; }


        /// <summary>
        /// Compara os dados atuais deste objeto com o parâmetro.
        /// </summary>
        /// <param name="obj">O objeto parâmetro a ser comparado.</param>
        /// <returns>Retorna True ou False, indicando se todos os detalhes da tradução correspondem ao objeto especificado no parâmetro.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(ReviewDTOResponse)) return false;

            ReviewDTOResponse review = (ReviewDTOResponse)obj;
            return ReviewId == review.ReviewId && CreatedDate == review.CreatedDate && StartDate == review.StartDate && CreatedByUserId == review.CreatedByUserId && Translations == review.Translations && Questions == review.Questions && ReviewStatus == review.ReviewStatus && ReviewType == review.ReviewType && review.SubjectType == review.SubjectType;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"ReviewId: {ReviewId}\nCreatedDate: {CreatedDate?.ToString("dd-MM-yyyy")}\nCreatedByuserId: {CreatedByUserId}\nTranslations count: {Translations?.Count()}\nQuestions count: {Questions?.Count()}";
        }
    }

    public static class ReviewExtensions
    {
        public static ReviewDTOResponse ToReviewDTOResponse(this Review review)
        {
            return new ReviewDTOResponse()
            {
                ReviewId = review.ReviewId,
                CreatedByUserId = review.CreatedByUserId,
                CreatedDate = review.CreatedDate,
                StartDate = review.StartDate,
                EndDate = review.EndDate,
                ReviewStatus = review.ReviewStatus,
                ReviewType = review.ReviewType,
                Questions = review.Questions?.Select(temp => temp.ToQuestionDTOResponse()).ToList() ?? null,
                Translations = review.Translations?.Select(temp => temp.ToTranslationDTOResponse()).ToList() ?? null,
                Departments = review.Departments?.Select(temp =>
                {
                    return new DepartmentDTOResponse()
                    {
                        DepartmentId = temp.DepartmentId,
                        DepartmentDescription = temp.Department!.Descricao
                    };
                }).ToList() ?? null,
                Employees = review.Employees?.Select(temp => temp.Employee!.ToEmployeeDTOResponse()).ToList(),
            };
        }
    }
}
