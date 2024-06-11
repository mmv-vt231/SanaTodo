import React from "react";
import { useDispatch, useSelector } from "react-redux";
import store from "../store/store";

function TodoForm() {
  const dispatch = useDispatch();
  const categories = useSelector(state => state.categories);

  console.log(store.getState());

  const handleSubmit = e => {
    e.preventDefault();

    const formData = new FormData(e.target);
    const data = Object.fromEntries(formData);

    dispatch({ type: "ADD_TODO", payload: data });

    e.target.reset();
  };

  return (
    <form className="row g-2" onSubmit={handleSubmit}>
      <div className="col-12">
        <input
          className="form-control"
          name="text"
          type="text"
          placeholder="Введіть задачу"
          required
        />
      </div>
      <div className="col-4">
        <select className="form-select" name="category" required>
          {categories.map(({ id, label }, i) => (
            <option key={i} value={id}>
              {label}
            </option>
          ))}
        </select>
      </div>
      <div className="col-4">
        <button className="btn btn-primary w-100">Додати</button>
      </div>
      <div className="col-4">
        <input className="form-control" name="endDate" type="datetime-local" />
      </div>
    </form>
  );
}

export default TodoForm;
