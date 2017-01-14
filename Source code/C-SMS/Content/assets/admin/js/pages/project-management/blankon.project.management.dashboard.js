var BlankonDashboardProjectManagement = function () {

    // =========================================================================
    // SETTINGS APP
    // =========================================================================
    var globalPluginsPath = BlankonApp.handleBaseURL()+'/assets/global/plugins/bower_components';

    return {

        // =========================================================================
        // CONSTRUCTOR APP
        // =========================================================================
        init: function () {
            BlankonDashboardProjectManagement.callModal();
            BlankonDashboardProjectManagement.counterOverview();
            BlankonDashboardProjectManagement.countNumber();
            BlankonDashboardProjectManagement.projectProgress();
            BlankonDashboardProjectManagement.projectSchedule();
            BlankonDashboardProjectManagement.topAssignChart();
        },

        // =========================================================================
        // CALL MODAL FIRST
        // =========================================================================
        callModal: function () {
            $('#modal-dashboard-project-management').modal(
                {
                    show: true,
                    keyboard: false
                }
            );
            $('#modal-dashboard-project-management').on('hidden.bs.modal', function (e) {
                BlankonDashboardProjectManagement.sessionTimeout();
            })
        },

        // =========================================================================
        // COUNTER OVERVIEW
        // =========================================================================
        counterOverview: function () {
            if($('.counter').length){
                $('.counter').counterUp({
                    delay: 10,
                    time: 4000
                });
            }
        },

        // =========================================================================
        // SESSION TIMEOUT
        // =========================================================================
        sessionTimeout: function () {
            if($('.demo-dashboard-session').length){
                $.sessionTimeout({
                    title: 'JUST DEMO Your session is about to expire!',
                    logoutButton: 'Logout',
                    keepAliveButton: 'Stay Connected',
                    message: 'Your session will be locked in 2 minute',
                    keepAliveUrl: '#',
                    logoutUrl: 'index.html',
                    redirUrl: 'lock-screen.html',
                    ignoreUserActivity: true,
                    warnAfter: 120000,
                    redirAfter: 240000
                });
            }
        },

        // =========================================================================
        // DEMO COUNT NUMBER
        // =========================================================================
        countNumber: function () {
            $('.count').each(function () {
                $(this).prop('Counter',0).animate({
                    Counter: $(this).text()
                }, {
                    duration: 7000,
                    easing: 'swing',
                    step: function (now) {
                        $(this).text(Math.ceil(now));
                    }
                });
            });
        },

        // =========================================================================
        // PROGRESS PROJECT
        // =========================================================================
        projectProgress: function () {
            $(window).resize(function() {
                window.area.redraw();
            });
            function morrisArea(){
                window.area = Morris.Area({
                    element: 'project-progress',
                    data: [
                        { y: '2008', a: 20, b: 30 },
                        { y: '2009', a: 40,  b: 50 },
                        { y: '2010', a: 30,  b: 40 },
                        { y: '2011', a: 50,  b: 60 },
                        { y: '2012', a: 40,  b: 50 },
                        { y: '2013', a: 60,  b: 70 },
                        { y: '2014', a: 50, b: 60 }
                    ],
                    xkey: 'y',
                    ykeys: ['a', 'b'],
                    labels: ['Work Done', 'Work Times'],
                    lineColors: ['#8CC152', '#00B1E1'],
                    lineWidth: '2px',
                    hideHover: true,
                    resize: true
                });
            }
            morrisArea();
        },

        // =========================================================================
        // PROJECT SCHEDULE
        // =========================================================================
        projectSchedule: function () {
            "use strict";

            var options = {
                events_source: globalPluginsPath+'/bootstrap-calendar/calendar-events-sample.json',
                view: 'month',
                tmpl_path: globalPluginsPath+'/bootstrap-calendar/tmpls/',
                tmpl_cache: false,
                day: '2013-03-12',
                onAfterEventsLoad: function(events) {
                    if(!events) {
                        return;
                    }
                    var list = $('#eventlist');
                    list.html('');

                    $.each(events, function(key, val) {
                        $(document.createElement('li'))
                            .html('<a href="' + val.url + '"><i class="fa fa-calendar mr-10"></i> ' + val.title + '</a>')
                            .appendTo(list);
                    });
                },
                onAfterViewLoad: function(view) {
                    $('.page-header h4').text(this.getTitle());
                    $('button').removeClass('active');
                    $('.calendar-menu-mobile ul li').removeClass('active');
                    $('button[data-calendar-view="' + view + '"]').addClass('active');
                    $('a[data-calendar-view="' + view + '"]').parent('li').addClass('active');
                },
                classes: {
                    months: {
                        general: 'label'
                    }
                }
            };

            var calendar = $('#calendar').calendar(options);

            $('[data-calendar-nav]').each(function() {
                var $this = $(this);
                $this.click(function() {
                    calendar.navigate($this.data('calendar-nav'));
                });
            });

            $('[data-calendar-view]').each(function() {
                var $this = $(this);
                $this.click(function() {
                    calendar.view($this.data('calendar-view'));
                });
            });

            $('#language').change(function(){
                calendar.setLanguage($(this).val());
                calendar.view();
            });

            $('#events-in-modal').change(function(){
                var val = $(this).is(':checked') ? $(this).val() : null;
                calendar.setOptions({modal: val});
            });
            $('#events-modal .modal-header, #events-modal .modal-footer').click(function(e){
                //e.preventDefault();
                //e.stopPropagation();
            });
        },

        // =========================================================================
        // TOP ASSIGNEES
        // =========================================================================
        topAssignChart: function () {
            $('.top-assign-chart').horizBarChart({
                selector: '.bar',
                speed: 3000
            });
        }

    };

}();

// Call main app init
BlankonDashboardProjectManagement.init();
