// Funcion encargada de mandar request a la API para hacer login
function Login() {

    var newUrl = "https://localhost:44350/api/Login";
    // Post a al loginController de la api
    $.post(newUrl, {Email: $("#userEmail").val(), Contrasenia: $("#userPassword").val()},
        function (registro, status) {
            
            alert("LOGIN EXITOSO\n usuario: " + registro.Name + "\n id: " + registro.Id);
            window.location.href = "http://localhost:52811/Home/ToMenu?role=" + registro.IdRol + "&name=" + registro.Name + "&userID=" + registro.Id;
            
        }).fail(function (data, status) {
            alert("error");
        });
    
}
