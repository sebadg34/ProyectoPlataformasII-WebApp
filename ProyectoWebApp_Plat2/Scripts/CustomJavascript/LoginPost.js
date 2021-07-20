// Funcion encargada de mandar request a la API para hacer login
function Login() {

    var newUrl = "https://localhost:44350/api/Login";
    // Post a al loginController de la api
    $.post(newUrl, {Email: $("#userEmail").val(), Contrasenia: $("#userPassword").val()},
        function (registro, status) {

            // alert("Vuelo " + idVuelo + " registrado correctamente.");
            Swal.fire(
                '¡Ingreso Exitoso!',
                'Usuario ' + registro.Name,
                'success'
            )
                .then(() => {
                        window.location.href = "http://localhost:52811/Home/ToMenu?rol=" +
                        registro.IdRol + "&nombre=" +
                        registro.Name + "&idUsuario=" +
                        registro.Id;
                });
           
            
            
        }).fail(function (data, status) {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'Verifique que los datos ingresados sean los correctos'
            })
        });
    
}
