using Business.DTOs.Auth;
using Infrastructure.Result.Abstract;

namespace Business.Abstract.Users
{
    public interface IUserRoleService
    {
        IDataResult<UserAddRoleDto> AddUserRole(UserAddRoleDto dto);
    }
}
