var reportHelper = {};
//-- getReportData
reportHelper.getReportDataWithDetails = function (url, isSummary) {
    $.ajax({
        url: url,
        type: 'GET',
        data: { isSummary: isSummary },
        beforeSend: function () {
            $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });

        },
        complete: function () {
            $.unblockUI();
        },
        success: function (data) {
            //if (!data || data == '') {
            //    $.ambiance({
            //        message: 'No record found!',
            //        title: "Notification!",
            //        type: "success",
            //        fade: false
            //    });
            //} else {
                var url = "../Reports/ReportViewer.aspx";
                window.open(url);
            //}
        },
        error: function () {
            $.unblockUI();
        }
    });
};
//------------------------------------------ end


//-- getReportData
reportHelper.getReportData = function (url) {
    $.ajax({
        url: url,
        type: 'GET',
        data: {},
        beforeSend: function () {
            $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });

        },
        complete: function () {
            $.unblockUI();
        },
        success: function (data) {
            //if (!data || data == '') {
            //    $.ambiance({
            //        message: 'No record found!',
            //        title: "Notification!",
            //        type: "success",
            //        fade: false
            //    });
            //} else {
                var url = "../Reports/ReportViewer.aspx";
                window.open(url);
            //}
        },
        error: function () {
            $.unblockUI();
        }
    });
};
//------------------------------------------ end