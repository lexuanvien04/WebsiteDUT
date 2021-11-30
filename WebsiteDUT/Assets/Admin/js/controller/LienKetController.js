var cm = {
    init: function () {
        cm.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/LienKets/LienKetTrangThai",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('kích hoạt');
                        
                    }
                    else {
                        btn.text('khoá');
                        
                    }
                }
            });
        });
    }
}
cm.init();