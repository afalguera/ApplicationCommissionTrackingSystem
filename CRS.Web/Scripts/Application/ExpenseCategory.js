var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var expenseCategoryCodeData = undefined;
    var expenseCategoryNameData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['ExpenseCategoryCode', 'ExpenseCategoryName'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Expense Category' : 'Add Expense Category';
    });

    self.id = ko.observable();
    self.expenseCategoryCode = ko.observable().extend({
        required: { message: 'Expense Category Code is required.' },
        validation: {
            message: 'Invalid Expense Category Code.',
            validator: function (val) {
                if (val == expenseCategoryCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/ExpenseCategory/IsExpenseCategoryCodeValid',
                    type: 'GET',
                    data: {
                        expenseCategoryCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.expenseCategoryName = ko.observable().extend({
        required: { message: 'Expense Category Name is required.' },
        validation: {
            message: 'Invalid Expense Category Name.',
            validator: function (val) {
                if (val == expenseCategoryNameData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/ExpenseCategory/IsExpenseCategoryNameValid',
                    type: 'GET',
                    data: {
                        expenseCategoryName: val
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
            url: '/ExpenseCategory/GetExpenseCategoryList',
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
                            ExpenseCategoryCode: item.Code,
                            ExpenseCategoryName: item.Name
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
        expenseCategoryCodeData = isEditMode ? data.ExpenseCategoryCode : undefined;
        expenseCategoryNameData = isEditMode ? data.ExpenseCategoryName : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.expenseCategoryCode(expenseCategoryCodeData);
        self.expenseCategoryName(expenseCategoryNameData);
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

    self.deleteExpenseCategory = function () {
        $.ajax({
            url: '/ExpenseCategory/DeleteExpenseCategory',
            type: 'DELETE',
            data: {
                expenseCategoryId: self.id()
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
        return self.expenseCategoryCode.isValid() &&
            self.expenseCategoryName.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();

        if (self.isValid()) {
            var options = {
                url: '/ExpenseCategory/SaveExpenseCategory',
                type: 'POST',
                data: {
                    Code: self.expenseCategoryCode(),
                    Name: self.expenseCategoryName()
                }
            }

            if (self.isEditMode()) {
                options.url = '/ExpenseCategory/UpdateExpenseCategory';
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
                    $('#expenseCategoryModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });
        }
    };

    self.unvalidate = function () {
        self.expenseCategoryCode.clearError();
        self.expenseCategoryName.clearError();
    }

    self.validate = function () {
        self.expenseCategoryCode.valueHasMutated();
        self.expenseCategoryName.valueHasMutated();
    }

    getItems();
}

$(function () {
    ko.applyBindings(new viewModel());
});