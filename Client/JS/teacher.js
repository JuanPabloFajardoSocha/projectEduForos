
document.addEventListener("DOMContentLoaded", () => {
    showPerfilInformation();

    if (sessionStorage.getItem("idUser") === null) {
        window.location.href = "../VIEWS/login.html"
    }

    let btnEditPerfil = document.getElementById("edit_perfil");
    var btnMenuUser = document.getElementById("btn_user_menu");
    var menuUser = document.getElementById("menu_user");
    let contentDiv = document.getElementById("content_div");
    let btnCloseSession = document.getElementById("btn_close_session");
    let btnshowCourses = document.getElementById("btn_ver_mis_cursos");
    let btncreateForum = document.getElementById("btn_crear_foro");
    let btnshowForums = document.getElementById("btn_ver_foros");
    let btnVideoConferencia = document.getElementById("btn_videoconferencia");

    btnVideoConferencia.addEventListener("click", () => {
        let div = document.getElementById('content_div');
        div.innerHTML = `<h1 class="" ><button id="btn_close_jitsi" class="btn_close_jitsi mr-8 w-10 h-10"><i class="fa fa-times text-red-800 fa-xl">Cerrar</i></button></h1>`;
        const domain = 'meet.jit.si';
        const options = {
            roomName: 'nombre_de_la_sala',
            width: 800,
            height: 600,
            userInfo: {
                displayName: 'Nombre de Usuario'
            },
            parentNode: div
        };

        const api = new JitsiMeetExternalAPI(domain, options);
        CerrarJitsi(api);
    })


    btnMenuUser.addEventListener("click", () => {
        menuUser.classList.toggle("hidden");
    })

    btnEditPerfil.addEventListener("click", (event) => {
        btnMenuUser.click();
        const idUser = sessionStorage.getItem("idUser");
        showEditForm(idUser, contentDiv);
    });

    btnCloseSession.addEventListener("click", (event) => {
        event.preventDefault();
        sessionStorage.removeItem("idUser")
        window.location.href = "../VIEWS/login.html";
    })

    btnshowCourses.addEventListener("click", () => {
        const idUser = sessionStorage.getItem("idUser");
        ShowCoursesToUser(idUser);
    })

    btncreateForum.addEventListener("click", () => {
        showFormRegisterForum();
    })

    btnshowForums.addEventListener("click", () => {
        showForums();
    })

    btnshowForums.click();
});

function CerrarJitsi(api) {
    let btnshowForums = document.getElementById("btn_ver_foros");
    let btnCloseJitsi = document.getElementById("btn_close_jitsi");
    btnCloseJitsi.addEventListener("click", () => {
        api.dispose();
        btnshowForums.click();
    })
}


document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('delete_forum')) {

        let idForum = event.target.getAttribute("id_forum_delete");

        const url = `https://localhost:7281/api/Forums/DeleteForum/${idForum}`;

        const options = {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            },

        };

        fetch(url, options)
            .then((response) => {
                if (response.ok) {
                    alertify.success("Registro eliminado exitosamente");
                    showForums();
                } else {
                    console.log(response.json());
                    alertify.error("No se pudo eliminar el registro intentelo nuevamente")
                }
            })
    }
})

document.addEventListener("click", (event) => {

    if (event.target && event.target.classList && event.target.classList.contains('ver_foro')) {
        let idForum = event.target.getAttribute("id_forum");
        let nombreForo = event.target.getAttribute("nombreForo");
        let descripcionForo = event.target.getAttribute("descripcionForo")
        let fechaInicio = event.target.getAttribute("fechaInicio")
        let fechaFin = event.target.getAttribute("fechaFin");
        showForum(idForum, nombreForo, descripcionForo, fechaInicio, fechaFin);
    }

});

document.addEventListener("click", (event) => {
    if (event.target && event.target.classList && event.target.classList.contains('delete_message')) {
        let idMessage = event.target.getAttribute("id_message_delete")
        let idForum = event.target.getAttribute("idForum")
        let idUser = event.target.getAttribute("idUser")

        if (idUser != sessionStorage.getItem("idUser")) {
            alertify.error("No puede eliminar Comentarios de otros participantes")
        } else {
            DeleteMessage(idMessage, idForum);
        }
    }
});

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



    getUserById(idUser)
        .then((response) => { return response.json() })
        .then((data) => {

            if (data.status === undefined) {

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

function ShowCoursesToUser(idUser) {
    let contentDiv = document.getElementById("content_div");
    contentDiv.innerHTML = "";
    const url = "https://localhost:7281/api/Course/getCoursesByUser";

    const data = {
        idUser: idUser
    }

    const options = {
        method: 'POST',
        headers: {
            'content-Type': 'application/json'
        },
        body: JSON.stringify(data)
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

                data.courses.forEach(course => {
                    let content =
                        `<div class="mt-4 max-w-sm bg-gray-800 border border-white rounded-lg shadow mr-4 ml-4">
                
                        <div class="flex flex-col items-center pb-4 mt-8 pl-2 pr-2">
                            <img class="w-12 h-12 rounded-full shadow-lg mb-4" src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcT-yJFPMLlp5dfCdu9XabMm72glyYJA1aihhw&usqp=CAU"
                                alt="Bonnie image" bg-white/>
                            
                            <span class="w-60 text-center inline-block break-words text-gray-300 text-xl mb-1 dark:text-white">Nombre: ${course.name} </span>
                            
                            <span class="w-60 h-12 mt-6 text-center inline-block break-words text-gray-300 mb-1 dark:text-white">Descripcion: ${course.description}</span>

                        </div>

                    </div>`
                    contentDiv.innerHTML += content;

                });

            } else {
                contentDiv.innerHTML = "No hay cursos registrados";
            }

        })
        .catch((error) => {
            console.log(error);
        })
}

function showFormRegisterForum() {

    let contentDiv = document.getElementById("content_div");
    const urlGetCourses = "https://localhost:7281/api/Course/getCoursesByUser";
    let idUser = sessionStorage.getItem("idUser");

    fetch('../VIEWS/formRegisterForum.html')
        .then(response => {
            if (response.ok) return response.text();
            else throw new Error("Error al cargar la pagina");
        })
        .then(html => {

            contentDiv.innerHTML = html;
            let selectCourses = document.getElementById("select_course");

            const data = {
                idUser: idUser
            }

            const options = {
                method: 'POST',
                headers: {
                    'content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            };

            fetch(urlGetCourses, options)
                .then((response) => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        throw new Error("Falla en la solicitud");
                    }
                })
                .then((data) => {
                    data.courses.forEach(course => {
                        const option = document.createElement('option');
                        option.value = course.idCourse;
                        option.text = course.name;
                        selectCourses.appendChild(option);
                    });
                    CreateForum();
                });

        })
        .catch(error => console.log(error))
}

function CreateForum() {
    const urlGetSubjects = "https://localhost:7281/api/CourseSubject/GetSubjectsByCourse";
    let selectSubjects = document.getElementById("select_subjects");
    let selectCourses = document.getElementById("select_course");
    let btnRegisterForum = document.getElementById("btn_registrar_foro");
    let info = document.getElementById("span_info");
    selectSubjects.addEventListener("click", () => {
        if (selectCourses.value === "") {
            info.textContent = "Seleccione un curso";
        }
    })

    selectCourses.addEventListener("change", () => {
        if (selectCourses.value !== "") {
            info.textContent = "";

            const data = {
                idCourse: selectCourses.value
            }

            const options = {
                method: 'POST',
                headers: {
                    'content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            };

            fetch(urlGetSubjects, options)
                .then((response) => {
                    if (response.ok) {
                        return response.json();
                    } else {
                        alertify.error("No hay asignaturas registradas en el curso")
                    }
                })
                .then((data) => {
                    selectSubjects.innerHTML = "";
                    const option = document.createElement('option');
                    option.value = "";
                    option.text = "Seleccione:";
                    selectSubjects.appendChild(option);

                    data.subjects.forEach(subject => {
                        const option = document.createElement('option');
                        option.value = subject.idSubject;
                        option.text = subject.nameSubject;
                        selectSubjects.appendChild(option);
                    });

                })


        }

    })
    let photoForum = document.getElementById("input_photo_forum");


    btnRegisterForum.addEventListener("click", (event) => {
        event.preventDefault();

        let nameForum = document.getElementById("input_nombre");
        let descriptionForum = document.getElementById("input_descripcion");

        let startDate = document.getElementById("input_fecha_inicio");
        let endDate = document.getElementById("input_fecha_fin");
        let idCourse = document.getElementById("select_course");
        let idUser = sessionStorage.getItem("idUser");
        let idSubject = document.getElementById("select_subjects");

        if (nameForum.value === "") {
            info.textContent = "El campo Nombre es requerido";
        } else if (selectCourses.value === "") {
            info.textContent = "El campo curso es requerido";
        } else if (selectSubjects.value === "") {
            info.textContent = "Sin asignatura no se puede crear un foro";
        } else if (startDate.value === "") {
            info.textContent = "La Fecha de Inicio es requerida";
        } else if (endDate.value === "") {
            info.textContent = "La Fecha Fin es requerida";
        } else {
            info.textContent = "";

            url = "https://localhost:7281/api/Forums/CreateForum";
            const formData = new FormData();
            formData.append("NameForum", nameForum.value);
            formData.append("Description", descriptionForum.value);

            if (photoForum.files.length > 0) {
                formData.append('Photo', photoForum.files[0]);
            } else {
                formData.append('UrlPhoto', null);
            }
            formData.append("StartDate", startDate.value);
            formData.append("EndDate", endDate.value);
            formData.append("IdSubject", idSubject.value);
            formData.append("IdCourse", idCourse.value);
            formData.append("IdUser", idUser);

            const options = {
                method: 'POST',
                body: formData
            };

            fetch(url, options)
                .then((response) => {
                    if (response.ok) {
                        alertify.success("Foro creado con exito!")
                        showForums();
                    } else {
                        return response.json();
                    }
                })
                .then((data) => {
                    console.log(data)
                })

        }
    })

    photoForum.addEventListener("change", () => {
        let file = photoForum.files[0];
        if (file) {
            let image = document.getElementById("image_forum_register");
            const reader = new FileReader();
            reader.readAsDataURL(file);

            reader.onload = function () {
                image.src = reader.result;
            };
        }
    })











}

function showForums() {
    let contentDiv = document.getElementById("content_div");
    const url = "https://localhost:7281/api/Forums/ListForums";

    const data = {
        idUser: sessionStorage.getItem("idUser")
    }
    const option = {
        method: 'POST',
        headers: {
            'content-Type': 'application/json'
        },
        body: JSON.stringify(data)

    }

    fetch(url, option)
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            contentDiv.innerHTML = "";
            if (data.length > 0) {



                data.forEach(forum => {
                    if (forum.urlPhoto == null) {
                        forum.urlPhoto = "https://upload.wikimedia.org/wikipedia/commons/9/91/Foro_Asturias_logo.svg"
                    }

                    const fechaStart = new Date(forum.startDate);
                    const fechaEnd = new Date(forum.endDate);

                    const año = fechaStart.getFullYear();
                    const mes = ('0' + (fechaStart.getMonth() + 1)).slice(-2);
                    const dia = ('0' + fechaStart.getDate()).slice(-2);
                    const fechaFormateada = `${año}-${mes}-${dia}`;
                    forum.startDate = fechaFormateada;

                    const añoEnd = fechaEnd.getFullYear();
                    const mesEnd = ('0' + (fechaEnd.getMonth() + 1)).slice(-2);
                    const diaEnd = ('0' + fechaEnd.getDate()).slice(-2);
                    const fechaFormateadaEnd = `${añoEnd}-${mesEnd}-${diaEnd}`;
                    forum.endDate = fechaFormateadaEnd;

                    let content =
                        `<div class="mt-4 max-w-sm bg-gray-800 border border-white rounded-lg shadow mr-4 ml-4">
                
                        <div class="flex flex-col items-center pb-4 mt-8 pl-2 pr-2">
                            <img class="w-12 h-12 rounded-full shadow-lg mb-4" src="${forum.urlPhoto}"
                                alt="Bonnie image" bg-white/>
                            
                            <span class="h-8 text-center inline-block break-words text-gray-300 text-xl mb-1 dark:text-white">Nombre: ${forum.nameForum} </span>                           
                            <span class="w-80 text-center inline-block break-words text-gray-300 text-medium dark:text-white">Descripcion: ${forum.description} </span>                                                                                
                            <span class="mt-4 text-center inline-block break-words text-gray-300 text-sm dark:text-white">Fecha Inicio: ${forum.startDate} </span>
                            <span class=" text-center inline-block break-words text-gray-300 text-sm dark:text-white">Fecha Fin: ${forum.endDate} </span>        

                        </div>

                        <div class="flex justify-center px-4 mb-2">

                            <button  id_forum="${forum.idForum}" nombreForo="${forum.nameForum}" descripcionForo="${forum.description}" fechaInicio="${forum.startDate}" fechaFin="${forum.endDate}"
                                class="ver_foro m-2 h-8 inline-block text-l text-blue-400 dark:text-gray-400 hover:bg-gray-100 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                                type="button">Ver mas...
                                <i class="fa fa-search fa-l"></i> 
                            </button>

                            <button  id_forum_delete="${forum.idForum}"
                                class=" delete_forum m-2 h-8 inline-block text-l text-red-800 dark:text-gray-400 hover:bg-red-300 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                                type="button">Eliminar
                                <i class="fa fa-trash fa-l"></i>  

                            </button>
                           
                        </div>
                       
                    </div>`

                    contentDiv.innerHTML += content;

                });
            } else {
                contentDiv.innerHTML = "No hay foros creados"
            }

        });

}

function showForum(idForum, nombreForo, descripcionForo, fechaInicio, fechaFin) {
    let contentDiv = document.getElementById("content_div");

    fetch('../VIEWS/forum.html')
        .then(response => {
            if (response.ok) return response.text();
            else throw new Error("Error al cargar la pagina");
        })
        .then(html => {

            contentDiv.innerHTML = html;
            ShowMessages(idForum, nombreForo, descripcionForo, fechaInicio, fechaFin);
            responseForum(idForum);
        })
        .catch(error => console.log(error))
}

function ShowMessages(idForum, nombreForo, descripcionForo, fechaInicio, fechaFin) {

    let tituloForo = document.getElementById("titulo_foro");
    let descripcion = document.getElementById("descripcion_foro");
    let fechaInicioForo = document.getElementById("fecha_inicio_foro");
    let fechaFinForo = document.getElementById("fecha_fin_foro");



    tituloForo.textContent = nombreForo;
    descripcion.textContent = descripcionForo;
    fechaInicioForo.textContent = fechaInicio;
    fechaFinForo.textContent = fechaFin;

    ShowMessages2(idForum);

}

function ShowMessages2(idForum) {

    let divRespuestas = document.getElementById("div_respuestas");
    const url = "https://localhost:7281/api/Message/GetMessagesByForum";

    const data = {
        idForum: idForum
    }

    const options = {
        method: 'POST',
        headers: {
            'content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    }

    fetch(url, options)
        .then((response) => {
            return response.json();
        })
        .then((data) => {
            console.log(data.status);
            let visibleSpan = "";
            let visibleImg = "";
            let botonVisibility = "";
            if (data.status == undefined) {
                divRespuestas.innerHTML = "";
                data.forEach(message => {

                    if (message.urlFile == null) {
                        visibleImg = "hidden";
                        visibleSpan = "";
                    }
                    if (message.message == null) {
                        visibleSpan = "hidden";
                        visibleImg = "";
                    }

                    if (message.idUser != sessionStorage.getItem("idUser")) {
                        botonVisibility = "hidden";
                    } else {
                        botonVisibility = "";
                    }

                    let content =
                        `
                        <div class="flex flex-col w-11/12 mt-2 bg-gray-800 border border-blue-400 mb-2 rounded-lg shadow mr-4 ml-4">
                
                        <div class="flex mb-4 mt-2">

                            <div class="flex w-full justify-between">
                                <span class="px-6 text-center inline-block break-words text-sm text-white mb-1 dark:text-white">${message.nameUser}  </span>
                            </div>

                            <div class="flex w-full justify-end">
                                <span class="px-6 text-center inline-block break-words text-white-400 text-sm mb-1 dark:text-white">${message.date}  </span>
                            </div>

                        </div>

                        <div class="flex w-full justify-center">
                            <span class="${visibleSpan} px-6 text-center inline-block break-words text-white-400 text-xl mb-1 dark:text-white">${message.message}  </span>
                        </div>    
                        
                        <div class="flex w-full justify-center">
                            <img class="${visibleImg} w-40 h-40 rounded-full text-center inline-block break-words text-white-400 text-xl mb-1 dark:text-white" src="${message.urlFile}">
                        </div>  


                        <div class="flex justify-end px-4 mb-2">

                            <button  id_message_delete="${message.idMessage}" idUser="${message.idUser}" idForum="${idForum}"
                                class="delete_message ${botonVisibility} m-2 h-8 inline-block text-l text-red-800 dark:text-gray-400 hover:bg-red-300 dark:hover:bg-gray-700  focus:outline-none focus:ring-gray-200 dark:focus:ring-gray-700 rounded-lg text-sm p-1.5"
                                type="button">Eliminar
                                <i class="fa fa-trash fa-l"></i>  

                            </button>
                           
                        </div>

                        
                       
                    </div>
                    `

                    divRespuestas.innerHTML += content

                });

            } else {
                divRespuestas.innerHTML = "Aun no hay respuestas en el foro";
            }
        })
}

function responseForum(idForum) {
    let btnComentar = document.getElementById("btn_comentar_foro");
    let idUser = sessionStorage.getItem("idUser");
    let inputPhoto = document.getElementById("input_file");
    let inputText = document.getElementById("input_text"); seleccionar_archivo
    let lblInputFile = document.getElementById("seleccionar_archivo");
    inputPhoto.addEventListener("change", () => {
        info.textContent = inputPhoto.files[0].name
        inputText.classList.add("hidden");
    })

    inputText.addEventListener("input", () => {
        if (inputText.value === "") {
            lblInputFile.classList.remove("hidden");
        } else {
            lblInputFile.classList.add("hidden");
        }

    })

    btnComentar.addEventListener("click", (event) => {
        event.preventDefault();
        const url = "https://localhost:7281/api/Message/CreateMessage"

        const formData = new FormData();
        formData.append("message", inputText.value);
        formData.append("date", Date.now());
        formData.append("idForum", idForum);
        formData.append("idUser", sessionStorage.getItem("idUser"));
        formData.append("file", inputPhoto.files[0]);

        const options = {
            method: 'POST',
            body: formData
        }

        fetch(url, options)
            .then((response) => {
                if (response.ok) {
                    alertify.success("Respuesta subida correctamente");
                    ShowMessages2(idForum);
                    inputText.value = "";
                    lblInputFile.classList.remove("hidden");

                } else {
                    alertify.error("Error al comentar, intentelo de nuevo");
                }
            })










    })
}

function DeleteMessage(idMessage, idForum) {

    const url = "https://localhost:7281/api/Message/DeleteMessage"
    const data = {
        idMessage: idMessage
    }

    const options = {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify(data)
    }

    fetch(url, options)
        .then((response) => {
            console.log(response);
            if (response.ok) {
                alertify.success("Respuesta eliminada");
                ShowMessages2(idForum)
            } else {
                alertify.error("Error al eliminar, Intentelo de nuevo");
            }
        })
}