using SmartMetric.Core.DTO.Response;
using SmartMetric.Core.DTO.UpdateRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.ServicesContracts.FormTemplate
{
    public interface IFormTemplateUpdaterService
    {
        Task<FormTemplateDTOResponse> UpdateFormTemplate(Guid? formtemplateId, FormTemplateDTOUpdate? formTemplateUpdate);
    }
}
