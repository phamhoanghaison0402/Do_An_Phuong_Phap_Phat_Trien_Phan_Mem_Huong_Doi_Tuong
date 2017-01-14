$(function () {
    $.scrollify({
        section: "section",
        setHeights: true
    });
});

$(document).ready(function () {
    var height = $(window).height() - $(".containerheader").height();
    $(".banneritem").css("height", height);
    $(".login-form").css("margin-top", "13%");
})

