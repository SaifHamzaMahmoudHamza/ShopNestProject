using Microsoft.AspNetCore.Mvc;

namespace ShopNest.API.Factories
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationResponse(ActionContext actionContext)
        {
            var error = actionContext.ModelState.
                 Where(x => x.Value?.Errors.Count > 0).
                 ToDictionary(X => X.Key, X => X.Value?.Errors.
                 Select(s => s.ErrorMessage)).
                 ToArray();
            var Problem = new ProblemDetails()
            {
                Title = "Validation Errors",
                Detail = "One or More Validation Errors Occured ",
                Status = StatusCodes.Status400BadRequest,
                Extensions =
                        {
                            {"Errors", error}
                        }
            };
            return new BadRequestObjectResult(Problem);
        }
    }
}
