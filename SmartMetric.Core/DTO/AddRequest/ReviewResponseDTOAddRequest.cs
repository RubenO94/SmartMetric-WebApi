using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.DTO.AddRequest
{
    public class ReviewResponseDTOAddRequest
    {
        public Guid QuestionId { get; set; }
        public Guid SubmissionId { get; set; }
        public Guid? SingleChoiceOptionId { get; set; }
        public string? TextResponse { get; set; }
        public int? RatingValue { get; set; }
    }

    //TODO: Adicionar metodo para conversão do objecto Request em objeto Entity
}
