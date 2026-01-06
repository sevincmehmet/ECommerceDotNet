using Core.Entities.Abstract;

namespace Entities.Concrete.Users
{
    public class Address : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Fk
        public string Title { get; set; }
        public string FullAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsBillingAddress { get; set; }

        // Navigation Property
        public virtual User User { get; set; }
    }
}
