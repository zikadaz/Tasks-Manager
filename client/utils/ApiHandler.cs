using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using Exceptions;
namespace ApiConnection
{   // Abre uma conexao com a API local
    public class ConnectionLocalApi
    {
        private readonly string url = "http://127.0.0.1:8080";
        // Lista dos endpoints que a API aceita
        private readonly List<string> endpoints =
        [
            "/",
            "/tarefas"
        ];
        private readonly HttpClient _httpClient;
    
        public ConnectionLocalApi()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            _httpClient = new HttpClient(handler);
            {
               _httpClient.BaseAddress = new Uri(url);
            };
        } 
        // Método para atingir a / [raiz] da API
        public async Task<string> TestConnection()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoints[0]);
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return content;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        // Localizar todas as tarefas cadastradas na API via POST
        public async Task<string> SearchTasks()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpoints[1]);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return content;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro ao conectar na api: {e.Message}");
                throw;
            }
        }
        // localizar uma tarefa especpífica por meio do id
        public async Task<string> SearchTaskById(int id)
        {
            string endpointEdited = $"{endpoints[1]}/{id}";
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(endpointEdited);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return content;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine($"$Erro ao conectar na api {e.Message}");
                throw;
            }
        }
        // Cria uma nova tarefa
        public async Task<string> CreateTask(string title, bool done)
        {
            if(title.Length > 50)
            {
                throw new TitleIsTooLongException();
            }
            try
            {
               var newTask = new {title, done};
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoints[1], newTask);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return content; 
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Erro ao fazer POST: {e.Message}");
                throw;
            }
        }
        public async Task<string> UpdateTask(int task_id)
        {
            // A implementar
            throw new NotImplementedException();
        }
        public async void DeleteTask(int task_id)
        {
            // A Implementar
            throw new NotImplementedException();
        }
    }
}