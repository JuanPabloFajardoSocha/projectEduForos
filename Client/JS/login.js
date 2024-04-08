function mostrarContrasena() {
    var inputPassword = document.getElementById("password");

    if (inputPassword.type === "password") {
        inputPassword.type = "text";
        inputPassword.focus()
    } else {
        inputPassword.type = "password";
        inputPassword.focus()
    }
}

document.addEventListener("DOMContentLoaded", function () {
    const formLogin = this.getElementById("form_login");
    formLogin.addEventListener("submit", function (event) {
        event.preventDefault();
        login();
    })
})


function login() {

    const url = "https://localhost:7281/api/login";
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;
    const info = document.getElementById("info");

    const data = {
        InstitutionalEmail: email,
        Password: password
    };

    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    fetch(url, options)
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            if (data.status == undefined) {
                sessionStorage.setItem("idUser", data.idUser);
                switch (data.userType) {
                    case "Administrador":
                        window.location.href = "../VIEWS/administrator.html";
                        break;
                    case "Profesor":
                        window.location.href = "../VIEWS/teacher.html";
                        break;
                    case "Estudiante":
                        window.location.href = "../VIEWS/student.html";
                        break;

                }
            } else {
                info.textContent = data.title
            }
        });

}



