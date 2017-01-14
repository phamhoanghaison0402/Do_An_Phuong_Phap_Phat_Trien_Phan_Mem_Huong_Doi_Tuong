'use strict';
var BlankonProjectManagementClient = function () {

    return {

        // =========================================================================
        // CONSTRUCTOR APP
        // =========================================================================
        init: function () {
            BlankonProjectManagementClient.listClientAll();
            BlankonProjectManagementClient.listClientCorporate();
            BlankonProjectManagementClient.listClientIndividual();
            BlankonProjectManagementClient.listClientOther();
        },

        // =========================================================================
        // DATATABLE LIST CLIENT ALL
        // =========================================================================
        listClientAll: function () {
            var responsiveHelperAjax = undefined;
            var breakpointDefinition = {
                tablet: 1024,
                phone : 480
            };

            var tableAjax = $('#datatable-client-all');

            // Using AJAX
            tableAjax.dataTable({
                autoWidth      : false,
                ajax           : BlankonApp.handleBaseURL()+'/assets/admin/data/project-clients/all.json',
                preDrawCallback: function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelperAjax) {
                        responsiveHelperAjax = new ResponsiveDatatablesHelper(tableAjax, breakpointDefinition);
                    }
                },
                rowCallback    : function (nRow) {
                    responsiveHelperAjax.createExpandIcon(nRow);
                },
                drawCallback   : function (oSettings) {
                    responsiveHelperAjax.respond();
                    // call actions on last column datatable
                    BlankonProjectManagementClient.handleActionViewDatatable();
                    BlankonProjectManagementClient.handleActionEditDatatable();
                    BlankonProjectManagementClient.handleActionDeleteDatatable();
                },
                fnRowCallback: function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {

                    // Create element on row
                    var imgTag = '<img src="'+aData[0]+'" alt="..." class="img-circle"/> '+aData[1];
                    if(aData[3] == 'corporate'){
                        var spanTag = '<span class="label label-danger text-capitalize">'+aData[3]+'</span>';
                    }
                    if(aData[3] == 'individual'){
                        var spanTag = '<span class="label label-warning text-capitalize">'+aData[3]+'</span>';
                    }
                    if(aData[3] == 'other'){
                        var spanTag = '<span class="label label-primary text-capitalize">'+aData[3]+'</span>';
                    }

                    if(aData[6] == 'active'){
                        var spanTagStatus = '<span class="fg-success text-capitalize">'+aData[6]+'</span>';
                    }
                    if(aData[6] == 'not active'){
                        var spanTagStatus = '<span class="fg-danger text-capitalize">'+aData[6]+'</span>';
                    }

                    $('td:eq(0)', nRow).html(imgTag);
                    $('td:eq(1)', nRow).html(aData[2]);
                    $('td:eq(2)', nRow).html(spanTag);
                    $('td:eq(3)', nRow).html(aData[4]);
                    $('td:eq(4)', nRow).html('<div class="text-right">'+aData[5]+'</div>');
                    $('td:eq(5)', nRow).html('<div class="text-center">'+spanTagStatus+'</div>');

                    return nRow;
                },
                'columnDefs': [
                    {
                        'targets': [2, 5, 6],
                        'sortable': false
                    },
                    {
                        'targets': 6,
                        'class': 'text-center',
                        'render': function ( data, type, full, meta ) {
                            return '<div class="btn-group">' +
                                '<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                                '<i class="fa fa-cogs"></i>' +
                                '</button>' +
                                '<ul class="dropdown-menu pull-right">' +
                                '<li>' +
                                '<a href="#" class="btn-view">View</a>' +
                                '</li>' +
                                '<li><a href="#" class="btn-edit">Edit</a></li>' +
                                '<li role="separator" class="divider"></li>' +
                                '<li><a href="#" class="btn-delete">Delete</a></li>' +
                                '</ul>' +
                                '</div>'
                        }
                    }
                ]
            });

        },

        // =========================================================================
        // DATATABLE LIST CLIENT CORPORATE
        // =========================================================================
        listClientCorporate: function () {
            var responsiveHelperAjax = undefined;
            var breakpointDefinition = {
                tablet: 1024,
                phone : 480
            };

            var tableAjax = $('#datatable-client-corporate');

            // Using AJAX
            tableAjax.dataTable({
                autoWidth      : false,
                ajax           : BlankonApp.handleBaseURL()+'/assets/admin/data/project-clients/corporate.json',
                preDrawCallback: function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelperAjax) {
                        responsiveHelperAjax = new ResponsiveDatatablesHelper(tableAjax, breakpointDefinition);
                    }
                },
                rowCallback    : function (nRow) {
                    responsiveHelperAjax.createExpandIcon(nRow);
                },
                drawCallback   : function (oSettings) {
                    responsiveHelperAjax.respond();
                },
                fnRowCallback: function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {

                    // Create element on row
                    var imgTag = '<img src="'+aData[0]+'" alt="..." class="img-circle"/> '+aData[1];
                    if(aData[3] == 'corporate'){
                        var spanTag = '<span class="label label-danger text-capitalize">'+aData[3]+'</span>';
                    }
                    if(aData[3] == 'individual'){
                        var spanTag = '<span class="label label-warning text-capitalize">'+aData[3]+'</span>';
                    }
                    if(aData[3] == 'other'){
                        var spanTag = '<span class="label label-primary text-capitalize">'+aData[3]+'</span>';
                    }

                    if(aData[6] == 'active'){
                        var spanTagStatus = '<span class="fg-success text-capitalize">'+aData[6]+'</span>';
                    }
                    if(aData[6] == 'not active'){
                        var spanTagStatus = '<span class="fg-danger text-capitalize">'+aData[6]+'</span>';
                    }

                    $('td:eq(0)', nRow).html(imgTag);
                    $('td:eq(1)', nRow).html(aData[2]);
                    $('td:eq(2)', nRow).html(spanTag);
                    $('td:eq(3)', nRow).html(aData[4]);
                    $('td:eq(4)', nRow).html('<div class="text-right">'+aData[5]+'</div>');
                    $('td:eq(5)', nRow).html('<div class="text-center">'+spanTagStatus+'</div>');

                    return nRow;
                },
                'columnDefs': [
                    {
                        'targets': [2, 5, 6],
                        'sortable': false
                    },
                    {
                        'targets': 6,
                        'class': 'text-center',
                        'render': function ( data, type, full, meta ) {
                            return '<div class="btn-group">' +
                                '<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                                '<i class="fa fa-cogs"></i>' +
                                '</button>' +
                                '<ul class="dropdown-menu pull-right">' +
                                '<li>' +
                                '<a href="#" class="btn-view">View</a>' +
                                '</li>' +
                                '<li><a href="#" class="btn-edit">Edit</a></li>' +
                                '<li role="separator" class="divider"></li>' +
                                '<li><a href="#" class="btn-delete">Delete</a></li>' +
                                '</ul>' +
                                '</div>'
                        }
                    }
                ]
            });

        },

        // =========================================================================
        // DATATABLE LIST CLIENT INDIVIDUAL
        // =========================================================================
        listClientIndividual: function () {
            var responsiveHelperAjax = undefined;
            var breakpointDefinition = {
                tablet: 1024,
                phone : 480
            };

            var tableAjax = $('#datatable-client-individual');

            // Using AJAX
            tableAjax.dataTable({
                autoWidth      : false,
                ajax           : BlankonApp.handleBaseURL()+'/assets/admin/data/project-clients/individual.json',
                preDrawCallback: function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelperAjax) {
                        responsiveHelperAjax = new ResponsiveDatatablesHelper(tableAjax, breakpointDefinition);
                    }
                },
                rowCallback    : function (nRow) {
                    responsiveHelperAjax.createExpandIcon(nRow);
                },
                drawCallback   : function (oSettings) {
                    responsiveHelperAjax.respond();
                },
                fnRowCallback: function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {

                    // Create element on row
                    var imgTag = '<img src="'+aData[0]+'" alt="..." class="img-circle"/> '+aData[1];
                    if(aData[3] == 'corporate'){
                        var spanTag = '<span class="label label-danger text-capitalize">'+aData[3]+'</span>';
                    }
                    if(aData[3] == 'individual'){
                        var spanTag = '<span class="label label-warning text-capitalize">'+aData[3]+'</span>';
                    }
                    if(aData[3] == 'other'){
                        var spanTag = '<span class="label label-primary text-capitalize">'+aData[3]+'</span>';
                    }

                    if(aData[6] == 'active'){
                        var spanTagStatus = '<span class="fg-success text-capitalize">'+aData[6]+'</span>';
                    }
                    if(aData[6] == 'not active'){
                        var spanTagStatus = '<span class="fg-danger text-capitalize">'+aData[6]+'</span>';
                    }

                    $('td:eq(0)', nRow).html(imgTag);
                    $('td:eq(1)', nRow).html(aData[2]);
                    $('td:eq(2)', nRow).html(spanTag);
                    $('td:eq(3)', nRow).html(aData[4]);
                    $('td:eq(4)', nRow).html('<div class="text-right">'+aData[5]+'</div>');
                    $('td:eq(5)', nRow).html('<div class="text-center">'+spanTagStatus+'</div>');

                    return nRow;
                },
                'columnDefs': [
                    {
                        'targets': [2, 5, 6],
                        'sortable': false
                    },
                    {
                        'targets': 6,
                        'class': 'text-center',
                        'render': function ( data, type, full, meta ) {
                            return '<div class="btn-group">' +
                                '<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                                '<i class="fa fa-cogs"></i>' +
                                '</button>' +
                                '<ul class="dropdown-menu pull-right">' +
                                '<li>' +
                                '<a href="#" class="btn-view">View</a>' +
                                '</li>' +
                                '<li><a href="#" class="btn-edit">Edit</a></li>' +
                                '<li role="separator" class="divider"></li>' +
                                '<li><a href="#" class="btn-delete">Delete</a></li>' +
                                '</ul>' +
                                '</div>'
                        }
                    }
                ]
            });
        },

        // =========================================================================
        // DATATABLE LIST CLIENT OTHER
        // =========================================================================
        listClientOther: function () {
            var responsiveHelperAjax = undefined;
            var breakpointDefinition = {
                tablet: 1024,
                phone : 480
            };

            var tableAjax = $('#datatable-client-other');

            // Using AJAX
            tableAjax.dataTable({
                autoWidth      : false,
                ajax           : BlankonApp.handleBaseURL()+'/assets/admin/data/project-clients/other.json',
                preDrawCallback: function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelperAjax) {
                        responsiveHelperAjax = new ResponsiveDatatablesHelper(tableAjax, breakpointDefinition);
                    }
                },
                rowCallback    : function (nRow) {
                    responsiveHelperAjax.createExpandIcon(nRow);
                },
                drawCallback   : function (oSettings) {
                    responsiveHelperAjax.respond();
                },
                fnRowCallback: function( nRow, aData, iDisplayIndex, iDisplayIndexFull ) {

                    // Create element on row
                    var imgTag = '<img src="'+aData[0]+'" alt="..." class="img-circle"/> '+aData[1];
                    if(aData[3] == 'corporate'){
                        var spanTag = '<span class="label label-danger text-capitalize">'+aData[3]+'</span>';
                    }
                    if(aData[3] == 'individual'){
                        var spanTag = '<span class="label label-warning text-capitalize">'+aData[3]+'</span>';
                    }
                    if(aData[3] == 'other'){
                        var spanTag = '<span class="label label-primary text-capitalize">'+aData[3]+'</span>';
                    }

                    if(aData[6] == 'active'){
                        var spanTagStatus = '<span class="fg-success text-capitalize">'+aData[6]+'</span>';
                    }
                    if(aData[6] == 'not active'){
                        var spanTagStatus = '<span class="fg-danger text-capitalize">'+aData[6]+'</span>';
                    }

                    $('td:eq(0)', nRow).html(imgTag);
                    $('td:eq(1)', nRow).html(aData[2]);
                    $('td:eq(2)', nRow).html(spanTag);
                    $('td:eq(3)', nRow).html(aData[4]);
                    $('td:eq(4)', nRow).html('<div class="text-right">'+aData[5]+'</div>');
                    $('td:eq(5)', nRow).html('<div class="text-center">'+spanTagStatus+'</div>');

                    return nRow;
                },
                'columnDefs': [
                    {
                        'targets': [2, 5, 6],
                        'sortable': false
                    },
                    {
                        'targets': 6,
                        'class': 'text-center',
                        'render': function ( data, type, full, meta ) {
                            return '<div class="btn-group">' +
                                '<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">' +
                                '<i class="fa fa-cogs"></i>' +
                                '</button>' +
                                '<ul class="dropdown-menu pull-right">' +
                                '<li>' +
                                '<a href="#" class="btn-view">View</a>' +
                                '</li>' +
                                '<li><a href="#" class="btn-edit">Edit</a></li>' +
                                '<li role="separator" class="divider"></li>' +
                                '<li><a href="#" class="btn-delete">Delete</a></li>' +
                                '</ul>' +
                                '</div>'
                        }
                    }
                ]
            });

        },

        // =========================================================================
        // ACTION VIEW ROW DATATABLES
        // =========================================================================
        handleActionViewDatatable: function () {
            $('#datatable-client-all, ' +
                '#datatable-client-corporate, ' +
                '#datatable-client-individual, ' +
                '#datatable-client-other').on('click', '.btn-view', function(){
                showModalDialog(this);
            });

            $('#modal-view-datatable').modal({ show: false });

            $('#modal-view-datatable').on('show.bs.modal', function (e){
                var $dlg = $(this);

                var $tr    = $($dlg.data('btn')).closest('tr');
                var $table = $($dlg.data('btn')).closest('table');
                var data = $table.DataTable().row($tr).data();

                var html = '<form class="form-horizontal">' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Client Name :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[1]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Last Activity :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[2]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Client Type :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[3]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Account Manager :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[4]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Total Balance :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[5]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Status :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[6]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '</form>';

                $('.row-name', $dlg).html(data[1]);

                $('.modal-body', $dlg).html(html);
            });

            function showModalDialog(elBtn){
                $('#modal-view-datatable').data('btn', elBtn);
                $('#modal-view-datatable').modal('show');
            }
        },

        // =========================================================================
        // ACTION EDIT ROW DATATABLES
        // =========================================================================
        handleActionEditDatatable: function () {
            $('#datatable-client-all, ' +
                '#datatable-client-corporate, ' +
                '#datatable-client-individual, ' +
                '#datatable-client-other').on('click', '.btn-edit', function(){
                showModalDialog(this);
            });

            $('#modal-edit-datatable').modal({ show: false });

            $('#modal-edit-datatable').on('show.bs.modal', function (e){
                var $dlg = $(this);

                var $tr    = $($dlg.data('btn')).closest('tr');
                var $table = $($dlg.data('btn')).closest('table');
                var data = $table.DataTable().row($tr).data();

                var html = '<form class="form-horizontal">' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Client Name</label>' +
                    '<div class="col-sm-9">' +
                    '<input type="hidden" value="' + $('<div/>').text(data[0]).html() + '">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[1]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Last Activity</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[2]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Client Type</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[3]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Account Manager</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[4]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Total Balance</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[5]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Status</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[6]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '</form>';

                $('.row-name', $dlg).html(data[1]);

                $('.modal-body', $dlg).html(html);
            });

            function showModalDialog(elBtn){
                $('#modal-edit-datatable').data('btn', elBtn);
                $('#modal-edit-datatable').modal('show');
            }
        },

        // =========================================================================
        // ACTION DELETE ROW DATATABLES
        // =========================================================================
        handleActionDeleteDatatable: function () {
            $('#datatable-client-all, ' +
                '#datatable-client-corporate, ' +
                '#datatable-client-individual, ' +
                '#datatable-client-other').on('click', '.btn-delete', function(){
                showModalDialog(this);
            });

            $('#modal-delete-datatable').modal({ show: false });

            $('#modal-delete-datatable').on('show.bs.modal', function (e){
                var $dlg = $(this);

                var $tr    = $($dlg.data('btn')).closest('tr');
                var $table = $($dlg.data('btn')).closest('table');
                var data = $table.DataTable().row($tr).data();

                $('.row-name', $dlg).html(data[1]);
            });

            function showModalDialog(elBtn){
                $('#modal-delete-datatable').data('btn', elBtn);
                $('#modal-delete-datatable').modal('show');
            }
        }

    };

}();

// Call main app init
BlankonProjectManagementClient.init();