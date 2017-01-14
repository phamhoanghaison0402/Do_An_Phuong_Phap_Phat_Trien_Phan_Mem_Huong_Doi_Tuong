'use strict';
var BlankonProjectManagementPayments = function () {

    // =========================================================================
    // SETTINGS APP
    // =========================================================================
    var getBaseURL = BlankonApp.handleBaseURL();

    return {

        // =========================================================================
        // CONSTRUCTOR APP
        // =========================================================================
        init: function () {
            BlankonProjectManagementPayments.handleDatatable();
            BlankonProjectManagementPayments.handleDatatableColors();
        },

        // =========================================================================
        // DATATABLE INIT
        // =========================================================================
        handleDatatable: function () {
            // Updates "Select all" control in a data table
            function updateDataTableSelectAllCtrl(table){
                var $table             = table.table().node();
                var $chkbox_all        = $('tbody input[type="checkbox"]', $table);
                var $chkbox_checked    = $('tbody input[type="checkbox"]:checked', $table);
                var chkbox_select_all  = $('thead input[name="select_all"]', $table).get(0);

                // If none of the checkboxes are checked
                if($chkbox_checked.length === 0){
                    chkbox_select_all.checked = false;
                    if('indeterminate' in chkbox_select_all){
                        chkbox_select_all.indeterminate = false;
                    }

                    // If all of the checkboxes are checked
                } else if ($chkbox_checked.length === $chkbox_all.length){
                    chkbox_select_all.checked = true;
                    if('indeterminate' in chkbox_select_all){
                        chkbox_select_all.indeterminate = false;
                    }

                    // If some of the checkboxes are checked
                } else {
                    chkbox_select_all.checked = true;
                    if('indeterminate' in chkbox_select_all){
                        chkbox_select_all.indeterminate = true;
                    }
                }
            }

            // Array holding selected row IDs
            var rows_selected = [];

            var responsiveHelper;
            var breakpointDefinition = {
                tablet: 1024,
                phone_landscape : 480,
                phone_portrait : 320
            };
            var tableID = $('#datatable-sample');
            var table = $('#datatable-sample').DataTable({
                'ajax': {
                    'url': getBaseURL+'/assets/admin/data/payments/datatable-sample.json'
                },
                'columnDefs': [
                    {
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        'className': 'dt-body-center',
                        'render': function (data, type, full, meta){
                            return '<div class="ckbox ckbox-primary">' +
                                '<input id="checkbox-item-'+data+'" type="checkbox" name="select_all" value="1" class="display-hide">' +
                                '<label for="checkbox-item-'+data+'"></label>' +
                                '</div>';
                        }
                    },
                    {
                        'targets': [5,8],
                        'sortable': false
                    },
                    {
                        'targets': 1,
                        'class': 'text-strong'
                    },
                    {
                        'targets': [4,5,6,7],
                        'class': 'text-center'
                    },
                    {
                        'targets': 8,
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
                ],
                'order': [[1, 'asc']],
                'autoWidth' : false,
                'iDisplayLength': 10,
                'lengthMenu': [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
                'select': true,
                'dom': 'Blfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            {
                                extend: 'copy',
                                exportOptions: {
                                    columns: [1,2,3,4,5,6]
                                }
                            },
                            {
                                extend: 'excel',
                                exportOptions: {
                                    columns: [1,2,3,4,5,6]
                                }
                            },
                            {
                                extend: 'csv',
                                exportOptions: {
                                    columns: [1,2,3,4,5,6]
                                }
                            },
                            {
                                extend: 'pdf',
                                exportOptions: {
                                    columns: [1,2,3,4,5,6]
                                }
                            },
                            {
                                extend: 'print',
                                exportOptions: {
                                    columns: [1,2,3,4,5,6]
                                }
                            }
                        ]
                    }
                ],
                'pagingType': 'full_numbers_no_ellipses',
                'preDrawCallback': function () {
                    // Initialize the responsive datatables helper once.
                    if (!responsiveHelper) {
                        responsiveHelper = new ResponsiveDatatablesHelper(tableID, breakpointDefinition);
                    }
                },
                'rowCallback' : function (nRow, row, data, dataIndex) {
                    // Get row ID
                    var rowId = data[0];

                    // If row ID is in the list of selected row IDs
                    if($.inArray(rowId, rows_selected) !== -1){
                        $(row).find('input[type="checkbox"]').prop('checked', true);
                        $(row).addClass('selected');
                    }

                    responsiveHelper.createExpandIcon(nRow);
                },
                'drawCallback' : function(oSettings) {
                    responsiveHelper.respond();
                    // call dropdown bootstrap
                    $('body .dropdown-toggle').dropdown();
                    // call actions on last column datatable
                    BlankonProjectManagementPayments.handleActionViewDatatable();
                    BlankonProjectManagementPayments.handleActionEditDatatable();
                    BlankonProjectManagementPayments.handleActionDeleteDatatable();
                }
            });

            // Change language dinamically
            $('.change-language').on('click', function () {

                // Change state language
                $('.text-language').text($(this).data('title'));

                table.destroy();
                table = null;

                var tableLanguage = BlankonProjectManagementPayments.handleNotificationDatatable('Table language '+$(this).data('title'));

                var rows_selected = [];

                var responsiveHelper;
                var breakpointDefinition = {
                    tablet: 1024,
                    phone_landscape : 480,
                    phone_portrait : 320
                };
                var tableID = $('#datatable-sample');
                table = $('#datatable-sample').DataTable( {
                    'language': {
                        'url': getBaseURL+'/assets/global/plugins/bower_components/datatables/i18n/'+$(this).data('language')+'.json'
                    },
                    'ajax': {
                        'url': getBaseURL+'/assets/admin/data/payments/datatable-sample.json'
                    },
                    'columnDefs': [
                        {
                            'targets': 0,
                            'searchable': false,
                            'orderable': false,
                            'className': 'dt-body-center',
                            'render': function (data, type, full, meta){
                                return '<div class="ckbox ckbox-primary">' +
                                    '<input id="checkbox-item-'+data+'" type="checkbox" name="select_all" value="1" class="display-hide">' +
                                    '<label for="checkbox-item-'+data+'"></label>' +
                                    '</div>';
                            }
                        },
                        {
                            'targets': [5,8],
                            'sortable': false
                        },
                        {
                            'targets': 1,
                            'class': 'text-strong'
                        },
                        {
                            'targets': [4,5,6,7],
                            'class': 'text-center'
                        },
                        {
                            'targets': 8,
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
                    ],
                    'order': [[1, 'asc']],
                    'autoWidth' : false,
                    'iDisplayLength': 10,
                    'lengthMenu': [[5, 10, 25, 50, 100, -1], [5, 10, 25, 50, 100, "All"]],
                    'select': true,
                    'dom': 'Blfrtip',
                    buttons: [
                        {
                            extend: 'collection',
                            text: 'Export',
                            buttons: [
                                {
                                    extend: 'copy',
                                    exportOptions: {
                                        columns: [1,2,3,4,5,6]
                                    }
                                },
                                {
                                    extend: 'excel',
                                    exportOptions: {
                                        columns: [1,2,3,4,5,6]
                                    }
                                },
                                {
                                    extend: 'csv',
                                    exportOptions: {
                                        columns: [1,2,3,4,5,6]
                                    }
                                },
                                {
                                    extend: 'pdf',
                                    exportOptions: {
                                        columns: [1,2,3,4,5,6]
                                    }
                                },
                                {
                                    extend: 'print',
                                    exportOptions: {
                                        columns: [1,2,3,4,5,6]
                                    }
                                }
                            ]
                        }
                    ],
                    'pagingType': 'full_numbers_no_ellipses',
                    'preDrawCallback': function () {
                        // Initialize the responsive datatables helper once.
                        if (!responsiveHelper) {
                            responsiveHelper = new ResponsiveDatatablesHelper(tableID, breakpointDefinition);
                        }
                    },
                    'rowCallback' : function (nRow, row, data, dataIndex) {
                        // Get row ID
                        var rowId = data[0];

                        // If row ID is in the list of selected row IDs
                        if($.inArray(rowId, rows_selected) !== -1){
                            $(row).find('input[type="checkbox"]').prop('checked', true);
                            $(row).addClass('selected');
                        }

                        responsiveHelper.createExpandIcon(nRow);
                    },
                    'drawCallback' : function(oSettings) {
                        responsiveHelper.respond();
                        // call dropdown bootstrap
                        $('body .dropdown-toggle').dropdown();
                        // call actions on last column datatable
                        BlankonProjectManagementPayments.handleActionViewDatatable();
                        BlankonProjectManagementPayments.handleActionEditDatatable();
                        BlankonProjectManagementPayments.handleActionDeleteDatatable();
                        // Call notifications
                        tableLanguage;
                    }
                } );
            });

            // Toggle column
            $('a.toggle-column').on( 'click', function (e) {
                e.preventDefault();

                // Change state
                $(this).parents('li').toggleClass('selected');

                // Get the column API object
                var column = table.column( $(this).attr('data-column') );

                // Toggle the visibility
                column.visible( ! column.visible() );

                // Call notifications
                BlankonProjectManagementPayments.handleNotificationDatatable($(this).text()+' Column');

            } );

            // Handle click on checkbox
            $('#datatable-sample tbody').on('click', '.ckbox, input[type="checkbox"]', function(e){
                var $row = $(this).closest('tr');

                // Get row data
                var data = table.row($row).data();

                // Get row ID
                var rowId = data[0];

                // Determine whether row ID is in the list of selected row IDs
                var index = $.inArray(rowId, rows_selected);

                // If checkbox is checked and row ID is not in list of selected row IDs
                if(this.checked && index === -1){
                    rows_selected.push(rowId);

                    // Otherwise, if checkbox is not checked and row ID is in list of selected row IDs
                } else if (!this.checked && index !== -1){
                    rows_selected.splice(index, 1);
                }

                if(this.checked){
                    $row.addClass('selected');
                } else {
                    $row.removeClass('selected');
                }

                // Update state of "Select all" control
                updateDataTableSelectAllCtrl(table);

                // Prevent click event from propagating to parent
                e.stopPropagation();
            });

            // Handle click on table cells with checkboxes
            $('#datatable-sample').on('click', 'tbody td', function(e){
                if($(this).is(':last-child')){
                    return false;
                }else{
                    $(this).parent().find('input[type="checkbox"]').trigger('click');
                }
            });

            // Handle click on "Select all" control
            $('#datatable-sample thead input[name="select_all"]').on('click', function(e){
                if(this.checked){
                    $('#datatable-sample tbody input[type="checkbox"]:not(:checked)').trigger('click');
                } else {
                    $('#datatable-sample tbody input[type="checkbox"]:checked').trigger('click');
                }

                // Prevent click event from propagating to parent
                e.stopPropagation();
            });

            // Handle table draw event
            table.on('draw', function(){
                // Update state of "Select all" control
                updateDataTableSelectAllCtrl(table);
            });

            // Handle form submission event
            $('#frm-example').on('submit', function(e){
                var form = this;

                // Iterate over all selected checkboxes
                $.each(rows_selected, function(index, rowId){
                    // Create a hidden element
                    $(form).append(
                        $('<input>')
                            .attr('type', 'hidden')
                            .attr('name', 'id[]')
                            .val(rowId)
                    );
                });

                // FOR DEMONSTRATION ONLY

                // Output form data to a console
                $('#example-console').text($(form).serialize());
                console.log("Form submission", $(form).serialize());

                // Remove added elements
                $('input[name="id\[\]"]', form).remove();

                // Prevent actual form submission
                e.preventDefault();
            });
        },

        // =========================================================================
        // ACTION VIEW ROW DATATABLES
        // =========================================================================
        handleActionViewDatatable: function () {
            $('#datatable-sample').on('click', '.btn-view', function(){
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
                    '<label class="col-sm-3 control-label">ID :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[1]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Client :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[2]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Project :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[3]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Date :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[4]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Transaction ID :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[5]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Payment Method :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[6]).html() + '</p>'+
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Amount Paid :</label>' +
                    '<div class="col-sm-9">' +
                    '<p class="form-control-static">' + $('<div/>').text(data[7]).html() + '</p>'+
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
            $('#datatable-sample').on('click', '.btn-edit', function(){
                showModalDialog(this);
            });

            $('#modal-edit-datatable').modal({ show: false });

            $('#modal-edit-datatable').on('show.bs.modal', function (e){
                var $dlg = $(this);

                var $tr    = $($dlg.data('btn')).closest('tr');
                var $table = $($dlg.data('btn')).closest('table');
                var data = $table.DataTable().row($tr).data();
                var selectStatNew = (data[8] == 'new') ? "selected" : "";
                var selectStatExp = (data[8] == 'overdue') ? "selected" : "";

                var html = '<form class="form-horizontal">' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Product</label>' +
                    '<div class="col-sm-9">' +
                    '<input type="hidden" value="' + $('<div/>').text(data[0]).html() + '">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[1]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Description</label>' +
                    '<div class="col-sm-9">' +
                    '<textarea class="form-control" rows="3">'+$('<div/>').text(data[2]).html()+'</textarea>' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Qty</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[3]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Unit Price</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[4]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Tax Rate</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[5]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Tax</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[6]).html() + '">' +
                    '</div>' +
                    '</div>' +
                    '<div class="form-group">' +
                    '<label class="col-sm-3 control-label">Total Price</label>' +
                    '<div class="col-sm-9">' +
                    '<input class="form-control" type="text" value="' + $('<div/>').text(data[7]).html() + '">' +
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
            $('#datatable-sample').on('click', '.btn-delete', function(){
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
        },

        handleNotificationDatatable: function (e) {
            // Call notification state
            var unique_id = $.gritter.add({
                // (string | mandatory) the heading of the notification
                title: e,
                // (string | mandatory) the text inside the notification
                text: 'Success changed!',
                // (string | optional) the image to display on the left
                image: BlankonApp.handleBaseURL()+'/assets/global/img/icon/64/check.png',
                // (bool | optional) if you want it to fade out on its own or just sit there
                sticky: false,
                // (int | optional) the time you want it to be alive for before fading out
                time: '',
                class_name: 'gritter-position'
            });

            // You can have it return a unique id, this can be used to manually remove it later using
            setTimeout(function () {
                $.gritter.remove(unique_id, {
                    fade: true,
                    speed: 'slow'
                });
            }, 1000);
        },

        // =========================================================================
        // DATATABLE COLORS
        // =========================================================================
        handleDatatableColors: function () {
            $('.dropdown-table-colors .dropdown-list').on('click', function () {
                if($('.table-default, .table-primary, .table-danger, .table-success, .table-info, .table-warning, .table-lilac, .table-inverse').length){
                    $('.table-default, .table-primary, .table-danger, .table-success, .table-info, .table-warning, .table-lilac, .table-inverse').removeClass();
                }
                $('#datatable-sample').addClass('table table-middle table-striped table-bordered table-condensed dataTable table-'+$(this).data('color'));

                // Call notifications
                BlankonProjectManagementPayments.handleNotificationDatatable('Table color '+$(this).data('color'));
            });
        }

    };

}();

// Call main app init
BlankonProjectManagementPayments.init();