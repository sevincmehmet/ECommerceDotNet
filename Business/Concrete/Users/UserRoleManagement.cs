using Business.Abstract.Users;
using Business.DTOs.Auth;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;
using Infrastructure.Result.Abstract;
using Infrastructure.Result.Concrete;

namespace Business.Concrete.Users
{
    public class UserRoleManagement : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserRoleManagement(IUserRoleRepository userRoleRepository, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        public IDataResult<UserAddRoleDto> AddUserRole(UserAddRoleDto dto)
        {
            // Veritabanından kontrolleri yapıyoruz
            var entityUser = _userRepository.Get(u => u.Id == dto.UserId);
            var entityRole = _roleRepository.Get(r => r.Id == dto.RoleId);

            // DÜZELTME: Eğer null ise (yani YOKSA) hata döndür
            if (entityUser == null)
            {
                return new ErrorDataResult<UserAddRoleDto>("Kullanıcı bulunamadı");
            }

            if (entityRole == null)
            {
                return new ErrorDataResult<UserAddRoleDto>("Rol bulunamadı");
            }

            // Mükerrer kayıt kontrolü (Opsiyonel ama önerilir)
            // Kullanıcı zaten bu role sahip mi diye bakabilirsin.

            _userRoleRepository.Add(new UserRole
            {
                UserId = dto.UserId,
                RoleId = dto.RoleId
            });

            return new SuccessDataResult<UserAddRoleDto>(dto, "Rol başarıyla eklendi");
        }
    }
}