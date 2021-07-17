function Register() {
    // Primero que todo, toma el email, la contrasenia y el id_rol, en este caso falso debido a que es cliente. Lo ingresa a la tabla Rol.
    $.post("https://localhost:44350/api/Users", {
        Email: $("#email").val(),
        Contrasenia: $("#contrasenia").val(),
        ID_Rol: "false"
    },
        function (data, status) {
            // alert("Data: " + data + "\nStatus: " + status);

            // Si los datos son válidos para el Rol, entonces procede a tomar el ID (clave primaria) del rol para poder registrar la informacion restante en la tabla Customer.
            if (status == "success") {
                $.get("https://localhost:44350/api/Users?email=" + $("#email").val(),
                    function (dataRol, status) {
                        // alert("Data: " + dataRol.ID + "\nStatus: " + status);

                        // Obtiene los valores temporales del rut o numero de pasaporte para poder insertarlos en la base de dato
                        var rutAux = "";
                        var pasaporteAux = "";

                        if ($("#rutPasaporte").val() == "rut") {
                            rutAux = $("#rutPasaporte2").val();
                        }
                        if ($("#rutPasaporte").val() == "numPasaporte") {
                            pasaporteAux = $("#rutPasaporte2").val();
                        }

                        // Aqui inserta los datos a la tabla Customer.
                        $.post("https://localhost:44350/api/Customers", {
                            Nombres: $("#nombre").val(),
                            Apellidos: $("#apellido").val(),
                            Rut: rutAux,
                            Numero_Pasaporte: pasaporteAux,
                            Direccion: $("#direccion").val(),
                            Numero_Direccion: $("#numero").val(),
                            Numero_Telefono: $("#numeroTelefono").val(),
                            Nombres_Emergencia: $("#nombreEmergencia").val(),
                            Apellidos_Emergencia: $("#apellidoEmergencia").val(),
                            Numero_Telefono_Emergencia: $("#numeroTelefonoEmergencia").val(),
                            ID: dataRol.ID
                        })
                            // Mensaje de que se registro bien
                            .done(function () {
                                // alert("Cliente registrado correctamente.");
                                Swal.fire(
                                    '¡Listo!!',
                                    'Se ha registrado correctamente.',
                                    'success'
                                ).then(() => { window.location.href = "/Home/ToMenu?rol=" + ID_Rol + "&nombre=" + Nombres + "&idUsuario=" + ID; });
                            })
                            // Mensaje de error
                            .fail(function (xhr, status, error) {
                                // alert("Datos inválidos al registrar el cliente, intente de nuevo. ");
                                Swal.fire({
                                    icon: 'error',
                                    title: 'Oops...',
                                    text: 'Datos inválidos al registrarse, intente de nuevo.'
                                })

                                // En caso de que el customer no se pueda ingresar, se debe eliminar el rol asignado anteriormente.
                                var path = 'https://localhost:44350/api/Users/' + dataRol.ID;
                                $.ajax({
                                    url: path,
                                    type: 'DELETE',
                                    success: function (result) {
                                        //alert(dataRol.ID + " BORRADO");
                                    }
                                });
                            });
                    }
                );
            }

        }
    )
        // Avisa si el correo ya existe, por ende, no se puede registrar
        .fail(function (xhr, status, error) {
            // alert("Datos inválidos al registrar el cliente, intente de nuevo. ");
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: '¡Ese correo ya existe! Intente de nuevo.'
            })
        });

}