var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };
    var cardTypeCodeData = undefined;
    var cardTypeNameData = undefined;
    var cardBrandNameData = undefined;
    var cardBrandCodeData = undefined;

    $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '500px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'top': '0' });
    });

    self.items = ko.observableArray();
    self.cardBrandList = ko.observableArray([]);
    self.selectedCardBrand = ko.observable();
    self.enableCardBrand = ko.observable();
    self.mode = ko.observable(modes.View);
    self.itemColumns = ['CardTypeCode', 'CardTypeName', 'CardBrandName', 'CardSubTypeName'];

    self.cardSubTypeList = ko.observableArray([]);
    self.selectedCardSubType = ko.observable();

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Card Type' : 'Add Card Type';
    });

    self.id = ko.observable();
    self.cardTypeCode = ko.observable().extend({
        required: { message: 'Card Type Code is required.' },
        validation: {
            message: 'Invalid Card Type Code.',
            validator: function (val) {
                if (val == cardTypeCodeData && self.isEditMode()) {
                    return true;
                }
                var result = false;
                exec({
                    url: '/CardType/IsCardTypeCodeValid',
                    type: 'GET',
                    data: {
                        cardTypeCode: val
                    }
                }, function (data) {
                    result = data;
                }, false);

                return result;
            }
        }
    });
    self.cardTypeName = ko.observable().extend({
        required: { message: 'Card Type Name is required.' }
    });
    //self.cardTypeName = ko.observable().extend({
    //    required: { message: 'Card Type Name is required.' },
    //    validation: {
    //        message: 'Invalid Card Type Name.',
    //        validator: function (val) {
    //            if (val == cardTypeNameData && self.isEditMode()) {
    //                return true;
    //            }
    //            var result = false;
    //            exec({
    //                url: '/CardType/IsCardTypeNameValid',
    //                type: 'GET',
    //                data: {
    //                    cardTypeName: val,
    //                    cardSubTypeCode: self.selectedCardSubType()
    //                }
    //            }, function (data) {
    //                result = data;
    //            }, false);

    //            return result;
    //        }
    //    }
    //});

    self.cardBrandName = ko.observable();
    self.cardBrandCode = ko.observable();
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
            url: '/CardType/GetCardTypeList',
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
                                CardTypeCode: item.Code,
                                CardTypeName: item.Name,
                                CardSubTypeName: item.CardSubTypeName,
                                CardSubTypeCode: item.CardSubTypeCode,
                                CardBrandName: item.CardBrandName,
                                CardBrandCode: item.CardBrandCode
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
        cardTypeCodeData = isEditMode ? data.CardTypeCode : undefined;
        cardTypeNameData = isEditMode ? data.CardTypeName : undefined;
        cardBrandCodeData = isEditMode ? data.CardBrandCode : undefined;
        cardBrandNameData = isEditMode ? data.CardBrandName : undefined;
        self.id(isEditMode ? data.Id : undefined);
        self.cardTypeCode(cardTypeCodeData);
        self.cardTypeName(cardTypeNameData);
        self.cardBrandCode(cardBrandCodeData);
        self.cardBrandName(cardBrandNameData);
        self.selectedCardSubType(isEditMode ? data.CardSubTypeCode : undefined);
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

    self.deleteCardType = function () {
        $.ajax({
            url: '/CardType/DeleteCardType',
            type: 'DELETE',
            data: {
                cardTypeId: self.id()
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
        return self.cardTypeCode.isValid() &&
            self.cardTypeName.isValid();
    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());

        if (self.isValid() && isValid) {
            var options = {
                url: '/CardType/SaveCardType',
                type: 'POST',
                data: {
                    Code: self.cardTypeCode(),
                    Name: self.cardTypeName(),
                    CardBrandCode: self.cardBrandCode(),
                    CardSubTypeCode: self.selectedCardSubType()
                }
            }

            if (self.isEditMode()) {
                options.url = '/CardType/UpdateCardType';
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
                    $('#cardTypeModal').modal('hide');
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
        self.cardTypeCode.clearError();
        self.cardTypeName.clearError();
    }

    self.validate = function () {
        self.cardTypeCode.valueHasMutated();
        self.cardTypeName.valueHasMutated();
    }

    getItems();
    getCardBrands();
    getCardSubTypes();

    //------------ get CardBrand List ------
   function getCardBrands() {
        var param = {
            url: '/CardType/GetCardBrandList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.cardBrandList(data);
            if (data.length == 1) {
                self.selectedCardBrand(data[0].Code);
                self.enableCardBrand(false);
            } else {
                self.enableCardBrand(true);
            }
        });
    };
    //------------------------- end

    //------------ get CardBrand List ------

   function getCardSubTypes() {
        var param = {
            url: '/CardType/GetCardSubTypes',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.cardSubTypeList(data);
        });
    };
    //------------------------- end

    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var cardBrand = self.cardBrandCode();

            if (typeof cardBrand == 'undefined' || cardBrand == '') {
                strMessage = strMessage + "Card Brand is required." + '<br/>';
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
}

$(function () {
    ko.applyBindings(new viewModel());
});