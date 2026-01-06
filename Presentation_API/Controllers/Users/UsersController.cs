using Business.Abstract.Users;
using Business.DTOs.Users;
using Infrastructure.Result.Abstract;
using Infrastructure.Security.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_API.Controllers.Base;
using System.Security.Claims;

namespace Presentation_API.Controllers.Users
{
    [Route("api/[controller]")] // api/users
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // --- BÖLÜM 1: TÜM KAYNAKLARI YÖNETEN METOTLAR (ADMIN KAPSAMI) ---

        // GET: api/users
        [HttpGet]
        [HasPermission("User.View")] // Artik Roles="Admin" yerine yetkiye bakıyoruz        
        public IActionResult Get()
        {
            IDataResult<List<UserDto>> result = _userService.GetAllUsers();
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 404);
        }

        // GET api/users/5
        [HttpGet("{id}")]
        [HasPermission("User.View")]
        public IActionResult GetUser(int id)
        {
            IDataResult<UserDto> result = _userService.GetUserById(id);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 404);
        }

        // GET api/users/5/addresses (Alt Kaynak)
        [HttpGet("{id}/addresses")]
        [HasPermission("User.View")]
        public IActionResult GetUserAddress(int id)
        {
            IDataResult<AdressDto> result = _userService.GetUserAddressById(id);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 404);
        }

        // PUT api/users/5
        [HttpPut("{id}")]
        [HasPermission("User.Edit")] // Kullanıcı düzenleme yetkisi
        public IActionResult Update(int id, UpdateUserDto dto)
        {
            IDataResult<UpdateUserDto> result = _userService.UpdateUser(id, dto);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 400);
        }

        // DELETE api/users/5
        [HttpDelete("{id}")]
        [HasPermission("User.Delete")]
        public IActionResult Delete(int id)
        {
            var result = _userService.DeleteUser(id);
            return HandleResult(result, successStatusCode: 200, errorStatusCode: 404);
        }


        // --- BÖLÜM 2: OTURUM AÇMIŞ KULLANICIYA AİT METOTLAR (ME KAPSAMI) ---

        // GET: api/users/me
        [HttpGet("me")]
        [Authorize]
        public IActionResult GetMyUser()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdString);
            IDataResult<UserDto> result = _userService.GetUserById(userId);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 404);
        }

        // GET api/users/me/address
        [HttpGet("me/address")]
        [Authorize]
        public IActionResult GetMyAddress()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdString);
            IDataResult<AdressDto> result = _userService.GetUserAddressById(userId);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 404);
        }

        // PUT api/users/me
        [HttpPut("me")]
        [Authorize]
        public IActionResult UpdateMyUser(UpdateUserDto dto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdString);
            IDataResult<UpdateUserDto> result = _userService.UpdateUser(userId, dto);
            return HandleDataResult(result, successStatusCode: 200, errorStatusCode: 400);
        }

        // DELETE api/users/me
        [HttpDelete("me")]
        [Authorize]
        public IActionResult DeleteMeUser()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized();
            }

            var userId = int.Parse(userIdString);
            var result = _userService.DeleteUser(userId);

            return HandleResult(result, successStatusCode: 200, errorStatusCode: 404);
        }
    }
}