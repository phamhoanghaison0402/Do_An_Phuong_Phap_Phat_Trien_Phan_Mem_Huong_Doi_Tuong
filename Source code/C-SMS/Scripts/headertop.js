$(document).ready(function () {
        $(window).bind('mousewheel', function (event) {
            if (event.originalEvent.wheelDelta >= 0) {
                $(".nav-left").fadeIn();
                $(".nav-right").fadeIn();
            }
            else {
                var endPos = -150;
                $(".nav-left").fadeOut();
                $(".nav-right").fadeOut();
            }
        });
})

$("#btnHeaderLogin").click(function () {
    $.scrollify.next();
    $(".nav-left").fadeOut();
    $(".nav-right").fadeOut();
});