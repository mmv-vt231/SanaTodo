import { applyMiddleware, createStore } from "redux";
import { createEpicMiddleware } from "redux-observable";
import rootReducer from "./reducer";
import rootEpic from "./epics";

const initialState = {
  categories: [],
  tasks: [],
};

const epicMiddleware = createEpicMiddleware();

const store = createStore(rootReducer, initialState, applyMiddleware(epicMiddleware));

epicMiddleware.run(rootEpic);

export default store;
