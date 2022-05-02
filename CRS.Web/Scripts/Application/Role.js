var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    //var regionCodeData = undefined;
    var roleNameData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['ID', 'Name'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Role' : 'Add Role';
    });

    self.id = ko.observable();
    self.duplicateRole = ko.observable();
  
    self.roleName = ko.observable().extend({
        required: { message: 'Role Name is required.' },
        validation: {
            message: 'Role Name already exists.',
            validator: function (val) {
                if (val == roleNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                self.isRoleExists();
                if (self.duplicateRole()) {
                    result = false;
                } else {
                    result = true;
                }
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

    function getItems() {
        //exec({
        //    url: '/Roles/GetRoleList',
        //    type: 'GET',
        //}, function (data) {
        //    var mappedData = $.map(data, function (item) {
        //        return result = {
        //            Id: item.ID,
        //            RegionCode: item.Code,
        //            RegionName: item.Name
        //        }
        //    });

        //    self.items(mappedData);
        //});
        $.ajax({
            url: '/Roles/GetRoleList',
            type: 'GET',
            data: {},
            beforeSend: function () {
                $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });
            },
            complete: function () {
                $.unblockUI();
            },
            success: function (data) {
                self.items(data);
            },
            error: function () {
                $.unblockUI();
            }
        });
    };

    function setFormData(data) {
        var isEditMode = self.isEditMode();
        //regionCodeData = isEditMode ? data.RegionCode : undefined;
        roleNameData = isEditMode ? data.Name : undefined;
        self.id(isEditMode ? data.ID : undefined);
        ////self.regionCode(regionCodeData);
        self.roleName(roleNameData);
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

    self.deleteRole = function () {
        $.ajax({
            url: '/Roles/DeleteRole',
            type: 'DELETE',
            data: {
                roleId: self.id()
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
        //exec({
        //    url: '/Roles/DeleteRole',
        //    type: 'DELETE',
        //    data: {
        //        roleId: self.id()
        //    }
        //}, function (data) {
        //    if (data) {
        //        getItems();
        //        $('#confirmDeleteModal').modal('hide');
        //    } else {
        //        alert('Error encountered');
        //    }
        //});
    }

    self.isValid = ko.computed(function () {
        //return self.regionCode.isValid() &&
        return self.roleName.isValid();
    });

    self.setId = function (data) {
        self.id(data.ID);
    }

    self.save = function () {
        self.validate();

        if (self.isValid()) {
            var options = {
                url: '/Roles/SaveRole',
                type: 'POST',
                data: {
                    Name: self.roleName()
                }
            }

            if (self.isEditMode()) {
                options.url = '/Roles/EditRole';
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
                    $('#roleModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });

            //exec(options, function (result) {
            //    if (result) {
            //        getItems();
            //        $('#regionModal').modal('hide');
            //    } else {
            //        alert('Error encountered');
            //    }
            //});
        }
    }

    self.unvalidate = function () {
        //self.regionCode.clearError();
        self.roleName.clearError();
    }

    self.validate = function () {
        //self.regionCode.valueHasMutated();
        self.roleName.valueHasMutated();
    }


    //-- validate if referror
    self.isRoleExists = function () {
        var param = {
            url: '/Roles/IsRoleExists',
            type: 'GET',
            data: { roleName: self.roleName() },
        };

        bt.ajax.exec(param, function (data) {
            self.duplicateRole(data.isDuplicate);
        });
    };
    //------------------------------------------ end

    getItems();
}

$(function () {
    ko.applyBindings(new viewModel());
});