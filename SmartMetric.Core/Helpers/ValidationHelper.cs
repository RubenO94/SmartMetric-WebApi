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
    public class ValidationHelper
    {
        /// <summary>
        /// Realiza validação de modelo em um objeto usando anotações de dados.
        /// </summary>
        /// <param name="obj">O objeto a ser validado.</param>
        /// <exception cref="HttpStatusException">Lançada se a validação falhar, contendo a primeira mensagem de erro encontrada.</exception>
        internal static void ModelValidation(object obj)
        {
            //Model Validations
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            // Lança uma exceção se a validação falhar, contendo a primeira mensagem de erro encontrada.
            if (!isValid)
            {
                throw new HttpStatusException(HttpStatusCode.BadRequest,validationResults.FirstOrDefault()?.ErrorMessage!);
            }
        }
    }

}
