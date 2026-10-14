package main

import (
	"fmt"
	"net/http"
	"task.com/go/handlers"
)

func main() {
	mux := http.NewServeMux()
	mux.HandleFunc("GET /", handlers.Home)
	mux.HandleFunc("POST /tarefas", handlers.CreateTask)
	mux.HandleFunc("GET /tarefas", handlers.ListTasks)
	mux.HandleFunc("GET /tarefas/{id}", handlers.ListTasksById)
	mux.HandleFunc("PUT /tarefas/{id}", handlers.UpdateTaskById)
	mux.HandleFunc("DELETE /tarefas/{id}", handlers.DeleteTaskById)
	err := http.ListenAndServe(":8080", mux)
	if err != nil {
		fmt.Println("Erro ao iniciar servidor.")
	}
}