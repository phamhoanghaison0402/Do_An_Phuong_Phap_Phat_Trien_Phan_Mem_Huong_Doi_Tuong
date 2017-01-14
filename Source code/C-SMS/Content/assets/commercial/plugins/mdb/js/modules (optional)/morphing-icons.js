// MORPHING HAMBURGER ICON
(function () {
    $('.hamburger-menu').on('click', function () {
        $(this).toggleClass('animate')
        $('.bar').toggleClass('animate');
    })
})();