const formulario = document.getElementById('register_passenger');
const entradas = document.querySelectorAll('#register_passenger input');

const expresionesRegulares = {
    nombre_apellido: /^([A-Za-záéíóúñÁÉÍÓÚ ])*$/,
    email: /^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$/,
    rut: /(^[0-9]{7,8}-([0-9]|k)$)/,
    pasaporte: /(^([A-Z]|[0-9]){9})$/,
    direccion: /^([A-Za-záéíóúñÁÉÍÓÚ .])*$/,
    numeroTelefono: /^9[0-9]{8}$/
}

var campos = [false, false, false, false, false, false, false, false, false];
var nombreCampos = ["nombre","apellido","email","rut/pasaporte","direccion","numero telefono","nombre emergencia","apellido emergencia","numero telefono emergencia"];

const validarFormulario = (e) => {
    switch (e.target.name) {
        case "nombre":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                campos[0] = true;
            } else {
                campos[0] = false;
            }
            break;
        case "apellido":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                campos[1] = true;
            } else {
                campos[1] = false;
            }
            break;
        case "email":
            if (expresionesRegulares.email.test(e.target.value)) {
                campos[2] = true;
            } else {
                campos[2] = false;
            }
            break;
        case "rutPasaporte2":
            if ($("#rutPasaporte").val() == "rut") {
                if (expresionesRegulares.rut.test(e.target.value)) {
                    campos[3] = true;
                } else {
                    campos[3] = false;
                }
            }
            if ($("#rutPasaporte").val() == "numPasaporte") {
                if (expresionesRegulares.pasaporte.test(e.target.value)) {
                    campos[3] = true;
                } else {
                    campos[3] = false;
                }
            }
            break;
        case "direccion":
            if (expresionesRegulares.direccion.test(e.target.value)) {
                campos[4] = true
            } else {
                campos[4] = false;
            }
            break;
        case "numeroTelefono":
            if (expresionesRegulares.numeroTelefono.test(e.target.value)) {
                campos[5] = true;
            } else {
                campos[5] = false;
            }
            break;
        case "nombreEmergencia":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                campos[6] = true;
            } else {
                campos[6] = false;
            }
            break;
        case "apellidoEmergencia":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                campos[7] = true;
            } else {
                campos[7] = false;
            }
            break;
        case "numeroTelefonoEmergencia":
            if (expresionesRegulares.numeroTelefono.test(e.target.value)) {
                campos[8] = true;
            } else {
                campos[8] = false;
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
            console.log("cambio de pagina pasaporte");
            url = "/Home/GoToVoucher?Nombres=" + $("#nombre").val() + "&Apellidos=" + $("#apellido").val() + "&Rut=" + null + "&Numero_Pasaporte=" + $("#rutPasaporte2").val() + "&Direccion=" + $("#direccion").val() + "&Numero_Direccion=" + $("#numero").val() + "&Numero_Telefono=" + $("#numeroTelefono").val() + "&Nombres_Emergencia=" + $("#nombreEmergencia").val() + "&Apellidos_Emergencia=" + $("#apellidoEmergencia").val() + "&Numero_Telefono_Emergencia=" + $("#numeroTelefonoEmergencia").val();
            window.location = url;
        }
    }
}

function verificarCampos() {
    for (let i = 0; i < 9; i++) {
        if (!campos[i]) {
            alert("el campo " + nombreCampos[i] + "esta mal escrito.");
            return false;
        }
    }
    alert("Reserva realizada correctamente");
    return true;
}