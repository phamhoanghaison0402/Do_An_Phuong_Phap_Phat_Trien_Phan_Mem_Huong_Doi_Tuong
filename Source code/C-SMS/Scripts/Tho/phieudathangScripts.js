$(document).ready(function () {
    //Add button click function
    $('#btNhanHang').click(function () {
    });

    $('#btThanhToan').click(function () {
    });
});
   
function createNhanHangDiv(id) {    
    document.innerHTML = "<div id=NhanHang" + id + "></div> <a id= 'aNhanHang" + id + "' onclick='onBtNhanHangClicked(" + id + ")' class='btn btn-default'>Xác Nhận</a>";
}

function getNhanHangId(id) {
    return "NhanHang" + id;
}

function onBtNhanHangClicked(self, id) {
    var r = confirm("Bạn xác nhận khách hàng đã nhận được hàng?");

    if (r) {
        self.innerHTML = "Đã nhận hàng";
        self.className = "";

        xacNhanNhanHang(id);

    } else {

    }
}

function createThanhToanDiv(id) {
    document.innerHTML = "<div id=ThanhToan" + id + "></div> <a id= 'aThanhToan" + id + "' onclick='onBtThanhToanClicked(" + id + ")' class='btn btn-default'>Xác Nhận</a>";
}

function getThanhToanId(id) {
    return "ThanhToan" + id;
}

function onBtThanhToanClicked(self, id) {
    var r = confirm("Bạn xác nhận khách hàng đã thanh toán hóa đơn?");

    if (r) {
        self.innerHTML = "Đã thanh toán";
        self.className = "";

        xacNhanThanhToan(id);

    } else {

    }
}

function xacNhanNhanHang(idx) {
    $.getJSON('/DatHang/XacNhanNhanHang',
                { id: idx },
                function (data) {
                });
}


function xacNhanThanhToan(idx) {
    $.getJSON('/DatHang/XacNhanThanhToan',
                { id: idx },
                function (data) {
                });
}