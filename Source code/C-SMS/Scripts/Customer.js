$(document).ready(function () {
    // Collapse Add new
    $(".x_content").css("display", "none");
    if ($("#FullName").val() != "")
        $(".collapse-link").click();
    if ($(".searchResult").hasClass("hidden")) {
        $(".collapse-link").click();
        $(".sidebar-nav.navbar-collapse.col-md-3.left_col").css("min-height", "710px");
    }
});

//Check null on search input
function KT() {
    if ($("#search").val() == "") {
        $("#search").notify(
              "Enter something",
              { position: "left" }
            );
        return false;
    } else {
        return true;
    }
}

// Change status of radio button
$('#Gender').change(function () { 
    if ($('#female').prop('checked')) {
        $('#Sex').val(false);
        $('#radio2').removeClass("btn-default").addClass("btn-primary");
        $('#radio1').removeClass("btn-primary").addClass("btn-default");
    }
    else {
        $('#Sex').val(true);
        $('#radio1').removeClass("btn-default").addClass("btn-primary");
        $('#radio2').removeClass("btn-primary").addClass("btn-default");
    }
});

// Change status of radio button
$('#GenderUpdate').change(function () {
    if ($('#femaleUpdate').prop('checked')) {
        $('#Sex').val(false);
        $('#radio2Update').removeClass("btn-default").addClass("btn-primary");
        $('#radio1Update').removeClass("btn-primary").addClass("btn-default");
    }
    else {
        $('#Sex').val(true);
        $('#radio1Update').removeClass("btn-default").addClass("btn-primary");
        $('#radio2Update').removeClass("btn-primary").addClass("btn-default");
    }
});

// Reset all input and action of form
$(".open").click(function () {
    if ($(".x_panel").height() <= 556 && $(".x_panel").height() > 57) {
        $(".sidebar-nav.navbar-collapse.col-md-3.left_col").css("min-height", "605px");
    }
    else {
        $(".sidebar-nav.navbar-collapse.col-md-3.left_col").css("min-height", "710px");
    }
    if ($(".open").hasClass("fa-chevron-up") || $(".x_title.active h2").text() == "Update Update a customer") {
        $("#form").attr("action", "/Customer/Insert");
        $(".open").attr("title", "Click to explain this!");
        $(".x_title h2").html("Add <small>Add new customer</small>");
        $("#FullName").val("");
        $('#radio1').removeClass("btn-default").addClass("btn-primary");
        $('#radio2').removeClass("btn-primary").addClass("btn-default");
        $("#Address").val("");
        $("#Phone").val("");
        $("#Email").val("");
        $("#SSN").val("");
        $("#Debt").val("");
        $(".Debt").css("display", "none");
        $(".Status").css("display", "none");
        $("#Phone").removeClass("error");
        $("#Email").removeClass("error");
        $("#SSN").removeClass("error");
        $(".notifyFullName").addClass("hidden");
        $("#FullName").removeClass("error");
        $(".notifyAddress").addClass("hidden");
        $("#Address").removeClass("error");
        $(".notifyPhone").addClass("hidden");
        $("#Phone").removeClass("error");
        $(".notifyEmail").addClass("hidden");
        $("#Email").removeClass("error");
        $(".notifySSN").addClass("hidden");
        $("#SSN").removeClass("error");
    }
    else {
        $(".open").attr("title", "Click to collapse this!");
    }
});

// Reset all input error
$(".btn_close").click(function () {
    ClearModal();
});

function ClearModal() {
    $(".notifyFullNameUpdate").addClass("hidden");
    $("#FullNameUpdate").removeClass("error");
    $(".notifyAddressUpdate").addClass("hidden");
    $("#AddressUpdate").removeClass("error");
    $(".notifyPhoneUpdate").addClass("hidden");
    $("#PhoneUpdate").removeClass("error");
    $(".notifyEmailUpdate").addClass("hidden");
    $("#EmailUpdate").removeClass("error");
    $(".notifySSNUpdate").addClass("hidden");
    $("#SSNUpdate").removeClass("error");
    $(".notifyDebtUpdate").addClass("hidden");
    $("#DebtUpdate").removeClass("error");
}