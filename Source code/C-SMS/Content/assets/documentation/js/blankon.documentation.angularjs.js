var BlankonDocumentationAngularJS = function () {

    // =========================================================================
    // SETTINGS APP
    // =========================================================================
    var getBaseURL = function () {
            var getUrl = window.location,
                baseUrl = getUrl .protocol + "//" + getUrl.host + "/" + getUrl.pathname.split('/')[1];
            return baseUrl;
        },
        getPluginPath   = getBaseURL()+'/assets/global/plugins/bower_components';

    return {

        // =========================================================================
        // CONSTRUCTOR APP
        // =========================================================================
        init: function () {
            BlankonDocumentationAngularJS.handleHeaderDropdownMenu();
            BlankonDocumentationAngularJS.handleSidebarLeftActive();
            BlankonDocumentationAngularJS.handleSmoothScroll();
            BlankonDocumentationAngularJS.handleCodePrettify();
            BlankonDocumentationAngularJS.handleSelect2();
            BlankonDocumentationAngularJS.handleSelect2Plugins();
            BlankonDocumentationAngularJS.handleSelect2QuickSearch();
            BlankonDocumentationAngularJS.handleDatatableCredit();
            BlankonDocumentationAngularJS.handleDatatableChangeLog();
        },

        // =========================================================================
        // HEADER DROPDOWN MENU
        // =========================================================================
        handleHeaderDropdownMenu: function () {
            $('.page-scroll-header').smoothScroll(
                {
                    offset: -66,

                    // one of 'top' or 'left'
                    direction: 'top',

                    // only use if you want to override default behavior
                    scrollTarget: null,

                    // fn(opts) function to be called before scrolling occurs.
                    // `this` is the element(s) being scrolled
                    beforeScroll: function() {},

                    // fn(opts) function to be called after scrolling occurs.
                    // `this` is the triggering element
                    afterScroll: function() {
                        $(this).parent('li').addClass('active').siblings().removeClass('active');
                    },
                    easing: 'swing',

                    // speed can be a number or 'auto'
                    // if 'auto', the speed will be calculated based on the formula:
                    // (current scroll position - target scroll position) / autoCoeffic
                    speed: 1000,

                    // autoCoefficent: Only used when speed set to "auto".
                    // The higher this number, the faster the scroll speed
                    autoCoefficient: 2,

                    // $.fn.smoothScroll only: whether to prevent the default click action
                    preventDefault: true
                }
            );
        },

        // =========================================================================
        // SIDEBAR LEFT NAVIGATION
        // =========================================================================
        handleSidebarLeftActive: function () {
            !function ($) {
                $(function(){
                    if (navigator.userAgent.match(/IEMobile\/10\.0/)) {
                        var msViewportStyle = document.createElement("style");
                        msViewportStyle.appendChild(
                            document.createTextNode(
                                "@-ms-viewport{width:auto!important}"
                            )
                        );
                        document.getElementsByTagName("head")[0].
                            appendChild(msViewportStyle);
                    }

                    var $window = $(window),
                        $body   = $(document.body),
                        navHeight = $('.navbar').outerHeight(true) + 66;

                    $body.scrollspy({
                        target: '#sidebar-left',
                        offset: navHeight
                    });

                    $window.on('load', function () {
                        $body.scrollspy('refresh')
                    })

                })

            }(jQuery);
        },

        // =========================================================================
        // SMOOTH SCROLL PAGE
        // =========================================================================
        handleSmoothScroll: function () {
            $('.page-scroll').smoothScroll(
                {
                    offset: -66,

                    // one of 'top' or 'left'
                    direction: 'top',

                    // only use if you want to override default behavior
                    scrollTarget: null,

                    // fn(opts) function to be called before scrolling occurs.
                    // `this` is the element(s) being scrolled
                    beforeScroll: function() {},

                    // fn(opts) function to be called after scrolling occurs.
                    // `this` is the triggering element
                    afterScroll: function() {},
                    easing: 'swing',

                    // speed can be a number or 'auto'
                    // if 'auto', the speed will be calculated based on the formula:
                    // (current scroll position - target scroll position) / autoCoeffic
                    speed: 1000,

                    // autoCoefficent: Only used when speed set to "auto".
                    // The higher this number, the faster the scroll speed
                    autoCoefficient: 2,

                    // $.fn.smoothScroll only: whether to prevent the default click action
                    preventDefault: true
                }
            );
        },

        // =========================================================================
        // CODE PRETTIFY
        // =========================================================================
        handleCodePrettify: function () {
            $('.tooltips').tooltip({
                selector: "[data-toggle=tooltip]",
                container: "body"
            });
            $("pre.html-code").snippet("html",{style:"matlab",clipboard: getPluginPath+"/jquery-snippet/ZeroClipboard.swf"});
            $("pre.html-code-no-menu").snippet("html",{style:"matlab",menu: false});
            $("pre.css-code").snippet("css",{style:"matlab",clipboard: getPluginPath+"/jquery-snippet/ZeroClipboard.swf"});
            $("pre.js-code").snippet("javascript",{style:"matlab",clipboard: getPluginPath+"/jquery-snippet/ZeroClipboard.swf"});
        },

        // =========================================================================
        // SELECT2
        // =========================================================================
        handleSelect2: function () {
            var $eventSelect = $('.page-select2');

            // Add element label on select2 just premium plugin
            function formatState (state) {
                if(state.id == 'cube-portfolio'){
                    var $state = $(
                        '<span>' + state.text + '&nbsp;<span class="label label-success">Premium $16</span></span>'
                    );
                }else if(state.id == 'glyphicons-pro'){
                    var $state = $(
                        '<span>' + state.text + '&nbsp;<span class="label label-success">Premium $59</span></span>'
                    );
                }else if(state.id == 'slider-revolution'){
                    var $state = $(
                        '<span>' + state.text + '&nbsp;<span class="label label-success">Premium $14</span></span>'
                    );
                }else{
                    return state.text;
                }
                return $state;
            }

            // Init select2
            $eventSelect.select2({
                templateResult: formatState
            });
        },

        // =========================================================================
        // SELECT2 PLUGINS
        // =========================================================================
        handleSelect2Plugins: function () {
            var $eventSelectPlugins = $('#select2-plugins');

            // Callback on plugins select
            $eventSelectPlugins.on('select2:select', function () {
                $.ajax({
                    url:getBaseURL()+'/documentation/admin/angularjs/partial-content/plugins/'+$('#select2-plugins').val()+'.html',
                    type:'GET',
                    success: function(data){
                        $('#plugins-content').html(data);
                        // Callback trigger code prettify
                        BlankonDocumentationAngularJS.handleCodePrettify();
                    }
                });
            });
        },

        // =========================================================================
        // SELECT2 QUICK SEARCH
        // =========================================================================
        handleSelect2QuickSearch: function () {
            var $eventSelectQuickSearch = $('#select2-quick-search');

            // Callback on quick search select
            $eventSelectQuickSearch.on('select2:select', function () {
                var url = $(this).val(); // get selected value
                if(url == '#'){
                    return false;
                }
                if (url) { // require a URL
                    if($('.live-preview-doc').length){
                        window.open(getBaseURL()+'/live-preview/admin/angularjs/#/'+url, '_blank') ; // redirect live preview
                    }else{
                        window.open(getBaseURL()+'/production/admin/angularjs/#/'+url, '_blank') ; // redirect templates
                    }

                }
                return false;
            });
        },

        // =========================================================================
        // DATATABLE CREDIT
        // =========================================================================
        handleDatatableCredit: function () {
            var responsiveHelper;
            var breakpointDefinition = {
                tablet: 1024,
                phone_landscape : 480,
                phone_portrait : 320
            };
            var tableElement = $('#table-credit');

            tableElement.DataTable({
                "ajax": getBaseURL()+"/documentation/admin/angularjs/partial-content/credit/data-credit.json",
                "columns": [
                    { "data": "name" },
                    { "data": "description" },
                    { "data": "version" },
                    { "data": "bower_packages" },
                    { "data": "url_homepage" },
                    { "data": "url_cdn" }
                ],
                "columnDefs": [
                    {
                        "targets": [1, 2, 3, 4, 5],
                        "sortable": false
                    },
                    {
                        "targets": 0,
                        "class": "text-capitalize"
                    },
                    {
                        "targets": 2,
                        "class": "text-center"
                    },
                    {
                        "targets": 3,
                        "render": function ( data ) {
                            return '<code>'+data+'</code>';
                        }
                    },
                    {
                        "targets": 4,
                        "class": "text-center",
                        "render": function ( data ) {
                            if(data!='-'){
                                return '<a href="'+data+'" target="_blank" class="btn btn-sm btn-success">view</a>';
                            }else{
                                return '<a href="javascript:void(0);" class="btn btn-sm btn-success page-modal-credit">view</a>';
                            }
                        }
                    },
                    {
                        "targets": 5,
                        "class": "text-center",
                        "render": function ( data ) {
                            if(data!='-'){
                                return '<a href="'+data+'" target="_blank" class="btn btn-sm btn-success">view</a>';
                            }else{
                                return '<a href="javascript:void(0);" class="btn btn-sm btn-success page-modal-credit">view</a>';
                            }
                        }
                    }
                ],
                "autoWidth" : false,
                "preDrawCallback": function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper) {
                        responsiveHelper = new ResponsiveDatatablesHelper(tableElement, breakpointDefinition);
                    }
                },
                "rowCallback" : function (nRow) {
                    responsiveHelper.createExpandIcon(nRow);
                },
                "drawCallback" : function(oSettings) {
                    $('.page-modal-credit').on('click', function () {
                        bootbox.dialog({
                            message: '<div class="text-vertically-center no-margin"><h1><i class="icon-close icons"></i>CDN PLUGIN NOT AVAILABLE</h1></div>',
                            title: 'Attention!',
                            buttons: false
                        });
                    });
                    responsiveHelper.respond();
                }
            });
        },

        // =========================================================================
        // DATATABLE CHANGE LOG
        // =========================================================================
        handleDatatableChangeLog: function () {
            var responsiveHelper;
            var breakpointDefinition = {
                tablet: 1024,
                phone_landscape : 480,
                phone_portrait : 320
            };
            var tableElement = $('#table-change-log');
            tableElement.DataTable({
                "order": [[ 0, "desc" ]],
                "lengthMenu": [[5, 10, 25, -1], [5, 10, 25, "All"]],
                "columnDefs": [
                    {
                        "targets": 1,
                        "sortable": false
                    }
                ],
                "autoWidth" : false,
                "preDrawCallback": function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper) {
                        responsiveHelper = new ResponsiveDatatablesHelper(tableElement, breakpointDefinition);
                    }
                },
                "rowCallback" : function (nRow) {
                    responsiveHelper.createExpandIcon(nRow);
                },
                "drawCallback" : function(oSettings) {
                    responsiveHelper.respond();
                }
            });
        }
    };

}();

// Call main app init
BlankonDocumentationAngularJS.init();
