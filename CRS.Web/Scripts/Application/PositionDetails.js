var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    //var positionDetailsCodeData = undefined;
    var positionDetailsNameData = undefined;
    var positionNameData = undefined;
    var positionCodeData = undefined;
    var positionTypeNameData = undefined;
    var positionTypeCodeData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.positionList = ko.observableArray([]);
    self.positionTypeList = ko.observableArray([]);
    self.selectedPosition = ko.observable();
    self.enablePosition = ko.observable();
    self.selectedPositionType = ko.observable();
    self.enablePositionType = ko.observable();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['Name', 'PositionName', 'PositionTypeName'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Position Details' : 'Add Position Details';
    });

    self.id = ko.observable();
    
    self.positionDetailsName = ko.observable().extend({
        required: { message: 'Position Details Name is required.' },
        validation: {
            message: 'Invalid Position Details Name.',
            validator: function (val) {
                if (val == positionDetailsNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/PositionDetails/IsPositionDetailsNameValid',
                    type: 'GET',
                    data: {
                        positionDetailsName: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.positionName = ko.observable();
    self.positionCode = ko.observable();
    self.positionTypeName = ko.observable();
    self.positionTypeCode = ko.observable();
    self.strMessage = ko.observable();

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
            url: '/PositionDetails/GetPositionDetailsList',
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
                        PositionName: item.PositionName,
                        PositionTypeCode: item.PositionType,
                        PositionTypeName: item.PositionTypeName,
                        Name: item.Name
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
        positionDetailsNameData = isEditMode ? data.Name : undefined;
        positionCodeData = isEditMode ? data.PositionCode : undefined;
        positionNameData = isEditMode ? data.PositionName : undefined;
        positionTypeNameData = isEditMode ? data.PositionTypeName : undefined;
        positionTypeCodeData = isEditMode ? data.PositionTypeCode : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.positionName(positionNameData);
        self.positionCode(positionCodeData);
        self.positionTypeName(positionTypeNameData);
        self.positionTypeCode(positionTypeCodeData);
        self.positionDetailsName(positionDetailsNameData);
    }

    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.unvalidate();
        self.getPositions();
        self.getPositionTypes();
    }

    self.edit = function (data) {
        self.mode(modes.Edit);
        setFormData(data);
        self.getPositions();
        self.getPositionTypes();
    }

    self.deletePositionDetails = function () {
        $.ajax({
            url: '/PositionDetails/DeletePositionDetails',
            type: 'DELETE',
            data: {
                positionDetailsId: self.id()
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
        return self.positionDetailsName.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());

        if (self.isValid() && isValid) {
         
            var options = {
                url: '/PositionDetails/SavePositionDetails',
                type: 'POST',
                data: {
                    Name: self.positionDetailsName(),
                    PositionCode: self.positionCode(),
                    PositionType: self.positionTypeCode()
                }
            }

            if (self.isEditMode()) {
                options.url = '/PositionDetails/UpdatePositionDetails';
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
                    $('#positionDetailsModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });
        }
        else {
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
        self.positionDetailsName.clearError();
    }

    self.validate = function () {
        self.positionDetailsName.valueHasMutated();
    }

    getItems();

    //------------ get Position List ------
    self.getPositions = function () {
        var param = {
            url: '/PositionDetails/GetPositionList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.positionList(data);
            if (data.length == 1) {
                self.selectedPosition(data[0].Code);
                self.enablePosition(false);
            } else {
                self.enablePosition(true);
            }
        });
    };
    //------------------------- end

    //------------ get Position Type List ------
    self.getPositionTypes = function () {
        var param = {
            url: '/PositionDetails/GetPositionTypeList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.positionTypeList(data);
            if (data.length == 1) {
                self.selectedPositionType(data[0].Code);
                self.enablePositionType(false);
            } else {
                self.enablePositionType(true);
            }
        });
    };
    //------------------------- end
    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var positionCode = self.positionCode();
            var positionTypeCode = self.positionTypeCode();

            if (typeof positionCode == 'undefined' || positionCode == '') {
                strMessage = strMessage + "Position Code is required." + '<br/>';
            }

            if (typeof positionTypeCode == 'undefined' || positionTypeCode == '') {
                strMessage = strMessage + "Position Type is required." + '<br/>';
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

    self.getPositions();
    self.getPositionTypes();
}

$(function () {
    ko.applyBindings(new viewModel());
});