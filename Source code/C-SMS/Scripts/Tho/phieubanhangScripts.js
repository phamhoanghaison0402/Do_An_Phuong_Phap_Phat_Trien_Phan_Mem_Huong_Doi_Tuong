$(document).ready(function () {
    var orderItems = [];
    //Add button click function
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
        var error = errorQuantity;

        //Add product to list if valid
        if (isValidItem == true && error == 0) {
            var i, j;
            var string_value_product = $('#maHangHoa').val().trim();

            //get value IdorderItems.pus
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
                    $('#productStock').siblings('span.error').css('visibility', 'hidden');
                    orderItems.push({
                        SoPhieuBanHang: $('#soPhieuBanHang').val().trim(),
                        MaHangHoa: $('#maHangHoa').val().trim(),
                        TenHangHoa: $("#tenHangHoa").val().trim(),
                        Gia: $('#donGia').val().trim().replace(/,/gi, ""),
                        SoLuong: parseInt($('#soLuong').val().trim()),
                        ThanhTien: parseInt($('#thanhTien').val().trim().replace(/,/gi, "")),
                    });

                    //Clear fields
                    $('#maHangHoa').focus().val('');
                    $('#tenHangHoa').val('');
                    $('#donGia').val('');
                    $('#soLuong').val('');
                    $('#thanhTien').val('');
                    $('#soLuongTon').val('');

                    //populate inventory ballot detail
                    GeneratedItemsTable();
                }
            }

            if (orderItems.length == 0) {
                $('#productStock').siblings('span.error').css('visibility', 'hidden');
                orderItems.push({
                    SoPhieuBanHang: $('#soPhieuBanHang').val().trim(),
                    MaHangHoa: $('#maHangHoa').val().trim(),
                    TenHangHoa: $("#tenHangHoa").val().trim(),
                    Gia: $('#donGia').val().trim().replace(/,/gi, ""),
                    SoLuong: parseInt($('#soLuong').val().trim()),
                    ThanhTien: parseInt($('#thanhTien').val().trim().replace(/,/gi, "")),
                });

                //Clear fields
                $('#maHangHoa').focus().val('');
                $('#tenHangHoa').val('');
                $('#donGia').val('');
                $('#soLuong').val('');
                $('#thanhTien').val('');
                $('#soLuongTon').val('');

                //populate inventory ballot detail
                GeneratedItemsTable();
            }
        }
    });

    //Print inventoy ballot when click button Print
    $('#print').click(function () {
        Print();
    });

    //Function print inventory ballot
    function Print() {
        var toPrint = document.getElementById('Items');
        var $table = $('<table id="productTables" style="border: solid; width:100%; text-align:center"/>');
        $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn Giá (VNĐ)</th><th>Số Lượng</th><th>Thành Tiền (VNĐ)</th></tr></thead>');
        var $tbody = $('<tbody/>');
        $.each(orderItems, function (i, val) {
            var $row = $('<tr style="border:solid">');
            $row.append($('<td/>').html(val.MaHangHoa));
            $row.append($('<td/>').html(val.TenHangHoa));
            $row.append($('<td/>').html(formatNumber(val.Gia)));
            $row.append($('<td/>').html(val.SoLuong));
            $row.append($('<td/>').html(formatNumber(val.ThanhTien)));
            $tbody.append($row);
        });
        console.log("current", orderItems);
        $table.append($tbody);
        $('#Items').html($table);

        var popupWin = window.open('', '_blank', 'width=800,height=600'); //create new page     
        popupWin.document.open(); //open new page
        popupWin.document.write('<html><body onload="window.print()">')
        
        popupWin.document.write('<p style="text-align:center"><img src="/Content/image/header.png" class="img-responsive watch-right" alt="" /></p>')
  
        popupWin.document.write('<p style="text-align:center; font-weight: bold; font-size: 30px">Phiếu Bán Hàng</p>')

        popupWin.document.write('<table style="border:solid; width:100%"; text-align:center">')
        popupWin.document.write('Thông tin phiếu bán hàng');
        popupWin.document.write('<tr><td>')
        popupWin.document.write('Số phiếu bán hàng: ');
        popupWin.document.write($('#soPhieuBanHang').val().trim());
        popupWin.document.write('</td>')

        popupWin.document.write('<td>')
        popupWin.document.write('Ngày lập: ');
        popupWin.document.write($('#ngayBan').val().trim());
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
        popupWin.document.write('Khách hàng: ');
        popupWin.document.write($('#tenKhachHang').val().trim());
        popupWin.document.write('</td>')

        popupWin.document.write('<td>')
        popupWin.document.write('Số điện thoại: ');
        popupWin.document.write($('#soDienThoai').val().trim());
        popupWin.document.write('</td></tr>')

        popupWin.document.write('<tr><td>')
        popupWin.document.write('Ghi chú: ');
        popupWin.document.write($('#ghiChu').val().trim());

        popupWin.document.write('</td></tr>')

        popupWin.document.write('</table>')

        popupWin.document.write('<br>');
        popupWin.document.write('Danh sách sản phẩm');
        popupWin.document.write(toPrint.innerHTML);

        popupWin.document.write('<p style="text-align:center">')
        popupWin.document.write('Nhân viên bán hàng')
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
            $('#orderItems').html('<span style="color:red;">Phải có ít nhất 1 hàng hóa</span>');
            isAllValid = false;
        }

        var errorTenKhachHang = 0;
        errorTenKhachHang = CheckTenKhachHang(errorTenKhachHang);
        var errorSoDienThoai = 0;
        errorSoDienThoai = CheckSoDienThoai(errorSoDienThoai);
        var error = errorTenKhachHang + errorSoDienThoai;



        //Save if valid
        if (isAllValid == true && error == 0) {
            var data = {
                SoPhieuBanHang: $('#soPhieuBanHang').val().trim(),
                NgayBan: $('#ngayBan').val().trim(),
                TenNhanVien: $('#tenNhanVien').val().trim(),
                MaNhanVien: $('#maNhanVien').val().trim(),
                GhiChu: $('#ghiChu').val().trim(),
                TenKhachHang: $('#tenKhachHang').val().trim(),
                SoDienThoai: $('#soDienThoai').val().trim(),
                TongTien: $('#tongTien').val().replace(/,/gi, "").trim(),
                chiTietPhieuBanHang: orderItems
            }

            $(this).val('Đang xử lý...');

            $.ajax({
                url: "/BanHang/LuuPhieuBanHang",
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
                        $('#soPhieuBanHang').val('');
                        $('#ngayBan').val('');
                        $('#tenNhanVien').val('');
                        $('#ghiChu').val('');
                        $('#orderItems').empty();
                        window.location.href = '/Admin/BanHang/';
                    }
                    else {
                        window.location.href = '/Admin/BanHang/Create';
                    }
                },
                error: function () {
                    alert('Đã xảy ra lỗi! xin hãy tạo lại phiếu bán hàng');
                }
            });
        }
    });

    //function for show added product in table
    function GeneratedItemsTable() {
        if (orderItems.length > 0) {
            var $table = $('<table id="productTable"  class="table table-bordered"/>');
            $table.append('<thead><tr><th>Mã Hàng Hóa</th><th>Tên Hàng Hóa</th><th>Đơn giá</th><th>Số lượng</th><th>Thành tiền</th><th>Hành Động</th></tr></thead>');
            var $tbody = $('<tbody/>');
            $.each(orderItems, function (i, val) {

                var $row = $('<tr/>');
                $row.append($('<td/>').html(val.MaHangHoa));
                $row.append($('<td/>').html(val.TenHangHoa));
                $row.append($('<td/>').html(formatNumber(val.Gia)));
                $row.append($('<td/>').html(val.SoLuong));
                $row.append($('<td/>').html(formatNumber(val.ThanhTien)));
                var $remove = $('<input type="button" value="Xóa" style="padding:1px 20px" class="btn-danger"/>');
                $remove.click(function (e) {
                    e.preventDefault();
                    orderItems.splice(i, 1);
                    GeneratedItemsTable();
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

        var tongTien = 0;
        for (var i = 0; i < orderItems.length; i++) {
            tongTien += parseInt(orderItems[i].ThanhTien);
        }

        document.getElementById('tongTien').value = formatNumber(tongTien);
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
        $.getJSON('/BanHang/LoadThongTinHangHoa',
                    { id: $('#maHangHoa').val() },
                    function (data) {
                        if (data != null) {
                            $.each(data, function (index, row) {
                                $("#tenHangHoa").val(row.TenHangHoa);
                                $("#donGia").val(formatNumber(row.GiaBan));
                                $("#soLuongTon").val(formatNumber(row.SoLuongTon));
                            });
                        }
                    });
    });
})

$(document).ready(function () {

    //this calculates values automatically
    Multiplica();
    $("#soLuong").on("keydown keyup change input paste propertychange", function () {
        Multiplica();
    });
});

function formatNumber(num) {
    return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,")
}

function Multiplica() {
    if (document.getElementById('soLuong').value == '' || document.getElementById('soLuong').value == 0) {
        document.getElementById('thanhTien').value = 0;
    }
    else {
        var unitPrice = document.getElementById('donGia').value.replace(/,/gi, "");
        var quantity = document.getElementById('soLuong').value;
        var result = parseInt(unitPrice) * parseInt(quantity);
        
        if (!isNaN(result)) {
            document.getElementById('thanhTien').value = formatNumber(result);
        }

        var quantityInventory = document.getElementById('soLuongTon').value;
        if (quantity > (parseInt(quantityInventory))) {
            document.getElementById('soLuong').value = quantityInventory;

            var result_ = parseInt(unitPrice) * parseInt(quantity);
            if (!isNaN(result_)) {
                document.getElementById('thanhTien').value = formatNumber(result_);
            }
        }
    }
}

$(document).ready(function () {
    $("#soLuong").on('keyup input propertychange paste change', function () {
        CheckQuantity();
    });

    $("#tenKhachHang").on('keyup input propertychange paste change', function () {
        CheckTenKhachHang();
    });

    $("#soDienThoai").on('keyup input propertychange paste change', function () {
        CheckSoDienThoai();
    });
});

// check quantity input
function CheckQuantity(error) {
    if ($("#soLuong").val() == '') {
        $(".messageErrorinputQuantity").text("Nhập số lượng!");
        $(".notifyinputQuantity").slideDown(250).removeClass("hidden");
        $("#soLuong").addClass("error");
        error++;
    }
    else {
        $(".notifyinputQuantity").addClass("hidden");
        $("#soLuong").removeClass("error");
    }
    $("#soLuong").blur(function () {
        $("#soLuong").val($("#soLuong").val().trim());
    });
    return error;
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

