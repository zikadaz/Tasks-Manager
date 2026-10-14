package handlers

import (
	"encoding/json"
	"net/http"
	"slices"
	"strconv"

	"task.com/go/model"
)
var TaskAPI []model.Task
func CreateTask(w http.ResponseWriter, r *http.Request) {
	var m model.Task
	if  err := json.NewDecoder(r.Body).Decode(&m); err != nil {
		http.Error(w, "Json inválido", http.StatusBadRequest)
		return
	}
	if m.Title == "" {
		http.Error(w, "Título não pode ser vazio", http.StatusBadRequest)
		return
	}
	if len(m.Title) > 50 {
		http.Error(w, "Task title lenght is too long.", http.StatusBadRequest)
		return
	}
	model.Mu.Lock()
	defer model.Mu.Unlock()
	m.Id = model.ProxyId
	model.ProxyId ++;
	model.TaskAPI = append(model.TaskAPI, m)
	w.Header().Set("Content-Type", "Application/json")
		json.NewEncoder(w).Encode(m)
}
func ListTasks(w http.ResponseWriter, r *http.Request) {
	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(model.TaskAPI)
}
func ListTasksById(w http.ResponseWriter, r *http.Request) {
	id, err := strconv.Atoi(r.PathValue("id"))
	if err != nil {
		http.Error(w, "ID invalido", http.StatusBadRequest)
		return
	}
	model.Mu.Lock()
	defer model.Mu.Unlock()
	for _, item := range model.TaskAPI {
		if item.Id == id {
			w.Header().Set("Content-Type", "application/json")
			json.NewEncoder(w).Encode(item)
			return
		}
	}
	http.Error(w, "Tarefa não encontrada.", http.StatusNotFound)
}
func UpdateTaskById(w http.ResponseWriter, r *http.Request) {
	id, err := strconv.Atoi(r.PathValue("id"))
	if err != nil {
		http.Error(w, "ID inválido", http.StatusBadRequest)
		return
	}
	var m model.Task
	if err := json.NewDecoder(r.Body).Decode(&m); err != nil {
		http.Error(w, "json inválido: "+err.Error(), http.StatusBadRequest)
		return
	}
	if m.Title == "" {
		http.Error(w, "Titulo não pode ser vazio", http.StatusBadRequest)
		return
	}
	if len(m.Title) > 50 {
		http.Error(w, "Título muito longo", http.StatusBadRequest)
		return
	}
	model.Mu.Lock()
	defer model.Mu.Unlock()
	for i := range model.TaskAPI {
		if model.TaskAPI[i].Id == id {
			m.Id = id
			model.TaskAPI[i] = m
			w.Header().Set("Content-Type", "application/json")
			json.NewEncoder(w).Encode(m)
			return
		}
	}
	http.Error(w, "Tarefa não encontrada", http.StatusNotFound)
}

func DeleteTaskById(w http.ResponseWriter, r *http.Request) {
	id, err := strconv.Atoi(r.PathValue("id"))
	if err != nil {
		http.Error(w, "id inválido", http.StatusBadRequest)
		return
	}
	model.Mu.Lock()
	defer model.Mu.Unlock()
	for i := range model.TaskAPI {
		if model.TaskAPI[i].Id == id {
			model.TaskAPI = slices.Delete(model.TaskAPI, i, i+1)
			w.WriteHeader(http.StatusNoContent)
			return
		}
	}
	http.Error(w, "Tarefa não encontrada.", http.StatusNotFound)
}

