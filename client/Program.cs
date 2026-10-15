using  System;
using System.Threading.Tasks;
using ApiConnection;

namespace Program 
{
    public class EntryPoint
    {
        public static async Task Main(string[] args){
            ConnectionLocalApi connection = new();
            await connection.CreateTask("Brincar com a gata", false); // nova tarefa [1]
            await connection.SearchTaskById(1); // procura pela tarefa com id 1
            await connection.UpdateTask(1, "Lavar o carro", false); // atualiza a tarefa com id 1
            await connection.DeleteById(1); // Deleta a tarefa com id 1
            /*
            CREATE (create task)
            READ    (search task)
            UPDATE  (update task)
            DELETE (delete task)
            */
        }
    }
}
