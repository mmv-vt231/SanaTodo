function completeTask(id, completed) {
    $.ajax({
        url: "todo/complete",
        type: "POST",
        data: {
            id,
            completed
        },
        success: () => {
            window.location.reload();
        }
    })
}

function deleteTask(id) {
    $.ajax({
        url: "todo/delete",
        type: "DELETE",
        data: {
            id,
        },
        success: () => {
            window.location.reload();
        }
    })
}
