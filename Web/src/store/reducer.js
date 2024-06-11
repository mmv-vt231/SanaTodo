export default function reducer(state, action) {
  switch (action.type) {
    case "ADD_TODO": {
      const id = state.index + 1;

      const { text, endDate, category } = action.payload;

      return {
        ...state,
        todos: [
          ...state.todos,
          {
            id,
            text,
            endDate,
            category,
            completed: false,
          },
        ],
        index: id,
      };
    }

    case "TOGGLE_TODO": {
      return {
        ...state,
        todos: state.todos.map(todo =>
          todo.id == action.payload.id ? { ...todo, completed: !todo.completed } : todo
        ),
      };
    }

    case "DELETE_TODO": {
      return {
        ...state,
        todos: state.todos.filter(todo => todo.id != action.payload.id),
      };
    }

    case "CHANGE_STORAGE": {
      return {
        ...state,
        storage: action.payload.storage,
      };
    }

    default:
      return state;
  }
}
