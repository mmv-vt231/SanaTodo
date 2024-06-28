import { store$, StoreContext } from "./store/rxStore";
import { useSubscribe } from "./hooks/useSubscribe";

import Todo from "./components/Todo";

import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { useEffect } from "react";

function App() {
  const store = useSubscribe(store$);

  useEffect(() => {
    console.log(store);
  }, [store]);

  return (
    <StoreContext.Provider value={store}>
      <div className="m-auto mt-5" style={{ maxWidth: "600px" }}>
        <h1 className="display-6 text-center">Todo</h1>
        <Todo />
      </div>
    </StoreContext.Provider>
  );
}

export default App;
