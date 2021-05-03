var chooese = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        $('#chooseLogin').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/User/Login";

        });
        $('#chooseRegister').off('click').on('click', function (e) {
            e.preventDefault();
            window.location.href = "/Home/Register";

        });
    }
}
chooese.init();