using BlazorMangas.Models.DTOs;

namespace BlazorMangas.Services.Api
{
    public interface IMangaService
    {
        Task<IEnumerable<MangaDTO>> GetMangas();
        Task<MangaDTO> GetManga(int id);
        Task<MangaDTO> CreateManga(MangaDTO manga);
        Task<MangaDTO> UpdateManga(MangaDTO manga);
        Task<bool> DeleteManga(int id);
        Task<IEnumerable<MangaDTO>> GetMangasPorCategoria(int id);
    }
}
