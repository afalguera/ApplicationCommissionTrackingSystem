var vm = {};
var summarySources = [];
var detailSources = [];

//--------------Datepicker handler-------------------
ko.bindingHandlers.datepicker = {

    init: function (element, valueAccessor, allBindingsAccessor) {

        var options = allBindingsAccessor().datepickerOptions || {};
        $(element).datepicker(options);

        //handle the field changing
        ko.utils.registerEventHandler(element, "change", function () {
            var observable = valueAccessor();
            observable($(element).datepicker("getDate"));
        });

        //handle disposal (if KO removes by the template binding)
        ko.utils.domNodeDisposal.addDisposeCallback(element, function () {
            $(element).datepicker("destroy");
        });
    }
};
//---------------------------------- end

//-------------Restrict Date on datepicker-------------------
function allowedDays(date) {
    // Weekend Days Sunday = 0 ... Sat =6
    if ((date.getDate() == 1 && date.getDay() != 0 && date.getDay() != 6) || date.getDay() == 1) {
        return [true, ''];
    }
    return [false, ''];
};
//---------------------------------- end

//--------------Format Date (MM/DD/YYYY)-------------------
var formatDateMMddYYYY = function (dd, mm, yyyy) {
    var formattedDate = '';
    if (parseInt(dd) < 10) { dd = '0' + dd } if (parseInt(mm) < 10) { mm = '0' + mm }
    formattedDate = mm + '/' + dd + '/' + yyyy;
    return formattedDate;
};
//---------------------------------- end

//--------------Set Week dates-------------------
function setWeeklyDate(date) {
    var dateTo = new Date(date);
    dateTo = formatDateMMddYYYY(dateTo.getDate(), dateTo.getMonth() + 1, dateTo.getFullYear());
    getNextWeek(dateTo);
};
//---------------------------------- end

//--------------Date prototype-------------------
Date.prototype.daysMoreLess =
       Date.prototype.daysMoreLess ||
       function (days) {
           days = days || 0;
           var ystrdy = new Date(this.setDate(this.getDate() + days));
           this.setDate(this.getDate() + -days);
           return ystrdy;
       };
//---------------------------------- end

//--------------Initialize jQuery------------------
$(function () {

    vm = new productivityReportViewModel();
    vm.init();
    ko.applyBindings(vm);

   
    //others
    $("#dtOthersFrom").datepicker({
        showOn: "button",
        buttonImage: "../Content/images/calendar-icon.png",
        buttonImageOnly: true,
        maxDate: '-1d'
    });
    $("#dtOthersFrom").mask("99/99/9999");
    $("#dtOthersFrom").datepicker("setDate", -1);
    $("#dtOthersTo").datepicker({
        showOn: "button",
        buttonImage: "../Content/images/calendar-icon.png",
        buttonImageOnly: true,
        maxDate: '-1d'
    });
    $("#dtOthersTo").mask("99/99/9999");
    $("#dtOthersTo").datepicker("setDate", -1);

});
//---------------------------------- end

//--------------EAPR Channel ViewModel-------------------
var productivityReportViewModel = function () {
    var self = this;
    self.dateFrom = ko.observable();
    self.dateTo = ko.observable();
    self.dtWeeklyFrom = ko.observable();
    self.dtWeeklyTo = ko.observable();

    self.summaryList = ko.observableArray();
    self.reportList = ko.observableArray();
    self.selectedReport = ko.observable();
    self.monthsList = ko.observableArray();
    self.quarterList = ko.observableArray();
    self.selectedMonth = ko.observable();
    self.selectedQuarter = ko.observable();
    self.yearsList = ko.observableArray();
    self.selectedYear = ko.observable();
    self.isUser = ko.observable(false);
    self.qMonth = ko.observable();
    self.qYear = ko.observable();
    self.showPrint = ko.observable(false);
    self.dtOthersFrom = ko.observable();
    self.dtOthersTo = ko.observable();

    //commission
    self.sourceCode = ko.observable();
    self.channelCode = ko.observable();
    self.channelName = ko.observable();

    self.isBranchName = ko.observable();

    self.statusList = ko.observableArray([]);
    self.selectedStatus = ko.observable();
    self.showStatus = ko.observable(true);
    self.selectedFormat = ko.observable();
    self.formatList = ko.observableArray([]);
    self.showFormat = ko.observable(true);
    //self.showChannel = ko.observable(true);
    //self.showBranch = ko.observable(true);

    self.channelList = ko.observableArray([]);
    self.selectedChannel = ko.observable();
    self.enableChannel = ko.observable();

    self.regionList = ko.observableArray([]);
    self.selectedRegion = ko.observable();
    self.enableRegion = ko.observable();

    self.districtList = ko.observableArray([]);
    self.selectedDistrict = ko.observable();
    self.enableDistrict = ko.observable();

    self.branchList = ko.observableArray([]);
    self.selectedBranch = ko.observable();
    self.enableBranch = ko.observable();
    //---------------------------------- end

    //---------------Initialize Page-------------------
    self.init = function () {
        self.allowSearch();
    };
    //---------------------------------- end


    //---------------Allow Search-------------------
    self.allowSearch = function () {
        var param = {
            url: '/ApplicationStatus/IsReferror',
            type: 'GET',
            data: {},
        };

        bt.ajax.exec(param, function (data) {
            self.isUser(data.isValid);
            productivityReportInit();
        });
    };
    //---------------------------------- end

    //---------------Report Types-------------------
    self.getReportModes = function () {
        var param = {
            url: '/ProductivityReport/GetReportTypes',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.reportList(data);
        });
        //var reportModes = [
        //          { Code: "PSUMMARY", Name: "Productivity" },
        //          { Code: "PCHART", Name: "Productivity (Charts)" },
        //          { Code: "TOPREJ", Name: "Top Rejected Reason" },
        //          { Code: "REJ", Name: "Rejected Per Reason" },
        //          { Code: "INC", Name: "Incomplete Referrals" },
        //          { Code: "INCRT99", Name: "Incomplete RT99" },
        //          { Code: "INCPDOC", Name: "Incomplete PDOC" }
        //];
        //self.reportList(reportModes);
    };
    //---------------------------------- end

    //---------------Format Types-------------------
    self.getFomatTypes = function () {
        var formatTypes = [
                  { Code: "W", Name: "Grouped per Week" },
                  { Code: "M", Name: "Grouped per Month" }
                ];
        self.formatList(formatTypes);
    };
    //---------------------------------- end

    //-- get status
    self.getStatuses = function () {
        var param = {
            url: '/ProductivityReport/GetStatusTypes',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.statusList(data);
        });
    };
    //------------------------------------------ end

    //---------------Years-------------------
    self.getYears = function () {
        var d = new Date();
        n = d.getMonth();
        y = d.getFullYear();
        p = y - 4;

        var years = new Array();

        for (var i = p; i <= y; i++) {
            years.push({ Code: i, Name: i });
        }
        self.yearsList(years);
        self.selectedYear(y);
    };
    //---------------------------------- end

    //---------------Onchange Report Type-------------------
    self.selectedReport.subscribe(function (val) {
        switch (val) {
            case 'PSUMMARY':
            case 'PCHART':
                self.selectedStatus(undefined);
                showYearly();
                break;
            case 'REJ':
                self.selectedStatus('REJECTED');
                //showOthers();
                showYearly();
            case 'OUTREJ':
                self.selectedStatus('OUTRIGHTREJECT');
                //showOthers();
                showYearly();
            case 'TOPREJ':
                self.selectedStatus(undefined);
                showYearly();
                break;
            case 'INC':
            case 'INCRT99':
            case 'INCPDOC':
                self.selectedStatus('INCOMPLETE');
                //showOthers();
                showYearly();
                break;          
            default:
                showYearly();
                break;
        }
    });
    //---------------------------------- end

    //--------------Div visibility-------------------
    var visibilityDiv = function (cssClass, isVisible) {
        var selectedDiv = $(cssClass);
        if (isVisible == true || isVisible == "true") {
            selectedDiv.css("display", "inline");
            selectedDiv.show();
        } else {
            selectedDiv.css("display", "none");
            selectedDiv.hide();
        }
    };
    //---------------------------------- end

    //---------------Initialize controls------------------- 
    var productivityReportInit = function () {
        var isAllowed = self.isUser();
        if (isAllowed == true || isAllowed == "true") {
            self.getReportModes([]);
            self.getYears([]);
            self.getChannels([]);
            //self.getBranches([]);
            self.getFomatTypes([]);
            self.getStatuses([]);
            showYearly();
            self.showStatus(true);
            self.showFormat(true);
            //self.showChannel(true);
            //self.showBranch(true);
          
        } else {
            self.isSummary(false);
            hideShowDiv(false);
        }

        //self.searchEAPRChannel({});
    };
    //---------------------------------- end

    //------------------showYearly---------------- 

    var showYearly = function () {
        visibilityDiv('.yearly', true);
        //visibilityDiv('.others', false);
        var selectedReport = self.selectedReport();
        self.showFormat(true);
        self.showStatus(true);
        //self.showChannel(true);
        //self.showBranch(true);
        if (selectedReport == 'PCHART') {
            self.showFormat(false);
            self.showStatus(false);
        }
        if (selectedReport == 'REJ' || selectedReport == 'OUTREJ' || selectedReport == 'INC'
            || selectedReport == 'INCRT99' || selectedReport == 'INCPDOC') {
            self.showStatus(false);
        }
        if (selectedReport == 'TOPREJ') {
            self.showFormat(false);
            self.showStatus(false);
            //self.showChannel(false);
            //self.showBranch(false);
        }

    };
    //---------------------------------- end

    //--------------showOthers-------------------- 
    var showOthers = function () {
        $("#dtOthersFrom").datepicker("setDate", -1);
        $("#dtOthersTo").datepicker("setDate", -1);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', true);
        self.showStatus(false);
        self.showFormat(true);
    };
    //---------------------------------- end

    //--get report data
    self.getReportData = function () {
        var url = '/ProductivityReport/ASPXView';
        reportHelper.getReportData(url);
    }
    //---------------------------------- end

    //-----------------set Date Range----------------- 
    var setDateRange = function () {
        var type = self.selectedReport();
        switch (type) {          
            case 'Y':
                var firstDay = new Date(self.selectedYear(), 0, 1);
                var startDate = formatDateMMddYYYY(firstDay.getDate(), firstDay.getMonth() + 1, firstDay.getFullYear());
                var lastDay = new Date(firstDay.getFullYear(), 12, 0);
                var endtDate = formatDateMMddYYYY(lastDay.getDate(), lastDay.getMonth() + 1, lastDay.getFullYear());
                self.dateFrom(startDate);
                self.dateTo(endtDate);
                break;
            case 'O':
                var dateFrom = $('#dtOthersFrom').val();
                var dateTo = $('#dtOthersTo').val();
                self.dateFrom(dateFrom);
                self.dateTo(dateTo);
                break;
            default:
                break;
        }
    };
    //---------------------------------- end

    //-- check if string if null, empty or whitespace ----------
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end


    //---View Reports-------------------------
    self.viewReports = function () {

        var options = {
            url: '',
            type: 'GET',
            data: {
                selectedYear: self.selectedYear(),
                channelCode: self.selectedChannel(),
                regionCode: self.selectedRegion(),
                districtCode: self.selectedDistrict(),
                branchCode: self.selectedBranch(),
            }
        }

        var reportType = self.selectedReport();
        switch (reportType) {
            case 'PSUMMARY':
                options.url = '/ProductivityReport/GetProductivityReportList',
                options.data.statusCode = self.selectedStatus();
                options.data.isMonthly = self.selectedFormat() == 'M' ? true : false;
                break;
            case 'PCHART':
                options.url = '/ProductivityReport/ProductivitySummaryChart'
                break;
            case 'TOPREJ':
                options.url = '/ProductivityReport/GetTopRejectedReport'
                break;
            case 'REJ':
            case 'OUTREJ':
            case 'INC' :
            case 'INCRT99' :
            case 'INCPDOC':
                var selectedStatus = '';
                if (reportType == 'REJ') {
                    selectedStatus = 'REJECTED';
                } else if (reportType == 'OUTREJ') {
                    selectedStatus = 'OUTRIGHTREJECT';
                }else{
                    selectedStatus = 'INCOMPLETE';
                }
                options.url = '/ProductivityReport/GetProductivityReasonReportList',
                options.data.statusCode = self.selectedStatus();
                options.data.isMonthly = self.selectedFormat() == 'M' ? true : false;
                options.data.reportType = self.selectedReport();
                break;

            default:
                break;
        }

        $.ajax({
            url: options.url,
            type: options.type,
            data: options.data,
            dataType: 'json',
            beforeSend: function () {
                $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });

            },
            complete: function () {
                $.unblockUI();
            },
            success: function (data) {
                //if (!data || data == '') {
                //    $.ambiance({
                //        message: 'No record found!',
                //        title: "Notificatiogn!",
                //        type: "success",
                //        fade: false
                //    });
                //} else {
                var url = "../Reports/ReportViewer.aspx";
                window.open(url);
                //}
               
            },
            error: function () {
                $.unblockUI();
            }
        });
    };
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

    //------------ get Region List ------
    self.getRegions = function () {
        var param = {
            url: '/CommissionDashboard/GetRegionList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.regionList(data);
            //self.enableRegion(true);
            if (data.length == 1) {
                self.selectedRegion(data[0].Code);
                self.enableRegion(false);
            } else {
                self.enableRegion(true);
            }
        });
    };
    //------------------------- end

    //------------ get District List ------
    self.getDistricts = function () {
        var regionCode = (typeof self.selectedRegion() !== 'undefined' ? self.selectedRegion() : '');
        var channelCode = (typeof self.selectedChannel() !== 'undefined' ? self.selectedChannel() : '');
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
            if (data.length == 1) {
                self.selectedDistrict(data[0].Code);
                self.enableDistrict(false);
            } else {
                self.enableDistrict(true);
            }
        });
    };
    //------------------------- end

    //------------ get Branch List ------
    self.getBranches = function () {
        var channelCode = (typeof self.selectedChannel() !== 'undefined' ? self.selectedChannel() : '');
        var districtCode = (typeof self.selectedDistrict() !== 'undefined' ? self.selectedDistrict() : '');

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
            if (data.length == 1) {
                self.selectedBranch(data[0].Code);
                self.enableBranch(false);
            } else {
                self.enableBranch(true);
            }
        });
    };
    //------------------------- end
    //---- onchange selected channel -----
    self.selectedChannel.subscribe(function (ch) {
        var search = ch;
        self.clearChannels();
            if (ch) {
            var channel = ko.utils.arrayFirst(self.channelList(), function (item) {
                return item.Code == search;
            });
            self.getRegions();
        }
        //}
    });
    //---- end ----------------------   

    //---- onchange selected region -----
    self.selectedRegion.subscribe(function () {
        var selectedRegion = self.selectedRegion();
        if (typeof selectedRegion == 'undefined' || selectedRegion == '') {
            self.clearDistrict();
        } else {
            self.getDistricts();
        }

    });
    //---- end ----------------------   

    //---- onchange selected district -----
    self.selectedDistrict.subscribe(function () {
        var selectedDistrict = self.selectedDistrict();
        if (typeof selectedDistrict == 'undefined' || selectedDistrict == '') {
            self.clearBranch();
        } else {
            self.getBranches();
        }
    });
    //---- end ---------------------- 

    //----Clear Region -----
    self.clearChannels = function () {
        self.regionList([]);
        self.selectedRegion('');
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
        self.selectedDistrict('');
        self.enableDistrict(false);
        self.clearBranch();
    };
    //---- end ----------------------   

    //----Clear Branch -----
    self.clearBranch = function () {
        self.branchList([]);
        self.selectedBranch('');
        self.enableBranch(false);

    };
    //---- end ----------------------






};
  





