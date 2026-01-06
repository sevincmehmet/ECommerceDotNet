using Core.Entities.Abstract;
using Entities.Concrete.Users;

namespace Entities.Concrete.Products
{
    public class Review : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // FK 
        public int UserId { get; set; } // Fk
        public short Rating { get; set; } // 1-5 arası
        public string CommentText { get; set; }
        public DateTime ReviewDate { get; set; }

        // Navigation Properties
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
