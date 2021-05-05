var proDetail = {
    init: function () {
        this.registerEvents();
    },
    registerEvents: function () {
        $('#EditImage').off('click').on('click', function (e) {
            e.preventDefault();
            $('#imageManamages').modal('show');
            $('#imageList').html('');
            proDetail.loadImage();
        });
        $('#btnChooImages').off('click').on('click', function (e) {
            e.preventDefault();
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $('#imageList').append('<div style="float:left"><img src="' + url + '" width="100"/><a href="#" class="deleteImg">*</a></div>');
                $('.deleteImg').off('click').on('click', function (e) {
                    e.preventDefault();
                    $(this).parent().remove();

                });
            };
         
            finder.popup();
        });
        $('#btnSaveImages').off('click').on('click', function () {
            var images = [];
            $.each($('#imageList img'), function (i, item) {
                images.push($(item).prop('src'));
            });
           
            $.ajax({
                url: "/Admin/Product/SaveImg",
                type: "POST",
                data: { id: $('#hidProductID').data('id'), images: JSON.stringify(images) },
                dataType: "json",
                success: function (res) {
                    if (res.status == true) {
                        $('#imageManamages').modal('hide');
                        $('#imageList').html('');
                    }
                }

            });

        });
        
      
    },
    loadImage: function () {
      
        $.ajax({
            url: "/Admin/Product/Loading",
            type: "GET",
            data: { id: $('#hidProductID').data('id') },
            dataType: "json",
            success: function (res) {
                if (res.status == true) {
                    var data = res.data;
                    $.each($(data), function (i, item) {
                        $('#imageList').append('<div style=" float: left;"><img src="' + item + '" width="100"/><a href="#" class="deleteImg">*</a></div>');
                        $('.deleteImg').off('click').on('click', function (e) {
                            e.preventDefault();
                            $(this).parent().remove();

                        });
                    });
                   
                }
            }

        });
    }
}
proDetail.init();