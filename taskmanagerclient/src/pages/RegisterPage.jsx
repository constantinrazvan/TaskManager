import React, { useState } from "react";
import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { registerUser } from '../features/auth/authSlice';

const RegisterPage = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const registrationStatus = useSelector((state) => state.auth.status);
  const registrationError = useSelector((state) => state.auth.error);

  const onSubmit = (e) => {
    e.preventDefault();

    if (!username.length || !password.length || !email.length) {
      setError("Trebuie sa completezi toate campurile");
    } else {
      dispatch(registerUser({ username, password, email }))
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
      <h1>Bine ai venit!</h1>
      
      <form onSubmit={onSubmit}>
        <input
          type="text"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <input
          type="email"
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
        <button type="submit">Register</button>
      </form>

      {error && <p>{error}</p>}
      {registrationStatus === 'loading' && <p>Loading...</p>}
      {registrationStatus === 'failed' && <p>{registrationError}</p>}
    </>
  );
};

export default RegisterPage;
