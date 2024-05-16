import React, { useEffect, useState } from "react";
import TodoCard from './TodoCard';
import axios from 'axios';

const Todos = () => {
  const [todos, setTodos] = useState([]);

  useEffect(() => { 
    fetchTodos();
  })

  const fetchTodos = () => {
    axios.get("http://localhost:5224/api/v1/todos/all")
      .then((response) => response.json())
      .then((data) => setTodos(data));
  };

  return (
    <>
      <h1> Bine ai venit!</h1>

      {todos.map((todo, index) => (
        <TodoCard key={index} title={todo.title} isDone={todo.isDone} />
      ))}
    </>
  );
};

export default Todos;
