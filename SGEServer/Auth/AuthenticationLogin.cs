using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using SGEServer.Models;
using Microsoft.AspNetCore.Components;

namespace SGEServer.Auth
{
    public class AuthenticationLogin : AuthenticationStateProvider
    {
        public static readonly string tokenKey = "SGEAPP";
        private readonly IJSRuntime _js;
        private readonly IConfiguration _config;

        public AuthenticationLogin(IJSRuntime js, IConfiguration config)
        {
            _js = js;
            _config = config;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                string token;

                token = await _js.GetFromLocalStorage(tokenKey);

                if (string.IsNullOrEmpty(token))
                {
                    return NotAuthenticate;
                }

                return CreateAuthenticationState(token);
            }
            catch
            {
                return NotAuthenticate;
            }
        }

        public async Task<AuthenticationState> ValidaSessao()
        {

            return await GetAuthenticationStateAsync();
        }

        public async Task Login(string token)
        {
            try
            {
                await _js.SetInLocalStorage(tokenKey, token);

                var authState = CreateAuthenticationState(token);

                NotifyAuthenticationStateChanged(Task.FromResult(authState));

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Logout()
        {
            try
            {
                await _js.RemoveItem(tokenKey);
                NotifyAuthenticationStateChanged(Task.FromResult(NotAuthenticate));
            }
            catch (Exception)
            {
                throw;
            }
        }
        private static AuthenticationState NotAuthenticate =>
            new(new ClaimsPrincipal(new ClaimsIdentity()));

        public AuthenticationState CreateAuthenticationState(string token)
        {
            return new AuthenticationState(new ClaimsPrincipal
                (new ClaimsIdentity(new TokenService(_config).GetClaims(token), "jwt")));
        }

        public static async Task<bool> AuthenticateUser(AuthenticationLogin AuthState, LoginModel Usr, NavigationManager Navigation)
        {
            bool isAuthenticated = false;

            var Auth = await AuthState.GetAuthenticationStateAsync();

            if (Auth.User.Identity != null && Auth.User.Identity.IsAuthenticated)
            {
                Usr.CodUsuario = int.Parse(Auth.User.FindFirst("UserId")?.Value ?? "0");
                Usr.Usuario = Auth.User.FindFirst("UserLogin")?.Value;
                Usr.Nome = Auth.User.FindFirst("UserNome")?.Value;
                Usr.CodEmpresa = int.Parse(Auth.User.FindFirst("UserIdEmpresa")?.Value ?? "0");
                Usr.Empresa = Auth.User.FindFirst("UserEmpresa")?.Value;

                isAuthenticated = true;

                if (Usr.CodUsuario <= 0)
                {
                    isAuthenticated = false;
                    await AuthState.Logout();
                    Navigation.NavigateTo("/");
                }
            }
            else
            {
                isAuthenticated = false;
                await AuthState.Logout();
                Navigation.NavigateTo("/");
            }

            return isAuthenticated;
        }
    }
}
