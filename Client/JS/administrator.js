

document.addEventListener("DOMContentLoaded", () => {

    if (sessionStorage.getItem("idUser") === null) {
        window.location.href = "../VIEWS/login.html"
    }
    showPerfilInformation();
    let btnShowAdministrators = document.getElementById("btn_administrators");
    let btnShowProfessors = document.getElementById("btn_professors");
    let btnShowStudents = document.getElementById("btn_Students");
    let btnRegisterUser = document.getElementById("btn_user_register");
    let btnEditPerfil = document.getElementById("edit_perfil");
    let btnRegisterCourses = document.getElementById("btn_register_course");
    let btnShowCourses = document.getElementById("show_courses");
    let btnRegisterSubjects = document.getElementById("register_subjects");
    let btnShowSubjects = document.getElementById("show_subjects");
    let btnCloseSession = document.getElementById("btn_close_session");

    let contentDiv = document.getElementById("content_div");

    var btnMenuUser = document.getElementById("btn_user_menu");
    var btnMenuUsers = document.getElementById("btn_users_submenu");
    var btnMenuCourses = document.getElementById("btn_courses_submenu");
    var btnSubmenuSubjects = document.getElementById("btn_subjects_submenu");
    var btnMenu = document.getElementById("nav-toggle");

    var menuUser = document.getElementById("menu_user");
    var menuSubjects = document.getElementById("submenu_subjects");
    var menuCourses = document.getElementById("submenu_courses");
    var menuUsers = document.getElementById("submenu_users");
    var nav = document.getElementById("nav-content");

    btnMenuUser.addEventListener("click", () => {
        menuUser.classList.toggle("hidden");
        menuUsers.classList.add("hidden");
        menuCourses.classList.add("hidden");
        menuSubjects.classList.add("hidden");
    })

    menuUser.addEventListener("mouseleave", () => {
        menuUser.classList.add("hidden");
    })

    btnMenuUsers.addEventListener("click", () => {

        menuUsers.classList.toggle("hidden");
        menuUser.classList.add("hidden");
        menuCourses.classList.add("hidden");
        menuSubjects.classList.add("hidden");
    })

    menuUsers.addEventListener("mouseleave", () => {
        menuUsers.classList.add("hidden");
    })

    btnMenuCourses.addEventListener("click", () => {

        menuCourses.classList.toggle("hidden");
        menuUsers.classList.add("hidden");
        menuUser.classList.add("hidden");
        menuSubjects.classList.add("hidden");
    })

    menuCourses.addEventListener("mouseleave", () => {
        menuCourses.classList.add("hidden");
    })

    btnSubmenuSubjects.addEventListener("click", () => {
        menuSubjects.classList.toggle("hidden");
        menuCourses.classList.add("hidden");
        menuUsers.classList.add("hidden");
        menuUser.classList.add("hidden");
    })

    menuSubjects.addEventListener("mouseleave", () => {
        menuSubjects.classList.add("hidden");
    })


    btnMenu.addEventListener("click", () => {

        if (contentDiv.classList.contains("top-20") && !contentDiv.classList.contains("top-80")) {
            contentDiv.classList.remove("top-20");
            contentDiv.classList.add("top-80");
        } else {
            contentDiv.classList.remove("top-80");
            contentDiv.classList.add("top-20");
        }

        if (nav.classList.contains("hidden")) {
            nav.classList.remove("hidden")
        } else {
            nav.classList.add("hidden")
        }
    })

    btnShowAdministrators.addEventListener("click", () => {
        const userType = "Administrador";
        getUsers(userType);
    })


    btnShowProfessors.addEventListener("click", () => {
        const userType = "Profesor";
        btnMenuUsers.click();
        getUsers(userType);
    })


    btnShowStudents.addEventListener("click", () => {
        const userType = "Estudiante";
        btnMenuUsers.click();
        getUsers(userType);
    })

    btnRegisterUser.addEventListener("click", () => {
        btnMenuUsers.click();

        fetch('../VIEWS/formRegister.html')
            .then(response => {
                if (response.ok) return response.text()
                else throw new Error("Error al cargar la pagina")
            })
            .then(html => {
                contentDiv.innerHTML = html;
                registerUser();
            })
            .catch(error => console.log(error));
    })

    btnEditPerfil.addEventListener("click", (event) => {
        btnMenuUser.click();
        const idUser = sessionStorage.getItem("idUser");
        showEditForm(idUser, contentDiv);
    });

    btnShowCourses.addEventListener("click", () => {
        showCourses();
    })


    btnRegisterCourses.addEventListener("click", () => {

        fetch('../VIEWS/formRegisterCourse.html')
            .then(response => {
                if (response.ok) return response.text()
                else throw new Error("Error al cargar la pagina")
            })
            .then(html => {
                contentDiv.innerHTML = html;
                registerCourse();

            })
            .catch(error => console.log(error));
    })

    btnRegisterSubjects.addEventListener("click", () => {

        fetch('../VIEWS/formRegisterSubjects.html')
            .then(response => {
                if (response.ok) return response.text()
                else throw new Error("Error al cargar la pagina")
            })
            .then(html => {
                contentDiv.innerHTML = html;
                RegisterSubjects();
            })
            .catch(error => console.log(error));
    })

    btnShowSubjects.addEventListener("click", () => {
        ShowSubjects();
    })

    btnCloseSession.addEventListener("click", (event) => {
        event.preventDefault();
        sessionStorage.removeItem("idUser")
        window.location.href = "../VIEWS/login.html";
    })

    btnShowAdministrators.click();
});


document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('btn_add')) {
        let idCourse = event.target.getAttribute("id_course");
        let idUser = event.target.getAttribute("id_user");

        const url = "https://localhost:7281/api/Course/addUserToCourse";
        const data = {
            IdCourse: idCourse,
            IdUser: idUser
        }
        const options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        };

        fetch(url, options)
            .then((response) => {
                if (response.ok) {
                    alertify.success("Curso asignado correctamente");
                    getUsers(userType);
                } else {
                    return response.json();
                }
            }).then((data) => {
                alertify.error(data.title);
            })

    }
});


document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('users_in_course')) {
        let idCourse = event.target.getAttribute("id_course_teacher");
        UsersInCourse(idCourse);
    }
})

document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('btn_delete_course_user')) {
        let idCourse = event.target.getAttribute("id_course");
        let idUser = event.target.getAttribute("id_user");

        const url = "https://localhost:7281/api/Course/DeleteUserToCourse";
        const data = {
            IdCourse: idCourse,
            IdUser: idUser
        }

        const options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        };

        fetch(url, options)
            .then((response) => {
                if (response.ok) {
                    alertify.success("Registro eliminado exitosamente");
                    UsersInCourse(idCourse);
                } else {
                    alertify.error("No se pudo eliminar el registro intentelo nuevamente")
                }
            })
    }
})

document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('btn_delete_course_subject')) {
        let idCourse = event.target.getAttribute("id_course_subject");
        let idSubject = event.target.getAttribute("id_subject");

        const url = "https://localhost:7281/api/CourseSubject/DeleteSubjectToCourse";
        const data = {
            IdCourse: idCourse,
            IdSubject: idSubject
        }

        const options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        };

        fetch(url, options)
            .then((response) => {
                if (response.ok) {
                    alertify.success("Registro eliminado exitosamente");
                    SubjectsInCourse(idCourse);
                } else {
                    console.log(response.json());
                    alertify.error("No se pudo eliminar el registro intentelo nuevamente")
                }
            })
    }
})


document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('delete_subject')) {
        let idSubjectDelete = event.target.getAttribute("id_subject_delete");

        alertify.confirm("¿Seguro?", 'Esta seguro de eliminar esta asignatura?', () => {

            const url = "https://localhost:7281/api/Subject/DeleteSubject";
            const data = {
                idSubject: idSubjectDelete,
            }

            const options = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            };

            fetch(url, options)
                .then((response) => {
                    if (response.ok) {
                        alertify.success("Asignatura eliminada exitosamente");
                        ShowSubjects();
                    } else {
                        alertify.error("No se pudo eliminar!, Intentelo nuevamente")
                    }
                })

        }, () => { });
    }
})

document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('add_course_subject')) {
        let idSubjectDelete = event.target.getAttribute("id_subject");
        showCoursesToSubject(idSubjectDelete);

    }
})

document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('btn_add_Subject')) {
        let idSubject = event.target.getAttribute("id_subject");
        let idCourse = event.target.getAttribute("id_course");

        const url = "https://localhost:7281/api/CourseSubject/AddCourseToSubject";
        const data = {
            idCourse: idCourse,
            idSubject: idSubject
        }
        const options = {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(data)
        };

        fetch(url, options)
            .then((response) => {
                if (response.ok) {
                    alertify.success("Curso asignado correctamente");
                    ShowSubjects();
                } else {
                    return response.json();
                }
            }).then((data) => {
                alertify.error(data.title);
            })

    }
})


document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('subjects_in_course')) {
        let idCourse = event.target.getAttribute("id_course_subject");
        SubjectsInCourse(idCourse);
    }
});

function showEditForm(idUser) {
    let contentDiv = document.getElementById("content_div");
    fetch('../VIEWS/formEditPerfil.html')
        .then(response => {
            if (response.ok) return response.text();
            else throw new Error("Error al cargar la pagina");
        })
        .then(html => {
            contentDiv.innerHTML = html;
            editUser(idUser);
        })
        .catch(error => console.log(error))
}

function getUserById(idUser) {

    const url = "https://localhost:7281/api/user/GetUserById";

    const data = {
        IdUser: idUser,
    };

    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };
    return fetch(url, options)
}

function getUsers(userType) {

    const url = "https://localhost:7281/api/user/GetRegisteredUsers";

    const data = {
        UserType: userType
    };

    const options = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    };

    return fetch(url, options)
        .then(response => {
            if (response.ok) return response.json();
            else throw new Error("Error en la solicitud")
        })
        .then(data => {
            showUsers(data, userType);
        })
        .catch(error => {
            console.log(error)
        })



}


function showPerfilInformation() {
    let idUser = sessionStorage.getItem("idUser");
    let userImage = document.getElementById("img_photo_user");
    let name = document.getElementById("lbl_name");
    let email = document.getElementById("lbl_institutional_email");
    let userType = document.getElementById("lbl_user_type");

    getUserById(idUser)
        .then((response) => { return response.json() })
        .then((data) => {
            if (data.status == undefined) {
                userImage.src = data.urlPhoto;
                name.textContent = data.firtsName + " " + data.surName;
                email.textContent = data.institutionalEmail;
                userType.textContent = data.userType
            } else {
                console.log(data)
            }
        })
}

function registerUser() {

    let btnRegister = document.getElementById("btn_register");
    let info = document.getElementById("span_info");
    let photo = document.getElementById("input_photo");
    let userDocumentType = document.getElementById("select_document_type");
    let userDocument = document.getElementById("input_document_number");
    let firtsName = document.getElementById("input_firtsName");
    let surName = document.getElementById("input_surName");
    let age = document.getElementById("input_age");
    let telephone = document.getElementById("input_telephone");
    let institutionalEmail = document.getElementById("input_email_institutional");
    let password = document.getElementById("input_password");
    let userType = document.getElementById("select_user_type");
    let profession = document.getElementById("input_profession");

    let btnShowAdministrators = document.getElementById("btn_administrators");
    let btnShowProfessors = document.getElementById("btn_professors");
    let btnShowStudents = document.getElementById("btn_Students");


    btnRegister.addEventListener("click", (event) => {
        event.preventDefault();

        if (userDocumentType.value === "") {
            info.textContent = "El campo Tipo de Documento es requerido";
        } else if (userDocument.value === "") {
            info.textContent = "El campo No de Documento es requerido";
        } else if (firtsName.value === "") {
            info.textContent = "El campo Nombre es requerido";
        } else if (surName.value === "") {
            info.textContent = "El campo Apellido es requerido";
        } else if (age.value === "") {
            info.textContent = "El campo Edad es requerido";
        } else if (telephone.value === "") {
            info.textContent = "El campo Telefono es requerido";
        } else if (institutionalEmail.value === "") {
            info.textContent = "El campo Email Institucional es requerido";
        } else if (password.value === "") {
            info.textContent = "El campo Contraseña es requerido";
        } else if (userType.value === "") {
            info.textContent = "El campo Tipo de Usuario es requerido";
        } else if (profession.value === "") {
            info.textContent = "El campo Tipo de Profesion es requerido";
        } else {
            info.textContent = "";
            const url = "https://localhost:7281/api/user/Register";

            const formData = new FormData();
            formData.append('UserDocumentType', userDocumentType.value);
            formData.append('UserDocument', userDocument.value);
            formData.append('FirstName', firtsName.value);
            formData.append('Surname', surName.value);
            formData.append('Age', age.value);
            formData.append('Telephone', telephone.value);
            formData.append('InstitutionalEmail', institutionalEmail.value);
            formData.append('Password', password.value);
            formData.append('UserType', userType.value);
            formData.append('Profession', profession.value);
            if (photo.files.length > 0) {
                formData.append('UrlPhoto', photo.files[0]);
            } else {
                formData.append('UrlPhoto', null);
            }

            const options = {
                method: 'POST',
                body: formData
            };

            fetch(url, options)
                .then(response => {
                    if (response.ok) {
                        alertify.success('Usuario registrado con exito');
                        switch (userType.value) {
                            case "Administrador":
                                getUsers("Administrador")
                                break;
                            case "Profesor":
                                getUsers("Profesor")
                                break;
                            case "Estudiante":
                                getUsers("Estudiante")
                                break;
                        }
                        return response.json();
                    } else {
                        return response.json();
                    }

                })
                .then(data => {
                    if (data.status == 400) {
                        info.textContent = data.title;
                    } else {
                        info.textContent = "";
                    }

                })




        }
    })


    userType.addEventListener("change", () => {
        if (userType.value != "Profesor") {
            profession.disabled = true;
            profession.value = "N/A"
        } else {
            profession.disabled = false;
            profession.value = ""
        }
    })

    photo.addEventListener("change", () => {
        let file = photo.files[0];
        if (file) {
            let image = document.getElementById("image_form_register");
            const reader = new FileReader();
            reader.readAsDataURL(file);

            reader.onload = function () {
                image.src = reader.result;
            };
        }
    })





}

function editUser(idUser) {
    let idSessionUser = sessionStorage.getItem("idUser");
    let imagePerfil = document.getElementById("img_perfil");
    let photo = document.getElementById("input_photo_perfil");
    let documentTypePerfil = document.getElementById("select_document_type_perfil");
    let userTypePerfil = document.getElementById("select_user_type_perfil");
    let documentNumber = document.getElementById("input_document_perfil");
    let firtsName = document.getElementById("input_firtsName_perfil");
    let surName = document.getElementById("input_surName_perfil");
    let age = document.getElementById("input_age_perfil");
    let telephone = document.getElementById("input_telephone_perfil");
    let institutionalEmail = document.getElementById("input_email_institutional_perfil");
    let personalEmail = document.getElementById("input_email_personal_perfil");
    let password = document.getElementById("input_password_perfil");
    let passwordConfirmation = document.getElementById("input_password_confirmation_perfil");
    let profession = document.getElementById("input_profession_perfil");
    let userType = document.getElementById("select_user_type_perfil");
    let info = document.getElementById("span_info_perfil");
    let btnEditPerfil = document.getElementById("btn_edit_perfil");
    let newPassword;


    getUserById(idUser)
        .then((response) => { return response.json() })
        .then((data) => {

            if (data.status === undefined) {

                let divPassword = document.getElementById("div_password");
                Array.from(documentTypePerfil.options).forEach(option => {
                    if (option.value === data.userDocumentType) {
                        option.selected = true;
                    }
                });

                Array.from(userTypePerfil.options).forEach(option => {
                    if (option.value === data.userType) {
                        option.selected = true;
                    }
                });

                if (data.id != idSessionUser) {

                    firtsName.disabled = true;
                    surName.disabled = true;
                    age.disabled = true;
                    telephone.disabled = true;
                    personalEmail.disabled = true;
                    divPassword.classList.add("hidden");
                    profession.disabled = true;
                }

                if (data.urlPhoto != null) {
                    imagePerfil.src = data.urlPhoto
                } else {
                    imagePerfil.src = "https://png.pngtree.com/background/20230522/original/pngtree-one-of-a-small-futuristic-robot-on-a-black-background-picture-image_2689979.jpg"
                }

                documentNumber.value = data.userDocument;
                firtsName.value = data.firtsName;
                surName.value = data.surName;
                age.value = data.age;
                telephone.value = data.telephone;
                institutionalEmail.value = data.institutionalEmail;
                personalEmail.value = data.personalEmail;
                profession.value = data.profession;
            }





        })

    photo.addEventListener("change", () => {
        let file = photo.files[0];
        if (file) {
            const reader = new FileReader();
            reader.readAsDataURL(file);

            reader.onload = function () {
                imagePerfil.src = reader.result;
            };
        }
    })

    btnEditPerfil.addEventListener("click", () => {

        if (documentNumber.value === "") {
            info.textContent = "El campo Documento es requerido"
        } else if (firtsName.value === "") {
            info.textContent = "El campo nombre es requerido"
        } else if (surName.value === "") {
            info.textContent = "El campo Apellido es requerido"
        } else if (age.value === "") {
            info.textContent = "El campo Edad es requerido"
        } else if (telephone.value === "") {
            info.textContent = "El campo Telefono es requerido"
        } else if (institutionalEmail.value === "") {
            info.textContent = "El campo Email Institucional es requerido"
        } else if (profession.value === "") {
            info.textContent = "El campo Profession Institucional es requerido"
        } else if (password.value != passwordConfirmation.value) {
            info.textContent = "Las contraseñas no son iguales"
        } else {
            info.textContent = "";

            const url = `https://localhost:7281/api/user/EditUser/${idUser}`;
            const formData = new FormData();


            if (photo.files.length > 0) {
                console.log(photo.files[0]);
                formData.append('File', photo.files[0]);
            }

            if (password.value != "") {
                formData.append('Password', password.value);
            } else {
                formData.append('Password', null);
            }

            formData.append('UserDocumentType', documentTypePerfil.value);
            formData.append('UserDocument', documentNumber.value);
            formData.append('FirstName', firtsName.value);
            formData.append('Surname', surName.value);
            formData.append('Age', age.value);
            formData.append('Telephone', telephone.value);
            formData.append('InstitutionalEmail', institutionalEmail.value);
            formData.append('PersonalEmail', personalEmail.value);
            formData.append('UserType', userType.value);
            formData.append('Profession', profession.value);


            const options = {
                method: 'PUT',
                body: formData
            };

            fetch(url, options)
                .then(response => {
                    if (response.ok) {
                        alertify.success('Informacion de usuario modificada');
                        showPerfilInformation();
                        getUsers(userType.value);

                    } else {
                        return response.json();
                    }

                })
                .then(data => {
                    if (data != undefined) {
                        console.log(data)
                        info.textContent = data.title;
                    }

                })


        }
    })

}

function deleteUser(idUser, userType) {

    const url = "https://localhost:7281/api/user/delete";

    data = {
        IdUser: idUser
    }
    const options = {
        method: 'POST',
        headers: {
            'content-type': 'application/json',
        },
        body: JSON.stringify(data)
    };

    fetch(url, options)
        .then((response) => {
            if (response.ok) {
                alertify.success("Usuario eliminado con exito");
                getUsers(userType);
            } else {
                alertify.error("No se pudo eliminar el usuario, Intentelo de nuevo!")
                throw new Error("Falla en la solicitud");
            }
        })
        .catch((error) => {
            console.log(error);
        })
}

function showUsers(data, userType) {

    let contentDiv = document.getElementById("content_div");
    contentDiv.innerHTML = "";
    let users = data.users

    if (users.length != 0) {

        for (i = 0; i < users.length; i++) {

            if (users[i].urlPhoto === null) {
                users[i].urlPhoto = "https://png.pngtree.com/background/20230522/original/pngtree-one-of-a-small-futuristic-robot-on-a-black-background-picture-image_2689979.jpg"
            }
            let content;
            if (userType === "Administrador") {

                content =
                    `<div class="mt-2 md:mt-0 max-w-sm bg-gray-800 border border-white rounded-lg shadow mr-4 ml-4">
                
                <div class="flex justify-end px-4 pt-4">
                    <button 
                        class="dropdown-btn inline-block text-blue-400 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                        type="button">
                        <span class="sr-only">Open dropdown</span>
                        <svg class="w-5 h-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor"
                            viewBox="0 0 16 3">
                            <path
                                d="M2 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3Zm6.041 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3ZM14 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3Z" />
                        </svg>
                    </button>      
                     
                    <div id="menu_card"
                        class="dropdown_card top-16 absolute z-10 hidden text-base list-none bg-gray-700 divide-y divide-gray-100 rounded-lg shadow w-20 dark:bg-gray-700">
                        <ul class="py-2">
                            <li>
                            <button id_edit="${users[i].id}"
                                class="w-full btn_edit block px-4 py-2 text-sm text-gray-100 hover:bg-blue-400 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Editar</button>
                        
                            <li>
                                <button id_delete="${users[i].id}" rol="${userType}"
                                    class="w-full btn_delete block px-4 py-2 text-sm text-red-600 hover:bg-blue-400 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Delete</button>
                            </li>
                           
                        </ul>
                    </div>
                </div>

                <div class="flex flex-col items-center pb-4 w-80">
                    <img class="w-20 h-20 rounded-full shadow-lg" src="${users[i].urlPhoto}"
                        alt="Bonnie image" bg-white/>
                    <span class="text-gray-300 mb-1 mt-6 dark:text-white">Nombre: ${users[i].firtsName} ${users[i].surName}</span>
                    <span class="text-gray-300 mb-1  dark:text-white">Email: ${users[i].institutionalEmail}</span>
                    <span class="text-gray-300 mb-1  dark:text-gray-400">Documento: ${users[i].userDocument}</span>
                    <span class="text-gray-300 mb-1  dark:text-gray-400">Telefono: ${users[i].telephone}</span>
                    <span class="text-gray-300 mb-1  dark:text-white">Rol: ${userType}</
                </div>
            </div>`
            } else {
                content =
                    `<div class="mt-2 md:mt-0 max-w-sm bg-gray-800 border border-white rounded-lg shadow mr-4 ml-4">
                
                <div class="flex justify-end px-4 pt-4">
                    <button 
                        class="dropdown-btn inline-block text-blue-400 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                        type="button">
                        <span class="sr-only">Open dropdown</span>
                        <svg class="w-5 h-5" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="currentColor"
                            viewBox="0 0 16 3">
                            <path
                                d="M2 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3Zm6.041 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3ZM14 0a1.5 1.5 0 1 1 0 3 1.5 1.5 0 0 1 0-3Z" />
                        </svg>
                    </button>      
                     
                    <div id="menu_card"
                        class="dropdown_card top-16 absolute z-10 hidden text-base list-none bg-gray-700 divide-y divide-gray-100 rounded-lg shadow w-20 dark:bg-gray-700">
                        <ul class="py-2">
                            <li>
                            <button id_edit="${users[i].id}"
                                class="w-full btn_edit block px-4 py-2 text-sm text-gray-100 hover:bg-blue-400 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Editar</button>
                        
                            <li>
                                <button id_delete="${users[i].id}" rol="${userType}"
                                    class="w-full btn_delete block px-4 py-2 text-sm text-red-600 hover:bg-blue-400 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Delete</button>
                            </li>
                            
                            <li>                            
                                <button id_user="${users[i].id}" rol_user="${userType}"
                                    class=" btn_add_course w-full block px-4 py-2 text-sm text-gray-100 hover:bg-blue-400 dark:hover:bg-gray-600 dark:text-gray-200 dark:hover:text-white">Agregar Curso</button>
                            </li>
                        </ul>
                    </div>
                </div>

                <div class="flex flex-col items-center pb-4 w-80">
                    <img class="w-20 h-20 rounded-full shadow-lg" src="${users[i].urlPhoto}"
                        alt="Bonnie image" bg-white/>
                    <span class="text-gray-300 mb-1 mt-6 dark:text-white">Nombre: ${users[i].firtsName} ${users[i].surName}</span>
                    <span class="text-gray-300 mb-1  dark:text-white">Email: ${users[i].institutionalEmail}</span>
                    <span class="text-gray-300 mb-1  dark:text-gray-400">Documento: ${users[i].userDocument}</span>
                    <span class="text-gray-300 mb-1  dark:text-gray-400">Telefono: ${users[i].telephone}</span>
                    <span class="text-gray-300 mb-1  dark:text-white">Rol: ${userType}</
                </div>
            </div>`
            }

            contentDiv.innerHTML += content;

        }
        optionsCard(userType);

    } else {
        let content = `<p>No Hay Usuarios Registrados</p>`;
        contentDiv.innerHTML = content
    }

}

function registerCourse() {

    let btnRegisterCourse = document.getElementById("btn_course_register");
    let nameCourse = document.getElementById("input_name_course");
    let descriptionCourse = document.getElementById("input_description_course");
    let infoCourse = document.getElementById("info_course");
    btnRegisterCourse.addEventListener("click", (event) => {
        event.preventDefault();

        if (nameCourse.value === "") {
            infoCourse.textContent = "El campo Nombre es requerido";
        } else {
            infoCourse.textContent = "";
            const url = "https://localhost:7281/api/Course/Create";

            data = {
                Name: nameCourse.value,
                Description: descriptionCourse.value
            }
            const options = {
                method: 'POST',
                headers: {
                    'content-type': 'application/json',
                },
                body: JSON.stringify(data)
            };

            fetch(url, options)
                .then((response) => {
                    if (response.ok) {
                        alertify.success("Curso creado con exito!");
                        showCourses();
                    } else {
                        return response.json();
                    }
                })
                .then((data) => {
                    if (data != undefined) {
                        infoCourse.textContent = data.title
                    }
                })
                .catch((error) => {
                    console.log(error);
                })
        }
    })
}

function optionsCard(userType) {

    const dropdownButtons = document.querySelectorAll('.dropdown-btn');
    dropdownButtons.forEach(button => {
        button.addEventListener('click', () => {
            const menu = button.parentNode.querySelector("#menu_card");
            menu.classList.toggle("hidden");
        });

        const menu = button.parentNode.querySelector("#menu_card");
        menu.addEventListener("mouseleave", () => {
            const menu = button.parentNode.querySelector("#menu_card");
            menu.classList.add("hidden");
        })
        const btnEdit = menu.querySelector(".btn_edit");
        const btnDelete = menu.querySelector(".btn_delete");


        if (userType != "Administrador") {
            let btnAddCourse = menu.querySelector(".btn_add_course");
            btnAddCourse.addEventListener("click", () => {
                menu.classList.toggle("hidden");
                const idUser = btnAddCourse.getAttribute("id_user");
                showCoursesToUser(idUser, userType);
            })

        }

        btnEdit.addEventListener("click", () => {

            menu.classList.toggle("hidden");
            const idEdit = btnEdit.getAttribute('id_edit');
            showEditForm(idEdit);

        })

        btnDelete.addEventListener("click", () => {
            menu.classList.toggle("hidden");
            const idDelete = btnDelete.getAttribute("id_delete");
            const userType = btnDelete.getAttribute("rol");
            alertify.confirm("¿Seguro?", 'Esta seguro de eliminar este usuario?', () => { deleteUser(idDelete, userType) }
                , function () { });

        })





    });



}

function showCourses() {
    let contentDiv = document.getElementById("content_div");
    contentDiv.innerHTML = "";
    const url = "https://localhost:7281/api/Course/List";

    const options = {
        method: 'GET'
    };

    fetch(url, options)
        .then((response) => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Falla en la solicitud");
            }
        })
        .then((data) => {
            if (data != null) {
                let description = "N/A";
                data.forEach(course => {
                    if (data.description != "") {
                        description = course.description;
                    }
                    let content =
                        `<div class="mt-4 max-w-sm bg-gray-800 border border-white rounded-lg shadow mr-4 ml-4">
                
                        <div class="flex flex-col items-center pb-4 mt-8 pl-2 pr-2">
                            <img class="w-12 h-12 rounded-full shadow-lg mb-4" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-yJFPMLlp5dfCdu9XabMm72glyYJA1aihhw&usqp=CAU"
                                alt="Bonnie image" bg-white/>
                            
                            <span class="w-60 text-center inline-block break-words text-gray-300 text-xl mb-1 dark:text-white">Nombre: ${course.name} </span>
                            
                            <span class="w-60 h-12 mt-6 text-center inline-block break-words text-gray-300 mb-1 dark:text-white">Descripcion: ${description}</span>

                        </div>

                        <div class="flex justify-center px-4 mb-2">

                            <button  id_course_teacher="${course.id}"
                                class=" users_in_course m-2 h-8 inline-block text-l text-blue-400 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                                type="button">Integrantes 
                                <i class="fa fa-chalkboard-teacher fa-l"></i> 
                            </button>

                            <button  id_course_subject="${course.id}"
                                class=" subjects_in_course m-2 h-8 inline-block text-l text-blue-400 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                                type="button">Asignaturas
                                <i class="fa fa-chalkboard-teacher fa-l"></i> 
                            </button>                         
                           
                        </div>

                        <div class="flex justify-center px-4 mb-2">                          

                            <button  idCourse="${course.id}"
                                class=" delete_course mb-2 h-8 inline-block text-l text-red-800 dark:text-gray-400 hover:bg-red-300 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                                type="button">Eliminar
                                <i class="fa fa-trash fa-l"></i>  

                            </button>
                           
                        </div>
                       
                    </div>`
                    contentDiv.innerHTML += content;

                });

            } else {
                contentDiv.innerHTML = "No hay cursos registrados";
            }
            deleteCourse();

        })
        .catch((error) => {
            console.log(error);
        })


}

function deleteCourse() {
    document.addEventListener("click", (event) => {
        if (event.target && event.target.classList && event.target.classList.contains('delete_course')) {
            const idCourse = event.target.getAttribute("idCourse");
            alertify.confirm("¿Seguro?", 'Esta seguro de eliminar este curso?', () => {

                const url = `https://localhost:7281/api/Course/Delete/${idCourse}`;

                const options = {
                    method: 'DELETE',
                    headers: {
                        'content-type': 'application/json',
                    },
                };

                fetch(url, options)
                    .then((response) => {
                        if (response.ok) {
                            alertify.success("Curso eliminado con exito");
                            showCourses();
                        } else {
                            alertify.error("No se pudo eliminar el Curso, Intentelo de nuevo!")
                            throw new Error("Falla en la solicitud");
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                    })

            }
                , function () { });

        }
    })
}

function showCoursesToUser(idUser, userType) {
    let contentDiv = document.getElementById("content_div");
    const url = "https://localhost:7281/api/Course/List";


    const options = {
        method: 'GET'
    };

    fetch(url, options)
        .then((response) => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Falla en la solicitud");
            }
        })
        .then((data) => {
            let table =
                `<div class="mt-4 mr-2 ml-2 w-1/2 md:w-1/2 bg-gray-800 border border-white rounded-lg" >
                    <table class="w-full text-gray-500 dark:text-gray-400">
                        <thead>

                            <tr>
                                <th class="text-center text-lg text-blue-300">Nombre</th>
                                <th class="text-center text-lg text-blue-300">Agregar</th>                               
                            </tr>
                        </thead>
                        
                        <tbody>`;

            data.forEach(course => {

                table += `<tr class="text-center">
                                     <td class="text-white">${course.name} </td>
                                     <td> <button id_course="${course.id}" id_user="${idUser}" class="btn_add fa fa-plus px-4 py-2 text-green-600 hover:bg-green-100 rounded-full"></button></></td>
                                 </tr>`
            });
            table +=
                `</tbody>
                    </table>
                </div>`
            contentDiv.innerHTML = "";
            contentDiv.innerHTML = table;

        })
        .catch((error) => {
            console.log(error);
        })
}

function UsersInCourse(idCourse) {

    const url = "https://localhost:7281/api/Course/getUsersByCourse";
    const data = {
        IdCourse: idCourse
    }

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
            console.log(data.users);
            let contentDiv = document.getElementById("content_div");
            if (data.users != undefined) {
                let table =
                    `<div class="mt-4 mr-2 ml-2 w-1/2 md:w-1/2 bg-gray-800 border border-white rounded-lg" >
                        <table class="w-full text-gray-500 dark:text-gray-400">
                            <thead>

                                <tr>
                                    <th class="text-center text-lg text-blue-300">Documento</th>
                                    <th class="text-center text-lg text-blue-300">Nombre</th> 
                                    <th class="text-center text-lg text-blue-300">Tipo de Usuario</th>
                                    <th class="text-center text-lg text-blue-300">Eliminar</th>
                                </tr>
                            </thead>

                            <tbody>`;

                data.users.forEach(user => {

                    table += `<tr class="text-center">
                                         <td class="text-white">${user.documentNumber} </td>
                                         <td class="text-white">${user.firtsName + " " + user.surName} </td>
                                         <td class="text-white">${user.userType} </td>
                                         <td> <button id_course="${idCourse}" id_user="${user.idUser}" class="btn_delete_course_user fa fa-trash px-4 py-2 text-red-600 hover:bg-green-100 rounded-full"></button></></td>
                                     </tr>`



                });
                table +=
                    `</tbody>
                        </table>
                    </div>`

                contentDiv.innerHTML = "";
                contentDiv.innerHTML = table;
            } else {
                contentDiv.innerHTML = "No hay usuarios asignados a este curso"
            }


        });

}

function RegisterSubjects() {
    let btnRegisterSubject = document.getElementById("btn_subjects_register");
    btnRegisterSubject.addEventListener("click", (event) => {
        event.preventDefault();
        let inputNameSubject = document.getElementById("input_name_subject");
        let infoSubject = document.getElementById("info_subject");

        if (inputNameSubject.value === "") {
            infoSubject.textContent = "El campo Nombre es requerido";
        } else {
            infoSubject.textContent = "";
            const url = "https://localhost:7281/api/Subject/Register";
            data = {
                Name: inputNameSubject.value
            }

            const option = {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            }

            fetch(url, option)
                .then((response) => {
                    if (response.ok) {
                        alertify.success("Asignatura registrada con exito");
                        ShowSubjects();
                    } else {
                        return response.json()
                    }
                })
                .then((data) => {
                    infoSubject.textContent = data.title;
                })
        }
    })
}

function ShowSubjects() {

    let contentDiv = document.getElementById("content_div");
    const url = "https://localhost:7281/api/Subject/GetSubjects";

    const option = {
        method: 'GET',
    }

    fetch(url, option)
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            contentDiv.innerHTML = "";
            console.log(data);
            data.subjects.forEach(subject => {

                let content =
                    `<div class="mt-4 max-w-sm bg-gray-800 border border-white rounded-lg shadow mr-4 ml-4">
                
                        <div class="flex flex-col items-center pb-4 mt-8 pl-2 pr-2">
                            <img class="w-12 h-12 rounded-full shadow-lg mb-4" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-yJFPMLlp5dfCdu9XabMm72glyYJA1aihhw&usqp=CAU"
                                alt="Bonnie image" bg-white/>
                            
                            <span class="w-60 h-12 text-center inline-block break-words text-gray-300 text-xl mb-1 dark:text-white">Nombre: ${subject.nameSubject} </span>                           

                        </div>

                        <div class="flex justify-center px-4 mb-2">

                            <button  id_subject="${subject.idSubject}"
                                class=" add_course_subject m-2 h-8 inline-block text-l text-blue-400 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                                type="button">Añadir Curso 
                                <i class="fa fa-chalkboard-teacher fa-l"></i> 
                            </button>

                            <button  id_subject_delete="${subject.idSubject}"
                                class=" delete_subject m-2 h-8 inline-block text-l text-red-800 dark:text-gray-400 hover:bg-red-300 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                                type="button">Eliminar
                                <i class="fa fa-trash fa-l"></i>  

                            </button>
                           
                        </div>
                       
                    </div>`

                contentDiv.innerHTML += content;

            });

        });


}

function showCoursesToSubject(idSubject) {
    let contentDiv = document.getElementById("content_div");
    const url = "https://localhost:7281/api/Course/List";


    const options = {
        method: 'GET'
    };

    fetch(url, options)
        .then((response) => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error("Falla en la solicitud");
            }
        })
        .then((data) => {
            let table =
                `<div class="mt-4 mr-2 ml-2 w-1/2 md:w-1/2 bg-gray-800 border border-white rounded-lg" >
                    <table class="w-full text-gray-500 dark:text-gray-400">
                        <thead>

                            <tr>
                                <th class="text-center text-lg text-blue-300">Nombre</th>
                                <th class="text-center text-lg text-blue-300">Agregar</th>                               
                            </tr>
                        </thead>
                        
                        <tbody>`;

            data.forEach(course => {

                table += `<tr class="text-center">
                                     <td class="text-white">${course.name} </td>
                                     <td> <button id_course="${course.id}" id_subject="${idSubject}" class="btn_add_Subject fa fa-plus px-4 py-2 text-green-600 hover:bg-green-100 rounded-full"></button></></td>
                                 </tr>`
            });
            table +=
                `</tbody>
                    </table>
                </div>`
            contentDiv.innerHTML = "";
            contentDiv.innerHTML = table;

        })
        .catch((error) => {
            console.log(error);
        })
}

function SubjectsInCourse(idCourse) {

    const url = "https://localhost:7281/api/CourseSubject/GetSubjectsByCourse";
    const data = {
        IdCourse: idCourse
    }

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
            console.log(data.subjects);
            let contentDiv = document.getElementById("content_div");
            if (data.subjects != undefined) {
                let table =
                    `<div class="mt-4 mr-2 ml-2 w-1/2 md:w-1/2 bg-gray-800 border border-white rounded-lg" >
                        <table class="w-full text-gray-500 dark:text-gray-400">
                            <thead>

                                <tr>                                   
                                    <th class="text-center text-lg text-blue-300">Nombre</th>                                    
                                    <th class="text-center text-lg text-blue-300">Eliminar</th>
                                </tr>
                            </thead>

                            <tbody>`;

                data.subjects.forEach(subject => {

                    table += `<tr class="text-center">
                                        
                                         <td class="text-white">${subject.nameSubject}</td>
                                         <td> <button id_course_subject="${idCourse}" id_subject="${subject.idSubject}" class="btn_delete_course_subject fa fa-trash px-4 py-2 text-red-600 hover:bg-green-100 rounded-full"></button></></td>
                                     </tr>`
                });
                table +=
                    `</tbody>
                        </table>
                    </div>`

                contentDiv.innerHTML = "";
                contentDiv.innerHTML = table;
            } else {
                contentDiv.innerHTML = "No hay asignaturas asignadas a este curso"
            }


        });

}






















