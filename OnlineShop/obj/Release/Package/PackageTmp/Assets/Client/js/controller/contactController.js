var contact = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        $('#btnSend').off('click').on('click', function (e) {
            e.preventDefault();
           
            var name = $('#txtName').val();
            var mobile = $('#txtMobile').val();
            var address = $('#txtAddress').val();
            var email = $('#txtEmail').val();
            var content = $('#txtContent').val();

            $.ajax({
                type: "POST",
                url: "/Contact/Send",
                data: { name: name, mobile: mobile, address: address, email: email, content: content },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        alert('Gửi Thành Công');
                    }
                }
        });
       
        });
    }
}
contact.init();