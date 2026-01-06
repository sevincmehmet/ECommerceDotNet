using Business.DTOs.Auth;
using Business.DTOs.Users;
using Infrastructure.Result.Abstract;

namespace Business.Abstract.Users
{
    public interface IUserService
    {
        /// <summary>
        /// Yeni bir kullanıcı kaydı oluşturur ve iş kurallarını uygular.
        /// </summary>
        /// <param name="username">Kullanıcı Adı</param>
        /// <param name="password">Şifre</param>
        /// <param name="email">E-posta Adresi</param>
        IDataResult<UserDto> Register(RegisterUserDto dto);
        /// <summary>
        /// Kullanıcı adı ve şifre üzerinden kullanıcının şifresini doğrular
        /// </summary>
        /// <param name="username">Kullanıcı Adı</param>
        /// <param name="password">Şifre</param>
        /// <exception cref="ArgumentException"></exception>
        IDataResult<TokenDto> Login(LoginUserDto dto);
        /// <summary>
        /// Tüm kullanıcıları getirir.
        /// </summary>
        /// <returns></returns>
        IDataResult<List<UserDto>> GetAllUsers();
        /// <summary>
        /// Id Değerine göre kullanıcıyı getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDataResult<UserDto> GetUserById(int id);
        /// <summary>
        /// Id Değerine göre kullanıcının kayıtlı tüm adres bilgilerini getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDataResult<AdressDto> GetUserAddressById(int id);
        /// <summary>
        /// Id Değerine göre kullanıcıyı siler.
        /// </summary>
        /// <param name="id"></param>
        IResult DeleteUser(int id);
        /// <summary>
        /// Kullanıcı bilgilerini günceller.
        /// </summary>
        /// <param name="dto"></param>
        IDataResult<UpdateUserDto> UpdateUser(int id, UpdateUserDto dto);
    }
}
