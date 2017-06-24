using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCC_ALCINDO.Models
{
    public class Cliente
    {
        [Key] // Diz que é a chave das propriedades
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Login { get; set; }

        
        public string Senha { get; set; }

        [Display(Name = "Endereço")]
        [Required]
        public string Endereco { get; set; }

        [Display(Name = "E-mail")]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public char Genero { get; set; }

        [Required]
        public string CPF { get; set; }


        public List<Animal> Animais { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        //Construtor
        public Cliente()
        {
            Animais = new List<Animal>();
        }
        public Cliente(string nome, string CPF)
        {
            Nome = nome;
            this.CPF = CPF;
            Animais = new List<Animal>();
        }

        public bool IsCpf()
        {
            string cpf = CPF;
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}