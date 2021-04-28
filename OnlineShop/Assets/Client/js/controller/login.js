var login = {
    init: function () {
        this.registerEvent();
    },
    registerEvent: function () {
        $('#btnLogin').off('click').on('click', function () {
            var username = $('#username').val();
            var password = $('#password').val();
         
            $.ajax({
                type: "POST",
                url: "/User/Login",
                data: { username: username, password: password },
                dataType: "json",
                success: function (res) {
                    if (res.finish == "success") {
                        window.location.href = "/trang-chu";
                    }
                   
                    else if (res.error != null) {
                        $('#alert').addClass('alert alert-danger');
                        $('#alert').text(res.error);
                    } /*else if (res.error  != null) {*/
                    //    $('#alert').addClass('alert alert-danger');
                    //    $('#alert').text(res.erroraccpassword);
                    //} else {
                    //    $('#alert').addClass('alert alert-danger');
                    //    $('#alert').text(res.error);
                    //}
                }


            });
        });
        $('#btnLoginFB').off('click').on('click', function () {
            $.ajax({
                type: "POST",
                url: "/User/LoginFacebook",
                dataType: "json",
                success: function (res) {
                    if (res.status == "success") {
                        window.location.href = "/trang-chu";
                    }

                    /*else if (res.error  != null) {*/
                    //    $('#alert').addClass('alert alert-danger');
                    //    $('#alert').text(res.erroraccpassword);
                    //} else {
                    //    $('#alert').addClass('alert alert-danger');
                    //    $('#alert').text(res.error);
                    //}
                }


            });

        });
    }
}
login.init();