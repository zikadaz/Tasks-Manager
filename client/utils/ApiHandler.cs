using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using Exceptions;
using System.Runtime.InteropServices;
using Model;
using System.Text.Json;
using System.Formats.Asn1;
using System.Text;
using System.Text.Unicode;
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
                // Cria uma nova tarefa com o título e "done"
               var newTask = new {title, done};
               // Posta a tarefa
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
        public async Task<string> PutTask(int taskId, TaskModel task)
        {
            // Transforma 'task' em  um json
            string json = JsonSerializer.Serialize(task);
            using var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );
            // procura a tarefa pelo endpoint e a atualiza
            HttpResponseMessage response = await _httpClient.PutAsync(
                $"tarefas/{task.Id}",
                content
            );
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> UpdateTask(int taskId, string title, bool done)
        {
            try {
                // pega a task usando de um método do cliente
                string taskJson = await SearchTaskById(taskId);
                TaskModel task = JsonSerializer.Deserialize<TaskModel>(taskJson)!;
                // verifica se 'title' é nulo ou vazio
                if(string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("O título não pode ser vazio.");
                    return "Título vazio ou nulo.";
                }
                // verifica se os parametros são iguais
                else if(title == task.title && done == task.Done)
                {
                    Console.WriteLine("Parâmetros idênticos. Não alterando nada.");
                    return "Nenhuma alteração necessária";
                }
                task.Done = done;
                task.title = title;
                return await PutTask(taskId, task);
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine($"Erro ao fazer UPDATE:  {e}");
                throw;
            }
        }
        // Deleta uma  tarefa pelo id
        public async Task<string> DeleteById(int taskId)
        {
            try
            {
                // Localiza a tarefa pelo Endpoint e a deleta
                HttpResponseMessage  response = await _httpClient.DeleteAsync(
                    $"tarefas/{taskId}"
                );
                response.EnsureSuccessStatusCode();
                return $"Tarefa {taskId}  exclúida com sucesso.";
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine($"Erro ao fazer o delete: {e.Message}");
                throw;
            }
        }
    }
}