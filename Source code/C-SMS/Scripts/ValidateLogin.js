//Check empty input 
function Checknull() {
    if ($("#Username").val() == "" || CheckEmpty($("#Username").val()) && $("#Password").val() == "" || CheckEmpty($("#Password").val())) {
        $("#Username").notify(
             "Enter something",
             { position: "right" }
        );
        $("#Password").notify(
            "Enter something",
            { position: "right" }
        );
        return false;
    }
    else if ($("#Username").val() == "") {
        $("#Username").notify(
              "Enter something",
              { position: "right" }
            );
        return false;
    } else if ($("#Password").val() == "") {
        $("#Password").notify(
              "Enter something",
              { position: "right" }
            );
        return false;
    } else {
        return true;
    }
}

//Check empty string
function CheckEmpty(s) {
    return s.replace(/^\s+|\s+$/gm, '').length == 0;
}