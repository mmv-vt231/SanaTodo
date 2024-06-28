import { createContext, useContext } from "react";
import { BehaviorSubject } from "rxjs";
import { fetcher } from "../utils/fetcher";

const initialState = {
  categories: [],
  tasks: [],
};

export const store$ = new BehaviorSubject(initialState);

export const StoreContext = createContext({});
export const useStore = () => useContext(StoreContext);

export const actions = {
  getTasks: async () => {
    const { tasks = [] } = await fetcher({
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
    });

    store$.next({ ...store$.getValue(), tasks });
  },
  addTask: async task => {
    const { createTask: newTask } = await fetcher({
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
        task,
      },
    });

    store$.next({
      ...store$.getValue(),
      tasks: [...store$.getValue().tasks, newTask],
    });
  },
  toggleTask: async (completed, taskId) => {
    await fetcher({
      query: `
        mutation completeTodo($completed: Boolean, $taskId: Int) {
          completeTask(completed: $completed, taskId: $taskId)
        }
      `,
      variables: {
        completed,
        taskId,
      },
    });

    store$.next({
      ...store$.getValue(),
      tasks: store$
        .getValue()
        .tasks.map(task => (task.id === taskId ? { ...task, completed: !task.completed } : task)),
    });
  },
  deleteTask: async taskId => {
    await fetcher({
      query: `
        mutation deleteTodo($taskId: Int) {
          deleteTask(taskId: $taskId)
        }
      `,
      variables: {
        taskId,
      },
    });

    store$.next({
      ...store$.getValue(),
      tasks: store$.getValue().tasks.filter(task => task.id !== taskId),
    });
  },
  getCategories: async () => {
    const { categories = [] } = await fetcher({
      query: `
        query {
          categories {
            id, 
            name
          }
        }
      `,
    });

    store$.next({ ...store$.getValue(), categories });
  },
};
