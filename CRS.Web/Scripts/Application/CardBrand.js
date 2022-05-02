var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var cardBrandCodeData = undefined;
    var cardBrandNameData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['CardBrandCode', 'CardBrandName'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Card Brand' : 'Add Card Brand';
    });

    self.id = ko.observable();
    self.cardBrandCode = ko.observable().extend({
        required: { message: 'Card Brand Code is required.' },
        validation: {
            message: 'Invalid Card Brand Code.',
            validator: function (val) {
                if (val == cardBrandCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/CardBrand/IsCardBrandCodeValid',
                    type: 'GET',
                    data: {
                        cardBrandCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.cardBrandName = ko.observable().extend({
        required: { message: 'Card Brand Name is required.' },
        validation: {
            message: 'Invalid Card Brand Name.',
            validator: function (val) {
                if (val == cardBrandNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/CardBrand/IsCardBrandNameValid',
                    type: 'GET',
                    data: {
                        cardBrandName: val
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
            url: '/CardBrand/GetCardBrandList',
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
                        CardBrandCode: item.Code,
                        CardBrandName: item.Name
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
        cardBrandCodeData = isEditMode ? data.CardBrandCode : undefined;
        cardBrandNameData = isEditMode ? data.CardBrandName : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.cardBrandCode(cardBrandCodeData);
        self.cardBrandName(cardBrandNameData);
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
    self.deleteCardBrand = function () {
        $.ajax({
            url: '/CardBrand/DeleteCardBrand',
            type: 'DELETE',
            data: {
                cardBrandId: self.id()
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
    //self.deleteCardBrand = function () {
        //exec({
        //    url: '/CardBrand/DeleteCardBrand',
        //    type: 'DELETE',
        //    data: {
        //        cardBrandId: self.id()
        //    }
        //}, function (data) {
        //    if (data) {
        //        getItems();
        //        $('#confirmDeleteModal').modal('hide');
        //    } else {
        //        alert('Error encountered');
        //    }
        //});
    //};

    self.isValid = ko.computed(function () {
        return self.cardBrandCode.isValid() &&
            self.cardBrandName.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    //--save function (begin) ---------------------------------------------------------//
    self.save = function () {
        self.validate();

        if (self.isValid()) {
            var options = {
                url: '/CardBrand/SaveCardBrand',
                type: 'POST',
                data: {
                    Code: self.cardBrandCode(),
                    Name: self.cardBrandName()
                }
            }

            if (self.isEditMode()) {
                options.url = '/CardBrand/UpdateCardBrand';
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
                    $('#cardBrandModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });
        }
    };

    self.unvalidate = function () {
        self.cardBrandCode.clearError();
        self.cardBrandName.clearError();
    }

    self.validate = function () {
        self.cardBrandCode.valueHasMutated();
        self.cardBrandName.valueHasMutated();
    }

    getItems();
}

$(function () {
    ko.applyBindings(new viewModel());
});