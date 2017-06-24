using System;
using System.ComponentModel.DataAnnotations;

namespace TCC_ALCINDO.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }

        [Required]
        public decimal Preco { get; set; }

        public DateTime DataCadastro { get; set; }
        [Required]
        public string Marca { get; set; }

        public Categoria Categoria { get; set; }
    }
}