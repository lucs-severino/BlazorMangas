using BlazorMangas.Models.DTOs;
using System.Net.Http.Json;

namespace BlazorMangas.Services.Api
{
    public class CategoriaService : ICategoriaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<CategoriaService> _logger { get; }
        private const string apiEnpoint = "/api/categorias/";

        private CategoriaDTO? categoria;
        private IEnumerable<CategoriaDTO>? categorias;

        public CategoriaService(IHttpClientFactory httpClientFactory, ILogger<CategoriaService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        public async Task<CategoriaDTO> GetCategoria(int id)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiMangas");
                var response = await httpClient.GetAsync(apiEnpoint + id);
                
                if (response.IsSuccessStatusCode)
                {
                    categoria = await response.Content.ReadFromJsonAsync<CategoriaDTO>();
                    return categoria;
                }
                else
                {   var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Erro ao obter categoria pelo id: {id} -  {message}");
                    throw new Exception($" Status Code: {response.StatusCode} -  {message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter categorias pelo id");
                throw new UnauthorizedAccessException();
            }
        }

        public async Task<List<CategoriaDTO>> GetCategorias()
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("ApiMangas");
                var result = await httpClient.GetFromJsonAsync<List<CategoriaDTO>>(apiEnpoint);
                return result;
            }
             catch(Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter categorias");
                throw new UnauthorizedAccessException();
            }
        }
    }
}
