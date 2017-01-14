'use strict';
var BlankonProjectManagementTicketCreate = function () {

    return {

        // =========================================================================
        // CONSTRUCTOR APP
        // =========================================================================
        init: function () {
            BlankonProjectManagementTicketCreate.chosenSelect();
        },

        // =========================================================================
        // CHOSEN SELECT
        // =========================================================================
        chosenSelect: function () {
            if($('.chosen-select').length){
                $('.chosen-select').chosen();
            }
        }

    };

}();

// Call main app init
BlankonProjectManagementTicketCreate.init();