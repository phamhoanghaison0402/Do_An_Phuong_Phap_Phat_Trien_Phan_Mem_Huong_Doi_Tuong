$(document).ready(function () {
    // Thêm dữ liệu gửi trả vào các input
    $("#EmployeeID").val('@(inputEmployee.EmployeeID)');
    $("#FullName").val('@(inputEmployee.FullName)');
    $("#Address").val('@(inputEmployee.Address)');
    $("#BirthDay").val('@(inputEmployee.BirthDay)');
    // Đặt lại trạng thái cho radio button
    if ('@(inputEmployee.Sex)' == 'True') {
        $('#radio1').removeClass("btn-default").addClass("btn-primary");
        $('#radio2').removeClass("btn-primary").addClass("btn-default");
    }
    // Đặt lại value cho dropdownlist
    var status = $("#StatusID option[value='" + '@(inputEmployee.StatusID)' + "']").prop("selected", "selected");
    eval(status);
    var position = $("#PositionID option[value='" + @(inputEmployee.PositionID) + "']").prop("selected", "selected");
    eval(position);
    // Hightline các trường bị lỗi
    if ('@(inputEmployee.Phone)' == "error") {
        $("#Phone").addClass("error");
    }
    else {
        $("#Phone").val('@(inputEmployee.Phone)');
    }
    if ('@(inputEmployee.Email)' == "error") {
        $("#Email").addClass("error");
    }
    else {
        $("#Email").val('@(inputEmployee.Email)');
    }
    if ('@(inputEmployee.SSN)' == "error") {
        $("#SSN").addClass("error");
    }
    else {
        $("#SSN").val('@(inputEmployee.SSN)');
    }
});