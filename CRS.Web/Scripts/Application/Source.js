var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var sourceCodeData = undefined;
    var sourceNameData = undefined;
    var channelNameData = undefined;
    var channelCodeData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.channelList = ko.observableArray([]);
    self.selectedChannel = ko.observable();
    self.enableChannel = ko.observable();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['SourceCode', 'SourceName', 'ChannelName',  'IsForCommission', 'ChannelCode'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit source' : 'Add source';
    });

    self.id = ko.observable();
    self.sourceCode = ko.observable().extend({
        required: { message: 'Source Code is required.' },
        validation: {
            message: 'Source Code already exists.',
            validator: function (val) {
                if (val == sourceCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/Source/IsSourceCodeValid',
                    type: 'GET',
                    data: {
                        sourceCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.sourceName = ko.observable();
    self.channelName = ko.observable();
    self.channelCode = ko.observable();
    self.strMessage = ko.observable();
    self.IsForCommission = ko.observable();

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
            url: '/Source/GetSourceList',
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
                        SourceCode: item.Code,
                        SourceName: item.Name,
                        ChannelCode: item.ChannelCode,
                        ChannelName: item.ChannelName,
                        IsForCommission: item.IsForCommission
                    }
                });

                self.items(mappedData);
            },
            error: function () {
                $.unblockUI();
            }
        });
    }

    function setFormData(data) {
        var isEditMode = self.isEditMode();
        sourceCodeData = isEditMode ? data.SourceCode : undefined;
        sourceNameData = isEditMode ? data.SourceName : '';
        channelCodeData = isEditMode ? data.ChannelCode : undefined;
        channelNameData = isEditMode ? data.ChannelName : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.sourceCode(sourceCodeData);
        self.sourceName(sourceNameData);
        self.selectedChannel(channelCodeData);
        self.channelName(channelNameData);
        self.IsForCommission(isEditMode ? data.IsForCommission : undefined);
    }

    self.add = function () {
        self.mode(modes.Add);
        setFormData({});
        self.unvalidate();
        self.getChannels();
    }

    self.edit = function (data) {
        self.mode(modes.Edit);
        setFormData(data);
        self.getChannels();
    }

    self.deleteSource = function () {
        $.ajax({
            url: '/Source/DeleteSource',
            type: 'DELETE',
            data: {
                sourceId: self.id()
            },
            beforeSend: function () {
                $.blockUI({ baseZ: 2000, message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });
            },
            complete: function () {
                $.unblockUI();
            },
            success: function (data) {
                $('#confirmDeleteModal').modal('hide');
                getItems();
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
    }

        self.isValid = ko.computed(function () {
            return self.sourceCode.isValid()
        });

        self.setId = function (data) {
            self.id(data.Id);
        }

        self.save = function () {
            self.validate();
            var isValid = isWhitespaceNotEmpty(self.strMessage());

            if (self.isValid() && isValid) {
                var options = {
                    url: '/Source/SaveSource',
                    type: 'POST',
                    data: {
                        Code: self.sourceCode(),
                        Name: self.sourceName(),
                        ChannelCode: self.selectedChannel(),
                        IsForCommission: self.IsForCommission
                    }
                }

                if (self.isEditMode()) {
                    options.url = '/Source/EditSource';
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
                        $('#sourceModal').modal('hide');
                        getItems();
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
        self.sourceCode.clearError();
    }

    self.validate = function () {
        self.sourceCode.valueHasMutated();
    }

    getItems();

    //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    //---------------get Channel List---------
    self.getChannels = function () {
        var param = {
            url: '/CommissionDashboard/GetChannelAllList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.channelList(data);
            //self.enableChannel(true);
            if (data.length == 1) {
                self.selectedChannel(data[0].Code);
                self.enableChannel(false);
            } else {
                self.enableChannel(true);
                self.selectedChannel('');
            }
        });
    };
    //------------------------- end

    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var channelId = self.selectedChannel();

            if (typeof channelId == 'undefined' || channelId == '') {
                strMessage = strMessage + "Channel is required." + '<br/>';
            }
            self.strMessage(strMessage);
        }
    });
    //------------------------------------------ end

    self.getChannels();
}

$(function () {
    ko.applyBindings(new viewModel());
});