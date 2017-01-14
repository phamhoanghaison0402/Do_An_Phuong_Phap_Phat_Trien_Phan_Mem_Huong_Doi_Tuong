var BlankonOnePageAppShowcase = function () {

    return {

        // =========================================================================
        // CONSTRUCTOR APP
        // =========================================================================
        init: function () {
            BlankonOnePageAppShowcase.handlePreloader();
            BlankonOnePageAppShowcase.handleScrollNav();
            BlankonOnePageAppShowcase.handleAnimateElement();
            BlankonOnePageAppShowcase.handleAppScreenshots();
            BlankonOnePageAppShowcase.handleTeamMembers();
            BlankonOnePageAppShowcase.handleTestimonial();
            BlankonOnePageAppShowcase.handleCopyrightYear();
            BlankonOnePageAppShowcase.handleBackToTop();
        },

        // =========================================================================
        // PRELOADER TRIGGER
        // =========================================================================
        handlePreloader: function () {
            setTimeout(function(){
                $('body').addClass('loaded');

                // Call banner with slider
                BlankonOnePageAppShowcase.handleBanner();
            }, 3000);
        },

        // =========================================================================
        // NAVIGATION SCROLL
        // =========================================================================
        handleScrollNav: function () {
            $('.page-scroll').bind('click', function(event) {
                var $anchor = $(this);
                $('html, body').stop().animate({
                    scrollTop: $($anchor.attr('href')).offset().top+5
                }, 1500, 'easeInOutExpo');
                event.preventDefault();
            });
        },

        // =========================================================================
        // ANIMATION ELEMENT
        // =========================================================================
        handleAnimateElement: function () {
            new WOW().init();
        },

        // =========================================================================
        // BANNER SLIDER
        // =========================================================================
        handleBanner: function () {
            var tpj=jQuery;
            var revapi66;
            tpj(document).ready(function() {
                if(tpj("#rev_slider_66_1").revolution == undefined){
                    revslider_showDoubleJqueryError("#rev_slider_66_1");
                }else{
                    revapi66 = tpj("#rev_slider_66_1").show().revolution({
                        sliderType:"standard",
                        jsFileLocation:"../../revolution/js/",
                        sliderLayout:"fullscreen",
                        dottedOverlay:"none",
                        delay:9000,
                        navigation: {
                            keyboardNavigation:"on",
                            keyboard_direction: "vertical",
                            mouseScrollNavigation:"off",
                            onHoverStop:"off",
                            touch:{
                                touchenabled:"on",
                                swipe_threshold: 75,
                                swipe_min_touches: 1,
                                swipe_direction: "vertical",
                                drag_block_vertical: false
                            }
                        },
                        responsiveLevels:[1240,1025,778,480],
                        gridwidth:[1400,1240,778,480],
                        gridheight:[868,768,960,720],
                        lazyType:"none",
                        shadow:0,
                        spinner:"off",
                        stopLoop:"on",
                        stopAfterLoops:0,
                        stopAtSlide:1,
                        shuffle:"off",
                        autoHeight:"off",
                        fullScreenAlignForce:"off",
                        fullScreenOffsetContainer: "",
                        fullScreenOffset: "",
                        disableProgressBar:"on",
                        hideThumbsOnMobile:"off",
                        hideSliderAtLimit:0,
                        hideCaptionAtLimit:0,
                        hideAllCaptionAtLilmit:0,
                        debugMode:false,
                        fallbacks: {
                            simplifyAll:"off",
                            nextSlideOnWindowFocus:"off",
                            disableFocusListener:false,
                        }
                    });
                }
            });	/*ready*/
        },

        // =========================================================================
        // APP SCREENSHOTS
        // =========================================================================
        handleAppScreenshots: function () {
            (function($, window, document, undefined) {
                'use strict';

                // init cubeportfolio
                var singlePage = $('#js-singlePage-container').children('div');
                $('#js-grid-slider-projects').cubeportfolio({
                    layoutMode: 'slider',
                    drag: true,
                    auto: false,
                    autoTimeout: 5000,
                    autoPauseOnHover: true,
                    showNavigation: true,
                    showPagination: false,
                    rewindNav: false,
                    scrollByPage: false,
                    gridAdjustment: 'responsive',
                    mediaQueries: [{
                        width: 1500,
                        cols: 5
                    }, {
                        width: 1100,
                        cols: 1
                    }, {
                        width: 800,
                        cols: 1
                    }, {
                        width: 480,
                        cols: 2
                    }, {
                        width: 320,
                        cols: 1
                    }],
                    gapHorizontal: 0,
                    gapVertical: 25,
                    caption: 'overlayBottomReveal',
                    displayType: 'lazyLoading',
                    displayTypeSpeed: 100,

                    // lightbox
                    lightboxDelegate: '.cbp-lightbox',
                    lightboxGallery: true,
                    lightboxTitleSrc: 'data-title',
                    lightboxCounter: '<div class="cbp-popup-lightbox-counter">{{current}} of {{total}}</div>',

                    // singlePage popup
                    singlePageDelegate: '.cbp-singlePage',
                    singlePageDeeplinking: true,
                    singlePageStickyNavigation: true,
                    singlePageCounter: '<div class="cbp-popup-singlePage-counter">{{current}} of {{total}}</div>',
                    singlePageAnimation: 'fade',
                    singlePageCallback: function(url, element) {
                        // to update singlePage content use the following method: this.updateSinglePage(yourContent)
                        var indexElement = $(element).parents('.cbp-item').index(),
                            item = singlePage.eq(indexElement);

                        this.updateSinglePage(item.html());
                    },
                });
            })(jQuery, window, document);
        },

        // =========================================================================
        // TEAM MEMBERS
        // =========================================================================
        handleTeamMembers: function () {
            (function($, window, document, undefined) {
                'use strict';

                // init cubeportfolio
                $('#js-grid-slider-team').cubeportfolio({
                    layoutMode: 'slider',
                    drag: true,
                    auto: false,
                    autoTimeout: 5000,
                    autoPauseOnHover: true,
                    showNavigation: false,
                    showPagination: true,
                    rewindNav: true,
                    scrollByPage: true,
                    gridAdjustment: 'responsive',
                    mediaQueries: [{
                        width: 1680,
                        cols: 5
                    }, {
                        width: 1350,
                        cols: 4
                    }, {
                        width: 800,
                        cols: 3
                    }, {
                        width: 480,
                        cols: 2
                    }, {
                        width: 320,
                        cols: 1
                    }],
                    gapHorizontal: 0,
                    gapVertical: 45,
                    caption: '',
                    displayType: 'lazyLoading',
                    displayTypeSpeed: 400,
                });
            })(jQuery, window, document);
        },

        // =========================================================================
        // TESTIMONIALS
        // =========================================================================
        handleTestimonial: function () {
            (function($, window, document, undefined) {
                'use strict';

                // init cubeportfolio
                $('#js-grid-slider-testimonials').cubeportfolio({
                    layoutMode: 'slider',
                    drag: true,
                    auto: false,
                    autoTimeout: 5000,
                    autoPauseOnHover: true,
                    showNavigation: true,
                    showPagination: true,
                    rewindNav: true,
                    scrollByPage: false,
                    gridAdjustment: 'responsive',
                    mediaQueries: [{
                        width: 1,
                        cols: 1
                    }],
                    gapHorizontal: 0,
                    gapVertical: 0,
                    caption: '',
                    displayType: 'default',
                });
            })(jQuery, window, document);
        },

        // =========================================================================
        // COPYRIGHT YEAR
        // =========================================================================
        handleCopyrightYear : function () {
            if($('#footer-copyright').length){
                var today = new Date();
                $('#footer-copyright').text(today.getFullYear());
            }
        },

        // =========================================================================
        // BACK TOP
        // =========================================================================
        handleBackToTop: function () {
            $('#back-top').hide();
            $(window).scroll(function () {
                if ($(this).scrollTop() > 600) {
                    $('#back-top').fadeIn();
                } else {
                    $('#back-top').fadeOut();
                }
            });
            // scroll body to 0px on click
            $('#back-top').click(function () {
                var $anchor = $(this);
                $('body,html').animate({
                    scrollTop: 0
                }, 800);
                return false;
            });
        }

    };

}();

// Call main app init
BlankonOnePageAppShowcase.init();