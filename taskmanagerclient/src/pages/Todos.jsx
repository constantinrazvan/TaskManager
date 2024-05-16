import React, { useState } from "react";
import TodoCard from './TodoCard';

const Todos = () => {
  const [email, setEmail] = useState("");
  const [todos, setTodos] = useState([]);

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
