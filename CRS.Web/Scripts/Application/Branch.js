var viewModel = function () {
    var self = this;
    //cascadingLocator.call(self);
    employeeSelector.call(self);
    var modes = { Add: 0, Edit: 1, View: 2 };
    var branchIdFilter = 0;
    var branchCodeData = undefined;
    var branchNameData = undefined;
    var channelCodeData = undefined;
    var regionCodeData = undefined;
    var districtCodeData = undefined;
    var employeeIdFilter = 0;


    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });


    $('body').on('show', '#employeeSelectorModal', function () {
        $(this).css({ 'width': '800px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['BranchCode', 'BranchName', 'ManagerName', 'ChannelName', 'RegionName', 'DistrictName', 'TIN', 'AccountName', 'AccountNumber', 'BankBranch', 'IsYGC', 'Actions'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Branch' : 'Add Branch';
    });

    self.duplicateBranch = ko.observable();

    self.channelList = ko.observableArray([]);
    self.selectedChannel = ko.observable();
    self.enableChannel = ko.observable();

    self.regionList = ko.observableArray([]);
    self.selectedRegion = ko.observable();
    self.enableRegion = ko.observable();

    self.districtList = ko.observableArray([]);
    self.selectedDistrict = ko.observable();
    self.enableDistrict = ko.observable();

    self.id = ko.observable();
    self.branchCode.extend({
        required: { message: 'Branch Code is required.' },
        validation: {
            message: 'Branch Code already exists.',
            validator: function (val) {
                if (val == branchCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Branch/IsBranchExists',
                    type: 'GET',
                    data: {
                        channelCode: self.selectedChannel(),
                        branchCode: val,
                        branchName: ''
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.branchName = ko.observable().extend({
        required: { message: 'Branch Name is required.' },
        validation: {
            message: 'Branch Name already exists.',
            validator: function (val) {
                var result = false;
                if (val == branchNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                if (self.selectedChannel()) {
                    self.isBranchExists();
                    if (self.duplicateBranch()) {
                        result = false;
                    } else {
                        result = true;
                    }
                } else {
                    result = true;
                }
                return result;
            }
        }
    });
    //self.channelId = ko.observable();
    //self.districtId = ko.observable();
    self.tin = ko.observable();
    self.accountName = ko.observable();
    self.accountNumber = ko.observable();
    self.bankBranch = ko.observable();
    self.isYGC = ko.observable();
    self.strMessage = ko.observable();

    //self.employeeId = ko.observable();
    //self.employeeSelected = ko.observable();
    //self.employeeId = ko.observable().extend({ required: { message: 'Employee is required.' } });
    //self.employeeSelected.extend({ required: { message: 'Employee is required.' } });
    self.employeeId = ko.observable().extend();
    self.employeeSelected.extend();

    function exec(options, callback, parseToJson, blockUI) {
        options.async = false;
        options.dataType = 'json';
        options.contentType = 'application/json; charset=utf-8';
        options.beforeSend = (blockUI || blockUI == undefined) ?
            $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' }) : undefined;
        options.data = (parseToJson || parseToJson == undefined) ? JSON.stringify(options.data) : options.data;

        $.ajax(options).done(callback).always(function () {
            if (blockUI || blockUI == undefined) {
                $.unblockUI();
            }
        });
    };

    function getItems() {
        $.ajax({
            url: '/Branch/GetBranchList',
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
                        BranchCode: item.Code,
                        BranchName: item.Name,
                        BranchId: item.BranchId,
                        ChannelName: item.ChannelName,
                        ChannelId: item.ChannelId,
                        DistrictName: item.DistrictName,
                        DistrictId: item.DistrictId,
                        TIN: item.TIN,
                        AccountName: item.AccountName,
                        AccountNumber: item.AccountNumber,
                        BankBranch: item.BankBranch,
                        IsYGC: item.IsYGC,
                        Actions: item.ID,
                        RegionCode: item.RegionCode,
                        RegionName: item.RegionName,
                        ManagerName: item.ManagerName,
                        EmployeeId: item.EmployeeId,
                        ChannelCode: item.ChannelCode,
                        DistrictCode: item.DistrictCode
                    }
                });

                self.items(mappedData);
            },
            error: function () {
                $.unblockUI();
            }
        });
    };;

    //function getCascadingLocation() {
    //    exec({
    //        url: '/Branch/GetCascadingLocationList',
    //        type: 'GET',
    //        data: {
    //            branchId: branchIdFilter
    //        }
    //    }, function (data) {
    //        self.channelId(data.ChannelId);
    //        self.regionCode(data.RegionCode);
    //        self.districtId(data.DistrictId);
    //        self.branchId(data.BranchId);
    //    }, false);
    //}

    function setFormData(data) {
        var isEditMode = self.isEditMode();
        channelCodeData = isEditMode ? data.ChannelCode : undefined;
        regionCodeData = isEditMode ? data.RegionCode : undefined;
        districtCodeData = isEditMode ? data.DistrictCode : undefined;
        branchCodeData = isEditMode ? data.BranchCode : undefined;
        branchNameData = isEditMode ? data.BranchName : undefined;
        branchIdFilter = isEditMode ? data.Id : undefined;
        employeeIdFilter = isEditMode ? data.EmployeeId : undefined;
        self.id(branchIdFilter);
        self.branchCode(branchCodeData);
        self.branchName(isEditMode ? data.BranchName : undefined);
        self.tin(isEditMode ? data.TIN : undefined);
        self.accountName(isEditMode ? data.AccountName : undefined);
        self.accountNumber(isEditMode ? data.AccountNumber : undefined);
        self.bankBranch(isEditMode ? data.BankBranch : undefined);
        self.isYGC(isEditMode ? data.IsYGC : undefined);
        self.selectedChannel(channelCodeData);
        self.selectedRegion(regionCodeData);
        self.selectedDistrict(districtCodeData);
        //self.employeeId(employeeIdFilter);
        //self.employeeSelected(employeeIdFilter);
    }

    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.unvalidate();
    }

    self.edit = function (data) {
        self.mode(modes.Edit);
        setFormData(data);
        getEmployee();
    }

    self.deleteBranch = function () {
        $.ajax({
            url: '/Branch/DeleteBranch',
            type: 'DELETE',
            data: {
                branchId: self.id()
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
    }

    self.isValid = ko.computed(function () {
        return self.branchCode.isValid() &&
            self.branchName.isValid()
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());

        if (self.isValid() && isValid) {
            var options = {
                url: '/Branch/SaveBranch',
                type: 'POST',
                data: {
                    Code: self.branchCode(),
                    Name : self.branchName(),
                    ChannelCode : self.selectedChannel(),
                    DistrictCode : self.selectedDistrict(),
                    TIN : self.tin(),
                    AccountName : self.accountName(),
                    AccountNumber : self.accountNumber(),
                    BankBranch : self.bankBranch(),
                    IsYGC: self.isYGC(),
                    EmployeeId: self.employeeSelected() ? self.employeeSelected().Id : 0
                }
            }

            if (self.isEditMode()) {
                options.url = '/Branch/EditBranch';
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
                    $('#branchModal').modal('hide');
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
        self.branchCode.clearError();
        self.branchName.clearError();
    }

    self.validate = function () {
        self.branchCode.valueHasMutated();
        self.branchName.valueHasMutated();
    }

    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var channelId = self.selectedChannel();
            var regionId = self.selectedRegion();
            var districtId = self.selectedDistrict();
        
            if (typeof channelId == 'undefined' || channelId == '') {
                strMessage = strMessage + "Channel is required." + '<br/>';
            }
                         
            if (typeof regionId == 'undefined' || regionId == '') {
                strMessage = strMessage + "Region is required." + '<br/>';
            }
            if (typeof districtId == 'undefined' || districtId == '') {
                strMessage = strMessage + "District is required." + '<br/>';
            }

            self.strMessage(strMessage);
        }
    });
    //------------------------------------------ end

    //-- validate if referror
    self.isBranchExists = function () {
        var param = {
            url: '/Branch/IsBranchExists',
            type: 'GET',
            data: {
                channelCode: self.selectedChannel(),
                branchCode: self.branchCode(),
                branchName: self.branchName()
            },
        };

        bt.ajax.exec(param, function (data) {
            self.duplicateBranch(data.isDuplicate);
        });
    };
    //------------------------------------------ end
    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end


    //---------------get Channel List---------
    self.getChannels = function () {
        var param = {
            url: '/CommissionDashboard/GetChannelAllList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.channelList(data);
            if (data.length == 1) {
                self.selectedChannel(data[0].Code);
                self.enableChannel(false);
            } else {
                self.enableChannel(true);
                self.selectedChannel('');
            }
        });
    };
    //------------------------- end

    //------------ get Region List ------
    self.getRegions = function () {
        var param = {
            url: '/CommissionDashboard/GetRegionList',
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

            if (regionCodeData && self.isEditMode()) {
                self.selectedRegion(regionCodeData);
            }
        });
    };
    //------------------------- end

    //------------ get District List ------
    self.getDistricts = function () {
        var regionCode = (typeof self.selectedRegion() !== 'undefined' ? self.selectedRegion() : '');
        var channelCode = (typeof self.selectedChannel() !== 'undefined' ? self.selectedChannel() : '');
        var param = {
            url: '/CommissionDashboard/GetDistrictList',
            type: 'GET',
            data: {
                regionCode: regionCode,
                channelCode: channelCode
            }
        };

        bt.ajax.exec(param, function (data) {
            self.districtList(data);
            if (data.length == 1) {
                self.selectedDistrict(data[0].Code);
                self.enableDistrict(false);
            } else {
                self.enableDistrict(true);
            }

            if (districtCodeData && self.isEditMode()) {
                self.selectedDistrict(districtCodeData);
            }
        });
    };
    //------------------------- end

  
    //---- onchange selected channel -----
    self.selectedChannel.subscribe(function (ch) {
        var search = ch;
        self.clearChannels();
        if (ch) {
            var channel = ko.utils.arrayFirst(self.channelList(), function (item) {
                return item.Code == search;
            });
            self.getRegions();
        }
    });
    //---- end ----------------------   

    //---- onchange selected region -----
    self.selectedRegion.subscribe(function () {
        var selectedRegion = self.selectedRegion();
        if (typeof selectedRegion == 'undefined' || selectedRegion == '') {
            self.clearDistrict();
        } else {
            self.getDistricts();
        }

    });
    //---- end ----------------------   

    //----Clear Region -----
    self.clearChannels = function () {
        self.regionList([]);
        self.selectedRegion('');
        self.enableRegion(false);
    };
    //---- end ----------------------  

    //----Clear Region -----
    self.clearRegion = function () {
          self.clearDistrict();

    };
    //---- end ----------------------   

    //----Clear District -----
    self.clearDistrict = function () {
        self.districtList([]);
        self.selectedDistrict('');
        self.enableDistrict(false);
    };
    //---- end ----------------------   

    function getEmployee() {
        exec({
            url: '/Employee/GetEmployee',
            type: 'GET',
            data: {
                employeeId: employeeIdFilter
            }
        }, function (data) {
            var mappedData = $.map([data], function (item) {
                return result = {
                    Id: item.ID,
                    Name: item.Name,
                    EmployeeNumber: item.EmployeeNumber
                };
            });
            self.employeeSelected(mappedData[0]);
        }, false);
    }

    //---- Initialize ---------------
    getItems();
    self.getChannels();
    //getEmployee();
    //---- end ----------------------

    self.clearEmployee = function () {
        self.employeeSelected(undefined);
    };
    
}

$(function () {
    viewModel.prototype = new cascadingLocator();
    ko.applyBindings(new viewModel());
});