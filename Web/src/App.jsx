import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

import Todo from "./components/Todo";

function App() {
  return (
    <>
      <div className="m-auto mt-5" style={{ maxWidth: "600px" }}>
        <h1 className="display-6 text-center">Todo</h1>
        <Todo />
      </div>
    </>
  );
}

export default App;
