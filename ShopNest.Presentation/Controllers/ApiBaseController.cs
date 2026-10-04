using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ShopNest.Shared.CommonResponses;
using Error = ShopNest.Shared.CommonResponses.Error;

namespace ShopNest.Presentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    //For applying the validation of each request and check if the result is Ok or Fail or Fail<Errors>
    public class ApiBaseController : ControllerBase
    {
        //1-Handle Result Without Value
        //A-If Result Success => 204 No Content 
        protected ActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            else
                return HandleProblem(result.errors);

        }
        //B-If Result Fail => return problem details => status code , ErrorDetails




        //2- Handle Result With Value
        //A-If Result Success => 200 ok => JsonValue  
        protected ActionResult<TValue> HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return HandleProblem(result.errors);
        }
        //B-If Result Fail => Problem details => StatusCode , ErrorDetails 

        #region Helper Methods
        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            //if No Error 
            if (errors.Count == 0)
                return Problem(statusCode: StatusCodes.Status500InternalServerError, title: "An Error Ocurred");
            //if multiple validation errors => handle as a valiation problem 
            if (errors.All(t => t.Status == ErrorType.Validation))
                return HandleValidationErrors(errors);

            //if single Error
            return HandleSingleError(errors[0]);

        }
        private ActionResult HandleValidationErrors(IReadOnlyList<Error> Errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var Error in Errors)
            {
                modelState.AddModelError(Error.Code, Error.Description);
            }
            return ValidationProblem(modelState);
        }
        private ObjectResult HandleSingleError(Error error)
        {

            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.Status.ToString(),
                statusCode: MapErrorTypeIntoStatusCode(error.Status)
                 );
        }
        private static int MapErrorTypeIntoStatusCode(ErrorType errorType)
        //Strategy Design Pattern
        {
            return errorType switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                ErrorType.unAuthorized => StatusCodes.Status401Unauthorized,
                ErrorType.InvalidCredentials => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
        }
        #endregion


    }
}
