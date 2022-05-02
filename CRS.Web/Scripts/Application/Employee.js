var viewModel = function () {
    var self = this;
    cascadingLocator.call(self);
    var modes = { Add: 0, Edit: 1, View: 2 };
    var branchIdFilter = 0;
    var employeeNumberData = undefined;
    var regionCodeData = undefined;
    var districtCodeData = undefined;
    var branchCodeData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['BranchName', 'EmployeeNumber', 'LastName', 'FirstName', 'MiddleName', 'IsActive', 'Actions'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Employee' : 'Add Employee';
    });

    self.id = ko.observable();
    self.employeeNumber = ko.observable().extend({
        required: { message: 'Employee Number is required.' },
        validation: {
            message: 'Employee Number already exists.',
            validator: function (val) {
                if (val == employeeNumberData) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Employee/IsEmployeeNumberValid',
                    type: 'GET',
                    data: {
                        employeeNumber: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.lastName = ko.observable().extend({ required: { message: 'Last Name is required.' } });
    self.firstName = ko.observable().extend({ required: { message: 'First Name is required.' } });
    self.middleName = ko.observable();
    self.isActive = ko.observable();
    //self.branchId.extend({ required: { message: 'Branch is required.' } })
    self.branchId = ko.observable();
    self.strMessage = ko.observable();

    self.channelList = ko.observableArray();
    self.channelCode = ko.observable();
    self.regionList = ko.observableArray();
    self.regionCode = ko.observable();
    self.districtCode = ko.observable();
    self.branchList = ko.observableArray();
    self.branchCode = ko.observable();

    self.enableRegion = ko.observable(false);
    self.enableDistrict = ko.observable(false);
    self.enableBranch = ko.observable(false);

    function exec(options, callback, parseToJson) {
        options.async = false;
        options.dataType = 'json';
        options.contentType = 'application/json; charset=utf-8';
        options.beforeSend = $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });
        options.data = (parseToJson || parseToJson == undefined) ? JSON.stringify(options.data) : options.data;
        $.ajax(options).done(callback).always(function () {
            $.unblockUI()
        });
    }

    function getItems() {
        $.ajax({
            url: '/Employee/GetEmployeeList',
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
                        BranchId: item.BranchId,
                        BranchName: item.BranchName,
                        EmployeeNumber: item.EmployeeNumber,
                        LastName: item.LastName,
                        FirstName: item.FirstName,
                        MiddleName: item.MiddleName,
                        IsActive: item.IsActive,
                        ChannelCode: item.ChannelCode,
                        RegionCode: item.RegionCode,
                        DistrictCode: item.DistrictCode,
                        BranchCode: item.BranchCode,
                        Actions: item.ID
                    }
                });
                self.items(mappedData);
            },
            error: function () {
                $.unblockUI();
            }
        });
        //exec({
        //    url: '/Employee/GetEmployeeList',
        //    type: 'GET',
        //}, function (data) {
        //    var mappedData = $.map(data, function (item) {
        //        return result = {
        //            Id: item.ID,
        //            BranchId: item.BranchId,
        //            BranchName: item.BranchName,
        //            EmployeeNumber: item.EmployeeNumber,
        //            LastName: item.LastName,
        //            FirstName: item.FirstName,
        //            MiddleName: item.MiddleName,
        //            IsActive: item.IsActive,
        //            Actions: item.ID
        //        }
        //    });

        //    self.items(mappedData);
        //});
    };

    function getCascadingLocation() {
        exec({
            url: '/Branch/GetCascadingLocationList',
            type: 'GET',
            data: {
                branchId: branchIdFilter
            }
        }, function (data) {
            self.channelCode(data.ChannelCode);
            self.regionCode(data.RegionCode);
            //self.areaCode(data.AreaCode);
            self.districtCode(data.DistrictCode);
            self.branchId(data.BranchId);
        }, false);
    }

    function setFormData(data) {
        var isEditMode = self.isEditMode();
        employeeNumberData = isEditMode ? data.EmployeeNumber : undefined;
        regionCodeData = isEditMode ? data.RegionCode : undefined;
        districtCodeData = isEditMode ? data.DistrictCode : undefined;
        branchCodeData = isEditMode ? data.BranchCode : undefined;
        self.id(isEditMode ? data.Id : undefined);
        branchIdFilter = isEditMode ? data.BranchId : undefined;
        self.employeeNumber(employeeNumberData);
        self.lastName(isEditMode ? data.LastName : undefined);
        self.firstName(isEditMode ? data.FirstName : undefined);
        self.middleName(isEditMode ? data.MiddleName : undefined);
        self.isActive(isEditMode ? data.IsActive : undefined);
        self.channelCode(isEditMode ? data.ChannelCode : undefined);
        self.regionCode(regionCodeData);
        self.districtCode(districtCodeData);
        self.branchCode(branchCodeData);
    }

    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.channelCode(undefined);
        self.regionCode(undefined);
        self.unvalidate();
    }

    self.edit = function (data) {
        self.mode(modes.Edit);
        setFormData(data);
        getCascadingLocation();
    }

    self.deleteEmployee = function () {
        //exec({
        //    url: '/Employee/DeleteEmployee',
        //    type: 'DELETE',
        //    data: {
        //        employeeId: self.id()
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
            url: '/Employee/DeleteEmployee',
            type: 'DELETE',
            data: {
                employeeId: self.id()
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
        return self.lastName.isValid() &&
            self.firstName.isValid()
        //&& self.branchId.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());
        if (self.isValid() && isValid) {
            var options = {
                url: '/Employee/SaveEmployee',
                type: 'POST',
                data: {
                    //BranchId: self.branchId(),
                    BranchCode: self.branchCode(),
                    EmployeeNumber: self.employeeNumber(),
                    LastName: self.lastName(),
                    FirstName: self.firstName(),
                    MiddleName: self.middleName(),
                    IsActive: self.isActive()
                }
            }

            if (self.isEditMode()) {
                options.url = '/Employee/EditEmployee';
                options.type = 'POST';
                options.data.Id = self.id();
            }

            //exec(options, function (result) {
            //    if (result) {
            //        getItems();
            //        $('#employeeModal').modal('hide');
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
                    $('#employeeModal').modal('hide');
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
    }

    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var channelId = self.channelCode();
            var regionId = self.regionCode();
            var districtId = self.districtCode();
            var branchId = self.branchCode();
            if (typeof channelId == 'undefined' || channelId == '') {
                strMessage = strMessage + "Channel is required." + '<br/>';
            }
            if (typeof regionId == 'undefined' || regionId == '') {
                strMessage = strMessage + "Region is required." + '<br/>';
            }
            if (typeof districtId == 'undefined' || districtId == '') {
                strMessage = strMessage + "District is required." + '<br/>';
            }
            if (typeof branchId == 'undefined' || branchId == '') {
                strMessage = strMessage + "Branch is required.";
            }
            self.strMessage(strMessage);
        }
    });

    self.unvalidate = function () {
        self.lastName.clearError();
        self.firstName.clearError();
        self.employeeNumber.clearError();
        //self.branchId.clearError();
    }

    self.validate = function () {
        self.lastName.valueHasMutated();
        self.firstName.valueHasMutated();
        self.employeeNumber.valueHasMutated();
        //self.branchId.valueHasMutated();
    }

    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    //---------------get Channel List---------
    self.getChannels = function () {
        var param = {
            url: '/CommissionDashboard/GetChannelList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.channelList(data);
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
            self.enableRegion(true);
            if (regionCodeData && self.isEditMode()) {
                self.regionCode(regionCodeData);
            }

        });
    };
    //------------------------- end

    //------------ get District List ------
    self.getDistricts = function () {
        var regionCode = (typeof self.regionCode() !== 'undefined' ? self.regionCode() : '');
        var channelCode = (typeof self.channelCode() !== 'undefined' ? self.channelCode() : '');
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
            self.enableDistrict(true);
            if (districtCodeData && self.isEditMode()) {
                self.districtCode(districtCodeData);
            }
        });
    };
    //------------------------- end


    //------------ get Branch List ------
    self.getBranches = function () {
        var channelCode = (typeof self.channelCode() !== 'undefined' ? self.channelCode() : '');
        var districtCode = (typeof self.districtCode() !== 'undefined' ? self.districtCode() : '');

        var param = {
            url: '/CommissionDashboard/GetBranchList',
            type: 'GET',
            data: {
                channelCode: channelCode,
                districtCode: districtCode
            }
        };

        bt.ajax.exec(param, function (data) {
            self.branchList(data);
            self.enableBranch(true);
            if (branchCodeData && self.isEditMode()) {
                self.branchCode(branchCodeData);
            }
        });
    };
    //------------------------- end


    //---- onchange selected channel -----
    self.channelCode.subscribe(function (ch) {
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
    self.regionCode.subscribe(function () {
        var selectedRegion = self.regionCode();
        if (typeof selectedRegion == 'undefined' || selectedRegion == '') {
            self.clearDistrict();
        } else {
            self.getDistricts();
        }

    });
    //---- end ----------------------   

    //---- onchange selected district -----
    self.districtCode.subscribe(function () {
        var selectedDistrict = self.districtCode();
        if (typeof selectedDistrict == 'undefined' || selectedDistrict == '') {
            self.clearBranch();
        } else {
            self.getBranches();
        }
    });
    //---- end ---------------------- 

    //----Clear Region -----
    self.clearChannels = function () {
        self.regionList([]);
        self.regionCode('');
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
        self.districtCode('');
        self.enableDistrict(false);
        self.clearBranch();
    };
    //---- end ----------------------   

    //----Clear Branch -----
    self.clearBranch = function () {
        self.branchList([]);
        self.branchCode('');
        self.enableBranch(false);

    };
    //---- end ----------------------



    getItems();
    self.getChannels();
    self.getRegions();
}

$(function () {
    viewModel.prototype = new cascadingLocator();
    ko.applyBindings(new viewModel());

});