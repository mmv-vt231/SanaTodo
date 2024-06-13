import React from "react";
import { useSelector, shallowEqual, useDispatch } from "react-redux";
import TodoItem from "./TodoItem";

const selectTodos = state =>
  state.todos.map(todo => ({
    ...todo,
    category: state.categories.filter(el => el.id == todo.category)[0].label,
  }));

function TodoList() {
  const dispatch = useDispatch();
  const storage = useSelector(state => state.storage);
  const todos = useSelector(selectTodos, shallowEqual);

  const handleChangeStorage = e => {
    const storage = e.target.value;

    dispatch({ type: "CHANGE_STORAGE", payload: { storage } });
    localStorage.setItem("storage", storage);
  };

  return (
    <div className="d-flex py-2 px-3 flex-column gap-2 border rounded">
      {todos.length ? (
        todos.map(todo => <TodoItem key={todo.id} {...todo} />)
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
