/*
 * jQuery File Upload Plugin JS Example 6.5.1
 * https://github.com/blueimp/jQuery-File-Upload
 *
 * Copyright 2010, Sebastian Tschan
 * https://blueimp.net
 *
 * Licensed under the MIT license:
 * http://www.opensource.org/licenses/MIT
 */

/*jslint nomen: true, unparam: true, regexp: true */
/*global $, window, document */

$(function () {
    'use strict';

    // Initialize the jQuery File Upload widget:
    $('#fileupload').fileupload();

    $('#fileupload').fileupload('option', {
            maxFileSize: 5000000, // 5MB
            resizeMaxWidth: 1920,
            acceptFileTypes: /(\.|\/)(gif|jpe?g|png|txt|xls?x|doc?x|pdf)$/i,
            resizeMaxHeight: 1200,
            maxNumberOfFiles: 5,
            add: function (e, data) {
                var total_size = 0;
                var bAllow = true;

                $.each(data.files, function (index, file) {
                    if (file.size >= 1000000) { //1Mb
                        $.ambiance({
                            message: "Cannot upload file greater than 1MB.",
                            type: "error",
                            timeout: 30
                        });
                        bAllow = false;
                        return false;
                       
                    }
                    total_size = total_size + file.size;
                });

                if (total_size > 5000000) {//5Mb
                    $.ambiance({
                        message: "Cannot upload file greater than 5MB.",
                        type: "error",
                        timeout: 30
                    });
                    return false;
                } else {
                    if (bAllow) {
                        data.submit();
                    } else {
                        return false;
                    }
                    
                }
              
            }
    });

    $('#fileupload').bind('fileuploaddestroy', function (e, data) {
        var filename = data.url.substring(data.url.indexOf("=") + 1);
        var param = {
            url: '/ApplicationStatus/Delete',
            type: 'GET',
            data: { id: filename }
        };

        bt.ajax.exec(param, function (data) {
        });
    });
});
