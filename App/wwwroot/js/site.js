function httpRequest(url, type = "GET", data = {}, onSuccess = () => { }) {
    const storage = localStorage.getItem("storage") ?? "";

    $.ajax({
        url,
        type,
        headers: {
            storage
        },
        data,
        success: onSuccess
    })
}

function completeTask(id, completed) {
    const data = {
        id,
        completed
    };

    httpRequest(
        "Home/CompleteTask",
        "POST",
        data,
        () => window.location.reload()
    ); 
}

function changeStorage() {
    const storage = document.getElementById("storage").value;
    const data = {
        storage
    };

    httpRequest(
        "Home/ChangeStorage",
        "POST",
        data,
        () => {
            localStorage.setItem("storage", storage);
            window.location.reload();
        }
    );
}

function deleteTask(id) {
    const data = {
        id,
    };

    httpRequest(
        "Home/DeleteTask",
        "DELETE",
        data,
        () => window.location.reload()
    );
}
