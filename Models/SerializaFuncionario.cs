using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace P1_pps.Models
{
    public class SerializaFuncionario
    {

        private static string filePath = "F:\\Funcionario.json";

        // Serializar e salvar a lista de funcionários
        public static void Save(List<Funcionario> listaFuncionarios)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true // Formatação legível
                };
                
                string jsonString = System.Text.Json.JsonSerializer.Serialize(listaFuncionarios, options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao salvar os dados", ex);
            }
        }

        // Carregar e desserializar a lista de funcionários
        public static List<Funcionario> Load()
        {
            try
            {
                // Verifica se o arquivo existe
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Arquivo não encontrado: " + filePath);

                    // Cria o arquivo vazio e fecha imediatamente
                    using (File.Create(filePath)) { }

                    // Retorna uma lista vazia, já que o arquivo foi criado agora e está vazio
                    return new List<Funcionario>();
                }

                // Se o arquivo existe, lê o conteúdo
                string jsonString = File.ReadAllText(filePath);

                // Se o arquivo estiver vazio, retorna uma lista vazia para evitar erro de deserialização
                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    return new List<Funcionario>();
                }

                // Deserializa e retorna a lista de funcionários usando Newtonsoft.Json
                return JsonConvert.DeserializeObject<List<Funcionario>>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao carregar os dados", ex);
            }
        }

    }
}
