(function () {
    function scrollHorizontally(e) {
        e = window.event || e;
        var delta = Math.max(-1, Math.min(1, (e.wheelDelta || -e.detail)));
        document.getElementById('scroll').scrollLeft -= (delta * 40); // Multiplied by 40
        e.preventDefault();
    }
    if (document.getElementById('scroll').addEventListener) {
        // IE9, Chrome, Safari, Opera
        document.getElementById('scroll').addEventListener("mousewheel", scrollHorizontally, false);
        // Firefox
        document.getElementById('scroll').addEventListener("DOMMouseScroll", scrollHorizontally, false);
    } else {
        // IE 6/7/8
        document.getElementById('scroll').attachEvent("onmousewheel", scrollHorizontally);
    }
})();