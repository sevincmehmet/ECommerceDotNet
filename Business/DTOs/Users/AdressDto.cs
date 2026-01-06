namespace Business.DTOs.Users
{
    public class AdressDto
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Fk
        public string Title { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsBillingAddress { get; set; }

    }
}
