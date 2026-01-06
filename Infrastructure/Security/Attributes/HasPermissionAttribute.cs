using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Security.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(string permission) : base(permission)
        {
            // Policy adını direkt yetki adı (Örn: "Product_Create") yapıyoruz
        }
    }
}
