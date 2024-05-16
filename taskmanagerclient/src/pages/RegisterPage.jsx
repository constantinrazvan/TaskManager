import React, {useState} from "react";

const RegisterPage = () => { 

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [email, setEmail] = useState("");

    const [error, setError] = useState("");

    function onSubmit() {
        if(!username.length || !password.length || !email.length) {
            setError("Trebuie sa completezi toate campurile");
        } else {
            axios.post("http://localhost:5224/api/v1/users/newUser", {
                username,
                password,
                email
            }).then((response) => {
                console.log(response);
            }).catch((error) => {
                console.log(error);
            });
        }
    }

    return (
        <>
        <h1> Bine ai venit!</h1>
        
        <form onSubmit={onSubmit}>
            <input type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)}/>
            <input type="text" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)}/>
            <input type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)}/>
            <button type="submit">Register</button>
        </form>
        </>
    )
}

export default RegisterPage;