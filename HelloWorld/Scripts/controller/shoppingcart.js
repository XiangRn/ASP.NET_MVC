var shopcart = {
    init: function () {
        this.registerEvent();
    },
    registerEvent: function () {
        $('.AddCart').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: "/Home/AddCart",
                type: "POST",
                dataType: "json",
                data: { id: $(this).data('id') },
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Cart";
                    }
                }

            });
        });
    }
}
shopcart.init();