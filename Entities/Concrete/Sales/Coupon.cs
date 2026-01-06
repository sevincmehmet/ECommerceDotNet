using Core.Entities.Abstract;

namespace Entities.Concrete.Sales
{
    public class Coupon : IEntity
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
