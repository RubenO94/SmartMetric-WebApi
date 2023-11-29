using SmartMetric.Core.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Deleters
{
    public interface IReviewDeleterService
    {
        Task<ApiResponse<bool>> DeleteReviewById(Guid? reviewId);
    }
}
