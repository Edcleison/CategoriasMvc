using CategoriasMvc.Models;

namespace CategoriasMvc.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token);
        Task<ProdutoViewModel> CriaProduto(ProdutoViewModel produtoViewModel, string token);
        Task<ProdutoViewModel> GetProdutoPorId(int id, string token);
        Task<bool> AtualizarProduto(int id, ProdutoViewModel produtoViewModel, string token);
        Task<bool> DeletarProduto(int id, string token);
    }
}
