import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";

const initialFormData = {
  text: "",
  category: "1",
  endDate: "",
};

function TodoForm() {
  const [formData, setFormData] = useState(initialFormData);

  const { text, category, endDate } = formData;

  const dispatch = useDispatch();
  const categories = useSelector(state => state.categories);

  const handleChange = ({ target }) => {
    const { name, value } = target;

    setFormData(data => ({ ...data, [name]: value }));
  };

  const handleSubmit = () => {
    dispatch({ type: "ADD_TODO", payload: formData });
    setFormData(initialFormData);
  };

  return (
    <div className="row g-2">
      <div className="col-12">
        <input
          className="form-control"
          name="text"
          type="text"
          placeholder="Введіть задачу"
          onChange={handleChange}
          value={text}
          required
        />
      </div>
      <div className="col-4">
        <select
          className="form-select"
          name="category"
          onChange={handleChange}
          value={category}
          required
        >
          {categories.map(({ id, label }, i) => (
            <option key={i} value={id}>
              {label}
            </option>
          ))}
        </select>
      </div>
      <div className="col-4">
        <button className="btn btn-primary w-100" type="button" onClick={handleSubmit}>
          Додати
        </button>
      </div>
      <div className="col-4">
        <input
          className="form-control"
          name="endDate"
          type="datetime-local"
          onChange={handleChange}
          value={endDate}
        />
      </div>
    </div>
  );
}

export default TodoForm;
