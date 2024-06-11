import { createStore } from "redux";
import rootReducer from "./reducer";

const initialState = {
  categories: [
    {
      id: 1,
      label: "Дім",
    },
    {
      id: 2,
      label: "Робота",
    },
    {
      id: 3,
      label: "Навчання",
    },
    {
      id: 4,
      label: "Інше",
    },
  ],
  todos: [],
  index: 0,
  storage: localStorage.getItem("storage") ?? "db",
};

const store = createStore(rootReducer, initialState);

export default store;
