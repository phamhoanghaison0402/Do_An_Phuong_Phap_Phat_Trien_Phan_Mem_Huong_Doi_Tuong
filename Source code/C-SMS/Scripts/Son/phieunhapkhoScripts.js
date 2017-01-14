$(document).ready(function () {
    var orderItems = [];
    $('#add').click(function () {
        var isValidItem = true;

        if ($('#tenHangHoa').val() == '') {
            isValidItem = false;
            $('#productItemError').text('Chưa có sản phẩm nào được chọn!');
        }
        else {
            $('#productItemError').hide();
        }

        var errorQuantity = 0;
        var errorPrice = 0;
        errorPrice = CheckPrice(errorPrice);
        errorQuantity = CheckQuantity(errorQuantity);

        var errorQuantity1 = 0;
        errorQuantity1 = CheckQuantity1(errorQuantity1);

        var errorQuantity2 = 0;
        errorQuantity2 = CheckQuantity2(errorQuantity2);

        var error = errorQuantity + errorPrice + errorQuantity1 + errorQuantity2;
  
        if (isValidItem == true && error == 0) {

            var i, j;
            var string_value_product = $('#maHangHoa').val().trim();

            var productID = string_value_product.slice(0, 10);

            if (orderItems.length > 0) {
                var test = true;
                var productIdOfTable = "";
                var row = document.getElementById('productTable').rows.length;
                for (var i = 1; i < row; i++) {
                    productIdOfTable = document.getElementById("productTable").rows[i].cells[0].innerHTML;

                    if (productIdOfTable == productID) {
                        test = false;
                        $('#maHangHoa').siblings('span.error').css('visibility', 'visible');
                        break;
                    }
                }

                if (test == true) {
                    $('#maHangHoa').siblings('span.error').css('visibility', 'hidden');
                    orderItems.push({
                        MaHangHoa: $('#maHangHoa').val().trim(),
                        TenHangHoa: $("#tenHangHoa").val().trim(),
                        DonViTinh: $('#donViTinh').val().trim(),
                        SoLuong: parseInt($('#soLuongNhap').val().trim()),
                        GiaNhap: parseInt($('#giaNhap').val().trim().replace(/,/gi, "")),
                        ThanhTien: parseInt($('#soLuongNhap').val().trim()) * parseInt($('#giaNhap').val().trim().replace(/,/gi, "")),
                    });

                    //Clear fields
                    $('#maHangHoa').focus().val('');
                    $('#tenHangHoa').val('');
                    $('#donViTinh').val('');
                    $('#soLuongNhap').val('');
                    $('#giaNhap').val('');
                    $('#thanhTien').val('');
         
                    GeneratedItemsTable();
                    SumTotalAmount();
                }
            }

            if (orderItems.length == 0) {
                orderItems.push({
                    MaHangHoa: $('#maHangHoa').val().trim(),
                    TenHangHoa: $("#tenHangHoa").val().trim(),
                    DonViTinh: $('#donViTinh').val().trim(),
                    SoLuong: parseInt($('#soLuongNhap').val().trim()),
                    GiaNhap: parseInt($('#giaNhap').val().trim()),
                    ThanhTien: parseInt($('#soLuongNhap').val().trim()) * parseInt($('#giaNhap').val().trim().replace(/,/gi, "")),
                });

                //Clear fields
                $('#maHangHoa').focus().val('');
                $('#tenHangHoa').val('');
                $('#donViTinh').val('');
                $('#soLuongNhap').val('');
                $('#giaNhap').val('');
                $('#thanhTien').val('');

                GeneratedItemsTable();
                SumTotalAmount();
            }
        }
    });

    $('#print').click(function () {
        Print();
    });

    function Print() {
        var toPrint = document.getElementById('Items');
        var $table = $('<table id="productTables" style="border: solid; width:100%; text-align:center"/>');
        $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn Vị Tính</th><th>Số Lượng Nhập</th><th>Giá Nhập (VND)</th><th>Thành Tiền (VND)</th></tr></thead>');
        var $tbody = $('<tbody/>');
        $.each(orderItems, function (i, val) {
            var $row = $('<tr style="border:solid">');
            $row.append($('<td/>').html(val.MaHangHoa));
            $row.append($('<td/>').html(val.TenHangHoa));
            $row.append($('<td/>').html(val.DonViTinh));
            $row.append($('<td/>').html(val.SoLuong));
            $row.append($('<td/>').html(formatNumber(val.GiaNhap)));
            $row.append($('<td/>').html(formatNumber(val.ThanhTien)));
            $tbody.append($row);
        });
        console.log("current", orderItems);
        $table.append($tbody);
        $('#Items').html($table);

        var popupWin = window.open('', '_blank', 'width=800,height=600'); //create new page     
        popupWin.document.open(); //open new page
        popupWin.document.write('<html><body onload="window.print()">')

        popupWin.document.write('<p style="text-align:center"><img src="/Content/image/header.png" class="img-responsive watch-right"  /></p>')

        popupWin.document.write('<p style="text-align:center; font-weight: bold; font-size: 30px">Phiếu Nhập Kho</p>')

        popupWin.document.write('<table style="border:solid; width:100%"; text-align:center">')
        popupWin.document.write('Thông tin phiếu nhập kho');
        popupWin.document.write('<tr><td>')
        popupWin.document.write('Số phiếu nhập kho: ');
        popupWin.document.write($('#soPhieuNhapKho').val().trim());
        popupWin.document.write('</td>')
        popupWin.document.write('<td>')
        popupWin.document.write('Ngày nhập: ');
        popupWin.document.write($('#ngayNhapKho').val().trim());
        popupWin.document.write('</td></tr>')

        popupWin.document.write('<tr><td>')
        popupWin.document.write('Nhân viên: ');
        popupWin.document.write($('#tenNhanVien').val().trim());
        popupWin.document.write('</td>')
        popupWin.document.write('<td>')
        popupWin.document.write('Nhà cung cấp: ');
        popupWin.document.write($('#nhacungcap').find("option:selected").text());
        popupWin.document.write('</td></tr>')

        popupWin.document.write('<tr><td>')
        popupWin.document.write('Tổng tiền: ');
        popupWin.document.write($('#tongTien').val().trim() + " VND");
        popupWin.document.write('</td>')
        popupWin.document.write('<td>')
        popupWin.document.write('Ghi chú: ');
        popupWin.document.write($('#ghiChu').val().trim());
        popupWin.document.write('</td></tr>')

        popupWin.document.write('</table>')

        popupWin.document.write('<br>');
        popupWin.document.write('Danh sách hàng hóa');
        popupWin.document.write(toPrint.innerHTML);

        popupWin.document.write('<p style="text-align:center">')
        popupWin.document.write('Nhân viên kho')
        popupWin.document.write('<br>')
        popupWin.document.write('(Ký tên)')
        popupWin.document.write('</p>')
        popupWin.document.write('</html>');
        popupWin.document.close();
    }

    //Save button click function
    $('#submit').click(function () {
        //validation of inventory ballot detail
        var isAllValid = true;
        if (orderItems.length == 0) {
            $('#orderItems').html('<span class="messageError" style="color:red;">Phải có ít nhất 1 hàng hóa</span>');
            isAllValid = false;
        }

        //Save if valid
        if (isAllValid) {
            var data = {
                SoPhieuNhapKho: $('#soPhieuNhapKho').val().trim(),
                NgayNhapKho: $('#ngayNhapKho').val().trim(),
                MaNhanVien: $('#maNhanVien').val().trim(),
                MaNhaCungCap: $('#nhacungcap').val().trim(),
                TongTien: parseFloat($('#tongTien').val().trim().replace(/,/gi, "")),
                GhiChu: $('#ghiChu').val().trim(),
                chiTietPhieuNhap: orderItems
            }

            $(this).val('Please wait...');

            $.ajax({
                url: "/NhapKho/LuuPhieuNhapKho",
                type: "POST",
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: "application/json",
                success: function (d) {
                    //check is successfully save to database
                    if (d.status == true) {
                        //will send status from server side
                        //clear form
                        orderItems = [];
                        $('#soPhieuNhapKho').val('');
                        $('#ngayNhapKho').val('');
                        $('#tenNhanVien').val('');
                        $('#ghiChu').val('');
                        $('#orderItems').empty();
                        window.location.href = '/Admin/NhapKho/';
                    }
                    else {
                        SetAlert("Something wrong! Please try again", "error");
                    }
                    $('#submit').val('Lưu Phiếu Nhập Kho');
                },
                error: function () {
                    alert('Error. Please try again.');
                    $('#submit').val('Lưu Phiếu Nhập Kho');
                }
            });
        }
    });

    function GeneratedItemsTable() {
        if (orderItems.length > 0) {
            var $table = $('<table id="productTable"  class="table table-bordered"/>');
            $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn Vị Tính</th><th>Số Lượng Nhập</th><th>Giá Nhập</th><th>Thành Tiền</th><th> Hành Động</th></tr></thead>');
            var $tbody = $('<tbody/>');
            $.each(orderItems, function (i, val) {
                var $row = $('<tr/>');
                $row.append($('<td/>').html(val.MaHangHoa));
                $row.append($('<td/>').html(val.TenHangHoa));
                $row.append($('<td/>').html(val.DonViTinh));
                $row.append($('<td/>').html(val.SoLuong));
                $row.append($('<td/>').html(formatNumber(val.GiaNhap)));
                $row.append($('<td/>').html(formatNumber(val.ThanhTien)));
                var $remove = $('<input type="button" value="Xóa" style="padding:1px 20px" class="btn-danger"/>');
                $remove.click(function (e) {
                    e.preventDefault();
                    orderItems.splice(i, 1);
                    GeneratedItemsTable();

                    if (orderItems.length == 0) {
                        $('#tongTien').val(0);
                    } else {
                        SumTotalAmount();
                    }

                    ClearValue();
                    $('#maHangHoa').focus().val('');

                });
                $row.append($('<td/>').html($remove));
                $tbody.append($row);
            });
            console.log("current", orderItems);
            $table.append($tbody);
            $('#orderItems').html($table);
        }
        else {
            $('#orderItems').html('');
        }
    }

    function SumTotalAmount() {
        var amount;

        var total = 0.0;
        var row = document.getElementById('productTable').rows.length;
        for (var i = 1; i < row; i++) {
            amount = document.getElementById("productTable").rows[i].cells[5].innerHTML.replace(/,/gi, "");

            total += parseFloat(amount);
        }
        $('#tongTien').val(formatNumber(parseFloat(total)));
    }

});

// function only enter number
function checkNumber(e, element) {
    var charcode = (e.which) ? e.which : e.keyCode;
    //Check number
    if (charcode > 31 && (charcode < 48 || charcode > 57)) {
        return false;
    }
    return true;
}

//hidden error when user enter into textbox productID
function HideErrorProductName() {
    if (document.getElementById('tenHangHoa').value != '') {
        $('#tenHangHoa').siblings('span.error').css('visibility', 'hidden');
    }
}

$(document).ready(function () {
    $('#maHangHoa').on("change", function () {
        $.getJSON('/NhapKho/LoadThongTinHangHoa',
                    { id: $('#maHangHoa').val() },
                    function (data) {
                        if (data != null) {
                            $.each(data, function (index, row) {
                                $("#tenHangHoa").val(row.TenHangHoa);
                                $("#donViTinh").val(row.DonViTinh);

                            });
                        }
                    });
    });
})

function ClearValue() {
    $("#productName").val('');
    $("#unitName").val('');
    $("#currentQuantity").val('');

    $('#productID').siblings('span.error').css('visibility', 'hidden');
    $('#productName').siblings('span.error').css('visibility', 'hidden');
    $('#checkQuantity').siblings('span.error').css('visibility', 'hidden');
}

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}

function HideErrorMaHangHoa() {
    if (document.getElementById('maHangHoa').value != '') {
        $('#maHangHoa').siblings('span.error').css('visibility', 'hidden');
    }
}

function HideErrorTenHangHoa() {
    if (document.getElementById('tenHangHoa').value != '') {
        $('#tenHangHoa').siblings('span.error').css('visibility', 'hidden');
    }
}

function HideErrorSoLuongNhap() {
    if (document.getElementById('soLuongNhap').value != '') {
        $('#soLuongNhap').siblings('span.error').css('visibility', 'hidden');
    }
}

function HideErrorGiaNhap() {
    if (document.getElementById('giaNhap').value != '') {
        $('#giaNhap').siblings('span.error').css('visibility', 'hidden');
    }
}

//$(document).ready(function () {
//    $('#soLuongNhap').on("input", function () {
//        var a = document.getElementById("giaNhap").value;
//        var b = document.getElementById("soLuongNhap").value;
//        var x = Number(a) * Number(b);
//        document.getElementById("thanhTien").value = x;
//    });
//})

//$(document).ready(function () {
//    $('#giaNhap').on("input", function () {
//        var a = document.getElementById("giaNhap").value;
//        var b = document.getElementById("soLuongNhap").value;
//        var x = Number(a) * Number(b);
//        document.getElementById("thanhTien").value = x;
//    });
//})

$(document).ready(function () {
    //this calculates values automatically
    Multiplica();
    $("#soLuongNhap").on("keydown keyup", function () {
        Multiplica();
    });

    $("#giaNhap").on("keydown keyup", function () {
        Multiplica();
    });
});

function Multiplica() {
    if (document.getElementById('soLuongNhap').value == '' || document.getElementById('giaNhap').value == 0) {
        document.getElementById('thanhTien').value = 0;
    }
    else {
        var unitPrice = document.getElementById('giaNhap').value.replace(/,/gi, "");
        var quantity = document.getElementById('soLuongNhap').value;
        var result = parseInt(unitPrice) * parseInt(quantity);
        if (!isNaN(result)) {
            document.getElementById('thanhTien').value = formatNumber(result);
        }

        //var quantityInventory = document.getElementById('soLuongTon').value;
        //if (quantity > (parseInt(quantityInventory))) {
        //    document.getElementById('soLuongXuat').value = quantityInventory;

        //    var result_ = parseInt(unitPrice) * parseInt(quantity);
        //    if (!isNaN(result_)) {
        //        document.getElementById('thanhTien').value = formatNumber(result_);
        //    }
        //}
    }
}

$(document).ready(function () {
    $("#soLuongNhap").on('keyup input propertychange paste change', function () {
        CheckQuantity();
        CheckQuantity1();
    });

    $("#giaNhap").on('keyup input propertychange paste change', function () {
        CheckPrice();
        CheckQuantity2();
    });
});

// check quantity input
function CheckQuantity(error) {
    if (!($('#soLuongNhap').val().trim() != '' && !isNaN($('#soLuongNhap').val().trim())) ) {
        $(".messageErrorinputQuantity").text("Nhập số lượng!");
        $(".notifyinputQuantity").slideDown(250).removeClass("hidden");
        $("#soLuongNhap").addClass("error");
        error++;
    }
    else {
        $(".notifyinputQuantity").addClass("hidden");
        $("#soLuongNhap").removeClass("error");
    }
    $("#soLuongNhap").blur(function () {
        $("#soLuongNhap").val($("#soLuongNhap").val().trim());
    });
    return error;
}

function CheckPrice(error) {
    if (!($('#giaNhap').val().trim() != '' && !isNaN($('#giaNhap').val().trim())) ) {
        $(".messageErrorinputPrice").text("Nhập giá!");
        $(".notifyinputPrice").slideDown(250).removeClass("hidden");
        $("#giaNhap").addClass("error");
        error++;
    }
    else {
        $(".notifyinputPrice").addClass("hidden");
        $("#giaNhap").removeClass("error");
    }
    $("#giaNhap").blur(function () {
        $("#giaNhap").val($("#giaNhap").val().trim());
    });
    return error;
}

function CheckQuantity1(error) {
    if (($('#soLuongNhap').val().trim() == '0') || ($('#soLuongNhap').val().trim() == '00') || ($('#soLuongNhap').val().trim() == '000') || ($('#soLuongNhap').val().trim() == '0000')) {
        $(".messageErrorinputQuantity1").text("Nhập số lượng lớn hơn 0!");
        $(".notifyinputQuantity1").slideDown(250).removeClass("hidden");
        $("#soLuongNhap").addClass("error");
        error++;
    }
    else {
        $(".notifyinputQuantity1").addClass("hidden");
        $("#soLuongNhap").removeClass("error");
    }
    $("#soLuongNhap").blur(function () {
        $("#soLuongNhap").val($("#soLuongNhap").val().trim());
    });
    return error;
}


function CheckQuantity2(error) {
    if (($('#giaNhap').val().trim() == '0') || ($('#giaNhap').val().trim() == '00') || ($('#giaNhap').val().trim() == '000') || ($('#giaNhap').val().trim() == '0000') || ($('#giaNhap').val().trim() == '00000') || ($('#giaNhap').val().trim() == '000000') || ($('#giaNhap').val().trim() == '0000000') || ($('#giaNhap').val().trim() == '00000000') || ($('#giaNhap').val().trim() == '000000000') || ($('#giaNhap').val().trim() == '0000000000')) {
        $(".messageErrorinputQuantity2").text("Giá nhập phải lớn hơn 0!");
        $(".notifyinputQuantity2").slideDown(250).removeClass("hidden");
        $("#giaNhap").addClass("error");
        error++;
    }
    else {
        $(".notifyinputQuantity2").addClass("hidden");
        $("#giaNhap").removeClass("error");
    }
    $("#giaNhap").blur(function () {
        $("#giaNhap").val($("#giaNhap").val().trim());
    });
    return error;
}