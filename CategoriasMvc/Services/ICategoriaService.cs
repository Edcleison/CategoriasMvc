using CategoriasMvc.Models;

namespace CategoriasMvc.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaViewModel>> GetCategorias();
        Task<CategoriaViewModel> CriaCategoria(CategoriaViewModel categoriaViewModel);
        Task<CategoriaViewModel> GetCategoriaPorId(int id);
        Task<bool> AtualizarCategoria(int id, CategoriaViewModel categoriaViewModel);
        Task<bool> DeletarCategoria(int id);

        
    }
}
