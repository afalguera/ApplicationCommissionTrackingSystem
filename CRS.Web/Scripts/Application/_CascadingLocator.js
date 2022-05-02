var cascadingLocator = function () {
    var self = this;
    self.channelList = ko.observableArray();
    self.channelId = ko.observable();
    self.channelCode = ko.observable();
    self.regionList = ko.observableArray();
    self.regionId = ko.observable();
    self.regionCode = ko.observable();
    //self.areaList = ko.observableArray();
    //self.areaId = ko.observable();
    //self.areaCode = ko.observable();
    self.districtList = ko.observableArray();
    self.districtId = ko.observable();
    self.districtCode = ko.observable();
    self.branchList = ko.observableArray();
    self.branchId = ko.observable();
    self.branchCode = ko.observable();

    function exec(options, callback) {
        options.async = false;
        options.dataType = 'json';
        options.contentType = 'application/json; charset=utf-8';
        $.ajax(options).always(callback);
    }

    self.getChannels = function () {
        exec({
            url: '/CommissionDashboard/GetChannelList',
            type: 'GET',
            data: {},
        }, function (data) {
            self.channelList(data);
        });
    }

    //self.getAreas = function () {
    //    exec({
    //        url: '/CommissionDashboard/GetAreaList',
    //        type: 'GET',
    //        data: {
    //            regionCode: self.regionCode()
    //        }
    //    }, function (data) {
    //        self.areaList(data);
    //    });
    //}

    self.getRegions = function () {
        exec({
            url: '/CommissionDashboard/GetRegionList',
            type: 'GET',
            data: {}
        }, function (data) {
            self.regionList(data);
        });
    }

    self.getDistricts = function () {
        exec({
            url: '/CommissionDashboard/GetDistrictList',
            type: 'GET',
            data: {
                regionCode: self.regionCode()
            }
        }, function (data) {
            self.districtList(data);
        });
    }

    self.getBranches = function () {
        exec({
            url: '/CommissionDashboard/GetBranchList',
            type: 'GET',
            data: {
                channelCode: self.channelCode(),
                districtCode: self.districtCode()
            }
        }, function (data) {
            self.branchList(data);
        });
    }

    self.channelCode.subscribe(function (data) {
        if ((data != undefined && data != '') &&
            (self.districtCode() != undefined && self.districtCode() != '')) {
            self.getBranches();
        } else {
            self.branchList([]);
        }
    });

    self.regionCode.subscribe(function (data) {
        if (data != undefined && data != '') {
            //self.getAreas();
            self.getDistricts();
        } else {
            //self.areaList([]);
            self.districtList([]);
        }
    });

    //self.areaCode.subscribe(function (data) {
    //    if (data != undefined && data != '') {
    //        self.getDistricts();
    //    } else {
    //        self.districtList([]);
    //    }
    //});

    self.districtCode.subscribe(function (data) {
        if (data != undefined && data != '' &&
            self.channelCode() != undefined && self.channelCode() != '') {
            self.getBranches();
        } else {
            self.branchList([]);
        }
    });
}