using System;
using System.ComponentModel.DataAnnotations;

namespace TCC_ALCINDO.Models
{
    public class Animal
    {

        public Animal()
        {
            Cliente = new Cliente();
        }

        public Animal(int idCliente)
        {
            Cliente = new Cliente()
            {
                Id = idCliente

            };

        }


        [Key] // Diz que é a chave das propriedades
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Porte { get; set; }

        [Required]
        [Display(Name = "Raça")]
        public string Raca { get; set; }

        [Display(Name = "Espécie")]
        [Required]
        public string Especie { get; set; }

        [Required]
        [Display(Name = "Gênero")]
        public char Genero { get; set; }

        public byte[] Foto { get; set; }

        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        public Cliente Cliente { get; set; }

    }
}