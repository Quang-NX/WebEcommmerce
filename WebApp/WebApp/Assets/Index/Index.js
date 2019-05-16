$(function () {
    $(".addCart,.add2").click(function () {
        $(".loader").show();
        var id = $(this).find("input").val();
        $.ajax({
            type: "GET",
            url: "/GioHang/ThemGioHangAjax?productId=" + id,
            dataType: "html",
            success: function (data) {
                $("#divGioHang").html(data);
                swal("Thành công", "Đã thêm vào giỏ hàng !", "success");
            },
            complete: function () {
                $(".loader").hide();
            }
        });
    });
    $("#divScrollTop").click(function () {
        $("body,html").animate({scrollTop:0},500);
    });
    //$(".add-to-cart-btn").click(function () {
    //    var id = $(this).find("input").val();
    //    $(".loader").show();
    //    $.ajax({
    //        type: "GET",
    //        url: "/GioHang/ThemGioHangAjax?productId=" + id,
    //        dataType: "html",
    //        success: function (data) {
    //            $("#divGioHang").html(data);
    //            swal("Thành công", "Đã thêm vào giỏ hàng !", "success");
    //        },
    //        complete: function () {
    //            $(".loader").hide();
    //        }
    //    });
    //});
    //$(".logo").click(function () {
    //    window.location.href = "";
    //});
    //$(".logo").click(function () {
    //    var href = $(this).attr("href");
    //    if (href.indexOf("javascript") >= 0)
    //        return;
    //    history.pushState(null, $(this).html, href);
    //});
    $(".hien").click(function () {
        $(this).addClass("active");
    });


    $("#btnDangNhap").click(function () {
        var bug = 0;
        if ($("#UserName").val() == '') {
            $("#TB_username").text("Tên đăng nhập không được bỏ trống");
            bug++;
        }

        else {
            $("#TB_username").text("");
        }

        if ($("#Password").val() == '') {
            $("#TB_password").text("Mật khẩu không được để trống");
            bug++;
        }
        else {
            $("#TB_password").text("");
        }

        if (bug != 0) {
            //ngăn form submit
            return false;
        }

    });
    $("#login").keypress(function (e) {
        if (e.which === 13) {
            $("#password").focus();
            return false;
        }
    });
    $("#btnCreateAccount").click(function () {
        var bug = 0;
        if ($("#UserName").val() == '') {
            $("#TB_UserName").text("Tên đăng nhập không được bỏ trống");
            bug++;
        }

        else {
            $("#TB_UserName").text("");
        }

        if ($("#Password").val() == '') {
            $("#TB_Password").text("Mật khẩu không được để trống");
            bug++;
        }
        else {
            $("#TB_Password").text("");
        }
        if ($("#ConfirmPassword").val() == '') {
            $("#TB_ConfirmPassword").text("Mật khẩu cũ không được để trống");
            bug++;
        }
        else {
            if ($("#ConfirmPassword").val() != $("#Password").val()) {
                $("#TB_ConfirmPassword").text("Mật khẩu không khớp");
                bug++;
            }
            else {
                $("#TB_ConfirmPassword").text("");
            }
        }

        if (bug != 0) {
            //ngăn form submit
            return false;
        }

    });
    
});