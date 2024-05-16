import React, { useState } from "react";

const AddTodo = () => {
    
    const [title, setTitle] = useState("");
    const [isDone, setIsDone] = useState(false);

    const [error, setError] = useState("");

    const [userId, setUserId] = useState();

    function onSubmit() {
        if(!title.length) {
            setError("Trebuie sa oferi un titlu!");
        } else {
            userId = 1;
            axios.post("http://localhost:5224/api/v1/todos/newTodo", {
                title,
                isDone,
                userId
            }).then((response) => {
                console.log(response);
            }).catch((error) => {
                console.log(error);
            });
        }
    }

    return (
        <>
            <h1> Add Todo </h1>

            <form onSubmit={onSubmit()}>
                <input type="text" value={title} onChange={(e) => setTitle(e.target.value)} />
                <input type="checkbox" value={isDone} onChange={(e) => setIsDone(e.target.value)} />
                <button type="submit"> Add Todo </button>
            </form>
        </>
    )
}

export default AddTodo;