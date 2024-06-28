import React, { useEffect, useState } from "react";
import { actions, useStore } from "../store/rxStore";

const initialFormData = {
  text: "",
  categoryId: 1,
  endDate: "",
};

function TodoForm() {
  const { categories } = useStore();

  const [formData, setFormData] = useState(initialFormData);
  const { text, categoryId, endDate } = formData;

  useEffect(() => {
    actions.getCategories();
  }, []);

  const handleChange = ({ target }) => {
    const { name, value } = target;

    setFormData(data => ({ ...data, [name]: value }));
  };

  const handleSubmit = () => {
    const correctEndDate = endDate ? new Date(endDate).toISOString() : null;

    const data = {
      text,
      endDate: correctEndDate,
      categoryId: +categoryId,
    };

    actions.addTask(data);
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
          name="categoryId"
          onChange={handleChange}
          value={categoryId}
          required
        >
          {categories?.map(({ id, name }, i) => (
            <option key={i} value={id}>
              {name}
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
