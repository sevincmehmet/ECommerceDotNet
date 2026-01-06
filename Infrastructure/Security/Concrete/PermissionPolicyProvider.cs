using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Infrastructure.Security.Concrete
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider BackupPolicyProvider { get; }

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            BackupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // Eğer "User.View" gibi bir isim gelirse, otomatik olarak ona uygun bir policy oluşturur
            var policy = new AuthorizationPolicyBuilder();
            policy.Requirements.Add(new PermissionRequirement(policyName));
            return Task.FromResult(policy.Build());
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => BackupPolicyProvider.GetDefaultPolicyAsync();
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => BackupPolicyProvider.GetFallbackPolicyAsync();
    }
}