// Copyright (c) 2015, Fujana Solutions - Moritz Maleck. All rights reserved.
// For licensing, see LICENSE.md

CKEDITOR.plugins.add( 'imageuploader', {
    init: function( editor ) {
        editor.config.filebrowserBrowseUrl = '/Lib/ckeditor/plugins/imageuploader/imgbrowser.php';
    }
});
//http://localhost:11878/../../../../uploads/content