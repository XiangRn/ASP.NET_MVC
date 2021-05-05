var cart = {
    init: function () {
        this.regEvents();
    },
    regEvents: function () {
        $('#btnContinue').off('click').on('click', function () {
            window.location.href = "/trang-chu";
        });
        $('#btnUpdate').off('click').on('click', function () {
            var listProduct = $('.quantity');
            var cartList = [];
            
            //lọc qua list
            //đây là vòng lặp each của Jquery
            listProduct.each(function (i, item) {    
               
                cartList.push({
                    Quantity: $(item).val(),                  
                    Product: {
                        ID: $(item).data('id')
                    }
                });
                
            });
            $.ajax({
                type: 'POST',
                url: '/Cart/Update',                
                data: {cartModel: JSON.stringify(cartList) },//parse đối tượng JSON sang 1 cái chuỗi
                dataType: 'json',                         
                success: function (res) {
                    if (res.status == true) {                       
                      window.location.href = "/gio-hang";
                    }
                }
            })
        });
        $('#btnDelete').off('click').on('click', function () {
        
            $.ajax({
                type: 'POST',
                url: '/Cart/Delete',
               /* data: { productId: id },*///parse đối tượng JSON sang 1 cái chuỗi
                dataType: 'json',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
        $('.close1').off('click').on('click', function () {            
            $.ajax({
                type: 'POST',
                url: '/Cart/DeleteEachItem',
                data: { productId: $(this).data('id') },//parse đối tượng JSON sang 1 cái chuỗi
                dataType: 'json',
                success: function (res) {
                    if (res.status == true) {
                        window.location.href = "/gio-hang";
                    }
                }
            })
        });
    }

}
cart.init();