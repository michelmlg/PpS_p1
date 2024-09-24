using Microsoft.AspNetCore.Mvc;
using P1_pps.Models;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Security;

namespace P1_pps.Controllers
{
    public class FuncionariosController : Controller
    {
        private List<Funcionario> funcionarios = new List<Funcionario>(); // Lista para armazenar funcionários

        public FuncionariosController()
        {
            funcionarios = SerializaFuncionario.Load();
            //funcionarios.Sort();

        }

        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult AddFuncionario(string nomeCompleto, string cpf, string dataNascimento, string email, string cargo, string dataAdmissao, string nomeChefe)
        {
            string novaChave = VerificaUltimo();

            // Valida a data de Nascimento
            if (!Valida.IsDataNascimentoValida(dataNascimento))
            {
                ModelState.AddModelError("dataNascimento", "A data de nascimento não pode ser maior que a data atual.");
                return View("Create");
            }
            if (!Valida.IsEmailValido(email))
            {
                ModelState.AddModelError("email", "Utilize um e-mail valido!");
                return View("Create");
            }
            if (!Valida.IsCpfValido(cpf))
            {
                ModelState.AddModelError("cpf", "O CPF informado é inválido.");
                return View("Create");
            }
            
            Funcionario novoFuncionario = new Funcionario(novaChave, nomeCompleto, cpf, dataNascimento, email, cargo, dataAdmissao, nomeChefe);

            funcionarios.Add(novoFuncionario);
            SerializaFuncionario.Save(funcionarios);

            return RedirectToAction("List", "Funcionarios");
        }

        private string VerificaUltimo()
        {
            if (funcionarios.Count == 0)
            {
                return "000001";
            }

            var ultimoFuncionario = funcionarios.OrderByDescending(f => int.Parse(f.ChaveNumerica)).FirstOrDefault();
            int ultimoValor = int.Parse(ultimoFuncionario.ChaveNumerica);

            return (ultimoValor + 1).ToString("D6");
        }

        [HttpGet]
        public IActionResult List()
        {
            funcionarios.Sort();
            return View(funcionarios);
        }


        [HttpGet]
        public IActionResult Edit(string ChaveNumerica)
        {
            Funcionario busca = new Funcionario(ChaveNumerica, "", "", "00/00/0000", "", "", "00/00/0000", "");

            int index = funcionarios.BinarySearch(busca);

            Funcionario retorno = new Funcionario("", "", "", "00/00/0000", "", "", "00/00/0000", "");

            retorno.ChaveNumerica = funcionarios[index].ChaveNumerica;
            retorno.NomeCompleto = funcionarios[index].NomeCompleto;
            retorno.Cpf = funcionarios[index].Cpf;
            retorno.DataNascimento = funcionarios[index].DataNascimento;
            retorno.Email = funcionarios[index].Email;
            retorno.Cargo = funcionarios[index].Cargo;
            retorno.DataAdmissao = funcionarios[index].DataAdmissao;
            retorno.NomeChefe = funcionarios[index].NomeChefe;

            return View(retorno);
        }


        [HttpPost]
        public IActionResult EditFuncionario(string chaveNumerica, string nomeCompleto, string cpf, string dataNascimento, string email, string cargo, string dataAdmissao, string nomeChefe)
        {

            if (!Valida.IsDataNascimentoValida(dataNascimento))
            {
                ModelState.AddModelError("dataNascimento", "A data de nascimento não pode ser maior que a data atual.");
                return View("Edit");
            }
            if (!Valida.IsEmailValido(email))
            {
                ModelState.AddModelError("email", "Utilize um e-mail valido!");
                return View("Edit");
            }
            if (!Valida.IsCpfValido(cpf))
            {
                ModelState.AddModelError("cpf", "O CPF informado é inválido.");
                return View("Edit");
            }


            Funcionario busca = new Funcionario(chaveNumerica, "", "", "00/00/0000", "", "", "00/00/0000", "");
            funcionarios.Sort();

            int index = funcionarios.BinarySearch(busca);

            if (index >= 0)
            {
                Funcionario fAlterado = new Funcionario(chaveNumerica, nomeCompleto, cpf, dataNascimento, email, cargo, dataAdmissao, nomeChefe);

                Console.WriteLine("Índice encontrado: " + index);
                Console.WriteLine("Funcionário antes da alteração: " + funcionarios[index]);

                funcionarios[index].ChaveNumerica = fAlterado.ChaveNumerica;
                funcionarios[index].NomeCompleto = fAlterado.NomeCompleto;
                funcionarios[index].Cpf = fAlterado.Cpf;
                funcionarios[index].DataNascimento = fAlterado.DataNascimento;
                funcionarios[index].Email = fAlterado.Email;
                funcionarios[index].Cargo = fAlterado.Cargo;
                funcionarios[index].DataAdmissao = fAlterado.DataAdmissao;
                funcionarios[index].NomeChefe = fAlterado.NomeChefe;

                SerializaFuncionario.Save(funcionarios);

                Console.WriteLine("Funcionário após a alteração: " + funcionarios[index]);
            }
            else
            {
                Console.WriteLine("Funcionário não encontrado.");
            }



            return RedirectToAction("List", "Funcionarios");


        }



        [HttpPost]
        public IActionResult DeleteFuncionario(string chaveNumerica)
        {
            Funcionario funcionario = new Funcionario(chaveNumerica, "", "", "00/00/0000", "", "", "00/00/0000", "");
            int index = funcionarios.BinarySearch(funcionario);

            funcionarios.RemoveAt(index);
            funcionarios.Sort();
            SerializaFuncionario.Save(funcionarios);

            return RedirectToAction("List", "Funcionarios");


        }

    }

}
