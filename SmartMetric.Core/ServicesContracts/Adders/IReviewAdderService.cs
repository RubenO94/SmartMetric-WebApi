using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Adders
{
    public interface IReviewAdderService
    {
        Task<ApiResponse<ReviewDTOResponse?>> AddReview(ReviewDTOAddRequest? request);
    }
}
