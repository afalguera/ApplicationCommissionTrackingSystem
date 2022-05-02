var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var yearData = undefined;
    var fullYearRunRateData = undefined;
    var mtdTargetBookingData = undefined;
    var fullYearTargetData = undefined;
    var ytdTargetData = undefined;
    var channelNameData = undefined;
    var channelCodeData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.channelList = ko.observableArray([]);
    self.selectedChannel = ko.observable();
    self.enableChannel = ko.observable();
    self.mode = ko.observable(modes.View);
    //self.itemColumns = ['Year', 'FullYearRunRate', 'MTDTargetBooking', 'FullYearTarget', 'YTDTarget', 'ChannelName', 'ChannelCode'];

    self.itemColumns = ['ChannelName', 'ChannelCode', 'Year', 'MTM1', 'MTM2', 'MTM3', 'MTM4', 'MTM5', 'MTM6',
                    'MTM7', 'MTM8', 'MTM9', 'MTM10', 'MTM11', 'MTM12' ];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Channel Target' : 'Add Channel Target';
    });

    self.strMessage = ko.observable();

    self.id = ko.observable();
    self.year = ko.observable().extend({
        required: { message: 'Year is required.' },
        validation: {
            message: 'Invalid Year.',
            validator: function (val) {
                if (val == yearData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/ChannelTarget/IsChannelTargetValid',
                    type: 'GET',
                    data: {
                        channelCode: $('#ddChannel').val(),
                        year: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.fullYearRunRate = ko.observable().extend({ required: { message: 'Full Year Run Rate is required.' } });
    self.channelName = ko.observable();
    self.channelCode = ko.observable();
    //self.channelName = ko.observable().extend({ required: { message: 'Channel Name is required.' } });
    //self.channelCode = ko.observable().extend({ required: { message: 'Channel Name is required.' } });
    //self.mtdTargetBooking = ko.observable().extend({ required: { message: 'MTD Target Booking is required.' } });
    //self.fullYearTarget = ko.observable().extend({ required: { message: 'Full Year Target is required.' } });
    //self.ytdTarget = ko.observable().extend({ required: { message: 'YTD Target is required.' } });

    self.mtm1 = ko.observable();
    self.mtm2 = ko.observable();
    self.mtm3 = ko.observable();
    self.mtm4 = ko.observable();
    self.mtm5 = ko.observable();
    self.mtm6 = ko.observable();
    self.mtm7 = ko.observable();
    self.mtm8 = ko.observable();
    self.mtm9 = ko.observable();
    self.mtm10 = ko.observable();
    self.mtm11 = ko.observable();
    self.mtm12 = ko.observable();
    self.isOneValue = ko.observable();
    self.mtmAll = ko.observable();

    function exec(options, callback, parseToJson, blockUI) {
        options.async = false;
        options.dataType = 'json';
        options.contentType = 'application/json; charset=utf-8';
        options.beforeSend = (blockUI || blockUI == undefined) ? 
            $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' }) : undefined;
        options.data = (parseToJson || parseToJson == undefined) ? JSON.stringify(options.data) : options.data;
        $.ajax(options).done(callback).always(function () {
            $.unblockUI();
        });
    }

    //--Get items (begin) ---------------------------------------------------------//
    function getItems() {
        $.ajax({
            url: '/ChannelTarget/GetChannelTargetList',
            type: 'GET',
            data: {},
            beforeSend: function () {
                $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });
            },
            complete: function () {
                $.unblockUI();
            },
            success: function (data) {
                var mappedData = $.map(data, function (item) {
                    return result = {
                        Id: item.ID,
                        Year: item.Year,
                        //YTDTarget: item.YTDTarget > 0 ? formatCurrency(new Number(item.YTDTarget).toFixed(2)) : '',
                        ChannelCode: item.ChannelCode,
                        ChannelName: item.ChannelName,
                        MTM1: item.MTM1 > 0 ? commaSeparateNumber(item.MTM1) : '',
                        MTM2: item.MTM2 > 0 ? commaSeparateNumber(item.MTM2) : '',
                        MTM3: item.MTM3 > 0 ? commaSeparateNumber(item.MTM3) : '',
                        MTM4: item.MTM4 > 0 ? commaSeparateNumber(item.MTM4) : '',
                        MTM5: item.MTM5 > 0 ? commaSeparateNumber(item.MTM5) : '',
                        MTM6: item.MTM6 > 0 ? commaSeparateNumber(item.MTM6) : '',
                        MTM7: item.MTM7 > 0 ? commaSeparateNumber(item.MTM7) : '',
                        MTM8: item.MTM8 > 0 ? commaSeparateNumber(item.MTM8) : '',
                        MTM9: item.MTM9 > 0 ? commaSeparateNumber(item.MTM9) : '',
                        MTM10: item.MTM10 > 0 ? commaSeparateNumber(item.MTM10) : '',
                        MTM11: item.MTM11 > 0 ? commaSeparateNumber(item.MTM11) : '',
                        MTM12: item.MTM12 > 0 ? commaSeparateNumber(item.MTM12) : '',
                    }
                });
                self.items(mappedData);
            },
            error: function () {
                $.unblockUI();
            }
        });
    };
    //--Get items (end) ---------------------------------------------------------//

    function setFormData(data) {
        var isEditMode = self.isEditMode();
        yearData = isEditMode ? data.Year : undefined;
        //ytdTargetData = isEditMode ? data.YTDTarget : undefined;
        channelCodeData = isEditMode ? data.ChannelCode : undefined;
        channelNameData = isEditMode ? data.ChannelName : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.year(yearData);
        //self.ytdTarget(ytdTargetData);
        self.channelCode(channelCodeData);
        self.channelName(channelNameData);
        self.mtm1(isEditMode ? replaceCommas(data.MTM1) : undefined);
        self.mtm2(isEditMode ? replaceCommas(data.MTM2) : undefined);
        self.mtm3(isEditMode ? replaceCommas(data.MTM3) : undefined);
        self.mtm4(isEditMode ? replaceCommas(data.MTM4) : undefined);
        self.mtm5(isEditMode ? replaceCommas(data.MTM5) : undefined);
        self.mtm6(isEditMode ? replaceCommas(data.MTM6) : undefined);
        self.mtm7(isEditMode ? replaceCommas(data.MTM7) : undefined);
        self.mtm8(isEditMode ? replaceCommas(data.MTM8) : undefined);
        self.mtm9(isEditMode ? replaceCommas(data.MTM9) : undefined);
        self.mtm10(isEditMode ? replaceCommas(data.MTM10) : undefined);
        self.mtm11(isEditMode ? replaceCommas(data.MTM11) : undefined);
        self.mtm12(isEditMode ? replaceCommas(data.MTM12) : undefined);
        self.isOneValue(false);
    }

    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.unvalidate();
    };

    self.edit = function (data) {
        self.mode(modes.Edit);
        self.hideShowDiv(false);
        setFormData(data);
    };

    //--delete function (begin) ---------------------------------------------------------//
    self.deleteChannelTarget = function () {
        $.ajax({
            url: '/ChannelTarget/DeleteChannelTarget',
            type: 'DELETE',
            data: {
                channelTargetId: self.id()
            },
            beforeSend: function () {
                $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });
            },
            complete: function () {
                $.unblockUI();
            },
            success: function (data) {
                getItems();
                $('#confirmDeleteModal').modal('hide');
            },
            error: function () {
                $.ambiance({
                    message: 'Error encountered',
                    type: "error",
                    title: "Error Notification!",
                    fade: false
                });
                $.unblockUI();
            }
        });
    };
    //--delete function (end) ---------------------------------------------------------//

    self.isValid = ko.computed(function () {
        return self.year.isValid()
               //&& self.ytdTarget.isValid()
        ;
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    //--save function (begin) ---------------------------------------------------------//
    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());

        if (self.isValid() && isValid) {
            var options = {
                url: '/ChannelTarget/SaveChannelTarget',
                type: 'POST',
                data: {
                    Year: self.year(),
                    ChannelCode: self.channelCode(),
                    //YTDTarget: self.ytdTarget(),
                    MTM1: self.isOneValue() ? self.mtmAll() : self.mtm1(),
                    MTM2: self.isOneValue() ? self.mtmAll() : self.mtm2(),
                    MTM3: self.isOneValue() ? self.mtmAll() : self.mtm3(),
                    MTM4: self.isOneValue() ? self.mtmAll() : self.mtm4(),
                    MTM5: self.isOneValue() ? self.mtmAll() : self.mtm5(),
                    MTM6: self.isOneValue() ? self.mtmAll() : self.mtm6(),
                    MTM7: self.isOneValue() ? self.mtmAll() : self.mtm7(),
                    MTM8: self.isOneValue() ? self.mtmAll() : self.mtm8(),
                    MTM9: self.isOneValue() ? self.mtmAll() : self.mtm9(),
                    MTM10: self.isOneValue() ? self.mtmAll() : self.mtm10(),
                    MTM11: self.isOneValue() ? self.mtmAll() : self.mtm11(),
                    MTM12: self.isOneValue() ? self.mtmAll() : self.mtm12(),
                }
            }

            if (self.isEditMode()) {
                options.url = '/ChannelTarget/EditChannelTarget';
                options.type = 'POST';
                options.data.Id = self.id();
            }

            $.ajax({
                url: options.url,
                type: options.type,
                data: options.data,
                beforeSend: function () {
                    $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });
                },
                complete: function () {
                    $.unblockUI();
                },
                success: function (data) {
                    getItems();
                    $('#channelTargetModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });
        }
    };
    //--save function (end) ---------------------------------------------------------//

    //self.save = function () {
    //    self.validate();
    //    var isValid = isWhitespaceNotEmpty(self.strMessage());

    //    if (self.isValid() && isValid) {
    //        var options = {
    //            url: '/ChannelTarget/SaveChannelTarget',
    //            type: 'POST',
    //            data: {
    //                Year: self.year(),
    //                ChannelCode: self.channelCode(),
    //                YTDTarget: self.ytdTarget(),
    //                MTM1: self.isOneValue() ? self.mtmAll() : self.mtm1(),
    //                MTM2: self.isOneValue() ? self.mtmAll() : self.mtm2(),
    //                MTM3: self.isOneValue() ? self.mtmAll() : self.mtm3(),
    //                MTM4: self.isOneValue() ? self.mtmAll() : self.mtm4(),
    //                MTM5: self.isOneValue() ? self.mtmAll() : self.mtm5(),
    //                MTM6: self.isOneValue() ? self.mtmAll() : self.mtm6(),
    //                MTM7: self.isOneValue() ? self.mtmAll() : self.mtm7(),
    //                MTM8: self.isOneValue() ? self.mtmAll() : self.mtm8(),
    //                MTM9: self.isOneValue() ? self.mtmAll() : self.mtm9(),
    //                MTM10: self.isOneValue() ? self.mtmAll() : self.mtm10(),
    //                MTM11: self.isOneValue() ? self.mtmAll() : self.mtm11(),
    //                MTM12: self.isOneValue() ? self.mtmAll() : self.mtm12(),
    //            }
    //        }

    //        if (self.isEditMode()) {
    //            options.url = '/ChannelTarget/UpdateChannelTarget';
    //            options.type = 'PUT';
    //            options.data.Id = self.id();
    //        }

    //        exec(options, function (result) {
    //            if (result) {
    //                getItems();
    //                $('#channelTargetModal').modal('hide');
    //            } else {
    //                alert('Error encountered');
    //            }
    //        });
    //    } else {
    //        var str = self.strMessage();
    //        if (!isWhitespaceNotEmpty(str)) {
    //            $.ambiance({
    //                message: str,
    //                type: "error",
    //                title: "Error Notification!",
    //                fade: false
    //            });
    //        }
    //        return false;
    //    }
    //}

    self.unvalidate = function () {
        self.year.clearError();
        //self.ytdTarget.clearError();
    }

    self.validate = function () {
        self.year.valueHasMutated();
        //self.ytdTarget.valueHasMutated();
    }

    //------------ get Channel List ------
    function getChannels() {
        var param = {
            url: '/ChannelTarget/GetChannelList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.channelList(data);
            if (data.length == 1) {
                self.selectedChannel(data[0].ChannelCode);
                self.enableChannel(false);
            } else {
                self.enableChannel(true);
            }
        });
    };
    //------------------------- end
    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var channel = self.channelCode();

            if (typeof channel == 'undefined' || channel == '') {
                strMessage = strMessage + "Channel is required." + '<br/>';
            }
            self.strMessage(strMessage);
        }
    });
    //------------------------------------------ end
    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    //-- hide/show div
    self.isOneValue.subscribe(function () {
        self.hideShowDiv(self.isOneValue());      
    });
    //------------------------------------------ end

    //-- hide show divs
    self.hideShowDiv = function (selection) {
        var divMonths = $('#divMonths');
        var divOneValue = $('#divOneValue');
        if (selection == true || selection == "true") {
            divOneValue.show();
            divMonths.hide();

        } else {
            divOneValue.hide();
            divMonths.show();
            self.mtmAll('');
        }
    };
    //------------------------------------------ end

    function formatCurrency(value) {
        var result = '';
        var decimalPlace = value.substring(value.indexOf('.'));
        var valueLength = value.length - decimalPlace.length;
        for (ctr = valueLength - 1; ctr >= 0; ctr--) {
            result = value[ctr] + result;
            if (ctr != 0 && (valueLength - ctr) % 3 == 0) {
                result = ',' + result;
            }
        }
        return result + decimalPlace;
    };


    function commaSeparateNumber(val) {
        while (/(\d+)(\d{3})/.test(val.toString())) {
            val = val.toString().replace(/(\d+)(\d{3})/, '$1' + ',' + '$2');
        }
        return val;
    };

    function replaceCommas(amount) {
        if (amount.toString().indexOf(',') === -1) {
            return amount;
        } else {
            var amountWithoutCommas = amount.replace(/,/g, '')
            return parseInt(amountWithoutCommas);
        }
    };

   

    //initialize
    getItems();
    getChannels();
    self.hideShowDiv(false);
}

$(function () {
    ko.applyBindings(new viewModel());
    $("#txtYear").mask("9999");
    $('[id^=txtMTM]').keyup(function () {
        if (this.value != this.value.replace(/[^0-9\.]/g, '')) {
            this.value = this.value.replace(/[^0-9\.]/g, '');
        }
    });
  

    ////$('#txtYTDTarget').autoNumeric('init', { vMax: '9999999999' });
    //$('#txtMTM1').autoNumeric('init', { vMax: '9999999', lZero: 'deny' });
    //$('#txtMTM2').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM3').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM4').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM5').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM6').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM7').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM8').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM9').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM10').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM11').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTM12').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
    //$('#txtMTMAll').autoNumeric('init', { vMax: '9999999999', lZero: 'deny' });
   
   
});



