var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/NguoiDung/ChangeStatus",
                data: { MaNguoiDung: id },
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
user.init();