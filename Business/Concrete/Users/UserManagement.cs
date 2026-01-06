using Business.Abstract.Users;
using Business.DTOs.Auth;
using Business.DTOs.Users;
using DataAccess.Abstract.Users;
using Entities.Concrete.Users;
using Infrastructure.Result.Abstract;
using Infrastructure.Result.Concrete;
using Infrastructure.Security.Abstract;
using System.Text.RegularExpressions;

namespace Business.Concrete.Users
{
    public class UserManagement(IUserRepository userRepository, ISecurityHelper securityService, IAddressRepository addressRepository, IUserRoleRepository userRoleRepository, IUserPermissionRepository userPermissionRepository, ITokenHelper tokenHelper) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly ISecurityHelper _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
        private readonly IAddressRepository _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
        private readonly IUserRoleRepository _userRoleRepository = userRoleRepository ?? throw new ArgumentNullException(nameof(userRoleRepository));
        private readonly IUserPermissionRepository _userPermissionRepository = userPermissionRepository ?? throw new ArgumentException(nameof(userPermissionRepository));
        private readonly ITokenHelper _tokenHelper = tokenHelper ?? throw new ArgumentException(nameof(tokenHelper));
        static readonly Regex emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

        // --- AUTH METOTLARI --- 
        public IDataResult<UserDto> Register(RegisterUserDto dto)
        {


            //Veriyi validate et (alanlar doğru uzunlukta mı, bu kullanıcı sistemde daha önce kayıtlı mı)

            // Entity'ni oluştur

            // Veritabanına ekle

            // Sonucu döndür

            // --- 1. Girdi Kontrolleri (Validation) ---
            if (string.IsNullOrWhiteSpace(dto.Username)) { return new ErrorDataResult<UserDto>("Kullanıcı adı boş olamaz."); }
            if (string.IsNullOrWhiteSpace(dto.Password)) { return new ErrorDataResult<UserDto>("Şifre boş olamaz."); }
            if (string.IsNullOrWhiteSpace(dto.Email)) { return new ErrorDataResult<UserDto>("E-posta adresi boş olamaz."); }

            // Uzunluk Kontrolleri
            if (dto.FirstName.Length < 2) { return new ErrorDataResult<UserDto>("Ad en az 2 karakter olmalıdır."); }
            if (dto.LastName.Length < 2) { return new ErrorDataResult<UserDto>("Soyad en az 2 karakter olmalıdır."); }
            if (dto.Username.Length < 2) { return new ErrorDataResult<UserDto>("Kullanıcı adı en az 2 karakter olmalıdır."); }
            if (dto.Password.Length < 8) { return new ErrorDataResult<UserDto>("Şifreniz en az 8 karakter olmalıdır."); }

            // E-posta Format Kontrolü
            if (!emailRegex.IsMatch(dto.Email))
            {
                return new ErrorDataResult<UserDto>("Geçerli bir e-posta adresi giriniz.");
            }

            // Şifre Karmaşıklık Kontrolü: En az 8 karakter, büyük harf, küçük harf ve rakam.
            if (!Regex.IsMatch(dto.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
            {
                return new ErrorDataResult<UserDto>("Şifre, en az 8 karakter uzunluğunda olmalı ve en az bir büyük harf, bir küçük harf ve bir rakam içermelidir.");
            }

            // --- 2. İş Kuralları: Tekrarlılık Kontrolü (Repository Kullanımı) ---

            // Kullanıcı Adı kontrolü: Eşleşen kullanıcı var mı?
            var existingUserByUsername = _userRepository.Get(u => u.Username == dto.Username);
            if (existingUserByUsername != null)
            {
                return new ErrorDataResult<UserDto>($"'{dto.Username}' kullanıcı adı zaten kullanımda.");
            }

            // E-posta kontrolü: Eşleşen e-posta var mı?
            var existingUserByEmail = _userRepository.Get(u => u.Email == dto.Email);
            if (existingUserByEmail != null)
            {
                return new ErrorDataResult<UserDto>($"'{dto.Email}' e-posta adresi zaten kullanımda.");
            }

            // --- 3. Veri İşleme ve Güvenlik (Hashing) ---

            // BCrypt ile Şifre Hashleme
            string hashedPassword = _securityService.HashPassword(dto.Password);


            // Yeni Kullanıcı Nesnesi Oluşturma (Entities.Concrete.Users.User)
            var newUser = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword, // BCrypt ile Şifre Hashleme
                RegistrationDate = DateTime.UtcNow,
                Status = true,
            };

            // --- 4. Veri Kaydı (Persistence) ---
            try
            {
                _userRepository.Add(newUser);
                var dtoUser = new UserDto
                {
                    Id = newUser.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Username = newUser.Username,
                    Email = newUser.Email,
                    RegistrationDate = newUser.RegistrationDate,
                    Status = newUser.Status,
                    Addresses = new List<AdressDto>() // Yeni kullanıcı için adresler boş olabilir
                };

                return new SuccessDataResult<UserDto>(dtoUser, "Kullanıcı başırılı bir şekilde kaydedildi");

            }
            catch (Exception ex)
            {
                // Veritabanı veya altyapı hatalarını yakalama ve genel hata fırlatma
                throw new Exception("Kullanıcı kayıt işlemi sırasında bir hata oluştu. Lütfen sistem yöneticisi ile iletişime geçin.", ex);
            }
        }

        public IDataResult<TokenDto> Login(LoginUserDto dto)
        {
            // 1. Girdi Kontrolleri
            if (string.IsNullOrWhiteSpace(dto.Username)) { throw new ArgumentException("Kullanıcı adı boş olamaz."); }
            if (string.IsNullOrWhiteSpace(dto.Password)) { throw new ArgumentException("Şifre boş olamaz."); }

            // 2. Kullanıcıyı ve Rolünü/Yetkilerini tek seferde (veya sırayla) al
            var user = _userRepository.Get(u => u.Username == dto.Username || u.Email == dto.Username);
            if (user == null) return new ErrorDataResult<TokenDto>(null, "Böyle bir kullanıcı bulunmamaktadır.");

            // 3. Şifre doğrulama
            bool isPasswordValid = _securityService.VerifyPassword(dto.Password, user.PasswordHash);
            if (!isPasswordValid) return new ErrorDataResult<TokenDto>(null, "Kullanıcı adı veya şifre yanlış");
            if (!user.Status) return new ErrorDataResult<TokenDto>(null, "Kullanıcı hesabı aktif değildir.");

            // --- YENİ YETKİ TOPLAMA MANTIĞI ---

            // 4. Kullanıcının Roller'ini Çek
            var userRoles = _userRoleRepository.GetList(ur => ur.UserId == user.Id).Select(ur => ur.Role.Name).ToList();

            // 5. Yetkileri (Permissions) Hesapla (Roller + Özel Yetkiler)
            // Not: Bu karmaşık sorgu için Repository'de özel bir metod yazman daha temiz olur.
            // Şimdilik mantık olarak buraya ekliyorum:
            //var permissions = GetUserTotalPermissions(user.Id);
            var permissions = GetUserTotalPermissions(user.Id);

            // 6. Token Oluştur (Hata veren satır burasıydı, şimdi 3 parametre gidiyor)
            var accessToken = _tokenHelper.CreateToken(user, userRoles, permissions);

            // 7. TokenDto Hazırla
            var tokenDto = new TokenDto
            {
                AccessToken = accessToken.Token,
                Expiration = accessToken.Expiration,
                UserId = user.Id,
                Username = user.Username
            };

            return new SuccessDataResult<TokenDto>(tokenDto, "Giriş başarılı.");
        }

        public List<string> GetUserTotalPermissions(int userId)
        {
            // 1. Rollerden gelen yetkiler (Burada Include kullanarak Role ve RolePermissions'ı çekmeyi unutma yoksa null döner)
            var rolePermissions = _userRoleRepository.GetList(ur => ur.UserId == userId)
                .SelectMany(ur => ur.Role.RolePermissions)
                .Select(rp => rp.Permission.Name);

            // 2. Kullanıcıya özel tanımlanmış yetkiler (IsDenied olmayanlar)
            var directPermissions = _userPermissionRepository.GetList(up => up.UserId == userId && !up.IsDenied)
                .Select(up => up.Permission.Name);

            // 3. Engellenmiş (IsDenied) yetkiler
            var deniedPermissions = _userPermissionRepository.GetList(up => up.UserId == userId && up.IsDenied)
                .Select(up => up.Permission.Name);

            return rolePermissions.Union(directPermissions).Except(deniedPermissions).Distinct().ToList();
        }

        // --- KULLANICI YÖNETİM METOTLARI/ CRUD İŞLEMLERİ ---
        public IDataResult<List<UserDto>> GetAllUsers()
        {
            if (_userRepository.GetList() == null || !_userRepository.GetList().Any())
            {
                return new ErrorDataResult<List<UserDto>>("Bu id değerine sahip bir kullanıcı bulunamadı.");
            }
            //TODO: İleride yetki eklendiği zaman yetki admin ise status false olanları göster gibi bir şart da eklenebilir. 
            var userList = _userRepository.GetList();


            // Eşleme (Mapping) İşlemi
            var dtos = userList.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Email = user.Email,
                RegistrationDate = user.RegistrationDate,
                Status = user.Status,
                Addresses = user.Addresses.Select(a => new AdressDto // Adres DTO'ya dönüşüm
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    City = a.City,
                    Country = a.Country,
                    FullAddress = a.FullAddress,
                    Title = a.Title,
                    IsBillingAddress = a.IsBillingAddress
                }).ToList(),
            }).ToList();

            return new SuccessDataResult<List<UserDto>>(dtos, "Kullanıcılar başarıyla getirildi.");
        }

        public IDataResult<AdressDto> GetUserAddressById(int id)
        {
            var addressEntity = _addressRepository.Get(a => a.UserId == id);
            if (addressEntity == null)
                return new ErrorDataResult<AdressDto>(null, "Bu kullanıcıya ait adres bilgisi bulunamadı.");

            var adressDto = new AdressDto
            {
                UserId = addressEntity.UserId,
                Title = addressEntity.Title,
                FullAddress = addressEntity.FullAddress,
                City = addressEntity.City,
                Country = addressEntity.Country,
                IsBillingAddress = addressEntity.IsBillingAddress
            };
            return new SuccessDataResult<AdressDto>(adressDto, "Kullanıcı adres bilgisi başarıyla getirildi.");
        }

        public IDataResult<UserDto> GetUserById(int id)
        {
            var userEntity = _userRepository.Get(u => u.Id == id);

            if (userEntity == null)
                return new ErrorDataResult<UserDto>("Kullanıcı bulunamadı.");

            var userDto = new UserDto
            {
                Id = userEntity.Id,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Username = userEntity.Username,
                Email = userEntity.Email,
                RegistrationDate = userEntity.RegistrationDate,
                Status = userEntity.Status,
                Addresses = userEntity.Addresses.Select(a => new AdressDto
                {
                    Id = a.Id,
                    UserId = a.UserId,
                    City = a.City,
                    Country = a.Country,
                    FullAddress = a.FullAddress,
                    Title = a.Title,
                    IsBillingAddress = a.IsBillingAddress
                }).ToList()
            };

            return new SuccessDataResult<UserDto>(userDto, "Kullanıcı başarıyla listelendi.");
        }

        public IResult DeleteUser(int id)
        {
            var user = _userRepository.Get(u => u.Id == id);
            if (user == null)
                return new ErrorResult("Kullanıcı bulunamadı.");
            _userRepository.Delete(user);
            return new SuccessResult("Kullanıcı başarıyla silindi");
        }

        public IDataResult<UpdateUserDto> UpdateUser(int id, UpdateUserDto dto)
        {
            var userEntity = _userRepository.Get(u => u.Id == id);
            if (userEntity == null)
                return new ErrorDataResult<UpdateUserDto>("Kullanıcı bulunamadı");

            if (dto.FirstName == null || dto.FirstName.Trim() == "")
            {
                dto.FirstName = userEntity.FirstName;
            }
            else if (dto.FirstName.Length < 2)
            {
                return new ErrorDataResult<UpdateUserDto>("Ad en az 2 karakter olmalıdır.");
            }

            if (dto.LastName == null || dto.LastName.Trim() == "")
            {
                dto.LastName = userEntity.LastName;
            }
            else if (dto.LastName.Length < 2)
            {
                return new ErrorDataResult<UpdateUserDto>("Ad en az 2 karakter olmalıdır.");
            }

            if (dto.Email == null || dto.Email.Trim() == "")
            {
                dto.Email = userEntity.Email;
            }
            else if (!emailRegex.IsMatch(dto.Email))
            {
                return new ErrorDataResult<UpdateUserDto>("Geçerli bir e-posta adresi giriniz.");
            }

            if (dto.Username == null || dto.Username.Trim() == "")
            {
                dto.Username = userEntity.Username;
            }
            else if (dto.Username.Length < 2)
            {
                return new ErrorDataResult<UpdateUserDto>("Kullanıcı adı en az 2 karakter olmalıdır.");
            }


            try
            {
                userEntity.FirstName = dto.FirstName;
                userEntity.LastName = dto.LastName;
                userEntity.Email = dto.Email;
                userEntity.Username = dto.Username;

                _userRepository.Update(userEntity);


                return new SuccessDataResult<UpdateUserDto>(dto, "Kullanıcı başarıyla güncellendi.");
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<UpdateUserDto>("Kullanıcı güncellenirken bir hata oluştu");
            }


        }
    }

}