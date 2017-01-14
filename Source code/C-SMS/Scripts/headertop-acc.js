$(document).ready(function () {
    $(".orange").css("color", "#ff8000");
    $(".navbar-title").fadeIn(1000);
    $(".sidebar-nav.navbar-collapse.col-md-3.left_col").css("min-height", "605px");
})

$(window).load(function () {
    if ($(".Ballot li.current-page").text() != "") {
        $('.Ballot').addClass("active");
    }
    if ($('.Ballot').hasClass("active")) {
        $('.Manager').removeClass("active");
        $('.Product').removeClass("active");
        $('.Report').removeClass("active");
    }
    if ($(".Manager li.current-page").text() != "") {
        $('.Manager').addClass("active");
    }
    if ($('.Manager').hasClass("active")) {
        $('.Report').removeClass("active");
        $('.Product').removeClass("active");
        $('.Ballot').removeClass("active");
    }
    if ($(".Product li.current-page").text() != "") {
        $('.Product').addClass("active");
    }
    if ($('.Product').hasClass("active")) {
        $('.Manager').removeClass("active");
        $('.Report').removeClass("active");
        $('.Ballot').removeClass("active");
    }
    if ($(".Report li.current-page").text() != "") {
        $('.Report').addClass("active");
    }
    if ($('.Report').hasClass("active")) {
        $('.Manager').removeClass("active");
        $('.Product').removeClass("active");
        $('.Ballot').removeClass("active");
    }
});

$('li.m_2').hover(function () {
    var index = $('li.m_2').index(this);
    $('li>ul li i').eq(index).css("color", "white")
}, function () {
    var index = $('li.m_2').index(this);
    $('li>ul li i').eq(index).css("color", "#ff8000")
});

$(".avatar").click(function () {
    if ($(".dropdown").hasClass("open")) {
        $(".dropdown-custom").fadeOut();
    }
    else {
        $(".dropdown-custom").fadeIn();
    }
})

$("#menu_toggle").click(function () {
    if ($BODY.hasClass("nav-md")) {
        $("#page-wrapper").css("margin", "0 0 0 187px");
        $(".sidebar-nav.navbar-collapse.col-md-3.left_col").css("min-width", "13.9%");
    }
    else {
        $(".sidebar-nav.navbar-collapse.col-md-3.left_col").css("min-width", "1%");
        $("#page-wrapper").css("margin", "0 0 0 83px");
    }
});
