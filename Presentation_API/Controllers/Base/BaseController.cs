using Business.DTOs;
using Infrastructure.Result.Abstract;
using Microsoft.AspNetCore.Mvc;
using IResult = Infrastructure.Result.Abstract.IResult;



namespace Presentation_API.Controllers.Base
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        // 1. IDataResult'ı ApiResponseDto'ya çevirir ve doğru HTTP kodunu döner.
        protected IActionResult HandleDataResult<T>(IDataResult<T> result, int successStatusCode = 200, int errorStatusCode = 404)
        {
            var apiResponse = new ApiResponseDto<T>
            {
                Success = result.Success,
                Message = result.Message,
                Data = result.Data
            };

            if (result.Success)
            {
                apiResponse.StatusCode = successStatusCode;
                return StatusCode(successStatusCode, apiResponse);
            }
            else
            {
                // Hata durumunda Data'yı object olarak içeren bir Response DTO dönüyoruz
                apiResponse.StatusCode = errorStatusCode;
                return StatusCode(errorStatusCode, new ApiResponseDto<object>
                {
                    Success = false,
                    Message = result.Message,
                    StatusCode = errorStatusCode,
                    Data = null
                });
            }
        }

        // 2. IResult'ı ApiResponseDto'ya çevirir ve doğru HTTP kodunu döner.
        protected IActionResult HandleResult(IResult result, int successStatusCode = 204, int errorStatusCode = 400)
        {
            var apiResponse = new ApiResponseDto<object>
            {
                Success = result.Success,
                Message = result.Message,
                Data = null
            };

            if (result.Success)
            {
                apiResponse.StatusCode = successStatusCode;
                // 204 No Content için gövde boş olmalıdır
                return StatusCode(successStatusCode, successStatusCode == 204 ? null : apiResponse);
            }
            else
            {
                apiResponse.StatusCode = errorStatusCode;
                return StatusCode(errorStatusCode, apiResponse);
            }
        }
    }
}