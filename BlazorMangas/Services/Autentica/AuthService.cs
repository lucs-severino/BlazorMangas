using Blazored.LocalStorage;
using BlazorMangas.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text;
using System.Text.Json;

namespace BlazorMangas.Services.Autentica
{
    public class AuthService : IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(IHttpClientFactory httpClientFactory, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            _httpClientFactory = httpClientFactory;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            try
            {
               var httpClient = _httpClientFactory.CreateClient("ApiMangas");
               var loginAsJson = JsonSerializer.Serialize(loginModel);
               var requestContent = new StringContent(loginAsJson, Encoding.UTF8, "application/json");
               
                var response =  await httpClient.PostAsync("api/Users/login", requestContent);

                var loginResult = JsonSerializer.Deserialize<LoginResult>
                    (await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (!response.IsSuccessStatusCode)
                {
                    return loginResult;
                }

                await _localStorage.SetItemAsync("authToken", loginResult.Token);

                await _localStorage.SetItemAsync("tokenExpiration", loginResult.Expiration);

                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);

                httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", loginResult.Token);

                return loginResult;


            }
             catch (Exception)
            {
               throw;
            }
               
        }

        public async Task Logout()
        {
           var httpClient = _httpClientFactory.CreateClient("ApiMangas");

           await _localStorage.RemoveItemAsync("authToken");

          ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
          httpClient.DefaultRequestHeaders.Authorization = null;


        }
    }
}
