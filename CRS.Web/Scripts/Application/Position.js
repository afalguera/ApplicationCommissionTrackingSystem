var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var positionCodeData = undefined;
    var positionNameData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['PositionCode', 'PositionName'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Position' : 'Add Position';
    });

    self.id = ko.observable();
    self.positionCode = ko.observable().extend({
        required: { message: 'Position Code is required.' },
        validation: {
            message: 'Invalid Position Code.',
            validator: function (val) {
                if (val == positionCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Position/IsPositionCodeValid',
                    type: 'GET',
                    data: {
                        positionCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.positionName = ko.observable().extend({
        required: { message: 'Position Name is required.' },
        validation: {
            message: 'Invalid Position Name.',
            validator: function (val) {
                if (val == positionNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Position/IsPositionNameValid',
                    type: 'GET',
                    data: {
                        positionName: val
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
            url: '/Position/GetPositionList',
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
                        PositionCode: item.Code,
                        PositionName: item.Name
                    }
                });
                self.items(mappedData);
            },
            error: function () {
                $.unblockUI();
            }
        });
    };
    
    function setFormData(data) {
        var isEditMode = self.isEditMode();
        positionCodeData = isEditMode ? data.PositionCode : undefined;
        positionNameData = isEditMode ? data.PositionName : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.positionCode(positionCodeData);
        self.positionName(positionNameData);
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

    self.deletePosition = function () {
        $.ajax({
            url: '/Position/DeletePosition',
            type: 'DELETE',
            data: {
                positionId: self.id()
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

    self.isValid = ko.computed(function () {
        return self.positionCode.isValid() &&
            self.positionName.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();

        if (self.isValid()) {
            var options = {
                url: '/Position/SavePosition',
                type: 'POST',
                data: {
                    Code: self.positionCode(),
                    Name: self.positionName()
                }
            }

            if (self.isEditMode()) {
                options.url = '/Position/UpdatePosition';
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
                    $('#positionModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });
        }
    };

    self.unvalidate = function () {
        self.positionCode.clearError();
        self.positionName.clearError();
    }

    self.validate = function () {
        self.positionCode.valueHasMutated();
        self.positionName.valueHasMutated();
    }

    getItems();
}

$(function () {
    ko.applyBindings(new viewModel());
});