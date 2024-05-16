import React, { useState } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { loginUser } from '../features/auth/authSlice';

const LoginPage = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const authStatus = useSelector((state) => state.auth.status);
  const authError = useSelector((state) => state.auth.error);

  const submitHandler = (e) => {
    e.preventDefault();

    if (!email.length || !password.length) {
      setError("Trebuie sa completezi toate campurile");
    } else {
      dispatch(loginUser({ email, password }))
        .then((response) => {
          if (response.meta.requestStatus === 'fulfilled') {
            navigate("/");
          } else {
            setError(response.error.message);
          }
        });
    }
  };

  return (
    <>
      <h1>Intra in cont</h1>

      <form className="login-form" onSubmit={submitHandler}>
        <input
          type="text"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit">Login</button>
      </form>

      {error && <p>{error}</p>}
      {authStatus === 'loading' && <p>Loading...</p>}
      {authStatus === 'failed' && <p>{authError}</p>}
    </>
  );
};

export default LoginPage;