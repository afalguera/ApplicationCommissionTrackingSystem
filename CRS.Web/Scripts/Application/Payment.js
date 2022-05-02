var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var paymentCodeData = undefined;
    var paymentNameData = undefined;
    var tinData = undefined;
    var isVatableData = undefined;
    var isGrossData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['PaymentCode', 'PaymentName', 'Tin', 'IsVatable', 'IsGross'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Payment' : 'Add Payment';
    });

    self.id = ko.observable();
    self.paymentCode = ko.observable().extend({
        required: { message: 'Payment Code is required.' },
        validation: {
            message: 'Invalid Payment Code.',
            validator: function (val) {
                if (val == paymentCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Payment/IsPaymentCodeValid',
                    type: 'GET',
                    data: {
                        paymentCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.paymentName = ko.observable().extend({
        required: { message: 'Payment Name is required.' },
        validation: {
            message: 'Invalid Payment Name.',
            validator: function (val) {
                if (val == paymentNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Payment/IsPaymentNameValid',
                    type: 'GET',
                    data: {
                        paymentName: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });


    self.tin = ko.observable().extend({ required: { message: 'TIN is required.' } });
    self.isVatable = ko.observable();
    self.isGross = ko.observable();

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
            url: '/Payment/GetPaymentList',
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
                        PaymentCode: item.Code,
                        PaymentName: item.Name,
                        Tin: item.Tin,
                        IsVatable: item.IsVatable,
                        IsGross: item.IsGross
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
        paymentCodeData = isEditMode ? data.PaymentCode : undefined;
        paymentNameData = isEditMode ? data.PaymentName : undefined;
        tinData = isEditMode ? data.Tin : undefined;
        isVatableData = isEditMode ? data.IsVatable : undefined;
        isGrossData = isEditMode ? data.IsGross : undefined;

        self.id(isEditMode ? data.Id : undefined);
        self.paymentCode(paymentCodeData);
        self.paymentName(paymentNameData);
        self.tin(tinData);
        self.isVatable(isVatableData);
        self.isGross(isGrossData);
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

    self.deletePayment = function () {
        $.ajax({
            url: '/Payment/DeletePayment',
            type: 'DELETE',
            data: {
                paymentId: self.id()
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
        return self.paymentCode.isValid() &&
            self.paymentName.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    //--save function (begin) ---------------------------------------------------------//
    self.save = function () {
        self.validate();

        if (self.isValid()) {
            var options = {
                url: '/Payment/SavePayment',
                type: 'POST',
                data: {
                    code: self.paymentCode(),
                    name: self.paymentName(),
                    tin: self.tin(),
                    isVatable: self.isVatable(),
                    isGross: self.isGross()
                }
            }

            if (self.isEditMode()) {
                    options.url = '/Payment/UpdatePayment';
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
                    $('#paymentModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });
        }
    };

    self.unvalidate = function () {
        self.paymentCode.clearError();
        self.paymentName.clearError();
    }

    self.validate = function () {
        self.paymentCode.valueHasMutated();
        self.paymentName.valueHasMutated();
    }

    getItems();
}

$(function () {
    ko.applyBindings(new viewModel());
});