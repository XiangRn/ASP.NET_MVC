var changeStatus = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        $('.changestatus').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            $.ajax({
                url: "/Home/ChangeStatus",
                type: "post",
                data: { id: btn.data('id') },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        btn.text('Active');
                    } else {
                        btn.text('Disactive');
                    }
                }
            });
        });
    }
}
changeStatus.init();