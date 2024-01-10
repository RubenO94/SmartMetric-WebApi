using SmartMetric.Core.DTO.AddRequest;
using SmartMetric.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMetric.Core.Helpers
{
    /// <summary>
    /// Classe utilitária para realizar validações de modelo em objetos usando anotações de dados.
    /// </summary>
    /// 

    public class ValidationHelper
    {
        internal static void ModelValidation(object obj)
        {
            //Model validations
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }
    }

}
