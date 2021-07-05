function Register() {

    $.get("https://localhost:44350/api/lastflight",
        function (data, status) {
            // alert("Data: " + data.ID + "\nStatus: " + status);

            // ---------------- Transformaciones y conversiones de las fechas ----------------

            // Obtiene variables de fechas y las convierte tipo fecha
            var fechaSalida = $("#fechaSalida").val();
            var horaSalida = $("#horaSalida").val();
            var hora = $("#horaLlegada").val();
            var minuto = $("#minutoLlegada").val();

            var formatoSalida = fechaSalida + "T" + horaSalida + ":00";

            // Transformacion de la duracion del vuelo a formato fecha
            var fechaLlegada = new Date(formatoSalida);
            var horaEnMilisegundo = 3600000 * parseInt(hora);
            var minutoEnMilisegundo = 60000 * parseInt(minuto);
            var tiempoEnMilisegundos = parseInt(horaEnMilisegundo) + parseInt(minutoEnMilisegundo);
            var suma = parseInt(fechaLlegada.getTime()) + parseInt(tiempoEnMilisegundos);

            // Ya que el formato Date es distinto al formato DateTime en la API se deben obtener una por una las variables y asi encadenarlas al final
            var fecha = new Date(suma);
            var anioLlegada = fecha.getFullYear();
            var mesLlegada = fecha.getMonth() + 1;
            var diaLlegada = fecha.getDate();
            var horaLlegada = fecha.getHours();
            var minutoLlegada = fecha.getMinutes();

            if (mesLlegada < 10) {
                mesLlegada = "0" + mesLlegada;
            }

            if (diaLlegada < 10) {
                diaLlegada = "0" + diaLlegada;
            }

            var formatoLlegada = anioLlegada + "-" + mesLlegada + "-" + diaLlegada + "T" + horaLlegada + ":" + minutoLlegada + ":00";

            // Si existe un ultimo vuelo registrado entonces procedera a registrar el vuelo
            if (status == "success") {
                var idVueloAnterior = data.ID_Vuelo;

                // Se realiza a aumentar el ID_Vuelo mediante una expresion regular que elimina la constante AP- para obtener el valor numerico y asi aumentarlo
                var exp = new RegExp("(^AP-?)");
                var inc = parseInt(idVueloAnterior.replace(exp, "")) + 1;

                if (inc < 10) {
                    var idVuelo = "AP-00" + inc;
                }
                else if (inc < 100) {
                    var idVuelo = "AP-0" + inc;
                }
                else {
                    var idVuelo = "AP-" + inc;
                }

                $.post("https://localhost:44350/api/Flights", {
                    ID_Vuelo: idVuelo,
                    Ciudad_Origen: $("#ciudadOrigen").val(),
                    Ciudad_Destino: $("#ciudadDestino").val(),
                    Fecha_Salida: formatoSalida,
                    Fecha_Llegada: formatoLlegada,
                    Cupos: $("#cupos").val(),
                    Categoria: $("#categoria").val()
                },
                    function (data, status) {
                        // alert("Data: " + data + "\nStatus: " + status);
                    }
                )
                    // Mensaje de que se registro bien
                    .done(function () {
                        // alert("Vuelo " + idVuelo + " registrado correctamente.");
                        Swal.fire(
                            '¡Listo!!',
                            'El vuelo ' + idVuelo + ' se ha registrado correctamente.',
                            'success'
                        )
                            .then(() => { window.location = "/Home/Menu"; });
                    })
                    // Mensaje de error
                    .fail(function (xhr, status, error) {
                        // alert("Datos inválidos al registrar el vuelo, intente de nuevo. ");
                        Swal.fire({
                            icon: 'error',
                            title: 'Oops...',
                            text: 'Datos inválidos al registrar el vuelo, intente de nuevo.'
                        })
                    });
            }
        }
    );
}