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
        errorQuantity = CheckQuantity(errorQuantity);

        var errorQuantity1 = 0;
        errorQuantity1 = CheckQuantity1(errorQuantity1);

        var error = errorQuantity + errorQuantity1;

        if (isValidItem == true && error == 0) {

            var i, j;
            var string_value_product = $('#maHangHoa').val().trim();

            var productID = string_value_product.slice(0, 10); // PR00001

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
                        SoLuong: parseInt($('#soLuongXuat').val().trim()),
                        Gia: parseInt($('#gia').val().trim().replace(/,/gi, "")),
                        ThanhTien: parseInt($('#soLuongXuat').val().trim()) * parseInt($('#gia').val().trim().replace(/,/gi, "")),
                    });

                    //Clear fields
                    $('#maHangHoa').focus().val('');
                    $('#tenHangHoa').val('');
                    $('#donViTinh').val('');
                    $('#soLuongTon').val('');
                    $('#soLuongXuat').val('');
                    $('#gia').val('');
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
                    SoLuong: parseInt($('#soLuongXuat').val().trim()),
                    Gia: parseInt($('#gia').val().trim().replace(/,/gi, "")),
                    ThanhTien: parseInt($('#soLuongXuat').val().trim()) * parseInt($('#gia').val().trim().replace(/,/gi, "")),
                });

                //Clear fields
                $('#maHangHoa').focus().val('');
                $('#tenHangHoa').val('');
                $('#donViTinh').val('');
                $('#soLuongTon').val('');
                $('#soLuongXuat').val('');
                $('#gia').val('');
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
        $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn Vị Tính</th><th>Số Lượng Xuất</th><th>Giá (VND)</th><th>Thành Tiền (VND)</th></tr></thead>');
        var $tbody = $('<tbody/>');
        $.each(orderItems, function (i, val) {
            var $row = $('<tr style="border:solid">');
            $row.append($('<td/>').html(val.MaHangHoa));
            $row.append($('<td/>').html(val.TenHangHoa));
            $row.append($('<td/>').html(val.DonViTinh));
            $row.append($('<td/>').html(val.SoLuong));
            $row.append($('<td/>').html(formatNumber(val.Gia)));
            $row.append($('<td/>').html(formatNumber(val.ThanhTien)));
            $tbody.append($row);
        });
        console.log("current", orderItems);
        $table.append($tbody);
        $('#Items').html($table);

        var popupWin = window.open('', '_blank', 'width=800,height=500'); //create new page     
        popupWin.document.open(); //open new page
        popupWin.document.write('<html><body onload="window.print()">')

        popupWin.document.write('<p style="text-align:center"><img src="/Content/image/header.png" class="img-responsive watch-right"  /></p>')

        popupWin.document.write('<p style="text-align:center; font-weight: bold; font-size: 30px">Phiếu Xuất Kho</p>')

        popupWin.document.write('<table style="border:solid; width:100%"; text-align:center">')
        popupWin.document.write('Thông tin phiếu xuất kho');
        popupWin.document.write('<tr><td>')
        popupWin.document.write('Số phiếu xuất kho: ');
        popupWin.document.write($('#soPhieuXuatKho').val().trim());
        popupWin.document.write('</td>')

        popupWin.document.write('<td>')
        popupWin.document.write('Ngày xuất: ');
        popupWin.document.write($('#ngayXuatKho').val().trim());
        popupWin.document.write('</td></tr>')

        popupWin.document.write('<tr><td>')
        popupWin.document.write('Nhân viên: ');
        popupWin.document.write($('#tenNhanVien').val().trim());
        popupWin.document.write('</td>')

        popupWin.document.write('<td>')
        popupWin.document.write('Tổng tiền: ');
        popupWin.document.write($('#tongTien').val().trim() + " VNĐ");
        popupWin.document.write('</td></tr>')


        popupWin.document.write('<tr><td>')
        popupWin.document.write('Ghi chú: ');
        popupWin.document.write($('#lyDoXuat').val().trim());
        popupWin.document.write('</td></tr>')

        popupWin.document.write('</table>')

        popupWin.document.write('<br>');
        popupWin.document.write('Danh sách sản phẩm');
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
        var isAllValid = true;
        if (orderItems.length == 0) {
            $('#orderItems').html('<span class="messageError" style="color:red;">Phải có ít nhất 1 hàng hóa</span>');
            isAllValid = false;
        }

        if (isAllValid) {
            var data = {
                SoPhieuXuatKho: $('#soPhieuXuatKho').val().trim(),
                NgayXuatKho: $('#ngayXuatKho').val().trim(),
                MaNhanVien: $('#maNhanVien').val().trim(),
                TongTien: parseFloat($('#tongTien').val().trim().replace(/,/gi, "")),
                LyDoXuat: $('#lyDoXuat').val().trim(),
                chiTietPhieuXuatKho: orderItems
            }

            $(this).val('Please wait...');

            $.ajax({
                url: "/XuatKho/LuuPhieuXuatKho",
                type: "POST",
                data: JSON.stringify(data),
                dataType: "JSON",
                contentType: "application/json",
                success: function (d) {
                    if (d.status == true) {
                        orderItems = [];
                        $('#soPhieuXuatKho').val('');
                        $('#ngayXuatKho').val('');
                        $('#tenNhanVien').val('');
                        $('#lyDoXuat').val('');
                        $('#orderItems').empty();
                        window.location.href = '/Admin/XuatKho/';
                    }
                    else {
                        SetAlert("Something wrong! Please try again", "error");
                    }
                    $('#submit').val('Lưu Phiếu Xuất Kho');
                },
                error: function () {
                    alert('Error. Please try again.');
                    $('#submit').val('Lưu Phiếu Xuất Kho');
                }
            });
        }
    });

    function GeneratedItemsTable() {
        if (orderItems.length > 0) {
            var $table = $('<table id="productTable"  class="table table-bordered" />');
            $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn Vị Tính</th><th>Số Lượng Xuất</th><th>Giá</th><th>Thành Tiền</th><th> Hành Động</th></tr></thead>');
            var $tbody = $('<tbody/>');
            $.each(orderItems, function (i, val) {
                var $row = $('<tr/>');
                $row.append($('<td/>').html(val.MaHangHoa));
                $row.append($('<td/>').html(val.TenHangHoa));
                $row.append($('<td/>').html(val.DonViTinh));
                $row.append($('<td/>').html(val.SoLuong));
                $row.append($('<td/>').html(formatNumber(val.Gia)));
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


$(document).ready(function () {
    $('#maHangHoa').on("change", function () {
        $.getJSON('/XuatKho/LoadThongTinHangHoa',
                    { id: $('#maHangHoa').val() },
                    function (data) {
                        if (data != null) {
                            $.each(data, function (index, row) {
                                $("#tenHangHoa").val(row.TenHangHoa);
                                $("#donViTinh").val(row.DonViTinh);
                                if (row.GiamGia == 0)
                                {
                                    $("#gia").val(formatNumber(row.GiaBan));
                                }
                                else {
                                    $("#gia").val(formatNumber(row.GiamGia));
                                }

                                $("#soLuongTon").val(row.SoLuongTon);
                            });
                        }
                        
                    });
    });
})

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

function HideErrorSoLuongXuat() {
    if (document.getElementById('soLuongXuat').value != '') {
        $('#soLuongXuat').siblings('span.error').css('visibility', 'hidden');
    }
}

$(document).ready(function () {
    //this calculates values automatically
    Multiplica();
    $("#soLuongXuat").on("keydown keyup", function () {
        Multiplica();
    });

    $("#unitPrice").on("keydown keyup", function () {
        Multiplica();
    });
});

function Multiplica() {
    if (document.getElementById('soLuongXuat').value == '' || document.getElementById('soLuongXuat').value == 0) {
        document.getElementById('thanhTien').value = 0;
    }
    else {
        var unitPrice = document.getElementById('gia').value.replace(/,/gi, "");
        var quantity = document.getElementById('soLuongXuat').value;
        var result = parseInt(unitPrice) * parseInt(quantity);
        if (!isNaN(result)) {
            document.getElementById('thanhTien').value = formatNumber(result);
        }

        var quantityInventory = document.getElementById('soLuongTon').value;
        if (quantity > (parseInt(quantityInventory))) {
            document.getElementById('soLuongXuat').value = quantityInventory;

            var result_ = parseInt(unitPrice) * parseInt(quantity);
            if (!isNaN(result_)) {
                document.getElementById('thanhTien').value = formatNumber(result_);
            }
        }
    }
}

$(document).ready(function () {
    $("#soLuongXuat").on('keyup input propertychange paste change', function () {
        CheckQuantity();
        CheckQuantity1();
    });
});

// check quantity input
function CheckQuantity(error) {
    if (!($('#soLuongXuat').val().trim() != '' && !isNaN($('#soLuongXuat').val().trim()))) {
        //if ($("#soLuongKiemTra").val() == '') {
        $(".messageErrorinputQuantity").text("Nhập số lượng!");
        $(".notifyinputQuantity").slideDown(250).removeClass("hidden");
        $("#soLuongXuat").addClass("error");
        error++;
    }
    else {
        $(".notifyinputQuantity").addClass("hidden");
        $("#soLuongXuat").removeClass("error");
    }
    $("#soLuongXuat").blur(function () {
        $("#soLuongXuat").val($("#soLuongXuat").val().trim());
    });
    return error;
}

function CheckQuantity1(error) {
    if (($('#soLuongXuat').val().trim() == '0') || ($('#soLuongXuat').val().trim() == '00') || ($('#soLuongXuat').val().trim() == '000') || ($('#soLuongXuat').val().trim() == '0000')) {
        $(".messageErrorinputQuantity1").text("Nhập số lượng lớn hơn 0!");
        $(".notifyinputQuantity1").slideDown(250).removeClass("hidden");
        $("#soLuongXuat").addClass("error");
        error++;
    }
    else {
        $(".notifyinputQuantity1").addClass("hidden");
        $("#soLuongXuat").removeClass("error");
    }
    $("#soLuongXuat").blur(function () {
        $("#soLuongXuat").val($("#soLuongXuat").val().trim());
    });
    return error;
}
