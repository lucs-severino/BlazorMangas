using Blazored.LocalStorage;

namespace BlazorMangas.Services.Api
{
    public class CustomHttpHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;
        public CustomHttpHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if(request.RequestUri.AbsolutePath.ToLower().Contains("login")
               || request.RequestUri.AbsolutePath.ToLower().Contains("register"))
            {
                return await base.SendAsync(request, cancellationToken);
            }
            var jwtToken = await _localStorage.GetItemAsync<string>("authToken");

            if(!string.IsNullOrEmpty(jwtToken))
            {
                request.Headers.Add("Authorization", $"Bearer {jwtToken}");
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
