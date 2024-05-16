import React, { useState } from "react";

const LoginPage = () => { 

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");

    const [error, setError] = useState("");

    function submitHandler() {
        if(!email.length || !password.length) {
            setError("Trebuie sa completezi toate campurile");
        } else {
            axios.post("http://localhost:5224/api/v1/users/login", {
                email,
                password
            }).then((response) => {
                console.log(response);
            }).catch((error) => {
                console.log(error);
            });
        }
    }

    return (
        <>
            <h1> Intra in cont </h1>

            <div className="login-form">
                <input type="text" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)}/>
                <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)}/>
                <button onClick={submitHandler}>Login</button>
            </div>
        </>
    )
}

export default LoginPage;