﻿//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using SmartMetric.Core.Exceptions;

//namespace SmartMetric.WebAPI.Controllers
//{
//    [AllowAnonymous]
//    [ApiExplorerSettings(IgnoreApi = true)]
//    public class ErrorsController : ControllerBase
//    {
//        [Route("error")]
//        public HttpStatusException Error()
//        {
//            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
//            var exception = context.Error; // Your exception
//            var code = 500; // Internal Server Error by default

//            if (exception is MyNotFoundException) code = 404; // Not Found
//            else if (exception is MyUnauthException) code = 401; // Unauthorized
//            else if (exception is MyException) code = 400; // Bad Request

//            Response.StatusCode = code; // You can use HttpStatusCode enum instead

//            return new MyErrorResponse(exception); // Your error model
//        }
//    }
//}
