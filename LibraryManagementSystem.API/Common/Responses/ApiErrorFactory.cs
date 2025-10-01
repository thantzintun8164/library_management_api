using LibraryManagementSystem.Application.Common.Exceptions;

namespace LibraryManagementSystem.API.Common.Responses
{
    public static class ApiErrorFactory
    {
        public static ApiErrorResponse Create(Exception exception)
        {
            return exception switch
            {
                NotFoundException => new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status404NotFound,
                    Title = "Not Found",
                    Detail = exception.Message
                },

                ForbiddenException => new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status403Forbidden,
                    Title = "Forbidden",
                    Detail = exception.Message
                },

                UnAuthorizedException => new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Title = "Unauthorized",
                    Detail = exception.Message
                },

                BadRequestException => new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = exception.Message
                },

                CustomValidationException vx => new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Title = "Validation Failed",
                    Detail = exception.Message,
                    Errors = vx.Errors
                },
                _ => new ApiErrorResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred."
                }
            };
        }
    }
}