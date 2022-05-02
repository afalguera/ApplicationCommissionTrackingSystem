var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var regionCodeData = undefined;
    var regionNameData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['RegionCode', 'RegionName'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Region' : 'Add Region';
    });

    self.id = ko.observable();
    self.regionCode = ko.observable().extend({
        required: { message: 'Region Code is required.' },
        validation: {
            message: 'Invalid Region Code.',
            validator: function (val) {
                if (val == regionCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Region/IsRegionCodeValid',
                    type: 'GET',
                    data: {
                        regionCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.regionName = ko.observable().extend({
        required: { message: 'Region Name is required.' },
        validation: {
            message: 'Invalid Region Name.',
            validator: function (val) {
                if (val == regionNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Region/IsRegionNameValid',
                    type: 'GET',
                    data: {
                        regionName: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });

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
            url: '/Region/GetRegionList',
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
                        RegionCode: item.Code,
                        RegionName: item.Name
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
        regionCodeData = isEditMode ? data.RegionCode : undefined;
        regionNameData = isEditMode ? data.RegionName : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.regionCode(regionCodeData);
        self.regionName(regionNameData);
    }

    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.unvalidate();
    }

    self.edit = function (data) {
        self.mode(modes.Edit);
        setFormData(data);
    }
    //--delete function (begin) ---------------------------------------------------------//
    self.deleteRegion = function () {
        $.ajax({
            url: '/Region/DeleteRegion',
            type: 'DELETE',
            data: {
                regionId: self.id()
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
        return self.regionCode.isValid() &&
            self.regionName.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();

        if (self.isValid()) {
            var options = {
                url: '/Region/SaveRegion',
                type: 'POST',
                data: {
                    Code: self.regionCode(),
                    Name: self.regionName()
                }
            }

            if (self.isEditMode()) {
                options.url = '/Region/EditRegion';
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
                    $('#regionModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });
        }
    };

    self.unvalidate = function () {
        self.regionCode.clearError();
        self.regionName.clearError();
    }

    self.validate = function () {
        self.regionCode.valueHasMutated();
        self.regionName.valueHasMutated();
    }

    getItems();
}

$(function () {
    ko.applyBindings(new viewModel());
});