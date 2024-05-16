import React from "react";
import Todos from "./pages/Todos";
import AddTodo from "./pages/AddTodo";
import RegisterPage from "./pages/RegisterPage";
import LoginPage from "./pages/LoginPage";

function App() {
  return (
    <>
      <Router>
        <Routes>
          <Route path="/todos" element={<Todos />} />
          <Route path="/addTodo" element={<AddTodo />} />
          <Route path="/register" element={<RegisterPage />} />
          <Route path="/login" element={<LoginPage />} />
        </Routes>
      </Router>
    </>
  );
}

export default App;
