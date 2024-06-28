import { useSubscribe } from "./useSubscribe";

export const useStore = initialState => {
  const store$ = new BehaviorSubject(initialState);
  const state = useSubscribe(store$);

  const getTasks = () => {
    fetch(url, {
      method: "POST",
      headers,
      body: JSON.stringify({
        query,
      }),
    })
      .then(response => response.json())
      .then(data => {
        store$.next({ ...store$.getValue(), todos: data });
      });
  };

  return { store: state, getTasks };
};
