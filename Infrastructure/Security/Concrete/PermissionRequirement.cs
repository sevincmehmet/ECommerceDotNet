using Microsoft.AspNetCore.Authorization;

namespace Infrastructure.Security.Concrete
{
    // BU SATIR KRİTİK: Miras almazsa Handler hata verir
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}