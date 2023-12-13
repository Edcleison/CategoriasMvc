using CategoriasMvc.Models;
using CategoriasMvc.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CategoriasMvc.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly ICategoriaService _categoriaService;
        private string token = string.Empty;

        public ProdutosController(IProdutoService produtoService, ICategoriaService categoriaService)
        {
            _produtoService = produtoService;
            _categoriaService = categoriaService;
        }

		[HttpGet]		 
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> Index()
        {
            //extrai o token do cookie
            var result = await _produtoService.GetProdutos(ObtemTokenJwt());

            if(result is null)
            {
                return View("Error");
            }
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> CriarNovoProduto()
        {
            ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");
            return View();
        }
        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> CriarNovoProduto(ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.CriaProduto(produtoViewModel,ObtemTokenJwt());

                if (result != null)
                    return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");   
            }
            return View(produtoViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> DetalhesProduto(int id)
        {
            var result = await _produtoService.GetProdutoPorId(id, ObtemTokenJwt());

            if (result is null)
            {
                return View("Error");
            }
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> AtualizarProduto(int id)
        {

            var result = await _produtoService.GetProdutoPorId(id,ObtemTokenJwt());

            if (result is null)
            {
                return View("Error");
            }
            ViewBag.CategoriaId = new SelectList(await _categoriaService.GetCategorias(), "CategoriaId", "Nome");
            return View(result);
        }
        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> AtualizarProduto(int id, ProdutoViewModel produtoViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _produtoService.AtualizarProduto(id, produtoViewModel,ObtemTokenJwt());

                if (result)
                    return RedirectToAction(nameof(Index));
            }
            ViewBag.Erro = "Erro ao atualizar Produto";
            return View(produtoViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var result = await _produtoService.GetProdutoPorId(id,ObtemTokenJwt());

            if (result is null)
                return View("Error");

            return View(result);
        }

        [HttpPost(), ActionName("DeletarProduto")] // permite especificar um nome de Action diferente do nome do Método
        public async Task<IActionResult> DeletaConfirmado(int id)
        {
            var result = await _produtoService.DeletarProduto(id,ObtemTokenJwt());

            if (result)
            {
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        private string ObtemTokenJwt()
        {
            if (HttpContext.Request.Cookies.ContainsKey("X-Access-Token"))
                token = HttpContext.Request.Cookies["X-Access-Token"].ToString();

            return token;
        }
    }
}
