using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace P1_pps.Models
{
    [Serializable]
    public class PessoaFisica
    {
        // Propriedades da classe PessoaFisica
        [JsonPropertyOrder(1)]
        public string ChaveNumerica { get; set; }
        
        [JsonPropertyOrder(2)]
        public string NomeCompleto { get; set; }

        [JsonPropertyOrder(3)]
        public string Cpf { get; set; }

        [JsonPropertyOrder(4)]
        [System.Text.Json.Serialization.JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataNascimento { get; set; }

        [JsonPropertyOrder(5)]
        public string Email { get; set; }


        // Construtor da classe PessoaFisica
        public PessoaFisica(string nomeCompleto, string cpf, string dataNascimento, string email)
        {
            NomeCompleto = nomeCompleto;
            Cpf = cpf;

            if(!(dataNascimento == "00/00/0000"))
            {
                DataNascimento = DateTime.ParseExact(dataNascimento, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else{
                DataNascimento = DateTime.Now;
            }

            Email = email;

        }
    }
    public class Funcionario : PessoaFisica, IComparable<Funcionario>
    {
        [JsonPropertyOrder(6)]
        public string Cargo { get; set; }

        [JsonPropertyOrder(7)]
        [System.Text.Json.Serialization.JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataAdmissao { get; set; }

        [JsonPropertyOrder(8)]
        public string NomeChefe { get; set; }

        // Implementação da interface IComparable<Funcionario> para comparação
        public int CompareTo(Funcionario funcionario)
        {
            //return string.Compare(this.ChaveNumerica, funcionario.ChaveNumerica, StringComparison.Ordinal);
            return this.ChaveNumerica.CompareTo(funcionario.ChaveNumerica);
        }
        public Funcionario(string chaveNumerica, string nomeCompleto, string cpf, string dataNascimento, string email, string cargo, string dataAdmissao, string nomeChefe)
            : base(nomeCompleto, cpf, dataNascimento, email)
        {
            this.ChaveNumerica = chaveNumerica;
            this.Cargo = cargo;

            if (!(dataNascimento == "00/00/0000"))
            {
                this.DataAdmissao = DateTime.ParseExact(dataAdmissao, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            else
            {
                this.DataAdmissao = DateTime.Now;
            }

            this.NomeChefe = nomeChefe;

            Console.WriteLine(this); 
        }

    }

}
