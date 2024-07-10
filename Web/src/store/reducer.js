export default function reducer(state, action) {
  const { type, payload } = action;

  switch (type) {
    case "GET_TASKS_FULFILLED": {
      return {
        ...state,
        tasks: payload.tasks,
      };
    }

    case "ADD_TASK_FULFILLED": {
      const newTask = payload.createTask;

      return {
        ...state,
        tasks: [...state.tasks, newTask],
      };
    }

    case "TOGGLE_TASK_FULFILLED": {
      const taskId = payload.completeTask;

      return {
        ...state,
        tasks: state.tasks.map(task =>
          task.id == taskId ? { ...task, completed: !task.completed } : task
        ),
      };
    }

    case "DELETE_TASK_FULFILLED": {
      const taskId = payload.deleteTask;

      return {
        ...state,
        tasks: state.tasks.filter(task => task.id != taskId),
      };
    }

    case "GET_CATEGORIES_FULFILLED": {
      return {
        ...state,
        categories: payload.categories,
      };
    }

    default:
      return state;
  }
}
