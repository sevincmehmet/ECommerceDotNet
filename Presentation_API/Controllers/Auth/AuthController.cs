using Business.Abstract.Users;
using Business.Concrete.Users;
using Business.DTOs;
using Business.DTOs.Auth;
using Business.DTOs.Users;
using DataAccess.Concrete.EntityFramework.Users;
using Infrastructure.Result.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_API.Controllers.Base;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Presentation_API.Controllers.Auth
{
    //  POST api/auth/login
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        // DI kullanılarak veri alınıyor
        IUserService _userService;
        IUserRoleService _userRoleService;

        public AuthController(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }
        // POST api/auth/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(ApiResponseDto<TokenDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 401)]
        public IActionResult Login([FromBody] LoginUserDto user)
        {
            IDataResult<TokenDto> result = _userService.Login(user);
            return HandleDataResult<TokenDto>(result, successStatusCode: 200, errorStatusCode: 401);
        }

        // POST api/users/register
        [HttpPost("register")]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 201)] // Başarılı kayıt, 201 Created
        [ProducesResponseType(typeof(ApiResponseDto<object>), 400)] // Hatalı veri/iş kuralı
        public IActionResult AddUser([FromBody] RegisterUserDto user)
        {
            // Business katmanınızın IResult döndüğünü varsayıyoruz.
            IDataResult<UserDto> result = _userService.Register(user);

            // HandleResult'ı kullanarak 201 Created veya 400 Bad Request dönüyoruz.
            // 201 Created dönüyoruz, gövdeyi döndürmek için:
            return HandleDataResult(result, successStatusCode: 201, errorStatusCode: 400);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-role")]
        [ProducesResponseType(typeof(ApiResponseDto<UserAddRoleDto>), 200)]
        [ProducesResponseType(typeof(ApiResponseDto<object>), 400)]
        public IActionResult AddUserRole([FromBody] UserAddRoleDto userRoleAddDto)
        {
            IDataResult<UserAddRoleDto> result = _userRoleService.AddUserRole(userRoleAddDto);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 400);
        }


    }
}
