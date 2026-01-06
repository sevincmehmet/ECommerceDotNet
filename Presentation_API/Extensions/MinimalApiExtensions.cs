using Business.DTOs;
using Infrastructure.Result.Abstract;
using IResult = Microsoft.AspNetCore.Http.IResult; // Minimal API IResult için kritik!

namespace Presentation_API.Helpers
{
    public static class MinimalResultHandler
    {
        public static IResult HandleDataResult<T>(IDataResult<T> result, int successStatusCode = 200, int errorStatusCode = 404)
        {
            var apiResponse = new ApiResponseDto<T>
            {
                Success = result.Success,
                Message = result.Message,
                Data = result.Data,
                StatusCode = result.Success ? successStatusCode : errorStatusCode
            };

            if (result.Success)
            {
                return Results.Json(apiResponse, statusCode: successStatusCode);
            }

            return Results.Json(new ApiResponseDto<object>
            {
                Success = false,
                Message = result.Message,
                StatusCode = errorStatusCode,
                Data = null
            }, statusCode: errorStatusCode);
        }

        public static IResult HandleResult(Infrastructure.Result.Abstract.IResult result, int successStatusCode = 200, int errorStatusCode = 400)
        {
            var apiResponse = new ApiResponseDto<object>
            {
                Success = result.Success,
                Message = result.Message,
                Data = null,
                StatusCode = result.Success ? successStatusCode : errorStatusCode
            };

            if (result.Success)
            {
                // 204 No Content ise body gönderme, değilse apiResponse'u gönder
                return successStatusCode == 204
                    ? Results.NoContent()
                    : Results.Json(apiResponse, statusCode: successStatusCode);
            }

            return Results.Json(apiResponse, statusCode: errorStatusCode);
        }
    }
}