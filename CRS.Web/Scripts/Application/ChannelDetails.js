var viewModel = function () {
    var self = this;
    var modes = { Add: 0, Edit: 1, View: 2 };

    self.items = ko.observableArray();
    self.channels = ko.observableArray();
    self.mode = ko.observable(modes.View);

     $('body').on('show', '.modal', function () {
        $(this).css({ 'width': '750px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'margin-left': -($(this).height() / 2), 'top': '0'});
     });

     $('body').on('show', '#confirmDeleteModal', function () {
         $(this).css({ 'width': '350px', 'margin-top': ($(window).height() - $(this).height()) / 2, 'margin-left': -($(this).height() / 2), 'top': '0' });
     });

     

    $(".display").removeAttr('style').css("font-size", "small");

    //self.itemColumns = ['ChannelName', 'IsTiering', 'IsUsage', 'IsInflows', 'UsageRate', 'UsagePoints', 'CommRate', 'CommPoints', 'TieringRate',
	//	'TieringPoints', 'TieringCount', 'InflowsRate', 'InflowsPoints', 'InflowsCount', 'IsCoreBrand', 'CoreBrandRate', 'TaxRate', 'IsCreditToBranch',
	//	'IsCarDealer', 'SERate', 'NonSERate', 'IsInflowIncentive', 'InflowIncentiveCount', 'InflowIncentiveRate', 'IsForEveryCountIncentive',
	//	'ForEveryCountIncentiveCount', 'ForEveryCountIncentiveRate', 'IsBranchIncentive', 'BranchIncentiveRate', 'BranchIncentiveCount', 'MainBranchName',
    //	'SecondaryBranchName', 'EffectiveStartDate', 'EffectiveEndDate', 'Actions'];

    self.itemColumns = ['ChannelName', 'CommRate', 'TieringRate',
     'TieringCount', 'InflowsRate', 'InflowsCount', 'TaxRate',
     'SERate', 'NonSERate','EffectiveStartDate', 'EffectiveEndDate', 'Actions'];

    self.isEditMode = ko.computed(function () {
        return self.mode() == modes.Edit;
    });
    self.isAddMode = ko.computed(function () {
        return self.mode() == modes.Add;
    });

    self.title = ko.computed(function () {
        return self.isEditMode() ? 'Edit Channel Details' : 'Add Channel Details';
    });

    self.id = ko.observable();
    //self.channelId = ko.observable().extend({ required: { message: 'Channel is required.' } });
    self.channelId = ko.observable();
    self.isTiering = ko.observable();
    self.tieringRate = ko.observable().extend({ required: { onlyIf: function () { return self.isTiering(); }, message: 'Tiering Rate is required.' } });
    self.tieringCount = ko.observable().extend({ required: { onlyIf: function () { return self.isTiering(); }, message: 'Tiering Count is required.' } });
    self.tieringPoints = ko.observable();
    //self.tieringPoints = ko.observable().extend({ required: { onlyIf: function () { return self.isTiering(); }, message: 'Tiering Points is required.' } });

    self.isUsage = ko.observable();
    self.usageRate = ko.observable().extend({ required: { onlyIf: function () { return self.isUsage(); }, message: 'Usage Rate is required.' } });
    self.usageCount = ko.observable().extend({ required: { onlyIf: function () { return self.isUsage(); }, message: 'Usage Count is required.' } });
    //self.usagePoints = ko.observable().extend({ required: { onlyIf: function () { return self.isUsage(); }, message: 'Usage Points is required.' } });
    self.usagePoints = ko.observable();

    self.isInflows = ko.observable();
    self.inflowsRate = ko.observable().extend({ required: { onlyIf: function () { return self.isInflows(); }, message: 'Inflows Rate is required.' } });
    self.inflowsCount = ko.observable().extend({ required: { onlyIf: function () { return self.isInflows(); }, message: 'Inflows Count is required.' } });
    self.inflowsPoints = ko.observable();
    //self.inflowsPoints = ko.observable().extend({ required: { onlyIf: function () { return self.isInflows(); }, message: 'Inflows Points is required.' } });

    //self.isCoreBrand = ko.observable();
    //self.coreBrandRate = ko.observable().extend({ required: { onlyIf: function () { return self.isCoreBrand(); }, message: 'Core Brand Rate is required.' } });

    self.isInflowIncentive = ko.observable();
    self.inflowIncentiveRate = ko.observable().extend({ required: { onlyIf: function () { return self.isInflowIncentive(); }, message: 'Inflow Incentive Rate is required.' } });
    self.inflowIncentiveCount = ko.observable().extend({ required: { onlyIf: function () { return self.isInflowIncentive(); }, message: 'Inflow Incentive Count is required.' } });

    self.isForEveryCountIncentive = ko.observable();
    self.forEveryCountIncentiveRate = ko.observable().extend({ required: { onlyIf: function () { return self.isForEveryCountIncentive(); }, message: 'For Every Rate Incentive Rate is required.' } });
    self.forEveryCountIncentiveCount = ko.observable().extend({ required: { onlyIf: function () { return self.isForEveryCountIncentive(); }, message: 'For Every Count Incentive Count is required.' } });

    self.commRate = ko.observable().extend({ required: { message: 'Comm Rate is required.' } });
    self.commPoints = ko.observable();
    self.taxRate = ko.observable();
    self.effectiveStartDate = ko.observable().extend({ required: { message: 'Effective Start Date is required.' } });
    self.effectiveEndDate = ko.observable().extend({ validation: {
        message: 'Must be greater than Effective Start Date.',
        validator: function (val) {
            if (self.effectiveStartDate() == undefined || self.effectiveStartDate() == ''  
                || val == '' || typeof val == 'undefined') {
                return true;
            }
            return Date.parse(self.effectiveStartDate()) < Date.parse(val);
        }
    }
    });

    self.isCreditToBranch = ko.observable();

    self.isCarDealer = ko.observable();
    self.seRate = ko.observable().extend({ required: { onlyIf: function () { return self.isCarDealer(); }, message: 'SE Rate is required.' } });
    self.nonSERate = ko.observable().extend({ required: { onlyIf: function () { return self.isCarDealer(); }, message: 'Non-SE Rate is required.' } });

    self.isBranchIncentive = ko.observable();
    self.branchIncentiveRate = ko.observable().extend({ required: { onlyIf: function () { return self.isBranchIncentive(); }, message: 'Branch Incentive Rate is required.' } });
    self.branchIncentiveCount = ko.observable().extend({ required: { onlyIf: function () { return self.isBranchIncentive(); }, message: 'Branch Incentive Count is required.' } });
    self.mainBranchName = ko.observable().extend({ required: { onlyIf: function () { return self.isBranchIncentive(); }, message: 'Main Branch Name is required.' } });
    self.secondaryBranchName = ko.observable().extend({ required: { onlyIf: function () { return self.isBranchIncentive(); }, message: 'Secondary Branch Name is required.' } });
    self.strMessage = ko.observable();
    self.isCardBrand = ko.observable();
    self.isCardType = ko.observable();

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

    function formatDate(value) {
        var toFormat = new Date(parseInt(value.substring(value.indexOf('(') + 1, value.indexOf(')'))));

        return (toFormat.getMonth() + 1) + '/' + toFormat.getDate() + '/' + toFormat.getFullYear()
    }

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

    function getItems() {
        $.ajax({
            url: '/ChannelDetails/GetChannelDetailsList',
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
                        ChannelId: item.ChannelId,
                        ChannelName: item.ChannelName,
                        IsTiering: item.IsTiering,
                        IsUsage: item.IsUsage,
                        IsInflows: item.IsInflows,
                        UsageRate: item.UsageRate > 0 ? formatCurrency(new Number(item.UsageRate).toFixed(2)) : '',
                        UsagePoints: item.UsagePoints > 0 ? formatCurrency(new Number(item.UsagePoints).toFixed(2)) : '',
                        CommRate: item.CommRate > 0 ? formatCurrency(new Number(item.CommRate).toFixed(2)) : '',
                        CommPoints: item.CommPoints > 0 ? formatCurrency(new Number(item.CommPoints).toFixed(2)) : '',
                        TieringRate: item.TieringRate > 0 ? formatCurrency(new Number(item.TieringRate).toFixed(2)) : '',
                        TieringPoints: item.TieringPoints > 0 ? formatCurrency(new Number(item.TieringPoints).toFixed(2)) : '',
                        TieringCount: item.TieringCount > 0 ? item.TieringCount : '',
                        InflowsRate: item.InflowsRate > 0 ? formatCurrency(new Number(item.InflowsRate).toFixed(2)) : '',
                        InflowsPoints: item.InflowsPoints > 0 ? formatCurrency(new Number(item.InflowsPoints).toFixed(2)) : '',
                        InflowsCount: item.InflowsCount > 0 ? item.InflowsCount : '',
                        IsCardBrand: item.IsCardBrand,
                        IsCardType: item.IsCardType,
                        //IsCoreBrand: item.IsCoreBrand,
                        //CoreBrandRate: item.CoreBrandRate > 0 ? formatCurrency(new Number(item.CoreBrandRate).toFixed(2)) : '',
                        TaxRate: item.TaxRate > 0 ? formatCurrency(new Number(item.TaxRate).toFixed(2)) : '',
                        IsCreditToBranch: item.IsCreditToBranch,
                        IsCarDealer: item.IsCarDealer,
                        SERate: item.SERate > 0 ? formatCurrency(new Number(item.SERate).toFixed(2)) : '',
                        NonSERate: item.NonSERate > 0 ? formatCurrency(new Number(item.NonSERate).toFixed(2)) : '',
                        IsInflowIncentive: item.IsInflowIncentive,
                        InflowIncentiveCount: item.InflowIncentiveCount > 0 ? item.InflowIncentiveCount : '',
                        InflowIncentiveRate: item.InflowIncentiveRate > 0 ? formatCurrency(new Number(item.inflowIncentiveRate).toFixed(2)) : '',
                        IsForEveryCountIncentive: item.IsForEveryCountIncentive,
                        ForEveryCountIncentiveCount: item.ForEveryCountIncentiveCount > 0 ? item.ForEveryCountIncentiveCount : '',
                        ForEveryCountIncentiveRate: item.ForEveryCountIncentiveRate > 0 ? formatCurrency(new Number(item.forEveryCountIncentiveRate).toFixed(2)) : '',
                        IsBranchIncentive: item.IsBranchIncentive,
                        BranchIncentiveRate: item.BranchIncentiveRate > 0 ? formatCurrency(new Number(item.BranchIncentiveRate).toFixed(2)) : '',
                        BranchIncentiveCount: item.BranchIncentiveCount > 0 ? item.BranchIncentiveCount : '',
                        MainBranchName: item.MainBranchName,
                        SecondaryBranchName: item.SecondaryBranchName,
                        EffectiveStartDate: formatDate(item.EffectiveStartDate),
                        EffectiveEndDate: formatDate(item.EffectiveEndDate),
                        Actions: item.ID
                    }
                });

                self.items(mappedData);
            },
            error: function () {
                $.unblockUI();
            }
        });
        //exec({
        //    url: '/ChannelDetails/GetChannelDetailsList',
        //    type: 'GET',
        //}, function (data) {
        //    var mappedData = $.map(data, function (item) {
        //        return result = {
        //            Id: item.ID,
        //            ChannelId: item.ChannelId,
        //            ChannelName: item.ChannelName,
        //            IsTiering: item.IsTiering,
        //            IsUsage: item.IsUsage,
        //            IsInflows: item.IsInflows,
        //            UsageRate: item.UsageRate > 0 ? formatCurrency(new Number(item.UsageRate).toFixed(2)) : '',
        //            UsagePoints: item.UsagePoints > 0 ? formatCurrency(new Number(item.UsagePoints).toFixed(2)) : '',
        //            CommRate: item.CommRate > 0 ? formatCurrency(new Number(item.CommRate).toFixed(2)) : '',
        //            CommPoints: item.CommPoints > 0 ? formatCurrency(new Number(item.CommPoints).toFixed(2)) : '',
        //            TieringRate: item.TieringRate > 0 ? formatCurrency(new Number(item.TieringRate).toFixed(2)) : '',
        //            TieringPoints: item.TieringPoints > 0 ? formatCurrency(new Number(item.TieringPoints).toFixed(2)) : '',
        //            TieringCount: item.TieringCount > 0 ? item.TieringCount : '',
        //            InflowsRate: item.InflowsRate > 0 ? formatCurrency(new Number(item.InflowsRate).toFixed(2)) : '',
        //            InflowsPoints: item.InflowsPoints > 0 ? formatCurrency(new Number(item.InflowsPoints).toFixed(2)) : '',
        //            InflowsCount: item.InflowsCount > 0 ? item.InflowsCount : '',
        //            IsCoreBrand: item.IsCoreBrand,
        //            CoreBrandRate: item.CoreBrandRate > 0 ? formatCurrency(new Number(item.CoreBrandRate).toFixed(2)) : '',
        //            TaxRate: item.TaxRate > 0 ? formatCurrency(new Number(item.TaxRate).toFixed(2)) : '',
        //            IsCreditToBranch: item.IsCreditToBranch,
        //            IsCarDealer: item.IsCarDealer,
        //            SERate: item.SERate > 0 ? formatCurrency(new Number(item.SERate).toFixed(2)) : '',
        //            NonSERate: item.NonSERate > 0 ? formatCurrency(new Number(item.NonSERate).toFixed(2)) : '',
        //            IsInflowIncentive: item.IsInflowIncentive,
        //            InflowIncentiveCount: item.InflowIncentiveCount > 0 ? item.InflowIncentiveCount : '',
        //            InflowIncentiveRate: item.InflowIncentiveRate > 0 ? formatCurrency(new Number(item.inflowIncentiveRate).toFixed(2)) : '',
        //            IsForEveryCountIncentive: item.IsForEveryCountIncentive,
        //            ForEveryCountIncentiveCount: item.ForEveryCountIncentiveCount > 0 ? item.ForEveryCountIncentiveCount : '',
        //            ForEveryCountIncentiveRate: item.ForEveryCountIncentiveRate > 0 ? formatCurrency(new Number(item.forEveryCountIncentiveRate).toFixed(2)) : '',
        //            IsBranchIncentive: item.IsBranchIncentive,
        //            BranchIncentiveRate: item.BranchIncentiveRate > 0 ? formatCurrency(new Number(item.branchIncentiveRate).toFixed(2)) : '',
        //            BranchIncentiveCount: item.BranchIncentiveCount > 0 ? item.BranchIncentiveCount : '',
        //            MainBranchName: item.MainBranchName,
        //            SecondaryBranchName: item.SecondaryBranchName,
        //            EffectiveStartDate: formatDate(item.EffectiveStartDate),
        //            EffectiveEndDate: formatDate(item.EffectiveEndDate),
        //            Actions: item.ID
        //        }
        //    });

        //    self.items(mappedData);
        //});
    }

    function getChannels() {
        exec({
            url: '/Channel/GetChannelList',
            type: 'GET',
        }, function (data) {
            var mappedData = $.map(data, function (item) {
                return result = {
                    Name: item.Name + ' - ' + item.Code,
                    Id: item.ID,
                }
            });

            self.channels(mappedData);
        });
    }

    function setFormData(data) {
        var isEditMode = self.isEditMode();
        var currdate = ((new Date().getMonth() + 1) + '/' + new Date().getDate() + '/' + new Date().getFullYear());
        self.id(isEditMode ? data.Id : undefined);
        self.channelId(isEditMode ? data.ChannelId : undefined);
        self.isTiering(isEditMode ? data.IsTiering : undefined);
        self.tieringRate(isEditMode ? data.TieringRate : undefined);
        self.tieringCount(isEditMode ? data.TieringCount : undefined);
        self.tieringPoints(isEditMode ? data.TieringPoints : undefined);
        self.isUsage(isEditMode ? data.IsUsage : undefined);
        self.usageRate(isEditMode ? data.UsageRate : undefined);
        self.usageCount(isEditMode ? data.UsageCount : undefined);
        self.usagePoints(isEditMode ? data.UsagePoints : undefined);
        self.isInflows(isEditMode ? data.IsInflows : undefined);
        self.inflowsRate(isEditMode ? data.InflowsRate : undefined);
        self.inflowsCount(isEditMode ? data.InflowsCount : undefined);
        self.inflowsPoints(isEditMode ? data.InflowsPoints : undefined);
        self.isCardBrand(isEditMode ? data.IsCardBrand : undefined);
        self.isCardType(isEditMode ? data.IsCardType : undefined);
        //self.isCoreBrand(isEditMode ? data.IsCoreBrand : undefined);
        //self.coreBrandRate(isEditMode ? data.CoreBrandRate : undefined);
        self.isInflowIncentive(isEditMode ? data.IsInflowIncentive : undefined);
        self.inflowIncentiveRate(isEditMode ? data.InflowIncentiveRate : undefined);
        self.inflowIncentiveCount(isEditMode ? data.InflowIncentiveCount : undefined);
        self.isForEveryCountIncentive(isEditMode ? data.IsForEveryCountIncentive : undefined);
        self.forEveryCountIncentiveRate(isEditMode ? data.ForEveryCountIncentiveRate : undefined);
        self.forEveryCountIncentiveCount(isEditMode ? data.ForEveryCountIncentiveCount : undefined);
        self.commRate(isEditMode ? data.CommRate : undefined);
        self.commPoints(isEditMode ? data.CommPoints : undefined);
        self.taxRate(isEditMode ? data.TaxRate : undefined);
        self.effectiveStartDate(isEditMode ? data.EffectiveStartDate : undefined);
        self.effectiveEndDate(isEditMode ? (data.EffectiveEndDate == currdate ? '' : data.EffectiveEndDate) : undefined);
        self.isCreditToBranch(isEditMode ? data.IsCreditToBranch : undefined);
        self.isCarDealer(isEditMode ? data.IsCarDealer : undefined);
        self.seRate(isEditMode ? data.SERate : undefined);
        self.nonSERate(isEditMode ? data.NonSERate : undefined);
        self.isBranchIncentive(isEditMode ? data.IsBranchIncentive : undefined);
        self.branchIncentiveRate(isEditMode ? data.BranchIncentiveRate : undefined);
        self.branchIncentiveCount(isEditMode ? data.BranchIncentiveCount : undefined);
        self.mainBranchName(isEditMode ? data.MainBranchName : undefined);
        self.secondaryBranchName(isEditMode ? data.SecondaryBranchName : undefined);
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

    self.deleteChannelDetails = function () {
        //exec({
        //    url: '/ChannelDetails/DeleteChannelDetails',
        //    type: 'DELETE',
        //    data: {
        //        channelDetailsId: self.id()
        //    }
        //}, function (data) {
        //    if (data) {
        //        getItems();
        //        $('#confirmDeleteModal').modal('hide');
        //    } else {
        //        alert('Error encountered');
        //    }
        //});
        $.ajax({
            url: '/ChannelDetails/DeleteChannelDetails',
            type: 'DELETE',
            data: {
                channelDetailsId: self.id()
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
                //alert('Error encountered');
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
        return self.tieringRate.isValid() && self.tieringCount.isValid() && self.usageRate.isValid() &&
            self.usageCount.isValid() && self.inflowsRate.isValid() && self.inflowsCount.isValid() &&
            self.inflowIncentiveRate.isValid() &&
            self.inflowIncentiveCount.isValid() && self.forEveryCountIncentiveRate.isValid() && self.forEveryCountIncentiveCount.isValid() &&
            self.effectiveStartDate.isValid() && self.effectiveEndDate.isValid() && self.seRate.isValid() && self.nonSERate.isValid() &&
            self.branchIncentiveRate.isValid() && self.branchIncentiveCount.isValid() && self.mainBranchName.isValid() && self.secondaryBranchName.isValid();
        //return self.tieringRate.isValid() && self.tieringCount.isValid() && self.tieringPoints.isValid() && self.usageRate.isValid() &&
        //    self.usageCount.isValid() && self.usagePoints.isValid() && self.inflowsRate.isValid() && self.inflowsCount.isValid() &&
        //    self.inflowsPoints.isValid() && self.coreBrandRate.isValid() && self.inflowIncentiveRate.isValid() &&
        //    self.inflowIncentiveCount.isValid() && self.forEveryCountIncentiveRate.isValid() && self.forEveryCountIncentiveCount.isValid() &&
        //    self.effectiveStartDate.isValid() && self.effectiveEndDate.isValid() && self.seRate.isValid() && self.nonSERate.isValid() &&
        //    self.branchIncentiveRate.isValid() && self.branchIncentiveCount.isValid() && self.mainBranchName.isValid() && self.secondaryBranchName.isValid();

    });

    self.setId = function (data) {
        self.id(data.Id);
    }

    self.save = function () {
        self.validate();
        var isValid = isWhitespaceNotEmpty(self.strMessage());
        if (self.isValid() && isValid) {
            var options = {
                url: '/ChannelDetails/SaveChannelDetails',
                type: 'POST',
                data: {
                    ChannelId: self.channelId(),
                    IsTiering: self.isTiering(),
                    IsUsage: self.isUsage(),
                    IsInflows: self.isInflows(),
                    UsageRate: self.usageRate(),
                    UsagePoints: self.usagePoints(),
                    CommRate: self.commRate(),
                    CommPoints: self.commPoints(),
                    TieringRate: self.tieringRate(),
                    TieringPoints: self.tieringPoints(),
                    TieringCount: self.tieringCount(),
                    InflowsRate: self.inflowsRate(),
                    InflowsPoints: self.inflowsPoints(),
                    InflowsCount: self.inflowsCount(),
                    IsCardBrand: self.isCardBrand(),
                    IsCardType: self.isCardType(),
                    //IsCoreBrand: self.isCoreBrand(),
                    //CoreBrandRate: self.coreBrandRate(),
                    TaxRate: self.taxRate(),
                    IsCreditToBranch: self.isCreditToBranch(),
                    IsCarDealer: self.isCarDealer(),
                    SERate: self.seRate(),
                    NonSERate: self.nonSERate(),
                    IsInflowIncentive: self.isInflowIncentive(),
                    InflowIncentiveCount: self.inflowIncentiveCount(),
                    InflowIncentiveRate: self.inflowIncentiveRate(),
                    IsForEveryCountIncentive: self.isForEveryCountIncentive(),
                    ForEveryCountIncentiveCount: self.forEveryCountIncentiveCount(),
                    ForEveryCountIncentiveRate: self.forEveryCountIncentiveRate(),
                    IsBranchIncentive: self.isBranchIncentive(),
                    BranchIncentiveRate: self.branchIncentiveRate(),
                    BranchIncentiveCount: self.branchIncentiveCount(),
                    MainBranchName: self.mainBranchName(),
                    SecondaryBranchName: self.secondaryBranchName(),
                    EffectiveStartDate: self.effectiveStartDate(),
                    EffectiveEndDate: self.effectiveEndDate()
                }
            }

            if (self.isEditMode()) {
                    options.url = '/ChannelDetails/EditChannelDetails';
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
                    getItems();
                    $('#channelDetailsModal').modal('hide');
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
        //self.validate();

        //if (self.isValid()) {
        //    var options = {
        //        url: '/ChannelDetails/SaveChannelDetails',
        //        type: 'POST',
        //        data: {
        //            ChannelId: self.channelId(),
        //            IsTiering: self.isTiering(),
        //            IsUsage: self.isUsage(),
        //            IsInflows: self.isInflows(),
        //            UsageRate: self.usageRate(),
        //            UsagePoints: self.usagePoints(),
        //            CommRate: self.commRate(),
        //            CommPoints: self.commPoints(),
        //            TieringRate: self.tieringRate(),
        //            TieringPoints: self.tieringPoints(),
        //            TieringCount: self.tieringCount(),
        //            InflowsRate: self.inflowsRate(),
        //            InflowsPoints: self.inflowsPoints(),
        //            InflowsCount: self.inflowsCount(),
        //            IsCoreBrand: self.isCoreBrand(),
        //            CoreBrandRate: self.coreBrandRate(),
        //            TaxRate: self.taxRate(),
        //            IsCreditToBranch: self.isCreditToBranch(),
        //            IsCarDealer: self.isCarDealer(),
        //            SERate: self.seRate(),
        //            NonSERate: self.nonSERate(),
        //            IsInflowIncentive: self.isInflowIncentive(),
        //            InflowIncentiveCount: self.inflowIncentiveCount(),
        //            InflowIncentiveRate: self.inflowIncentiveRate(),
        //            IsForEveryCountIncentive: self.isForEveryCountIncentive(),
        //            ForEveryCountIncentiveCount: self.forEveryCountIncentiveCount(),
        //            ForEveryCountIncentiveRate: self.forEveryCountIncentiveRate(),
        //            IsBranchIncentive: self.isBranchIncentive(),
        //            BranchIncentiveRate: self.branchIncentiveRate(),
        //            BranchIncentiveCount: self.branchIncentiveCount(),
        //            MainBranchName: self.mainBranchName(),
        //            SecondaryBranchName: self.secondaryBranchName(),
        //            EffectiveStartDate: self.effectiveStartDate(),
        //            EffectiveEndDate: self.effectiveEndDate()
        //        }
        //    }

        //    if (self.isEditMode()) {
        //        options.url = '/ChannelDetails/UpdateChannelDetails';
        //        options.type = 'PUT';
        //        options.data.Id = self.id();
        //    }

        //    exec(options, function (result) {
        //        if (result) {
        //            getItems();
        //            $('#channelDetailsModal').modal('hide');
        //        } else {
        //            alert('Error encountered');
        //        }
        //    });
        //}
    }

    self.unvalidate = function () {
        self.tieringRate.clearError();
        self.tieringCount.clearError();
        //self.tieringPoints.clearError();
        self.usageRate.clearError();
        self.usageCount.clearError();
        //self.usagePoints.clearError();
        self.inflowsRate.clearError();
        self.inflowsCount.clearError();
        //self.inflowsPoints.clearError();
        //self.coreBrandRate.clearError();
        self.inflowIncentiveRate.clearError();
        self.inflowIncentiveCount.clearError();
        self.forEveryCountIncentiveRate.clearError();
        self.forEveryCountIncentiveCount.clearError();
        self.effectiveStartDate.clearError();
        self.effectiveEndDate.clearError();
        self.seRate.clearError();
        self.nonSERate.clearError();
        self.branchIncentiveRate.clearError();
        self.branchIncentiveCount.clearError();
        self.mainBranchName.clearError();
        self.secondaryBranchName.clearError();
        self.commRate.clearError();
    }

    self.validate = function () {
        self.tieringRate.valueHasMutated();
        self.tieringCount.valueHasMutated();
        //self.tieringPoints.valueHasMutated();
        self.usageRate.valueHasMutated();
        self.usageCount.valueHasMutated();
        //self.usagePoints.valueHasMutated();
        self.inflowsRate.valueHasMutated();
        self.inflowsCount.valueHasMutated();
        //self.inflowsPoints.valueHasMutated();
        //self.coreBrandRate.valueHasMutated();
        self.inflowIncentiveRate.valueHasMutated();
        self.inflowIncentiveCount.valueHasMutated();
        self.forEveryCountIncentiveRate.valueHasMutated();
        self.forEveryCountIncentiveCount.valueHasMutated();
        self.effectiveStartDate.valueHasMutated();
        self.effectiveEndDate.valueHasMutated();
        self.seRate.valueHasMutated();
        self.nonSERate.valueHasMutated();
        self.branchIncentiveRate.valueHasMutated();
        self.branchIncentiveCount.valueHasMutated();
        self.mainBranchName.valueHasMutated();
        self.secondaryBranchName.valueHasMutated();
        self.commRate.valueHasMutated();
    }

    self.mode.subscribe(function (item) {
        if (item != modes.View) {
            getChannels();
        }
    });


    //-- validate before saving
    $(':button').click(function () {
        var sText = $(this).text();
        var strMessage = '';
        if (sText == 'Save') {
            var channelName = self.channelId();

            if (typeof channelName == 'undefined' || channelName == '') {
                strMessage = strMessage + "Channel is required." + '<br/>';
            }

            if (self.isCardBrand() && self.isCardType()) {
                strMessage = strMessage + "Please select only one between IsCardBrand or IsCardType." + '<br/>';
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


    getItems();
}

$(function () {
    $("#dtEFfStartDate").mask("99/99/9999");
    $("#dtEFfStartDate").datepicker("setDate", -1);
    $("#dtEffEndDate").mask("99/99/9999");
    $("#dtEffEndDate").datepicker("setDate", -1);
    $('#txtChannelRate').autoNumeric('init', { vMax: '9999999999.99' });
    $('#txtTaxRate').autoNumeric('init', { vMax: '999.99' });
    $('#txtSERate').autoNumeric('init', { vMax: '9999999999.99' });
    $('#txtNonSERate').autoNumeric('init', { vMax: '9999999999.99' });
    $('#txtBranchIncentiveRate').autoNumeric('init', { vMax: '9999999999.99' });
    $('#txtBranchIncentiveCount').autoNumeric('init', { vMax: '9999999999' });
    $('.txtDynamicRate').autoNumeric('init', { vMax: '9999999999.99' });
    $('.txtDynamicCount').filter_input({ regex: '[0-9]' });
    //$('.txtDynamicCount').autoNumeric('init', { vMax: '9999999999' });
    $('.txtDynamicPoints').autoNumeric('init', { vMax: '9999999999.99' });
    ko.applyBindings(new viewModel());
});