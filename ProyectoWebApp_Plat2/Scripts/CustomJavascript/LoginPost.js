function Login() {

    var newUrl = "https://localhost:44350/api/Login";

    $.ajax({

        type: "POST",
        url: newUrl,
        dataType: "json",
        data: {
            Email: $("#userEmail").val(),
            Contrasenia: $("#userPassword").val()
        },
        success: function (result) {
            alert("LOGIN EXITOSO\n " + result.Email + " " + result.ID);
        },
        error: function (result) {
            alert("ERROOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOR " + result);
            console.log('Login Fail!!!');
            console.log(result.Email + result.Contrasenia);
        }
    });
    
}