//by BT

var bt = {};

//this is dependent to knockout
bt.ajax = function () {
    var exec = function (param, callback) {
        $.ajax({
            url: param.url,
            type: param.type,
            data: param.data,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeBegin: function () {
            },
            beforeSend: function (XMLHttpRequest) {
                if (param.containerId) {
                    showLoading(param.containerId);
                }
            },
            success: function (responseData, textStatus, jqXHR) {
                callback(responseData);
            },
            error: function (XMLHttpRequest, textStatus) {

                errorMsg = XMLHttpRequest.statusText;
                // ma.notify.rError(errorMsg);

                callback(XMLHttpRequest);
            },
            complete: function (jsonData, textstatus) {
                if (param.containerId) {
                    hideLoading(param.containerId);
                }
            }
        });
    };

    var get = function (url, callback) {
        $.getJSON(url, function (itemReturn) {
            callback(itemReturn)
        });
    };

    var getWithPayload = function (paramObject, url, callback) {
        $.getJSON(url, paramObject, function (itemReturn) {
            callback(itemReturn)
        });
    };

    return {
        exec: exec,
        get: get,
        getWithPayload: getWithPayload
    };

}();


bt.date = function () {

    function getDate(dateStr) {

        if (dateStr == null) {
            return '';
        }
        else {
            var parseDate = Date.parse(dateStr);

            if (parseDate == null) {
                parseDate = dateStr;
            }

            var date = new Date(parseDate);

            var day = date.getDate(),
            month = date.getMonth(),
            year = date.getFullYear();

            var df = month + 1 + '/' + day + '/' + year;

            return df;
        }

    };

    function currentDate() {

        var dateTime = new Date();
        var month = dateTime.getMonth() + 1;
        var day = dateTime.getDate();
        var year = dateTime.getYear();

        var currentDate = month + "/" + day + "/" + year
        return currentDate;
    }


    return {
        getDate: getDate,
        currentDate: currentDate
    }

}();

bt.util = function () {
    function sum(items, property) {
        var total = 0;
        $.each(items, function () {

            if (this.hasOwnProperty(property)) {
                total += this[property];
            }
        });
        return total;
    };

    function getUniqueList(data, property) {
        var dupes = {};
        var singles = [];
        $.each(data, function (i, el) {

            if (this.hasOwnProperty(property)) {

                if (!dupes[this[property]]) {
                    dupes[this[property]] = true;
                    singles.push(el);
                }
            }
        });

        return singles;
    };

    return {
        sum: sum,
        getUniqueList: getUniqueList
    }
}();

bt.parse = function () {

    function float(str) {
        var formatted = str;

        if (formatted.indexOf("%") > -1) {
            formatted = str.replace("%", "");
        }
        return parseFloat(formatted);
    };

    return {
        float: float
    }
}();

bt.block = function () {

    var load = function () {
        var fMsg = String.format('<div style="margin:10px 0; z-index: 99999;"><div style="float:center; display:block;"><img src="/Content/img/preloader.gif" /></div><div style="float:center;"><h2><span style="margin-left:5px;"></span>{0}</h2></div></div>', 'Loading...');
        $.blockUI({ message: fMsg, overlayCSS: { opacity: 0.15 } });
    };

    var unload = function () {
        $.unblockUI({});
    };

    return {
        load: load,
        unload: unload
    }
}();

function showLoading(container) {
    $('#' + container).block({
        message: 'Loading...', css: {
            width: 'auto',
            border: 'solid 2px #79B7E7',
            color: '#E17009',
            padding: '6px',
            margin: '5px',
            'font-family': '"Helvetica Neue", Arial, "Liberation Sans", FreeSans, sans-serif',
            'font-weight': 'bold', 'font-size': '16px'
        },
        fadeIn: 0, fadeOut: 0, overlayCSS: { opacity: 0 }, baseZ: 1
    });
}

function hideLoading(container) {
    $('#' + container).unblock();
}

String.format = function () {
    for (var i = 1; i < arguments.length; i++) {
        var exp = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        arguments[0] = arguments[0].replace(exp, arguments[i]);
    }
    return arguments[0];
};