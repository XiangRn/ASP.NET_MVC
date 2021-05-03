var user = {
    init: function () {
        this.loadProvince();
        this.registerEvent();
        this.registerEvents();
    },
    registerEvent: function () {
      
        $('#ddlProvince').off('change').on('change', function () {
            var id = $(this).val();
            if (id != '') {
                user.loadDistrict(parseInt(id));
                
            }
            else {
                $('#ddlDistrict').hide();
                $('#ddlTown').hide();
            }
        });
       
    },
    registerEvents: function () {
       
        $('#ddlDistrict').off('change').on('change', function () {
            var districtid = $(this).val();
            if (districtid != '') {
                user.loadTown(parseInt(districtid));

            }
            else {
                $('#ddlTown').html('');
            }
        });
    },
    loadProvince: function () {
           $.ajax({
            url: "/User/loadProvince",
            dataTye: "json",
            type: "POST",
            success: function (res) {
                if (res.status == true) {
                    var html = '<option value="">---Chọn tỉnh thành----</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name +'</option>';
                    });
                    $('#ddlProvince').html(html);
                }
            }

        });


    },
    loadDistrict: function (id) {
        $.ajax({
            url: "/User/loadDistrict",
            dataTye: "json",
            data: { provinceID:id},
            type: "POST",
            success: function (res) {
                if (res.status == true) {
                    var html = '<option value="">---Chọn quận huyện----</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>';
                    });
                    $('#ddlDistrict').html(html);
                }
            }

        });

        
    },
    loadTown: function (districtid) {
        $.ajax({
            url: "/User/loadTown",
            dataTye: "json",
            data: {districtID: districtid },
            type: "POST",
            success: function (res) {
                if (res.status == true) {
                    var html = '<option value="">---Chọn xã phường----</option>';
                    var data = res.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.ID + '">' + item.Name + '</option>';
                    });
                    $('#ddlTown').html(html);
                }
            }

        });


    }
}
user.init();