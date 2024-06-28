import React, { useEffect } from "react";
import { actions, useStore } from "../store/rxStore";

import TodoItem from "./TodoItem";

function TodoList() {
  const { tasks = [] } = useStore();
  const storage = localStorage.getItem("storage") ?? "db";

  useEffect(() => {
    actions.getTasks();
  }, []);

  const handleChangeStorage = e => {
    const storage = e.target.value;
    localStorage.setItem("storage", storage);
    actions.getTasks();
  };

  return (
    <div className="d-flex py-2 px-3 flex-column gap-2 border rounded">
      {tasks.length ? (
        tasks.map(todo => <TodoItem key={todo.id} {...todo} />)
      ) : (
        <p className="text-center mt-3">Задачі відсутні</p>
      )}
      <div className="py-2 border-top">
        <div className="row g-2">
          <select
            className="form-select"
            onChange={handleChangeStorage}
            defaultValue={storage}
            required
          >
            <option value="db">База даних</option>
            <option value="xml">XML сховище</option>
          </select>
        </div>
      </div>
    </div>
  );
}

export default TodoList;
