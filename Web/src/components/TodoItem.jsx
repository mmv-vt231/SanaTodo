import React from "react";
import dateConverter from "../utils/dateConverter";
import { actions } from "../store/rxStore";

function TodoItem({ id, text, endDate, category, completed }) {
  const formatedEndDate = endDate && dateConverter(endDate);
  const textStyle = completed ? "text-decoration-line-through" : "";

  const handleToggleComplete = () => {
    actions.toggleTask(completed, id);
  };

  const handleDelete = e => {
    e.stopPropagation();
    actions.deleteTask(id);
  };

  return (
    <div
      className="row position-relative align-items-center bg-light p-3 rounded"
      style={{ cursor: "pointer" }}
      onClick={handleToggleComplete}
    >
      <div className="col-1 text-center">
        <input className="form-check-input" checked={completed} type="checkbox" readOnly />
      </div>
      <div className={`col-7 ${textStyle}`}>{text}</div>
      <div className="col-3 border-start border-primary">{category?.name}</div>
      <div className="col-1 text-center">
        <button className="btn btn-close" type="button" onClick={handleDelete}></button>
      </div>
      {endDate && (
        <span
          className="position-absolute d-flex justify-content-center"
          style={{ left: 0, bottom: "2px", color: "gray", fontSize: "10px" }}
        >
          до {formatedEndDate}
        </span>
      )}
    </div>
  );
}

export default TodoItem;
