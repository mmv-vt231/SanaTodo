import React from "react";
import { useDispatch } from "react-redux";
import dateConverter from "../utils/dateConverter";

function TodoItem({ id, text, endDate, category, completed }) {
  const dispatch = useDispatch();
  const formatedEndDate = endDate && dateConverter(endDate);
  const textStyle = completed ? "text-decoration-line-through" : "";

  const handleToggleComplete = () => {
    dispatch({ type: "TOGGLE_TODO", payload: { id } });
  };

  const handleDelete = () => {
    dispatch({ type: "DELETE_TODO", payload: { id } });
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
      <div className="col-3 border-start border-primary">{category}</div>
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
