var choosePDT = {
    init: function () {
        this.loadProvince();
        this.listDistrict();
        this.ListTown();
    },
    loadProvince: function () {
       
           
            $.ajax({
                url: "/User/LoadProvince",
                dataType: "json",
                type: "post",
                success: function (res) {
                    $('#ddlProvince').append('<option value="">---Chọn Tỉnh/Thành---</option>');
                    if (res.status == true) {
                        var data = res.data;
                        $.each(data, function (i, item) {
                            $('#ddlProvince').append('<option value="'+item.ID+'">'+item.Name+'</option>');

                        });
                    }
                }
            });

       
    },
    loadDistrict: function (id) {
        $.ajax({
            url: "/User/LoadDistrict",
            type: "post",
            data: { id: id },
            dataType: "json",
            success: function (res) {
                $('#ddlDistrict').append('<option value="">---Chọn Quận/Huyện --- </option>');
                if (res.status == true) {
                    var data = res.data;
                    $.each(data, function (i, item) {
                        $('#ddlDistrict').append('<option value="' + item.ID + '">' + item.Name + '</option>');

                    });
                }
            }

        });
    },
    listDistrict: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != null) {
                choosePDT.loadDistrict(id);
            } else {
                $('#ddlDistrict').hide();
                $('#ddlTown').hide();
            }
        });
    },
    LoadTown: function (id) {
        $.ajax({
            url:"/User/LoadTown",
            type: "post",
            data: { id: id },
            dataType: "json",
            success: function (res) {
                $('#ddlTown').append('<option value="">---Chọn Xã/Phường---</option>');
                if (res.status == true) {
                    var data = res.data;
                    $.each(data, function (i, item) {
                        $('#ddlTown').append('<option value="' + item.ID + '">' + item.Name + '</option>');

                    });
                }
            }
        });
    },
    ListTown: function () {
        $('#ddlDistrict').off('change').on('change', function () {
            var id = $(this).val();
            if (id != null) {
                choosePDT.LoadTown(id);
            } else {
                $('#ddlTown').hide();
            }
        });
    }

}
choosePDT.init();