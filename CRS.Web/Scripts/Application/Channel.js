var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var channelCodeData = undefined;
    var channelNameData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.positionDetails = ko.observableArray();
    self.mode = ko.observable(modes.View);
    //self.itemColumns = ['ChannelName', 'ChannelCode', 'PayeeName', 'PayeeTin', 'AccountName', 'AccountNumber', 'BankBranch',
    //   'ChannelRequestor', 'ChannelChecker', 'ChannelNoter', 'SalesManager', 'EAPRDescription', 'IsYGC', 'IsGross', 'IsVatable',
    //   'IsEAPR', 'IsRCBC', 'IsMyOrange', 'Actions'
    //];
    self.itemColumns = ['ChannelName', 'ChannelCode','ChannelRequestor', 'EAPRDescription', 'Actions'];


    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Channel' : 'Add Channel';
    });

    self.id = ko.observable();
    self.channelCode = ko.observable().extend({
        required: { message: 'Channel Code is required.' },
        validation: {
            message: 'Code already exists.',
            validator: function (val) {
                if (val == channelCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Channel/IsChannelCodeValid',
                    type: 'GET',
                    data: {
                        channelCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.channelName = ko.observable().extend({
        required: { message: 'Channel Name is required.' },
        validation: {
            message: 'Name already exists.',
            validator: function (val) {
                if (val == channelNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Channel/IsChannelNameValid',
                    type: 'GET',
                    data: {
                        channelName: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.payeeName = ko.observable(); //.extend({ required: { message: 'Payee Name is required.' } });
    self.payeeTin = ko.observable(); //.extend({ required: { message: 'Payee Tin is required.' } });
    self.accountName = ko.observable(); //.extend({ required: { message: 'Account Name is required.' } });
    self.accountNumber = ko.observable(); //.extend({ required: { message: 'Account Number is required.' } });
    self.bankBranch = ko.observable(); //.extend({ required: { message: 'Bank Branch is required.' } });
    //self.requestorId = ko.observable().extend({ required: { message: 'Requestor is required' } });
    self.requestorId = ko.observable();
    self.checkerId = ko.observable();
    self.noterId = ko.observable();
    self.salesManagerId = ko.observable();
    self.isYGC = ko.observable();
    self.eaprDescription = ko.observable();
    self.isGross = ko.observable();
    self.isVat = ko.observable();
    self.isEapr = ko.observable();
    self.isRcbc = ko.observable();
    self.isMyOrange = ko.observable();
    self.requestedByList = ko.observableArray();
    self.checkedByList = ko.observableArray();
    self.notedByList = ko.observableArray();
    self.approverList = ko.observableArray();
    self.strMessage = ko.observable();
    self.enableChannel = ko.observable();

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

    function getItems() {
        //exec({
        //    url: '/Channel/GetChannelList',
        //    type: 'GET',
        //}, function (data) {
        //    var mappedData = $.map(data, function (item) {
        //        return result = {
        //            Id: item.ID,
        //            ChannelCode: item.Code,
        //            ChannelName: item.Name,
        //            PayeeName: item.PayeeName,
        //            PayeeTin: item.PayeeTin,
        //            AccountName: item.AccountName,
        //            AccountNumber: item.AccountNumber,
        //            BankBranch: item.BankBranch,
        //            ChannelRequestor: item.ChannelRequestor,
        //            ChannelRequestorId: item.ChannelRequestorId,
        //            ChannelChecker: item.ChannelChecker,
        //            ChannelCheckerId: item.ChannelCheckerId,
        //            ChannelNoter: item.ChannelNoter,
        //            ChannelNoterId: item.ChannelNoterId,
        //            SalesManager: item.SalesManager,
        //            SalesManagerId: item.SalesManagerId,
        //            EAPRDescription: item.EAPRDescription,
        //            IsYGC: item.IsYGC,
        //            IsGross: item.IsGross,
        //            IsVatable: item.IsVatable,
        //            IsEAPR: item.IsEAPR,
        //            IsRCBC: item.IsRCBC,
        //            IsMyOrange: item.IsMyOrange,
        //            Actions: item.ID
        //        }
        //    });

        //    self.items(mappedData);
        //});
        $.ajax({
            url: '/Channel/GetChannelList',
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
                        ChannelCode: item.Code,
                        ChannelName: item.Name,
                        PayeeName: item.PayeeName,
                        PayeeTin: item.PayeeTin,
                        AccountName: item.AccountName,
                        AccountNumber: item.AccountNumber,
                        BankBranch: item.BankBranch,
                        ChannelRequestor: item.ChannelRequestor,
                        ChannelRequestorId: item.ChannelRequestorId,
                        ChannelChecker: item.ChannelChecker,
                        ChannelCheckerId: item.ChannelCheckerId,
                        ChannelNoter: item.ChannelNoter,
                        ChannelNoterId: item.ChannelNoterId,
                        SalesManager: item.SalesManager,
                        SalesManagerId: item.SalesManagerId,
                        EAPRDescription: item.EAPRDescription,
                        IsYGC: item.IsYGC,
                        IsGross: item.IsGross,
                        IsVatable: item.IsVatable,
                        IsEAPR: item.IsEAPR,
                        IsRCBC: item.IsRCBC,
                        IsMyOrange: item.IsMyOrange,
                        Actions: item.ID
                    }
                });

                self.items(mappedData);
            },
            error: function () {
                $.unblockUI();
            }
        });

    }

    function getPositions()
    {
        exec({
            url: '/Channel/GetPositionDetailsList',
            type: 'GET',
        }, function (data) {
            self.positionDetails(data);
            self.requestedByList(ko.utils.arrayFilter(self.positionDetails(), function (item) {
                return item.PositionType == "R";
            }));
            self.checkedByList(ko.utils.arrayFilter(self.positionDetails(), function (item) {
                return item.PositionType == "C";
            }));
            self.notedByList(ko.utils.arrayFilter(self.positionDetails(), function (item) {
                return item.PositionType == "N";
            }));
            self.approverList(ko.utils.arrayFilter(self.positionDetails(), function (item) {
                return item.PositionType == "A";
            }));
        });
    }

    function setFormData(data) {
        var isEditMode = self.isEditMode();
        channelCodeData = isEditMode ? data.ChannelCode : undefined;
        channelNameData = isEditMode ? data.ChannelName : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.channelCode(channelCodeData);
        self.channelName(isEditMode ? data.ChannelName : undefined);
        self.payeeName(isEditMode ? data.PayeeName : undefined);
        self.payeeTin(isEditMode ? data.PayeeTin : undefined);
        self.accountName(isEditMode ? data.AccountName : undefined);
        self.accountNumber(isEditMode ? data.AccountNumber : undefined);
        self.bankBranch(isEditMode ? data.BankBranch : undefined);
        self.requestorId(isEditMode ? data.ChannelRequestorId : undefined);
        self.checkerId(isEditMode ? data.ChannelCheckerId : undefined);
        self.noterId(isEditMode ? data.ChannelNoterId : undefined);
        self.salesManagerId(isEditMode ? data.SalesManagerId : undefined);
        self.isYGC(isEditMode ? data.IsYGC : undefined);
        self.isGross(isEditMode ? data.IsGross : undefined);
        self.isVat(isEditMode ? data.IsVatable : undefined);
        self.isEapr(isEditMode ? data.IsEAPR : undefined);
        self.isRcbc(isEditMode ? data.IsRCBC : undefined);
        self.isMyOrange(isEditMode ? data.IsMyOrange : undefined);
        self.eaprDescription(isEditMode ? data.EAPRDescription : undefined);
    }

    self.add = function () {
        self.enableChannel(true);
        self.mode(modes.Add);
        setFormData({});
        self.unvalidate();
    }

    self.edit = function (data) {
        self.enableChannel(false);
        self.mode(modes.Edit);
        setFormData(data);
    }

    self.deleteChannel = function () {
        //exec({
        //    url: '/Channel/DeleteChannel',
        //    type: 'DELETE',
        //    data: {
        //        channelId: self.id()
        //    }
        //}, function (data) {
        //    if (data) {
        //        getItems();
        //        $('#confirmDeleteModal').modal('hide');
        //    } else {
        //        alert('Error encountered');
        //    }
        //});
        $.ajax({
            url: '/Channel/DeleteChannel',
            type: 'DELETE',
            data: {
                channelId: self.id()
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
                //alert('Error encountered');
                $.ambiance({
                    message: 'Error encountered',
                    type: "error",
                    title: "Error Notification!",
                    fade: false
                });
                $.unblockUI();
            }
        });
    }

    self.isValid = ko.computed(function () {
        return self.channelCode.isValid() &&
            self.channelName.isValid(); // &&
            //self.payeeName.isValid() &&
            //self.payeeTin.isValid() &&
            //self.accountName.isValid() &&
            //self.accountNumber.isValid() &&
            //self.bankBranch.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    //self.save = function () {
    //    self.validate();
        
    //    if (self.isValid()) {
    //        //alert('isvalid');
    //        var options = {
    //            url: '/Channel/SaveChannel',
    //            type: 'POST',
    //            data: {
    //                Code: self.channelCode(),
    //                Name: self.channelName(),
    //                PayeeName: self.payeeName(),
    //                PayeeTin: self.payeeTin(),
    //                AccountName: self.accountName(),
    //                AccountNumber: self.accountNumber(),
    //                BankBranch: self.bankBranch(),
    //                ChannelRequestorId: self.requestorId(),
    //                ChannelCheckerId: self.checkerId(),
    //                ChannelNoterId: self.noterId(),
    //                SalesManagerId: self.salesManagerId(),
    //                IsYGC: self.isYGC(),
    //                IsGross: self.isGross(),
    //                IsVatable: self.isVat(),
    //                IsEAPR: self.isEapr(),
    //                IsRCBC: self.isRcbc(),
    //                IsMyOrange: self.isMyOrange(),
    //                EAPRDescription: self.eaprDescription()
    //            }
    //        }

    //        if (self.isEditMode()) {
    //            options.url = '/Channel/UpdateChannel';
    //            options.type = 'PUT';
    //            options.data.Id = self.id();
    //        }

    //        exec(options, function (result) {
    //            if (result) {
    //                getItems();
    //                $('#channelModal').modal('hide');
    //            } else {
    //                alert('Error encountered');
    //            }
    //        });
    //    }
    //}
    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());
        if (self.isValid() && isValid) {
            var options = {
                url: '/Channel/SaveChannel',
                type: 'POST',
                data: {
                    Code: self.channelCode(),
                    Name: self.channelName(),
                    PayeeName: self.payeeName(),
                    PayeeTin: self.payeeTin(),
                    AccountName: self.accountName(),
                    AccountNumber: self.accountNumber(),
                    BankBranch: self.bankBranch(),
                    ChannelRequestorId: self.requestorId(),
                    ChannelCheckerId: self.checkerId(),
                    ChannelNoterId: self.noterId(),
                    SalesManagerId: self.salesManagerId(),
                    IsYGC: self.isYGC(),
                    IsGross: self.isGross(),
                    IsVatable: self.isVat(),
                    IsEAPR: self.isEapr(),
                    IsRCBC: self.isRcbc(),
                    IsMyOrange: self.isMyOrange(),
                    EAPRDescription: self.eaprDescription()
                }
            }

            if (self.isEditMode()) {
                    options.url = '/Channel/EditChannel';
                    options.type = 'POST';
                    options.data.Id = self.id();
            }

            //exec(options, function (result) {
            //    if (result) {
            //        getItems();
            //        $('#managerModal').modal('hide');
            //    } else {
            //        alert('Error encountered');
            //    }
            //});
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
                    $('#channelModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });
        } else {
            var str = self.strMessage();
            if (!isWhitespaceNotEmpty(str)) {
                $.ambiance({
                    message: str,
                    type: "error",
                    title: "Error Notification!",
                    fade: false
                });
            }
            return false;
        }
    };



    self.unvalidate = function () {
        self.channelCode.clearError();
        self.channelName.clearError();
        //self.payeeName.clearError();
        //self.payeeTin.clearError();
        //self.accountName.clearError();
        //self.accountNumber.clearError();
        //self.bankBranch.clearError();
    }

    self.validate = function () {
        self.channelCode.valueHasMutated();
        self.channelName.valueHasMutated();
        //self.payeeName.valueHasMutated();
        //self.payeeTin.valueHasMutated();
        //self.accountName.valueHasMutated();
        //self.accountNumber.valueHasMutated();
        //self.bankBranch.valueHasMutated();
    }

    self.mode.subscribe(function (item) {
        if (item != modes.View) {
            getPositions();
        }
    });

    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var requestorId = self.requestorId();
       
            if (typeof requestorId == 'undefined' || requestorId == '') {
                strMessage = strMessage + "Requested By is required." + '<br/>';
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

    getItems();
}

$(function () {
    ko.applyBindings(new viewModel());
    $('#txteaprDesc').mouseover(function(){
        $(this).focus();
    });
});