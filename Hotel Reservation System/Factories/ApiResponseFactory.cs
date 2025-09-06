using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.API.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState.Where(error => error.Value.Errors.Any())
                                           .Select(e => new ValidationErorr
                                           {
                                               Field = e.Key,
                                               Errors = e.Value.Errors.Select(err => err.ErrorMessage)

                                           });
            var response = new ValidationErorrResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validation errors occurred.",
                Errors = errors
            };
            return new BadRequestObjectResult(response);
        }
    }
}
