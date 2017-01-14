//Notify if press CAPSLOCK
$("input[type='password']").keypress(function (e) {
    var kc = e.which;
    var isUp = (kc >= 65 && kc <= 90) ? true : false;
    var isLow = (kc >= 97 && kc <= 122) ? true : false;
    var isShift = (e.shiftKey) ? e.shiftKey : ((kc == 16) ? true : false);
    if (!($(".notifyjs-wrapper").hasClass("notifyjs-hidable")) && ((isUp && !isShift) || (isLow && isShift))) {
        $.notify("Warning: CAPSLOCK is on!!!", "warn");
    }
});

//Check empty string
function CheckEmpty(s) {
    return s.replace(/^\s+|\s+$/gm, '').length == 0;
}

//Check password input
function PassWord() {
    if (($("#PassWord").val() == "" || CheckEmpty($("#PassWord").val()))) {
        $(".messageErrorPassWord").text("Không được để trống!!!");
        $(".notifyPassWord").slideDown(250).removeClass("hidden");
        //$("#PassWord").addClass("error");
        //$("#NewPassword").val("");
    }
    else {
        if ($("#PassWord").val().length < 8) {
            $(".messageErrorPassWord").text("Mật khẩu mới của bạn quá ngắn!!!");
            $(".notifyPassWord").slideDown(250).removeClass("hidden");
            //$("#PassWord").addClass("error");
        }
        else {
            $(".notifyPassWord").addClass("hidden");
            //$("#PassWord").removeClass("error");
        }
    }
}

function CheckTenKhachHang(error) {
    if ($("#tenKhachHang").val() == '') {
        $(".messageErrorinputTenKhachHang").text("Nhập tên khách hàng!");
        $(".notifyinputTenKhachHang").slideDown(250).removeClass("hidden");
        error++;
    }
    else {
        $(".notifyinputTenKhachHang").addClass("hidden");
        $("#tenKhachHang").removeClass("error");
    }
    $("#tenKhachHang").blur(function () {
        $("#tenKhachHang").val($("#tenKhachHang").val().trim());
    });
    return error;
}

//Check new password input
function NewPassword() {
    if (($("#PassWord").val() == "" || CheckEmpty($("#PassWord").val()))) {
        $(".messageErrorPassWord").text("Không được để trống!!!");
        $(".notifyPassWord").slideDown(250).removeClass("hidden");
        //$("#PassWord").addClass("error");
        $("#PassWord").focus();
        $("#NewPassword").val("");
    }
    else {
        if (($("#NewPassword").val() == "" || CheckEmpty($("#NewPassword").val())) && $("#NewPassword").hasClass("error")) {
            $(".messageErrorNewPassword").text("Không được để trống!!!");
            $(".notifyNewPassword").slideDown(250).removeClass("hidden");
            //$("#NewPassword").addClass("error");
        }
        else {
            if ($("#NewPassword").val() != $("#PassWord").val()) {
                $(".messageErrorNewPassword").text("Mật khẩu nhập lại không chính xác!!!");
                $(".notifyNewPassword").slideDown(250).removeClass("hidden");
                //$("#NewPassword").addClass("error");
            }
            else {
                $(".notifyNewPassword").addClass("hidden");
                //$("#NewPassword").removeClass("error");
            }
        }
    }
}

//Envent of inputs
$("#PassWord").on('focus', function () {
    OldPassword();
});

$("#PassWord").on('keyup input propertychange paste change', function () {
    PassWord();
});

$("#NewPassword").on('keyup input propertychange paste change focus', function () {
    NewPassword();
});

$("#btnSave").on('focus', function () {
    OldPassword();
    PassWord();
    NewPassword();
    if (($("#NewPassword").val() == "" || CheckEmpty($("#NewPassword").val()))) {
        $(".messageErrorNewPassword").text("Không được để trống!!!");
        $(".notifyNewPassword").slideDown(250).removeClass("hidden");
        $("#NewPassword").addClass("error");
        $("#NewPassword").focus();
    }
});