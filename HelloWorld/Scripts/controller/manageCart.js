var manageCart = {
    init: function () {
        this.registerEvent();
    },
    registerEvent: function () {
        //$('.update').off('click').on('click', function (e) {
        //    e.preventDefault();
         
        //    $.ajax({
        //        url: "/Home/UpdateItem",
        //        data: { id: $(this).data('id'), quantity: $('.quantity' + $(this).data('id')).val() },
        //        dataType: "json",
        //        type: "POST",
        //        success: function (res) {
        //            if (res.status == true) {
        //                window.location.href = "/Home/Cart";
        //            }
        //        }

        //    });
        //});
        $('#btnUpdate').off('click').on('click', function (e) {
            e.preventDefault();
            var quantity = $('.quantity');
            var listquantity = [];
            $.each(quantity, function (i, item) {
                listquantity.push({
                    Product: { ID: $(item).data('id') },
                    Quantity: $(item).val()
                }                   
                );
            });
            $.ajax({
                url: "/Home/Update",
                type: "post",
                data: { quantity: JSON.stringify(listquantity) },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Cart";
                    }
                }

            });

        });
        $('.delete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: "/Home/DeleteItem",
                type: "post",
                data: { id: $(this).data('id') },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Cart";
                    }
                }

            });

        });
        $('#btnDelete').off('click').on('click', function (e) {
            e.preventDefault();
            $.ajax({
                url: "/Home/Delete",
                type: "post",
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/Home/Cart";
                    }
                }
            });

        })
    }
}
manageCart.init();