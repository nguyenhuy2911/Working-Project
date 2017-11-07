/**
 * @license Copyright (c) 2003-2017, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.height = '400px';
    //config.extraPlugins = 'imageuploader';
    config.filebrowserBrowseUrl = '/Lib/ckfinder/ckfinder.html',
    config.filebrowserImageBrowseUrl = '/Lib/ckfinder/ckfinder.html?type=Images',
    config.filebrowserUploadUrl = '/Lib/ckfinder/connector?command=QuickUpload&type=Files',
    config.filebrowserImageUploadUrl = '/Lib/ckfinder/connector?command=QuickUpload&type=Images'
};
