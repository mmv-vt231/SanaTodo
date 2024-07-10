import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";

import TodoItem from "./TodoItem";

function TodoList() {
  const dispatch = useDispatch();
  const tasks = useSelector(state => state.tasks);
  const storage = localStorage.getItem("storage") ?? "db";

  useEffect(() => {
    dispatch({ type: "GET_TASKS" });
  }, []);

  const handleChangeStorage = e => {
    const storage = e.target.value;
    localStorage.setItem("storage", storage);
    dispatch({ type: "GET_TASKS" });
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
