using Business.Abstract.Users;
using Business.DTOs.Users;
using DataAccess.Abstract.Users;
using DataAccess.Concrete.EntityFramework.Users;

namespace Business.Concrete.Users
{
    public class AddressManagement : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressManagement(EfAddressRepository efAddressRepository)
        {
            _addressRepository = efAddressRepository;
        }

        public List<AdressDto> GetAllAddresses()
        {
            if (_addressRepository == null)
                throw new ArgumentNullException(nameof(_addressRepository));

            if (_addressRepository.GetList() == null || !_addressRepository.GetList().Any())
                throw new InvalidOperationException("No addresses found.");

            var AddressList = _addressRepository.GetList();
            List<AdressDto> AddressDtos = new List<AdressDto>();

            foreach (var Adress in AddressList)
            {
                // Additional processing can be done here if needed
                AddressDtos.Add(new AdressDto
                {
                    Id = Adress.Id,
                    UserId = Adress.UserId,
                    Title = Adress.Title,
                    FullAddress = Adress.FullAddress,
                    City = Adress.City,
                    Country = Adress.Country,
                    IsBillingAddress = Adress.IsBillingAddress
                });
            }

            return AddressDtos;
        }
    }
}
