namespace Business.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; } // Kullanıcı Adı
        public string Email { get; set; } // UNIQUE
        public bool Status { get; set; }
        public DateTime RegistrationDate { get; set; }

        public IEnumerable<AdressDto> Addresses { get; set; }
    }
}
