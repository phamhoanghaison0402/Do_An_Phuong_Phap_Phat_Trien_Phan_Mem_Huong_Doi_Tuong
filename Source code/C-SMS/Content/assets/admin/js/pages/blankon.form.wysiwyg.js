var BlankonFormWysiwyg = function () {

    return {

        // =========================================================================
        // CONSTRUCTOR APP
        // =========================================================================
        init: function () {
            BlankonFormWysiwyg.bootstrapWYSIHTML5();
            BlankonFormWysiwyg.summernote();
            BlankonFormWysiwyg.summernote1();
        },

        // =========================================================================
        // BOOTSTRAP WYSIHTML5
        // =========================================================================
        bootstrapWYSIHTML5: function () {
            if ($('#wysihtml5-textarea').length) {
                $('#wysihtml5-textarea').wysihtml5();
            }
        },

        // =========================================================================
        // SUMMERNOTE
        // =========================================================================
        summernote: function () {
            if ($('#summernote-textarea').length) {
                $('#summernote-textarea').summernote();
            }
        },
        summernote1: function () {
            if ($('#summernote-textarea1').length) {
                $('#summernote-textarea1').summernote();
            }
        }

    };

}();

// Call main app init
BlankonFormWysiwyg.init();