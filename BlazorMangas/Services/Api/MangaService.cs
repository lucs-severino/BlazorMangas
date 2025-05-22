using BlazorMangas.Models.DTOs;
using System.Net.Http;
using System.Text.Json;

namespace BlazorMangas.Services.Api
{
    public class MangaService : IMangaService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ILogger<MangaService> _logger;
        private const string apiEnpoint = "/api/mangas";
        private readonly JsonSerializerOptions _options;

        private MangaDTO? manga;
        private List<MangaDTO>? mangas;

        public MangaService(IHttpClientFactory httpClient, JsonSerializerOptions options, ILogger<MangaService> logger)
        {
            _httpClientFactory = httpClient;
            _logger = logger;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }


        public Task<IEnumerable<MangaDTO>> GetMangas()
        {
            throw new NotImplementedException();
        }
        public Task<MangaDTO> GetManga(int id)
        {
            throw new NotImplementedException();
        }
        public Task<MangaDTO> CreateManga(MangaDTO manga)
        {
            throw new NotImplementedException();
        }
        public Task<MangaDTO> UpdateManga(MangaDTO manga)
        {
            throw new NotImplementedException();
        }
        public Task<bool> DeleteManga(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<MangaDTO>> GetMangasPorCategoria(int id)
        {
            throw new NotImplementedException();
        }
    }
    {
    }
}
