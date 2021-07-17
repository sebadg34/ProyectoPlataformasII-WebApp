const formulario = document.getElementById('register_passenger');
const entradas = document.querySelectorAll('#register_passenger input');

const expresionesRegulares = {
    nombre_apellido: /^([A-Za-záéíóúñÁÉÍÓÚ ])*$/,
    email: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/,
    rut: /(^[0-9]{7,8}-([0-9]|k)$)/,
    pasaporte: /(^([A-Z]|[0-9]){9})$/,
    direccion: /^([A-Za-záéíóúñÁÉÍÓÚ .])*$/,
    numeroTelefono: /^9[0-9]{8}$/
}

const campos = {
    nombre: false,
    apellido: false,
    email: false,
    rutPasaporte: false,
    direccion: false,
    numeroTelefono: false,
    nombreEmergencia: false,
    apellidoEmergencia: false,
    numeroTelefonoEmergencia: false
}

const validarFormulario = (e) => {
    switch (e.target.name) {
        case "nombre":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                campos.nombre = true;
            } else {
                campos.nombre = false;
            }
            break;
        case "apellido":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                campos.apellido = true;
            } else {
                campos.apellido = false;
            }
            break;
        case "email":
            if (expresionesRegulares.email.test(e.target.value)) {
                campos.email = true;
            } else {
                campos.email = false;
            }
            break;
        case "rutPasaporte2":
            if ($("#rutPasaporte").val() == "rut") {
                if (expresionesRegulares.rut.test(e.target.value)) {
                    campos.rutPasaporte = true;
                } else {
                    campos.rutPasaporte = false;
                }
            }
            if ($("#rutPasaporte").val() == "numPasaporte") {
                if (expresionesRegulares.pasaporte.test(e.target.value)) {
                    campos.rutPasaporte = true;
                } else {
                    campos.rutPasaporte = false;
                }
            }
            break;
        case "direccion":
            if (expresionesRegulares.direccion.test(e.target.value)) {
                campos.direccion = true;
            } else {
                campos.direccion = false;
            }
            break;
        case "numeroTelefono":
            if (expresionesRegulares.numeroTelefono.test(e.target.value)) {
                campos.numeroTelefono = true;
            } else {
                campos.numeroTelefono = false;
            }
            break;
        case "nombreEmergencia":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                campos.nombreEmergencia = true;
            } else {
                campos.nombreEmergencia = false;
            }
            break;
        case "apellidoEmergencia":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                campos.apellidoEmergencia = true;
            } else {
                campos.apellidoEmergencia = false;
            }
            break;
        case "numeroTelefonoEmergencia":
            if (expresionesRegulares.numeroTelefono.test(e.target.value)) {
                campos.numeroTelefonoEmergencia = true;
            } else {
                campos.numeroTelefonoEmergencia = false;
            }
            break;
    }
}

entradas.forEach((entrada) => {
    entrada.addEventListener('keyup', validarFormulario);
    entrada.addEventListener('blur', validarFormulario);
});

function reservar() {
    if (verificarCampos()) {
        if ($("#rutPasaporte").val() == "rut") {
            console.log("cambio de pagina rut");
            url = "/Home/GoToVoucher?Nombres=" + $("#nombre").val() + "&Apellidos=" + $("#apellido").val() + "&Rut=" + $("#rutPasaporte2").val() + "&Numero_Pasaporte=" + null + "&Direccion=" + $("#direccion").val() + "&Numero_Direccion=" + $("#numero").val() + "&Numero_Telefono=" + $("#numeroTelefono").val() + "&Nombres_Emergencia=" + $("#nombreEmergencia").val() + "&Apellidos_Emergencia=" + $("#apellidoEmergencia").val() + "&Numero_Telefono_Emergencia=" + $("#numeroTelefonoEmergencia").val();
            window.location = url;
        }
        else if ($("#rutPasaporte").val() == "numPasaporte") {
            Swal.fire(
                '¡Listo!!',
                'Se ha reservado correctamente.',
                'success'
            );
            console.log("cambio de pagina pasaporte");
            url = "/Home/GoToVoucher?Nombres=" + $("#nombre").val() + "&Apellidos=" + $("#apellido").val() + "&Rut=" + null + "&Numero_Pasaporte=" + $("#rutPasaporte2").val() + "&Direccion=" + $("#direccion").val() + "&Numero_Direccion=" + $("#numero").val() + "&Numero_Telefono=" + $("#numeroTelefono").val() + "&Nombres_Emergencia=" + $("#nombreEmergencia").val() + "&Apellidos_Emergencia=" + $("#apellidoEmergencia").val() + "&Numero_Telefono_Emergencia=" + $("#numeroTelefonoEmergencia").val();
            window.location = url;
        }
    }
}

function verificarCampos() {
    for (i = 0; i < 9; i++) {
        if (campos[i]) {
            return false;
        }
    }
    return true;
}