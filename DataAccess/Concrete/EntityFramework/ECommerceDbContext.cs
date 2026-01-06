using Entities.Concrete.Orders;
using Entities.Concrete.Products;
using Entities.Concrete.Sales;
using Entities.Concrete.ShoppingCart;
using Entities.Concrete.Users;
using Microsoft.EntityFrameworkCore;


namespace DataAccess.Concrete.EntityFramework
{
    public class ECommerceDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();

            // Burada kendi veritabanı bağlantı dizenizi (Connection String) kullanmalısınız.
            // Örnek olarak SQL Server ve yerel sunucuyu kullandık:
            optionsBuilder.UseSqlServer(@"Server=2488-1NB\SQLEXPRESS;Database=ECommerceDb;Trusted_Connection=true;TrustServerCertificate=True");
        }

        // * --- Users --- *
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserPermission> UserPermission { get; set; }

        // * --- Products --- *
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Review> Reviews { get; set; }

        // * --- Orders --- *
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

        // * --- Sales --- *
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<PaymentTransaction> PaymentTransactions { get; set; }

        // * --- Shopping Carts --- *
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Entities.Concrete.ShoppingCart.ShoppingCart> ShoppingCarts { get; set; }

        // ECommerceDbContext sınıfının içine ekle

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ... (Diğer tüm HasIndex ve HasPrecision tanımlamaları kalacak)

            // 1. User - Address İlişkisi Kısıtlama
            modelBuilder.Entity<Address>()
                .HasOne(a => a.User)
                .WithMany(u => u.Addresses) // User sınıfına eklediğimiz ICollection<Address>'ı kullanır
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict); // CASCADE ENGELENDİ

            // 2. Order - User İlişkisi Kısıtlama
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany() // User sınıfında List<Order> tanımlanmamışsa
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict); // CASCADE ENGELENDİ

            // 3. Order - Address (ShippingAddressId) İlişkisi Kısıtlama
            // Eğer Order sınıfında ShippingAddressId adında bir FK varsa, 
            // bunu Address tablosundaki bir adrese bağlar.
            modelBuilder.Entity<Order>()
                .HasOne<Address>() // Navigation Property adı Order sınıfında yoksa boş bırakılabilir veya belirtilebilir
                .WithMany()
                .HasForeignKey(o => o.ShippingAddressId)
                .OnDelete(DeleteBehavior.Restrict); // CASCADE ENGELENDİ

            // RolePermission tablosu için hem RoleId hem de PermissionId'yi birlikte anahtar yapıyoruz
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            // UserPermission tablosu için hem UserId hem de PermissionId'yi birlikte anahtar yapıyoruz
            modelBuilder.Entity<UserPermission>()
                .HasKey(up => new { up.UserId, up.PermissionId });

            // Product ve User arasındaki ilişkiyi düzenle
            modelBuilder.Entity<Product>()
                .HasOne(p => p.CreatedByUser) // Ürünün bir sahibi var
                .WithMany(u => u.Products)    // Kullanıcının birçok ürünü olabilir
                .HasForeignKey(p => p.CreatedByUserId)
                .OnDelete(DeleteBehavior.NoAction); // KRİTİK SATIR: Otomatik silmeyi kapat
            // --- DECIMAL HASSASİYETİ (PRECISION) DÜZELTMELERİ ---

            // Order
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);

            // OrderItem
            modelBuilder.Entity<OrderItem>().Property(oi => oi.UnitPrice).HasPrecision(18, 2);

            // Product
            modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasPrecision(18, 2);

            // Coupon
            modelBuilder.Entity<Coupon>().Property(c => c.DiscountRate).HasPrecision(18, 2);

            // Invoice
            modelBuilder.Entity<Invoice>().Property(i => i.TotalAmount).HasPrecision(18, 2);

            // PaymentTransaction
            modelBuilder.Entity<PaymentTransaction>().Property(pt => pt.Amount).HasPrecision(18, 2);

            // Eğer tüm decimal alanların aynı hassasiyette olmasını istiyorsan
            // alternatif olarak HasColumnType("decimal(18,2)") de kullanabilirsin.
        }

    }
}
