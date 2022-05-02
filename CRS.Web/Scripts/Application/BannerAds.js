var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['ImageName', 'ImagePath', 'TargetUrl', 'RoleName', 'PeriodFromString', 'PeriodToString'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Banner Ads' : 'Add Banner Ads';
    });

    self.id = ko.observable();
    self.imageName = ko.observable().extend({ required: { message: 'Name is required.' } });
    self.imagePath = ko.observable().extend({ required: { message: 'Path is required.' } });
    //self.targetUrl = ko.observable().extend({ required: { message: 'Url is required.' } });
    //required: { message: 'Url is required.' },
    self.targetUrl = ko.observable().extend({
            validation: {
            message: 'Invalid url.',
            validator: function (val) {
                if (val == '' || typeof val == 'undefined') {
                    return true;
                }
                var isUrl = self.checkUrl(val) ? true : false;
                return isUrl;
            }
        }
    });
    self.selectedRole = ko.observable();
    self.roleList = ko.observableArray();
    self.startDate = ko.observable().extend({
        required: { message: 'Start Date is required.' },
        validation: {
            message: 'Invalid date.',
            validator: function (val) {
                var isValid = isDate(val) ? true : false;
                return isValid;
            }
        }
    });
    //self.endDate = ko.observable().extend({ required: { message: 'End Date is required.' } });
    self.endDate = ko.observable().extend({
        required: { message: 'End Date is required.' },
        validation: {
            message: 'Invalid date.',
            validator: function (val) {
                var isValid = isDate(val) ? true : false;
                return isValid;
            },
           
            //message: 'Must be greater than Start Date.',
            //validator: function (val) {
            //    if (self.startDate() == undefined || self.startDate() == '') {
            //        return true;
            //    }
            //    return Date.parse(self.startDate()) < Date.parse(val);
            //}
        }
      
    });
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

    function getItems() {

        $.ajax({
            url: '/CMSBannerAdsImage/GetCMSBannerAds',
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
        self.id(isEditMode ? data.ID : undefined);
        self.imageName(isEditMode ? data.ImageName : undefined);
        self.imagePath(isEditMode ? data.ImagePath : undefined);
        self.targetUrl(isEditMode ? data.TargetUrl : undefined);
        self.selectedRole(isEditMode ? data.RoleId : undefined);
        self.startDate(isEditMode ? data.PeriodFromString : undefined);
        self.endDate(isEditMode ? data.PeriodToString : undefined);
        $("#txtPath").val(isEditMode ? data.ImagePath : undefined);
     
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

    self.deleteBannerAds = function () {
        $.ajax({
            url: '/CMSBannerAdsImage/DeleteCMSBannerAds',
            type: 'DELETE',
            data: {
                bannerAdsId: self.id()
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
        return self.imageName.isValid()
             && self.imagePath.isValid()
             && self.targetUrl.isValid()
             && self.startDate.isValid()
             && self.endDate.isValid();
      });

    self.setId = function (data) {
        self.id(data.ID);
    }

    self.save = function () {
        self.validate();
        var vlid = self.isValid();
        var isValid = isWhitespaceNotEmpty(self.strMessage());
        if (self.isValid() && isValid) {
            var options = {
                url: '/CMSBannerAdsImage/SaveCMSBannerAd',
                type: 'POST',
                data: {
                    ImageName: self.imageName(),
                    ImagePath: self.imagePath(),
                    TargetUrl: self.targetUrl(),
                    RoleId: self.selectedRole(),
                    PeriodFrom: self.startDate(),
                    PeriodTo: self.endDate(),
                 
                }
            }

            if (self.isEditMode()) {
                options.url = '/CMSBannerAdsImage/UpdateCMSBannerAds';
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
                    $('#bannerAdsModal').modal('hide');
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
    };

    self.unvalidate = function () {
        self.imageName.clearError();
        self.imagePath.clearError();
        self.targetUrl.clearError();
        self.startDate.clearError();
        self.endDate.clearError();
    }

    self.validate = function () {
        self.imageName.valueHasMutated();
        self.imagePath.valueHasMutated();
        self.targetUrl.valueHasMutated();
        self.startDate.valueHasMutated();
        self.endDate.valueHasMutated();
    }

    function getRoles() {
        exec({
            url: '/Roles/GetRoleList',
            type: 'GET',
            data: {}
        }, function (data) {
            self.roleList(data);
        });
    };

    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var roleID = self.selectedRole();
            var strPath = $("#txtPath").val();
            if (typeof roleID == 'undefined' || roleID == '') {
                strMessage = strMessage + "Role is required." +  "<br/>";
            }
            if (typeof strPath == 'undefined' || strPath == '') {
                strMessage = strMessage + "Path is required." + "<br/>";
            }

            if(Date.parse(self.startDate()) > Date.parse(self.endDate())){
	             strMessage = strMessage + "End Date should be greater than Start Date.";
            }
            self.strMessage(strMessage);
        }
    });

    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    getItems();
    //self.getRoles();
    getRoles();

    self.checkUrl = function (url) {
        return url.match(/(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?/);
    };

    function isDate(txtDate, separator) {
        var aoDate,           // needed for creating array and object
            ms,               // date in milliseconds
            month, day, year; // (integer) month, day and year
        // if separator is not defined then set '/'
        if (separator === undefined) {
            separator = '/';
        }
        // split input date to month, day and year
        aoDate = txtDate.split(separator);
        // array length should be exactly 3 (no more no less)
        if (aoDate.length !== 3) {
            return false;
        }
        // define month, day and year from array (expected format is m/d/yyyy)
        // subtraction will cast variables to integer implicitly
        month = aoDate[0] - 1; // because months in JS start from 0
        day = aoDate[1] - 0;
        year = aoDate[2] - 0;
        // test year range
        if (year < 1000 || year > 3000) {
            return false;
        }
        // convert input date to milliseconds
        ms = (new Date(year, month, day)).getTime();
        // initialize Date() object from milliseconds (reuse aoDate variable)
        aoDate = new Date();
        aoDate.setTime(ms);
        // compare input date and parts from Date() object
        // if difference exists then input date is not valid
        if (aoDate.getFullYear() !== year ||
            aoDate.getMonth() !== month ||
            aoDate.getDate() !== day) {
            return false;
        }
        // date is OK, return true
        return true;
    }

    $("#Dialog-Box").dialog({
        resizable: false,
        height: 800,
        width: 1280,
        modal: true,
        autoOpen: false,
        overlay: { backgroundColor: "#000", opacity: 2.5 },
        buttons: {
            "Select Image": function () {
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });

    $('#btnImageSelector').click(function () {
        $('#bannerAdsModal').modal('hide');
        $('#Dialog-Box').dialog('open');
        return false;
       
    });

    $(".galImg").click(function () {
            var image = $(this).attr("rel");
            $('#feature').fadeOut('slow', function () {
                $('#feature').html('<img src="' + image + '"/>');
                $('#feature').fadeIn('slow');
                //$("#ImagePath").val(image)
                $("#txtPath").val(image);
                self.imagePath(image);
                $('#Dialog-Box').dialog('close');
                $('#bannerAdsModal').modal('show');
                   return false;
        });
    });

    //self.OpenDialog() = function() {
    //    $('#Dialog-Box').dialog('open');
    //}
}

$(function () {
    ko.applyBindings(new viewModel());
    $("#dtStartDate").mask("99/99/9999");
    //$("#dtStartDate").datepicker("setDate", -1);
    $("#dtEndDate").mask("99/99/9999");
    //$("#dtEndDate").datepicker("setDate", -1);
    //$(".galImg").click(function () {
    //    var image = $(this).attr("rel");
    //    $('#feature').fadeOut('slow', function () {
    //        $('#feature').html('<img src="' + image + '"/>');
    //        $('#feature').fadeIn('slow');
    //        //$("#ImagePath").val(image)
    //        $("#txtPath").val(image);
    //        $('#Dialog-Box').dialog('close');
    //        $('#bannerAdsModal').modal('show');
    //           return false;
    //    });
    //});

    var thumbs = $(".thumb");
    $.each(thumbs, function (i, val) {
        var img = new Image();
        img.src = $(this).attr("src");
        var width = img.width;
        var height = img.height;
        var dimension = width + 'x' + height;
        $(this).attr('title', dimension);
    });

  });