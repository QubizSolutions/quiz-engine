/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here.
	// For complete reference see:
	// http://docs.ckeditor.com/#!/api/CKEDITOR.config

	// The toolbar groups arrangement, optimized for two toolbar rows.
    config.filebrowserImageUploadUrl = '/home/UploadImage';

    config.skin = 'office2013';
    config.width = 1000;
    config.removePlugins = 'elementspath';
    config.toolbar = 'Basic';
    config.contentsCss
    config.toolbar_Basic =
    [
        ['Bold', 'Italic', 'Underline', '-', 'NumberedList', 'BulletedList', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'Image', '-', 'TextColor', 'BGColor']
    ];
    if (this.ID == 'Answer') {
        config.height = 250;
    }
};
