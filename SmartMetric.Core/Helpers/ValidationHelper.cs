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

    //public class ValidationHelper
    //{
    //    /// <summary>
    //    /// Realiza validação de modelo em um objeto usando anotações de dados.
    //    /// </summary>
    //    /// <param name="obj">O objeto a ser validado.</param>
    //    /// <exception cref="HttpStatusException">Lançada se a validação falhar, contendo a primeira mensagem de erro encontrada.</exception>
    //    internal static void ModelValidation(object obj)
    //    {
    //        //Model Validations
    //        ValidationContext validationContext = new ValidationContext(obj);
    //        List<ValidationResult> validationResults = new List<ValidationResult>();
    //        bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

    //        IDictionary<string, string> failures = validationResults
    //                .ToDictionary(
    //                    validationResult => validationResult.MemberNames.FirstOrDefault() ?? string.Empty,
    //                    validationResult => validationResult.ErrorMessage!);

    //        //If Model is of type RatingOption
    //        if (obj is RatingOptionDTOAddRequest ratingOptionDTOAddRequest && ratingOptionDTOAddRequest.Translations != null)
    //        {
    //            foreach (var item in ratingOptionDTOAddRequest.Translations)
    //            {
    //                if (item.Language == null || item.Description == null || item.Description.Length < 10)
    //                {
    //                    throw new Exceptions.ValidationException(failures);
    //                }
    //            }
    //        }

    //        // Lança uma exceção se a validação falhar, contendo as mensagens de erro encontradas.
    //        if (!isValid)
    //        {
    //            throw new Exceptions.ValidationException(failures);
    //        }
    //    }
    //}

}
