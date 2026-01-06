using Business.DTOs.Users;

namespace Business.Abstract.Users
{
    public interface IAddressService
    {
        List<AdressDto> GetAllAddresses();
    }
}
