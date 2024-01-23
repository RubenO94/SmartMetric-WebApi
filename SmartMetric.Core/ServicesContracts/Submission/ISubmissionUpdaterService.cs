using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.Submission
{
    public interface ISubmissionUpdaterService
    {
        /// <summary>
        /// Adiciona data da submissão e associa uma lista de respostas à submissão
        /// </summary>
        /// <param name="submissionId"></param>
        /// <param name="submission"></param>
        /// <returns></returns>
        Task<ApiResponse<bool>> UpdateSubmission(Guid submissionId, SubmissionFormDTOUpdate submission);
    }
}
