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
                console.log(e.target.name + " está bien escrito");
                campos[0] = true;
                console.log(campos[0]);
            } else {
                console.log(e.target.name + "nombre no está bien escrito");
                campos[0] = false;
                console.log(campos[0]);
            }
            break;
        case "apellido":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                console.log(e.target.name + " está bien escrito");
                campos[1] = true;
                console.log(campos[1]);
            } else {
                console.log(e.target.name + "nombre no está bien escrito");
                campos[1] = false;
                console.log(campos[1]);
            }
            break;
        case "email":
            if (expresionesRegulares.email.test(e.target.value)) {
                console.log(e.target.name + " está bien escrito");
                campos[2] = true;
                console.log(campos[2]);
            } else {
                console.log(e.target.name + "nombre no está bien escrito");
                campos[2] = false;
                console.log(campos[2]);
            }
            break;
        case "rutPasaporte2":
            if ($("#rutPasaporte").val() == "rut") {
                if (expresionesRegulares.rut.test(e.target.value)) {
                    console.log(e.target.name + " está bien escrito");
                    campos[3] = true;
                    console.log(campos[3]);
                } else {
                    console.log(e.target.name + "nombre no está bien escrito");
                    campos[3] = false;
                    console.log(campos[3]);
                }
            }
            if ($("#rutPasaporte").val() == "numPasaporte") {
                if (expresionesRegulares.pasaporte.test(e.target.value)) {
                    console.log(e.target.name + " está bien escrito");
                    campos[3] = true;
                    console.log(campos[3]);
                } else {
                    console.log(e.target.name + "nombre no está bien escrito");
                    campos[3] = false;
                    console.log(campos[3]);
                }
            }
            break;
        case "direccion":
            if (expresionesRegulares.direccion.test(e.target.value)) {
                console.log(e.target.name + " está bien escrito");
                campos[4] = true
                console.log(campos[4]);
            } else {
                console.log(e.target.name + "nombre no está bien escrito");
                campos[4] = false;
                console.log(campos[4]);
            }
            break;
        case "numeroTelefono":
            if (expresionesRegulares.numeroTelefono.test(e.target.value)) {
                console.log(e.target.name + " está bien escrito");
                campos[5] = true;
                console.log(campos[5]);
            } else {
                console.log(e.target.name + "nombre no está bien escrito");
                campos[5] = false;
                console.log(campos[5]);
            }
            break;
        case "nombreEmergencia":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                console.log(e.target.name + " está bien escrito");
                campos[6] = true;
                console.log(campos[6]);
            } else {
                console.log(e.target.name + "nombre no está bien escrito");
                campos[6] = false;
                console.log(campos[6]);
            }
            break;
        case "apellidoEmergencia":
            if (expresionesRegulares.nombre_apellido.test(e.target.value)) {
                console.log(e.target.name + " está bien escrito");
                campos[7] = true;
                console.log(campos[7]);
            } else {
                console.log(e.target.name + "nombre no está bien escrito");
                campos[7] = false;
                console.log(campos[7]);
            }
            break;
        case "numeroTelefonoEmergencia":
            if (expresionesRegulares.numeroTelefono.test(e.target.value)) {
                console.log(e.target.name + " está bien escrito");
                campos[8] = true;
                console.log(campos[8]);
            } else {
                console.log(e.target.name + "nombre no está bien escrito");
                campos[8] = false;
                console.log(campos[8]);
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
            Swal.fire(
                '¡Listo!!',
                'Se ha hecho la reserva correctamente.',
                'success'
            ).then(() => {
                url = "/Home/GoToVoucher?Nombres=" + $("#nombre").val() + "&Apellidos=" + $("#apellido").val() + "&Rut=" + $("#rutPasaporte2").val() + "&Numero_Pasaporte=" + null + "&Direccion=" + $("#direccion").val() + "&Numero_Direccion=" + $("#numero").val() + "&Numero_Telefono=" + $("#numeroTelefono").val() + "&Nombres_Emergencia=" + $("#nombreEmergencia").val() + "&Apellidos_Emergencia=" + $("#apellidoEmergencia").val() + "&Numero_Telefono_Emergencia=" + $("#numeroTelefonoEmergencia").val();
                window.location = url;
            });
        }
        else if ($("#rutPasaporte").val() == "numPasaporte") {
            Swal.fire(
                '¡Listo!!',
                'Se ha hecho la reserva correctamente.',
                'success'
            ).then(() => {
                url = "/Home/GoToVoucher?Nombres=" + $("#nombre").val() + "&Apellidos=" + $("#apellido").val() + "&Rut=" + null + "&Numero_Pasaporte=" + $("#rutPasaporte2").val() + "&Direccion=" + $("#direccion").val() + "&Numero_Direccion=" + $("#numero").val() + "&Numero_Telefono=" + $("#numeroTelefono").val() + "&Nombres_Emergencia=" + $("#nombreEmergencia").val() + "&Apellidos_Emergencia=" + $("#apellidoEmergencia").val() + "&Numero_Telefono_Emergencia=" + $("#numeroTelefonoEmergencia").val();
                window.location = url;
            });
        }
    } else {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Datos inválidos al realizar la reserva, intente de nuevo.'
        })
    }    
}

function verificarCampos() {
    for (let i = 0; i < 9; i++) {
        if (!campos[i]) {
            console.log("verificando campo " + i + " :" + campos[i]);
            return false;
        }
        console.log("verificando campo " + i + " :" + campos[i]);
    }
    return true;
}