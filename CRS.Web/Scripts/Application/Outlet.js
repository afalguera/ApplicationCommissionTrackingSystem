var viewModel = function () {
    var self = this;
    cascadingLocator.call(self);
    var modes = { Add: 0, Edit: 1, View: 2 };
    var branchIdFilter = 0;
    var outletCodeData = undefined;
    var outletNameData = undefined;


    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    //$('body').on('show', '#confirmDeleteModal', function () {
    //    $(this).css({ 'width': '200px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    //});

    //$('body').on('show', '#outletModal', function () {
    //    var size = { width: $(window).width(), height: $(window).height() };
    //    var offsetBody = 150;
    //    var offset = 20;
    //    var width = 500;
    //    $('.modal-body').css('height', size.height - (offsetBody));
    //    $(this).css({ 'width': width, 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
      
    //});

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['OutletCode', 'OutletName', 'BranchName', 'Actions'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Outlet' : 'Add Outlet';
    });

    self.strMessage = ko.observable();
    self.duplicateOutlet = ko.observable();

    self.id = ko.observable();
    self.outletCode = ko.observable().extend({
        required: { message: 'Outlet Code is required.' },
        validation: {
            message: 'Outlet Code already exists.',
            validator: function (val) {
                if (val == outletCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Outlet/IsOutletCodeValid',
                    type: 'GET',
                    data: {
                        outletCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });

    //self.outletName = ko.observable().extend({ required: { message: 'Outlet Name is required.' } });
    self.outletName = ko.observable().extend({
        required: { message: 'Outlet Name is required.' },
        validation: {
            message: 'Outlet Name already exists.',
            validator: function (val) {
                var result = false;
                if (val == outletNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                self.isOutletExists();
                if (self.duplicateOutlet()) {
                    result = false;
                } else {
                    result = true;
                }
                return result;
            }
        }
    });

    //self.branchId.extend({ required: { message: 'Branch is required.' } });
    self.branchId = ko.observable();

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
    }

    function getItems() {
        $.ajax({
            url: '/Outlet/GetOutletList',
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
                            OutletCode: item.Code,
                            OutletName: item.Name,
                            BranchId: item.BranchId,
                            BranchName: item.BranchName,
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
        //    url: '/Outlet/GetOutletList',
        //    type: 'GET',
        //}, function (data) {
        //    var mappedData = $.map(data, function (item) {
        //        return result = {
        //            Id: item.ID,
        //            OutletCode: item.Code,
        //            OutletName: item.Name,
        //            BranchId: item.BranchId,
        //            BranchName: item.BranchName,
        //            Actions: item.ID
        //        }
        //    });

        //    self.items(mappedData);
        //});
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
        outletCodeData = isEditMode ? data.OutletCode : undefined;
        outletNameData = isEditMode ? data.OutletName : undefined;
        self.outletCode(outletCodeData);
        branchIdFilter = isEditMode ? data.BranchId : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.outletName(isEditMode ? data.OutletName : undefined);
        self.branchId(isEditMode ? data.BranchId : undefined);
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

    //self.deleteOutlet = function () {
    //    exec({
    //        url: '/Outlet/DeleteOutlet',
    //        type: 'DELETE',
    //        data: {
    //            outletId: self.id()
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
    self.deleteOutlet = function () {
        $.ajax({
            url: '/Outlet/DeleteOutlet',
            type: 'DELETE',
            data: {
                outletId: self.id()
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
    };

    self.isValid = ko.computed(function () {
        return self.outletCode.isValid() &&
            self.outletName.isValid();
            //&&
            //self.branchId.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        //self.validate();

        //if (self.isValid()) {
        //    var options = {
        //        url: '/Outlet/SaveOutlet',
        //        type: 'POST',
        //        data: {
        //            Code: self.outletCode(),
        //            Name: self.outletName(),
        //            BranchId: self.branchId()
        //        }
        //    }

        //    if (self.isEditMode()) {
        //        options.url = '/Outlet/UpdateOutlet';
        //        options.type = 'PUT';
        //        options.data.Id = self.id();
        //    }

        //    exec(options, function (result) {
        //        if (result) {
        //            getItems();
        //            $('#outletModal').modal('hide');
        //        } else {
        //            alert('Error encountered');
        //        }
        //    });
        //}
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());
        if (self.isValid() && isValid) {
            var options = {
                url: '/Outlet/SaveOutlet',
                type: 'POST',
                data: {
                    Code: self.outletCode(),
                    Name: self.outletName(),
                    BranchId: self.branchId()
                }
            }

            if (self.isEditMode()) {
                options.url = '/Outlet/UpdateOutlet';
                options.type = 'PUT';
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
                    $('#outletModal').modal('hide');
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
        self.outletCode.clearError();
        self.outletName.clearError();
        //self.branchId.clearError();
    }

    self.validate = function () {
        self.outletCode.valueHasMutated();
        self.outletName.valueHasMutated();
        //self.branchId.valueHasMutated();
    }

    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end
    //-- validate if outlet name already exists
    self.isOutletExists = function () {
        var param = {
            url: '/Outlet/IsOutletExists',
            type: 'GET',
            data: { outletName: self.outletName() },
        };

        bt.ajax.exec(param, function (data) {
            self.duplicateOutlet(data.isDuplicate);
        });
    };
    //------------------------------------------ end


    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var channelId = self.channelCode();
            var regionId = self.regionCode();
            var districtId = self.districtCode();
            var branchId = self.branchId();

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
            self.strMessage(strMessage);
        }
    });
    //------------------------------------------ end
   
    getItems();
    self.getChannels();
    self.getRegions();
}

$(function () {
    viewModel.prototype = new cascadingLocator();
    ko.applyBindings(new viewModel());
});
