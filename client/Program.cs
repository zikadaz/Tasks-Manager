using  System;
using System.Threading.Tasks;
using ApiConnection;

namespace Program 
{
    public class EntryPoint
    {
        public static async Task Main(string[] args){
            ConnectionLocalApi connection = new();
            await connection.CreateTask("Brincar com a gata", false); // nova tarefa
            await connection.SearchTasks();
            await connection.TestConnection(); // {"connected":true}
        }
    }
}