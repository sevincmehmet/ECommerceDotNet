using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Infrastructure.Security.Concrete
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // 1. Kullanıcı giriş yapmış mı?
            if (!context.User.Identity.IsAuthenticated) return Task.CompletedTask;

            // 2. Token içindeki "permission" tipindeki tüm claim'leri al
            var permissions = context.User.Claims
                .Where(c => c.Type == "permission")
                .Select(c => c.Value);

            // 3. KRİTİK NOKTA: Eğer kullanıcı Admin ise veya aranan yetkiye sahipse geçsin
            if (context.User.IsInRole("Admin") || permissions.Contains(requirement.Permission))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}