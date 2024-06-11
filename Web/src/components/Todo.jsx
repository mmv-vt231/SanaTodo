import React from "react";
import TodoList from "./TodoList";
import TodoForm from "./TodoForm";

function Todo() {
  return (
    <div className="d-flex flex-column gap-2">
      <TodoForm />
      <TodoList />
    </div>
  );
}

export default Todo;
