using CategoriasMvc.Models;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CategoriasMvc.Services
{
    public class ProdutoService : IProdutoService
    {

        private const string apiEndpoint = "/api/1/produtos/";

        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        private ProdutoViewModel produtoViewModel;
        private IEnumerable<ProdutoViewModel> produtosVM;

        public ProdutoService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
        }

        public async Task<IEnumerable<ProdutoViewModel>> GetProdutos(string token)
        {
            var client = _clientFactory.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token,client);

            using (var response = await client.GetAsync(apiEndpoint))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    produtosVM = await JsonSerializer.DeserializeAsync<IEnumerable<ProdutoViewModel>>(apiResponse, _options);
                }
                else
                {
                    return null;
                }

            }
            return produtosVM;
        }
        public async Task<ProdutoViewModel> GetProdutoPorId(int id, string token)
        {
            var client = _clientFactory.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.GetAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();

                    produtoViewModel = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }

            }
            return produtoViewModel;
        }
        public async Task<ProdutoViewModel> CriaProduto(ProdutoViewModel produtoViewModel, string token)
        {
            var client = _clientFactory.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);

            var produto = JsonSerializer.Serialize(produtoViewModel);
            StringContent content = new StringContent(produto, Encoding.UTF8, "application/json");

            using (var response = await client.PostAsync(apiEndpoint, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    produtoViewModel = await JsonSerializer.DeserializeAsync<ProdutoViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return produtoViewModel;
        }
        public async Task<bool> AtualizarProduto(int id, ProdutoViewModel produtoViewModel, string token)
        {
            var client = _clientFactory.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.PutAsJsonAsync(apiEndpoint + id, produtoViewModel))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeletarProduto(int id, string token)
        {
            var client = _clientFactory.CreateClient("ProdutosApi");
            PutTokenInHeaderAuthorization(token, client);

            using (var response = await client.DeleteAsync(apiEndpoint + id))
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        } 
        
        private static void PutTokenInHeaderAuthorization(string token, HttpClient httpClient)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
