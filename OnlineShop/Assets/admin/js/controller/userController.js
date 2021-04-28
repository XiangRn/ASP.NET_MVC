var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var idt = btn.data('id');//data-id: thì cái id này là data('id') bên này
            $.ajax({
                type: "POST",
                url: "/Admin/User/ChangeStatus",
                data: '{id: "' + idt + '" }',
                dataType: "json",
                contentType:'application/json; charset=utf-8',
                success: function (response) {
                  
                    if (response.status == true) {
                        btn.text('Active');
                      
                    }
                    else {
                        btn.text('Blocked');
                        
                    }
                }
            });
        });

    }
}
user.init();

