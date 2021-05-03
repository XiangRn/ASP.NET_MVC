var logout = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        $('#btnLogout').off('click').on('click', function () {
            $.ajax({
                dataType: "json",
                type: "POST",
                url: "/User/Logout",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/trang-chu";
                    }
                }

            });
        });
    }
}
logout.init();