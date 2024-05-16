import React from "react";

const TodoCard = (props) => {
  return (
    <div className="todo-card">
      <h3>{props.title}</h3>
      <input 
        type="checkbox" 
        checked={props.isDone} 
        onChange={() => props.toggleTodoDone(props.id)} 
      />
    </div>
  );
};

export default TodoCard;
