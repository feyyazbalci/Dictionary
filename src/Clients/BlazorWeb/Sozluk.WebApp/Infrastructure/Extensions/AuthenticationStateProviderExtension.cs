using Microsoft.AspNetCore.Components.Authorization;
using Sozluk.WebApp.Infrastructure.Auth;
using System.Security.Claims;

namespace Sozluk.WebApp.Infrastructure.Extensions
{
    public static class AuthenticationStateProviderExtension
    {
        public static async Task<Guid> GetUserId(this AuthenticationStateProvider provider)
        {
            var state = await provider.GetAuthenticationStateAsync();
            var userId = state.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return new Guid(userId);
        } 
    }
}
