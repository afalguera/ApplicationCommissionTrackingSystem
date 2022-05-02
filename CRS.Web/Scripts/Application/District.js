var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var districtCodeData = undefined;
    var districtNameData = undefined;
    var regionNameData = undefined;
    var regionCodeData = undefined;
    var channelCodeData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.regionList = ko.observableArray([]);
    self.selectedRegion = ko.observable();
    self.enableRegion = ko.observable();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['DistrictCode', 'DistrictName', 'RegionName', 'ChannelName', 'DistrictTIN',
        'DistrictAccountName', 'DistrictAccountNumber', 'DistrictBankBranch'];

    self.isEditMode = ko.computed(function () { 
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit District' : 'Add District';
    });

    self.id = ko.observable();
    self.districtCode = ko.observable().extend({
        required: { message: 'District Code is required.' },
        validation: {
            message: 'Invalid District Code.',
            validator: function (val) {
                if (val == districtCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/District/IsDistrictCodeValid',
                    type: 'GET',
                    data: {
                        districtCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.districtName = ko.observable().extend({
        required: { message: 'District Name is required.' },
        validation: {
            message: 'Invalid District Name.',
            validator: function (val) {
                if (val == districtNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/District/IsDistrictNameValid',
                    type: 'GET',
                    data: {
                        districtName: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.regionName = ko.observable();
    self.regionCode = ko.observable();
    self.strMessage = ko.observable();
    self.tin = ko.observable();

    self.channelList = ko.observableArray([]);
    self.selectedChannel = ko.observable();
    self.enableChannel = ko.observable();

    self.accountName = ko.observable();
    self.accountNumber = ko.observable();
    self.bankBranch = ko.observable();

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
            url: '/District/GetDistrictList',
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
                            DistrictCode: item.Code,
                            DistrictName: item.Name,
                            RegionCode: item.RegionCode,
                            RegionName: item.RegionName,
                            ChannelCode: item.ChannelCode,
                            ChannelName: item.ChannelName,
                            DistrictTIN: item.DistrictTIN,
                            DistrictAccountName: item.DistrictAccountName,
                            DistrictAccountNumber: item.DistrictAccountNumber,
                            DistrictBankBranch: item.DistrictBankBranch
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

    //function getItems() {
    //    exec({
    //        url: '/District/GetDistrictList',
    //        type: 'GET',
    //    }, function (data) {
    //        var mappedData = $.map(data, function (item) {
    //            return result = {
    //                Id: item.ID,
    //                DistrictCode: item.Code,
    //                DistrictName: item.Name,
    //                RegionCode: item.RegionCode,
    //                RegionName: item.RegionName
    //            }
    //        });

    //        self.items(mappedData);
    //    });
    //}

    function setFormData(data) {
        var isEditMode = self.isEditMode();
        districtCodeData = isEditMode ? data.DistrictCode : undefined;
        districtNameData = isEditMode ? data.DistrictName : undefined;
        regionCodeData = isEditMode ? data.RegionCode : undefined;
        regionNameData = isEditMode ? data.RegionName : undefined;
        channelCodeData = isEditMode ? data.ChannelCode : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.districtCode(districtCodeData);
        self.districtName(districtNameData);
        self.regionCode(regionCodeData);
        self.regionName(regionNameData);
        self.selectedChannel(channelCodeData);
        self.tin(data.DistrictTIN);
        self.accountName(data.DistrictAccountName);
        self.accountNumber(data.DistrictAccountNumber);
        self.bankBranch(data.DistrictBankBranch);
    }

    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.unvalidate();
        self.getRegions();
    }

    self.edit = function (data) {
        self.mode(modes.Edit);
        setFormData(data);
        self.getRegions();
    }

    self.deleteDistrict = function () {
        $.ajax({
            url: '/District/DeleteDistrict',
            type: 'DELETE',
            data: {
                districtId: self.id()
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


    //self.deleteDistrict = function () {
    //    exec({
    //        url: '/District/DeleteDistrict',
    //        type: 'DELETE',
    //        data: {
    //            districtId: self.id()
    //        }
    //    }, function (data) {
    //        if (data) {
    //            getItems();
    //            $('#confirmDeleteModal').modal('hide');
    //        } else {
    //            alert('Error encountered');
    //        }
    //    });
    //}

    self.isValid = ko.computed(function () {
        return self.districtCode.isValid() &&
            self.districtName.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());
        
        if (self.isValid() && isValid) {
            var options = {
                url: '/District/SaveDistrict',
                type: 'POST',
                data: {
                    Code: self.districtCode(),
                    Name: self.districtName(),
                    RegionCode: self.regionCode(),
                    ChannelCode: self.selectedChannel(),
                    DistrictTIN: self.tin(),
                    DistrictAccountName: self.accountName(),
                    DistrictAccountNumber: self.accountNumber(),
                    DistrictBankBranch: self.bankBranch()
                }
            }

            if (self.isEditMode()) {
                options.url = '/District/EditDistrict';
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
                    $('#districtModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });

            //exec(options, function (result) {
            //    if (result) {
            //        getItems();
            //        $('#districtModal').modal('hide');
            //    } else {
            //        alert('Error encountered');
            //    }
            //});
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
    }

    self.unvalidate = function () {
        self.districtCode.clearError();
        self.districtName.clearError();
    }

    self.validate = function () {
        self.districtCode.valueHasMutated();
        self.districtName.valueHasMutated();
    }

    getItems();

    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    //------------ get Region List ------
    self.getRegions = function () {
        var param = {
            url: '/District/GetRegionList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.regionList(data);
            if (data.length == 1) {
                self.selectedRegion(data[0].Code);
                self.enableRegion(false);
            } else {
                self.enableRegion(true);
            }
        });
    };
    //------------------------- end

    //---------------get Channel List---------
    self.getChannels = function () {
        var param = {
            url: '/CommissionDashboard/GetChannelAllList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.channelList(data);
       });
    };
    //------------------------- end


    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var regionId = self.regionCode();

            if (typeof regionId == 'undefined' || regionId == '') {
                strMessage = strMessage + "Region is required." + '<br/>';
            }
            self.strMessage(strMessage);
        }
    });
    //------------------------------------------ end

    self.getChannels();
    self.getRegions();
}

$(function () {
    ko.applyBindings(new viewModel());
});