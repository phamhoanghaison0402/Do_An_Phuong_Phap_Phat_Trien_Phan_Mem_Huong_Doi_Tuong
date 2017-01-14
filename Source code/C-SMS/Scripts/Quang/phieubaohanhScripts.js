//Lay model name theo ten san pham
$(document).ready(function () {
    $('#modelName').on("change", function () {
        $.getJSON('/BaoHanh/LoadThongTinHangHoa',
                    { id: $('#modelName').val() },
                    function (data) {
                        if (data != null) {
                            $.each(data, function (index, row) {
                                $("#maHangHoa").val(row.TenHangHoa);
                            });
                        }
                    });
    });
})

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

function CheckSoDienThoai(error) {
    if ($("#soDienThoai").val() == '') {
        $(".messageErrorinputSoDienThoai").text("Nhập số điện thoại!");
        $(".notifyinputSoDienThoai").slideDown(250).removeClass("hidden");
        error++;
    }
    else {
        $(".notifyinputSoDienThoai").addClass("hidden");
        $("#soDienThoai").removeClass("error");
    }
    $("#soDienThoai").blur(function () {
        $("#soDienThoai").val($("#soDienThoai").val().trim());
    });
    return error;
}

function CheckModelName(error) {
    if ($("#maHangHoa").val() == '') {
        $(".messageErrorinputModelName").text("Chọn model name!");
        $(".notifyinputModelName").slideDown(250).removeClass("hidden");
        error++;
    }
    else {
        $(".notifyinputModelName").addClass("hidden");
        $("#maHangHoa").removeClass("error");
    }
    $("#maHangHoa").blur(function () {
        $("#maHangHoa").val($("#maHangHoa").val().trim());
    });
    return error;
}

$(document).ready(function () {
   
    $("#tenKhachHang").on('keyup input propertychange paste change', function () {
        CheckTenKhachHang();
    });

    $("#soDienThoai").on('keyup input propertychange paste change', function () {
        CheckSoDienThoai();
    });

    $("#maHangHoa").on('keyup input propertychange paste change', function () {
        CheckModelName();
    });
});
