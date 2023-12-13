﻿using System.ComponentModel.DataAnnotations;

namespace CategoriasMvc.Models
{
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome da produto é obrigatório")]
        public string? Nome { get; set; }

        [Required(ErrorMessage ="A descrição do produto é obrigatória")] 
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto")]
        public decimal Preco { get; set; }

        [Display(Name = "Informe o caminho da imagem do Produto")]
        public string? ImagemUrl { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
    }
}
