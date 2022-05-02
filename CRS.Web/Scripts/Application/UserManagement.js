var userManagementViewModel = function () {
    var userNameData = undefined;
    var regionCodeData = undefined;
    var districtCodeData = undefined;
    var branchCodeData = undefined;

    var self = this;
    self.isInit = ko.observable(true);
    self.channelList = ko.observableArray();
    self.channelCode = ko.observable();
    self.regionList = ko.observableArray();
    self.regionCode = ko.observable();
    self.roleList = ko.observableArray();
    //self.roleId = ko.observable();
    self.roleId = ko.observable();

    self.areaList = ko.observableArray();
    //self.areaCode = ko.observable();
    self.districtList = ko.observableArray();
    self.districtCode = ko.observable();
    self.branchList = ko.observableArray();
    self.branchCode = ko.observable();

    self.enableRegion = ko.observable(false);
    self.enableDistrict = ko.observable(false);
    self.enableBranch = ko.observable(false);
    self.id = ko.observable();
    self.items = ko.observableArray();

    self.lastName = ko.observable().extend({ required: { message: 'LastName is required.' } });
    self.firstName = ko.observable().extend({ required: { message: 'FirstName is required.' } });
    self.middleName = ko.observable();
    self.isAdd = ko.observable();
    self.title = ko.observable();
    self.userName = ko.observable().extend({
        required: { message: 'UserName is required.' },
        validation: {
            message: 'UserName already exists.',
            validator: function (val) {
                if (!self.isAdd()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/UserManagement/IsUserExists',
                    type: 'GET',
                    data: {
                        userName : val
                    }
                }, function (data) {
                    result = data;
                }, false);
                 return result;
            }
        }
    });

    self.email = ko.observable().extend({ required: { message: 'Email is required.' }, email: true });
    self.referrorCode = ko.observable();
    self.enableReferrorCode = ko.observable(false);
    self.duplicateUser = ko.observable();
    self.strMessage = ko.observable();

    $('body').on('show', '#confirmDeleteModal', function () {
        $(this).css({ 'width': '350px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'margin-left': -($(this).height() / 2), 'top': '0' });
    });

    self.itemColumns = ['FullName', 'UserName', 'RoleName', 'Email', 'ChannelName', 'RegionName',
    'DistrictName', 'BranchName', 'ReferrorName', 'Actions'];

    $(".colorButton").removeAttr('style').css("width", "100px");

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

    self.setId = function (data) {
        self.id(data.ID);
    }

    self.getRoles = function () {
        exec({
            url: '/Roles/GetRoleList',
            type: 'GET',
            data: {}
        }, function (data) {
            self.roleList(data);
        });
    }
    //---------------get Channel List---------
    self.getChannels = function () {
        var param = {
            url: '/CommissionDashboard/GetChannelList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.channelList(data);
         });
    };
    //------------------------- end

    //------------ get Region List ------
    self.getRegions = function () {
        var param = {
            url: '/CommissionDashboard/GetRegionList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.regionList(data);
            self.enableRegion(true);
            if (regionCodeData && !self.isAdd()) {
                self.regionCode(regionCodeData);
            }

        });
    };
    //------------------------- end

    //------------ get District List ------
    self.getDistricts = function () {
        var regionCode = (typeof self.regionCode() !== 'undefined' ? self.regionCode() : '');
        var channelCode = (typeof self.channelCode() !== 'undefined' ? self.channelCode() : '');
        var param = {
            url: '/CommissionDashboard/GetDistrictList',
            type: 'GET',
            data: {
                regionCode: regionCode,
                channelCode: channelCode
            }
        };

        bt.ajax.exec(param, function (data) {
            self.districtList(data);
            self.enableDistrict(true);
            if (districtCodeData && !self.isAdd()) {
                self.districtCode(districtCodeData);
            }
        });
    };
    //------------------------- end

    //------------delete user ------
    self.deleteUser = function () {
        exec({
            url: '/UserManagement/DeleteUser',
            type: 'DELETE',
            data: {
                userId: self.id()
            }
        }, function (data) {
            if (data) {
                self.reload();
                $('#confirmDeleteModal').modal('hide');
            } else {
                alert('Error encountered');
            }
        });
    };
    //------------------------- end

    //------------add user ------
    self.add = function () {
        self.title('Add User');
        self.isAdd(true);
        self.setFormValues({});
        self.unvalidate();

    };
    //------------------------- end

    //------------edit user ------
    self.edit = function (data) {
        self.title('Edit User');
        self.isAdd(false);
        self.id(data.ID);
        self.setFormValues(data);
    };
    //------------------------- end

   
    //--------saving----------
    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());
        if (self.isValid() && isValid) {
            var userId = self.isAdd() ? 0 : self.id();
            var options = {
                url: '/UserManagement/Maintenance',
                type: 'POST',
                data: {
                    LastName: self.lastName(),
                    FirstName: self.firstName(),
                    MiddleName: self.middleName(),
                    UserName: self.userName(),
                    Email: self.email(),
                    Role: self.roleId(),
                    Channel: self.channelCode(),
                    RegionCode: self.regionCode(),
                    DistrictCode: self.districtCode(),
                    BranchCode: self.branchCode(),
                    ReferrorCode: self.referrorCode(),
                    ID: userId
                }
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
                    self.reload();
                    $('#userMgtModal').modal('hide');
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
    }

    //------------edit user ------
    self.isValid = ko.computed(function () {
        return self.lastName.isValid()
              && self.firstName.isValid()
              && self.userName.isValid()
              && self.email.isValid();
    });
    //------------------------- end
   
    //-- search
    self.getUserList = function () {
        $.ajax({
            url: '/UserManagement/GetUserList',
            type: 'GET',
            data: {},
            beforeSend: function () {
                $.blockUI({ message: '<h4><img src="../Content/images/cube_40px.GIF" /><br/> Just a moment...</h4>' });
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
    //------------------------------------------ end
  
    //------------ get Branch List ------
    self.getBranches = function () {
        var channelCode = (typeof self.channelCode() !== 'undefined' ? self.channelCode() : '');
        var districtCode = (typeof self.districtCode() !== 'undefined' ? self.districtCode() : '');

        var param = {
            url: '/CommissionDashboard/GetBranchList',
            type: 'GET',
            data: {
                channelCode: channelCode,
                districtCode: districtCode
            }
        };

        bt.ajax.exec(param, function (data) {
            self.branchList(data);
            self.enableBranch(true);
            if (branchCodeData && !self.isAdd()) {
                self.branchCode(branchCodeData);
            }
        });
    };
    //------------------------- end


    //---- onchange selected channel -----
    self.channelCode.subscribe(function (ch) {
        var search = ch;
        self.enableReferrorCode(false);
        self.clearChannels();
        if (ch) {
            var channel = ko.utils.arrayFirst(self.channelList(), function (item) {
                return item.Code == search;
            });
            self.getRegions();
        }
    });
    //---- end ----------------------   

    //---- onchange selected region -----
    self.regionCode.subscribe(function () {
        var selectedRegion = self.regionCode();
        self.enableReferrorCode(false);
        if (typeof selectedRegion == 'undefined' || selectedRegion == '') {
            self.clearDistrict();
        } else {
            self.getDistricts();
        }

    });
    //---- end ----------------------   

    //---- onchange selected district -----
    self.districtCode.subscribe(function () {
        var selectedDistrict = self.districtCode();
        self.enableReferrorCode(false);
        if (typeof selectedDistrict == 'undefined' || selectedDistrict == '') {
            self.clearBranch();
        } else {
            self.getBranches();
        }
    });
    //---- end ---------------------- 

    //---- onchange selected branch -----
    self.branchCode.subscribe(function () {
        var selectedBranch = self.branchCode();
        if (typeof selectedBranch == 'undefined' || selectedBranch == '') {
            self.enableReferrorCode(false);
        } else {
            self.enableReferrorCode(true);
        }
    });
    //---- end ---------------------- 

    //---- unvalidate -----
    self.unvalidate = function () {
        self.lastName.clearError();
        self.firstName.clearError();
        self.userName.clearError();
        self.email.clearError();
    }
    //---- end ---------------------- 

    //---- validate -----
    self.validate = function () {
        self.lastName.valueHasMutated();
        self.firstName.valueHasMutated();
        self.userName.valueHasMutated();
        self.email.valueHasMutated();
    }
    //---- end ---------------------- 


    //----Clear Region -----
    self.clearChannels = function () {
        self.regionList([]);
        self.regionCode('');
        self.enableRegion(false);
    };
    //---- end ----------------------  

    //----Clear Region -----
    self.clearRegion = function () {
        self.clearDistrict();

    };
    //---- end ----------------------   

    //----Clear District -----
    self.clearDistrict = function () {
        self.districtList([]);
        self.districtCode('');
        self.enableDistrict(false);
        self.clearBranch();
    };
    //---- end ----------------------   

    //----Clear Branch -----
    self.clearBranch = function () {
        self.branchList([]);
        self.branchCode('');
        self.enableBranch(false);

    };
    //---- end ----------------------

    self.setFormValues = function (data) {
        var isAddMode = self.isAdd();
        regionCodeData = isAddMode ? undefined : data.RegionCode;
        districtCodeData = isAddMode ? undefined : data.DistrictCode;
        branchCodeData = isAddMode ? undefined : data.BranchCode;
        self.lastName(isAddMode ? undefined : data.LastName);
        self.firstName(isAddMode ? undefined : data.FirstName);
        self.middleName(isAddMode ? undefined : data.MiddleName);
        self.userName(isAddMode ? undefined : data.UserName);
        self.email(isAddMode ? undefined : data.Email);
        self.roleId(isAddMode ? undefined : data.Role);
        self.channelCode(isAddMode ? undefined : data.Channel);
        self.regionCode(regionCodeData);
        self.districtCode(districtCodeData);
        self.branchCode(branchCodeData);
        self.referrorCode(isAddMode ? undefined : data.ReferrorCode);
    };

    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var roleID = self.roleId();
       
            if (typeof roleID == 'undefined' || roleID == '') {
                strMessage = strMessage + "Role is required.";
            }
            self.strMessage(strMessage);
        }
    });

    self.reload = function () {
        self.getUserList();
    };

    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    self.getUserList();
    self.getChannels();
    self.getRegions();
    self.getRoles();
};

$(function () {
    ko.applyBindings(new userManagementViewModel());
});