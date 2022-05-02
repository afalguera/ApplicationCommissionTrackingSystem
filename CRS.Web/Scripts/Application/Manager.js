var viewModel = function () {
    var self = this;
    employeeSelector.call(self);
    cascadingLocator.call(self);
    var modes = { Add: 0, Edit: 1, View: 2 };
    var branchIdFilter = 0;
    var employeeIdFilter = 0;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });


    $('body').on('show', '#employeeSelectorModal', function () {
        $(this).css({ 'width': '850px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.typeList = ko.observableArray();
    self.outletList = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['Name', 'Description', 'BranchName', 'Actions'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Manager' : 'Add Manager';
    });

    self.id = ko.observable();
    self.employeeId = ko.observable().extend({ required: { message: 'Employee is required.' } });
    //self.branchId.extend({ required: { message: 'Branch is required.' } });
    self.branchId = ko.observable();
    self.employeeSelected.extend({ required: { message: 'Employee is required.' } });
    self.managerTypeId = ko.observable();
    //self.managerTypeId = ko.observable().extend({ required: { message: 'Manager Type is required.' } });
    //self.outletId = ko.observable().extend({ required: { onlyIf: function () { return self.managerTypeId() == 2; }, message: 'Outlet is required.' } });
    //self.outletId = ko.observable();
    //self.showType = ko.observable(false);
    self.strMessage = ko.observable();
    //self.selectedOptionId = ko.observable(self.outletList[0].Id);
    //self.outletId = ko.computed(function () {
    //    return ko.utils.arrayFirst(self.outletList, function (item) {
    //        return item.id === self.outletId();
    //    });
    //});
 
    function exec(options, callback, parseToJson, blockUI) {
        options.async = false;
        options.dataType = 'json';
        options.contentType = 'application/json; charset=utf-8';
        options.beforeSend = (blockUI || blockUI == undefined) ?
            $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' }) : undefined;
        options.data = (parseToJson || parseToJson == undefined) ? JSON.stringify(options.data) : options.data;

        $.ajax(options).done(callback).always(function () {
            if (blockUI || blockUI == undefined) {
                $.unblockUI();
            }
        });
    }

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

    function getItems() {
        //exec({
        //    url: '/Manager/GetManagerList',
        //    type: 'GET'
        //}, function (data) {
        //    var mappedData = $.map(data, function (item) {
        //        return result = {
        //            Id: item.ID,
        //            Name: item.Name,
        //            Description: item.Description,
        //            BranchId: item.BranchId,
        //            BranchName: item.BranchName,
        //            OutletId: item.OutletId,
        //            OutletName: item.OutletName,
        //            EmployeeId: item.EmployeeId,
        //            ManagerTypeId: item.ManagerTypeId,
        //            Actions: item.ID
        //        };
        //    });

        //    self.items(mappedData);
        //});
        $.ajax({
            url: '/Manager/GetManagerList',
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
                        Name: item.Name,
                        Description: item.Description,
                        BranchId: item.BranchId,
                        BranchName: item.BranchName,
                        OutletId: item.OutletId,
                        OutletName: item.OutletName,
                        EmployeeId: item.EmployeeId,
                        ManagerTypeId: item.ManagerTypeId,
                        Actions: item.ID
                    };
                });
                self.items(mappedData);
            },
            error: function () {
                $.unblockUI();
            }
        });
    };

    //function getOutlets(branchId) {
    //    exec({
    //        url: '/Manager/GetOutletList',
    //        type: 'GET',
    //        data: {
    //            branchId: branchId
    //        },
    //    }, function (data) {
    //        var mappedData = $.map(data, function (item) {
    //            return result = {
    //                Id: item.ID,
    //                Name: item.Name
    //            }
    //        });

    //        self.outletList(mappedData);
    //    });
    //}
    //-- validate if referror
    //self.getOutlets = function (branchId) {
    //    var param = {
    //        url: '/Manager/GetOutletList',
    //        type: 'GET',
    //        data: { branchId: branchId },
    //    };

    //    bt.ajax.exec(param, function (data) {
    //        var mappedData = $.map(data, function (item) {
    //            return result = {
    //                Id: item.ID,
    //                Name: item.Name
    //            }
    //        });

    //        self.outletList(mappedData);
    //    });
    //};
    //------------------------------------------ end

    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var managerTypeId = self.managerTypeId();
            var channelId = self.channelCode();
            var regionId = self.regionCode();
            var districtId = self.districtCode();
            var branchId = self.branchId();
            //var outletId = self.outletId();

            if (typeof managerTypeId == 'undefined' || managerTypeId == '') {
                strMessage = strMessage + "Manager Type is required." + '<br/>';
            }
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
                strMessage = strMessage + "Branch is required." + '<br/>';
            }
            //if (self.showType()) {
            //    if (typeof outletId == 'undefined' || outletId == '') {
            //        strMessage = strMessage + "Outlet is required.";
            //    }
            //}
            self.strMessage(strMessage);
        }
    });
    //------------------------------------------ end

    function getTypes() {
        exec({
            url: '/Manager/GetManagerTypeList',
            type: 'GET',
        }, function (data) {
            self.typeList(data);
        });
    }

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
        branchIdFilter = isEditMode ? data.BranchId : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.branchId(branchIdFilter);
        //self.showType(false);
        //var outletId = isEditMode ? data.OutletId : undefined;
        //self.outletId(outletId);
        employeeIdFilter = isEditMode ? data.EmployeeId : undefined;
        self.managerTypeId(isEditMode ? data.ManagerTypeId : undefined);
        //if (isEditMode) {
        //    var desc = $("#ddType option:selected").text();
        //    if (desc == "OutletMgr") {
        //        //$("#ddType option:selected").text(data.OutletName);
        //        self.showType(true);
        //        //self.getOutlets(branchIdFilter);
        //    }         
        //}
    }

    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.channelCode(undefined);
        self.regionCode(undefined);
        self.employeeSelected(undefined);
        self.branchId(undefined);
        //self.outletId(undefined);
        //self.showType(false);
        self.unvalidate();
    }

    self.edit = function (data) {
        self.mode(modes.Edit);
        setFormData(data);
        getCascadingLocation();
        getEmployee();
    }

    self.deleteManager = function () {
        //exec({
        //    url: '/Manager/DeleteManager',
        //    type: 'DELETE',
        //    data: {
        //        managerId: self.id()
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
            url: '/Manager/DeleteManager',
            type: 'DELETE',
            data: {
                managerId: self.id()
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
        return self.employeeSelected.isValid();
            //self.branchId.isValid() &&
            //&& self.managerTypeId.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());
        if (self.isValid() && isValid) {
            var options = {
                url: '/Manager/SaveManager',
                type: 'POST',
                data: {
                    BranchId: self.branchId(),
                    //OutletId: self.outletId(),
                    EmployeeId: self.employeeSelected().Id,
                    ManagerTypeId: self.managerTypeId()
                }
            }

            if (self.isEditMode()) {
                options.url = '/Manager/UpdateManager';
                options.type = 'PUT';
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
                    $('#managerModal').modal('hide');
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

    //---- onchange selected manager type ---
    //self.managerTypeId.subscribe(function (val) {
    //    var desc = $("#ddType option:selected").text();
    //    //var branchId = (typeof self.branchId() !== 'undefined' ? parseInt(self.branchId()) : 0);
    //    self.showType(false);
    //    if (desc == 'OutletMgr' || val == '2')  {
    //        self.showType(true);
    //        if (self.branchId()) {
    //            self.getOutlets(self.branchId());
    //        }
           
    //    }
    //});
    //---------------------------------- end
    //---- onchange selected manager type ---
    //self.branchId.subscribe(function (val) {
    //    var bAllow = (typeof val !== 'undefined' ? true : false);
    //    if (self.showType() && bAllow) {
    //        self.getOutlets(val);
    //    }
        
    //});
    //---------------------------------- end

    self.unvalidate = function () {
        //self.branchId.clearError();
        //self.outletId.clearError();
        self.employeeSelected.clearError();
        //self.managerTypeId.clearError();
    }

    self.validate = function () {
        //self.branchId.valueHasMutated();
        //self.outletId.valueHasMutated();
        self.employeeSelected.valueHasMutated();
        //self.managerTypeId.valueHasMutated();
    }

    self.mode.subscribe(function (item) {
        if (item != modes.View) {
            //getOutlets();
            getTypes();
        }
    });

    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    getItems();
    self.getChannels();
    self.getRegions();
}

$(function () {
    ko.applyBindings(new viewModel());
});