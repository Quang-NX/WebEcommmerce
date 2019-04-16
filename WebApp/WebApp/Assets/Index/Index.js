$(function () {
    $(".addCart").click(function () {
        var id = $(this).find("input").val();
        $.ajax({
            type: "GET",
            url: "/GioHang/ThemGioHangAjax?productId=" + id,
            dataType: "html",
            success: function (data) {
                $("#divGioHang").html(data);
            }
        });
    });
    $(".hien").click(function () {
        alert(1);
        $(this).addClass("active");
    });

});