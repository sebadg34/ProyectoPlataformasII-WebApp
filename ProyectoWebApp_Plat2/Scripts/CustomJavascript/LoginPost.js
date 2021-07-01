function Login() {

    var newUrl = "https://localhost:44350/api/Login";
    var rol_id;
    var rol_nombre;
    var rol_user;
    
    $.post(newUrl, { Email: $("#userEmail").val(), Contrasenia: $("#userPassword").val() },

        function (registro, status) {

            var userUrl = "";

            alert("LOGIN EXITOSO\n usuario: " + registro.Email + "\n id: " + registro.ID);

            // en el caso de ser cliente comun
            if (registro.ID_Rol == 0) {
                console.log("CLIENTE ENTRANDO");
                userUrl = "https://localhost:44350/api/customers/" + registro.ID;
            }
            // en el caso de ser administrador 
            if (registro.ID_Rol == 1) {
                console.log("ADMIN ENTRANDO");
                userUrl = "https://localhost:44350/api/managers/" + registro.ID;
            }
            
            $.get(userUrl, function (usuario) {
                
                
                rol_id = registro.ID_Rol;
                rol_nombre = usuario.Nombres;
                rol_user = usuario.ID;
                window.location.href = "http://localhost:52811/Login/ToMenu?role=" + registro.ID_Rol + "&name=" + usuario.Nombres + "&idUsuario=" + usuario.ID;
                 
            });

        }).fail(function () {
            alert("error");
        });
    
   

    //
    //    $.ajax({
    //        type: "POST",
    //        url: newUrl,
    //        dataType: "json",
    //        data: {
    //            Email: $("#userEmail").val(),
    //            Contrasenia: $("#userPassword").val()
    //        },
    //        success: function (rol) {
    //            alert("LOGIN EXITOSO\n " + rol.Email + " " + rol.ID);
    //            var userUrl;
    //            //en el caso de ser cliente
    //            if (rol.ID_Rol == 0) {
    //                console.log("CLIENTE ENTRANDO");
    //                userUrl = "https://localhost:44350/api/customers/" + rol.ID;
    //
    //            }
    //            //en el caso de ser administrador 
    //            if (rol.ID_Rol == 1) {
    //                console.log("ADMIN ENTRANDO");
    //                userUrl = "https://localhost:44350/api/managers/" + rol.ID;
    //            }
    //
    //            $.ajax({
    //                type: "GET",
    //                url: userUrl,
    //                dataType: 'json',
    //
    //                success: function (user) {
    //                    alert(user.Nombres);
    //                    
    //                    this.rol_id = rol.ID_Rol;
    //                    this.rol_nombre = user.Nombres;
    //                    this.rol_user = user.ID;
    //
    //
    //
    //                    alert(rol_id);
    //                    changeView(rol_id, rol_nombre, rol_user);
    //                }
    //
    //
    //            });
    //
    //            
    //            
    //        },
    //        error: function (result) {
    //            alert("ERROOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOR " + result);
    //            console.log('Login Fail!!!');
    //            console.log(result.Email + result.Contrasenia);
    //            
    //        }
    //
    //
    //    });
    //    
   
}

function changeView(rol, nombre, id) {
   return [ rol, nombre, id ];  
}