var employeeSelector = function () {
    var self = this;
    self.employeeSelected = ko.observable();
    self.employeeSelectedDisplay = ko.computed(function () {
        if (self.employeeSelected() != undefined) {
            return (self.employeeSelected().Name != undefined && self.employeeSelected().Name != '') ? self.employeeSelected().Name + 
                ((self.employeeSelected().EmployeeNumber != undefined && self.employeeSelected().EmployeeNumber != '') ?
                ' - ' + self.employeeSelected().EmployeeNumber : '') : 'Choose';
        }
        return 'Choose';
    });
    self.employeeSelectorList = ko.observableArray();
    self.employeeSelectorColumns = ['Actions', 'BranchName', 'EmployeeNumber', 'LastName', 'FirstName', 'MiddleName'];

    self.cancelEmployeeSelection = function () {
        self.employeeSelected(undefined);
    }

    self.selectEmployee = function (data) {
        self.employeeSelected(data);
    }

    function getItems() {
        $.ajax({
            async: false,
            dataType: 'json',
            contentType: 'application/json; charset=utf-8',
            beforeSend: $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' }),
            url: '/Employee/GetEmployeeList',
            type: 'GET'
        }).done(function (data) {
                var mappedData = $.map(data, function (item) {
                    return result = {
                        Id: item.ID,
                        BranchId: item.BranchId,
                        BranchName: item.BranchName,
                        EmployeeNumber: item.EmployeeNumber,
                        Name: item.Name,
                        LastName: item.LastName,
                        FirstName: item.FirstName,
                        MiddleName: item.MiddleName,
                        IsActive: item.IsActive,
                        Actions: item.ID
                    }
                });

                self.employeeSelectorList(mappedData);
            }).always(function() {
                $.unblockUI();
            });
    }

    getItems();
}