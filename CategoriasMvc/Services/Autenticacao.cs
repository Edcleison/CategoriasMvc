﻿using CategoriasMvc.Models;
using System.Text;
using System.Text.Json;

namespace CategoriasMvc.Services
{
    public class Autenticacao : IAutenticacao
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;

        const string apiEndpointAutentica = "/api/autoriza/login/";
        private TokenViewModel tokenUsuario;

        public Autenticacao(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<TokenViewModel> AutenticaUsuario(UsuarioViewModel usuarioViewModel)
        {
            var client = _clientFactory.CreateClient("AutenticaApi");

            var usuario = JsonSerializer.Serialize(usuarioViewModel);
            StringContent content = new StringContent(usuario, Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(apiEndpointAutentica, content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    tokenUsuario = await JsonSerializer.DeserializeAsync<TokenViewModel>(apiResponse, _options);
                }
                else
                {
                    return null;
                }
            }
            return tokenUsuario;
        }
    }
}
