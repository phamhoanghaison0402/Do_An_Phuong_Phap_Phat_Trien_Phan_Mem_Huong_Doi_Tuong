/* PROGRESS BAR */
var bar = $('.progress-bar-animated');
$(function () {
    $(bar).each(function () {
        bar_width = $(this).attr('aria-valuenow');
        $(this).width(bar_width + '%');
    });
});

/* PANEL */

function upTo(el, tagName) {
    tagName = tagName.toLowerCase();
    while (el && el.parentNode) {
        el = el.parentNode;
        if (el.tagName && el.tagName.toLowerCase() == tagName) {
            return el;
        }
    }

    // Many DOM methods return null if they don't 
    // find the element they are searching for
    // It would be OK to omit the following and just
    // return undefined
    return null;
}
//Add / remove active class to LI element inside popout 
var selector = '.popout .panel-heading';
var parentSelector = '.popout .panel .active';

$(selector).on('click', function () {

    parentDiv = upTo(this, 'DIV');
    console.log(parentDiv);
    if ($(this).hasClass('active')) {

        $(selector).removeClass('active');
        $(".popout .panel").removeClass('active');

    } else {
        $(selector).removeClass('active');
        $(".popout .panel").removeClass('active');
        $(this).addClass('active');
        $(parentDiv).addClass('active');
    }
});