import { combineEpics, ofType } from "redux-observable";
import { from, map, mergeMap } from "rxjs";
import { fetcher } from "../utils/fetcher";

const getTasks = action$ =>
  action$.pipe(
    ofType("GET_TASKS"),
    mergeMap(() =>
      from(
        fetcher({
          query: `
            query {
              tasks {
                id, 
                text,
                completed,
                endDate,
                category {
                  name
                }
              }
            }
          `,
        })
      ).pipe(map(tasks => ({ type: "GET_TASKS_FULFILLED", payload: tasks })))
    )
  );

const getCategories = action$ =>
  action$.pipe(
    ofType("GET_CATEGORIES"),
    mergeMap(() =>
      from(
        fetcher({
          query: `
            query {
              categories {
                id, 
                name
              }
            }
          `,
        })
      ).pipe(map(categories => ({ type: "GET_CATEGORIES_FULFILLED", payload: categories })))
    )
  );

const addTask = action$ =>
  action$.pipe(
    ofType("ADD_TASK"),
    mergeMap(({ payload }) =>
      from(
        fetcher({
          query: `
            mutation addTodo($task: TaskInputType) {
              createTask(task: $task) {
                id, 
                text,
                completed,
                endDate,
                category {
                  name
                }
              }
            }
          `,
          variables: {
            task: payload,
          },
        })
      ).pipe(map(newTask => ({ type: "ADD_TASK_FULFILLED", payload: newTask })))
    )
  );

const toggleTask = action$ =>
  action$.pipe(
    ofType("TOGGLE_TASK"),
    mergeMap(({ payload }) =>
      from(
        fetcher({
          query: `
            mutation completeTodo($completed: Boolean, $taskId: Int) {
              completeTask(completed: $completed, taskId: $taskId)
            }
          `,
          variables: {
            completed: payload.completed,
            taskId: payload.id,
          },
        })
      ).pipe(map(task => ({ type: "TOGGLE_TASK_FULFILLED", payload: task })))
    )
  );

const deleteTask = action$ =>
  action$.pipe(
    ofType("DELETE_TASK"),
    mergeMap(({ payload }) =>
      from(
        fetcher({
          query: `
            mutation deleteTodo($taskId: Int) {
              deleteTask(taskId: $taskId)
            }
          `,
          variables: {
            taskId: payload.id,
          },
        })
      ).pipe(map(task => ({ type: "DELETE_TASK_FULFILLED", payload: task })))
    )
  );

export default combineEpics(getTasks, addTask, toggleTask, deleteTask, getCategories);
