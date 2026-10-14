package model

import "sync"

// modelo da tarefa
type Task struct {
	Id		int `josn:"id"`
	Title	string `json:"title"`
	Done	bool `json:"done"`
}

var (
	// map [id][id, titulo, concluida]
	ProxyId = 1
	Mu sync.Mutex
	TaskAPI []Task
)
