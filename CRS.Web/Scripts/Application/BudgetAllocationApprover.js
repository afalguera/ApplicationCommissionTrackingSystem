var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var budgetAllocationApproverCodeData = undefined;
    var approverTitleData = undefined;
    var approverNameData = undefined;
    var approverAmountLowerData = undefined;
    var approverAmountUpperData = undefined;
    var remarksData = undefined;


    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['BudgetAllocationApproverCode', 'ApproverTitle', 'ApproverName', 'ApproverAmountLower', 'ApproverAmountUpper', 'Remarks'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Budget Allocation Approver' : 'Add Budget Allocation Approver';
    });

    self.id = ko.observable();
    self.budgetAllocationApproverCode = ko.observable().extend({
        required: { message: 'Budget Allocation Approver Code is required.' },
        validation: {
            message: 'Invalid Budget Allocation Approver Code.',
            validator: function (val) {
                if (val == budgetAllocationApproverCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/BudgetAllocationApprover/IsBudgetAllocationApproverCodeValid',
                    type: 'GET',
                    data: {
                        budgetAllocationApproverCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.approverTitle = ko.observable().extend({
        required: { message: 'Approver Title is required.' },
        validation: {
            message: 'Invalid Approver Title.',
            validator: function (val) {
                if (val == approverTitleData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/BudgetAllocationApprover/IsBudgetAllocationApproverTitleValid',
                    type: 'GET',
                    data: {
                        budgetAllocationApproverTitle: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });

    self.approverName = ko.observable();
    //self.approverTitle = ko.observable().extend({ required: { message: 'Approver Title is required.' } });
    self.approverAmountLower = ko.observable().extend({ required: { message: 'Amount Lower is required.' } });
    //self.approverAmountUpper = ko.observable().extend({ required: { message: 'Amount Upper is required.' } });
    self.approverAmountUpper = ko.observable();
    self.remarks = ko.observable();

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
            url: '/BudgetAllocationApprover/GetBudgetAllocationApproverList',
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
                            BudgetAllocationApproverCode: item.Code,
                            ApproverTitle: item.ApproverTitle,
                            ApproverName: item.ApproverName,
                            ApproverAmountLower: item.ApproverAmountLower > 0 ? formatCurrency(new Number(item.ApproverAmountLower).toFixed(2)) : '',
                            ApproverAmountUpper: item.ApproverAmountUpper > 0 ? formatCurrency(new Number(item.ApproverAmountUpper).toFixed(2)) : '',
                            Remarks: item.Remarks
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
    
    //--Populate fields (begin) ---------------------------------------------------------//
    function setFormData(data) {
        var isEditMode = self.isEditMode();
        budgetAllocationApproverCodeData = isEditMode ? data.BudgetAllocationApproverCode : undefined;
        approverTitleData = isEditMode ? data.ApproverTitle : undefined;
        approverNameData = isEditMode ? data.ApproverName : undefined;
        approverAmountLowerData = isEditMode ? data.ApproverAmountLower : undefined;
        approverAmountUpperData = isEditMode ? data.ApproverAmountUpper : undefined;
        remarksData = isEditMode ? data.Remarks : undefined;

        self.id(isEditMode ? data.Id : undefined);
        self.budgetAllocationApproverCode(budgetAllocationApproverCodeData);
        self.approverTitle(approverTitleData);
        self.approverName(approverNameData);
        self.approverAmountLower(approverAmountLowerData);
        self.approverAmountUpper(approverAmountUpperData);
        self.remarks(remarksData);
    };
    //--Populate fields (end) ---------------------------------------------------------//

    //--add function (begin) ---------------------------------------------------------//
    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.unvalidate();
    }
    //--add function (end) ---------------------------------------------------------//

    //--edit function (begin) ---------------------------------------------------------//
    self.edit = function (data) {
        self.mode(modes.Edit);
        setFormData(data);
    }
    //--edit function (end) ---------------------------------------------------------//

    //--delete function (begin) ---------------------------------------------------------//
    self.deleteBudgetAllocationApprover = function () {
        $.ajax({
            url: '/BudgetAllocationApprover/DeleteBudgetAllocationApprover',
            type: 'DELETE',
            data: {
                budgetAllocationApproverId: self.id()
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

    //--validate function (begin) ---------------------------------------------------------//
    self.isValid = ko.computed(function () {
        return self.budgetAllocationApproverCode.isValid()
            && self.approverTitle.isValid()
            && self.approverAmountLower.isValid()
            //&& self.approverAmountUpper.isValid()
        ;
    });
    //--validate function (end) ---------------------------------------------------------//

    //--set Id function (begin) ---------------------------------------------------------//
    self.setId = function (data) {
        self.id(data.Id);
    }
    //--set Id function (end) ---------------------------------------------------------//


    //--save function (begin) ---------------------------------------------------------//
    self.save = function () {
        self.validate();
        
        if (self.isValid()) {
            var options = {
                url: '/BudgetAllocationApprover/SaveBudgetAllocationApprover',
                type: 'POST',
                data: {
                    code: self.budgetAllocationApproverCode(),
                    approverTitle: self.approverTitle(),
                    approverName: self.approverName(),
                    approverAmountLower: self.approverAmountLower(),
                    approverAmountUpper: self.approverAmountUpper(),
                    remarks: self.remarks()
                }
            }

            if (self.isEditMode()) {
                options.url = '/BudgetAllocationApprover/UpdateBudgetAllocationApprover';
                options.type = 'PUT';
                options.data.id = self.id();
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
                    $('#budgetAllocationApproverModal').modal('hide');
                },
                error: function () {
                    alert('Error encountered');
                    $.unblockUI();
                }
            });

        }
    }
    //--save function (end) ---------------------------------------------------------//

    //--ko unvalidate (begin) ---------------------------------------------------------//
    self.unvalidate = function () {
        self.budgetAllocationApproverCode.clearError();
        self.approverTitle.clearError();
        self.approverAmountLower.clearError();
        //self.approverAmountUpper.clearError()
    }
    //--ko unvalidate (end) ---------------------------------------------------------//

    //--ko validate (begin) ---------------------------------------------------------//
    self.validate = function () {
        self.budgetAllocationApproverCode.valueHasMutated();
        self.approverTitle.valueHasMutated();
        self.approverAmountLower.valueHasMutated();
        //self.approverAmountUpper.valueHasMutated();
    }
    //--ko validate (end) ---------------------------------------------------------//

    //--format currency (begin) ---------------------------------------------------------//
    function formatCurrency(value) {
        var result = '';
        var decimalPlace = value.substring(value.indexOf('.'));
        var valueLength = value.length - decimalPlace.length;
        for (ctr = valueLength - 1; ctr >= 0; ctr--) {
            result = value[ctr] + result;
            if (ctr != 0 && (valueLength - ctr) % 3 == 0) {
                result = ',' + result;
            }
        }
        return result + decimalPlace;
    }
    //--format currency (end) ---------------------------------------------------------//

    //--Initialize (begin) ---------------------------------------------------------//
    getItems();
    //--Initialize (end) ---------------------------------------------------------//
}

$(function () {
    $('#txtApproverAmountLower').autoNumeric('init', { vMax: '9999999999.99' });
    $('#txtApproverAmountUpper').autoNumeric('init', { vMax: '9999999999.99' });
    ko.applyBindings(new viewModel());
});