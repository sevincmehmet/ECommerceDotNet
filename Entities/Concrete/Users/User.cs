using Core.Entities.Abstract;
using Entities.Concrete.Products;

namespace Entities.Concrete.Users
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; } // Kullanıcı Adı
        public string Email { get; set; } // UNIQUE
        public string PasswordHash { get; set; }
        //public byte[] PasswordHash { get; set; } -> BCrypt kullandığımız için string olarak saklıyoruz.
        // BCrypt kullanıldığında bu alana gerek yok -> İleride şifreleme algoritması değişirse tutulabilir.
        //public byte[] PasswordSalt { get; set; } 
        public bool Status { get; set; }
        public DateTime RegistrationDate { get; set; }

        // Navigation Property
        public virtual List<Address> Addresses { get; set; } = [];
        public virtual List<Product> Products { get; set; }

    }
}
