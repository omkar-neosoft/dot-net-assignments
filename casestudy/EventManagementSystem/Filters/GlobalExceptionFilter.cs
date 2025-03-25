using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EventManagementSystem.Filters {
    public class GlobalExceptionFilter : IExceptionFilter {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) {
            _logger = logger;
        }

        public void OnException(ExceptionContext context) {
            _logger.LogError(context.Exception, "Unhandled Exception Occurred");

            //var result = new ViewResult { ViewName = "Error" };
            //context.Result = result;
            //context.ExceptionHandled = true;

            var exception = context.Exception;
            var response = context.HttpContext.Response;
            string errorMessage = "An unexpected error occurred. Please try again later."; // Default error message

            if (exception is UnauthorizedAccessException) {
                errorMessage = "You are not authorized to access this page.";
                response.StatusCode = (int)HttpStatusCode.Forbidden;
            } else if (exception is NullReferenceException) {
                errorMessage = "Oops! Something went wrong. Some required data is missing.";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            } else if (exception is InvalidOperationException) {
                errorMessage = "The requested operation is not valid.";
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            } else {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            // Redirect to Error View with Dynamic Message
            context.Result = new ViewResult {
                ViewName = "Error",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary(
                    new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), context.ModelState) {
                    ["ErrorMessage"] = errorMessage
                }
            };

            context.ExceptionHandled = true;
        }
    }
}
