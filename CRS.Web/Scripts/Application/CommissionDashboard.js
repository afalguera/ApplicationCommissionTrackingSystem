var vm = {};
var summarySources = [];
var detailSources = [];

//------------JS functions------------- 
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

//
function allowedDays(date) {
    // Weekend Days Sunday = 0 ... Sat =6
    if ((date.getDate() == 1 && date.getDay() != 0 && date.getDay() != 6) || date.getDay() == 1) {
        return [true, ''];
    }
    return [false, ''];
};

var formatDateMMddYYYY = function (dd, mm, yyyy) {
    var formattedDate = '';
    if (parseInt(dd) < 10) { dd = '0' + dd } if (parseInt(mm) < 10) { mm = '0' + mm }
    formattedDate = mm + '/' + dd + '/' + yyyy;
    return formattedDate;
};


function setWeeklyDate(date) {
    var dateTo = new Date(date);
    dateTo = formatDateMMddYYYY(dateTo.getDate(), dateTo.getMonth() + 1, dateTo.getFullYear());
    getNextWeek(dateTo);
};

function setInitialWeek() {
    var dateObj = new Date();
    var prevDate = dateObj.daysMoreLess(-1);
    var pDay = prevDate.getDate();
    var pMonth = prevDate.getMonth() + 1;
    var pYear = prevDate.getFullYear();
    var pFullDate = formatDateMMddYYYY(1, pMonth, pYear);
    $("#dtWeeklyFrom").val(pFullDate);
    getNextWeek(pFullDate);

};

function getNextWeek(date) {
    var nextDate = new Date(date);
    var nDay = nextDate.getDay();
    var nxtDay = 0;
    switch (nDay) {
        case 0:
            nxtDay = 7;
            break;
        case 1:
            nxtDay = 6;
            break;
        default:
            nxtDay = 14 - nDay;
            break;
    }

    nextDate = nextDate.daysMoreLess(nxtDay);
    var nFullDate = formatDateMMddYYYY(nextDate.getDate(), nextDate.getMonth() + 1, nextDate.getFullYear());
    $("#dtWeeklyTo").val(nFullDate);

};


Date.prototype.daysMoreLess =
       Date.prototype.daysMoreLess ||
       function (days) {
           days = days || 0;
           var ystrdy = new Date(this.setDate(this.getDate() + days));
           this.setDate(this.getDate() + -days);
           return ystrdy;
       };
//---------------------------------------- end

//------------Initialize jquery------------- 
$(function () {

    vm = new commDashboardViewModel();
    vm.init();
    ko.applyBindings(vm);

    //daily
    $("#dtDaily").datepicker({
        showOn: "button",
        buttonImage: "../Content/images/calendar-icon.png",
        buttonImageOnly: true,
        maxDate: '-1d'
    });
    $("#dtDaily").mask("99/99/9999");
    $("#dtDaily").datepicker("setDate", -1);
    //weekly
    $("#dtWeeklyFrom").mask("99/99/9999");
    $("#dtWeeklyTo").datepicker('disable');
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

    //inputs
    $('#txtSourceCode').filter_input({ regex: '[0-9]' });
    $('#txtApplicationNumber').filter_input({ regex: '[0-9]' });
    $('#txtName').filter_input({ regex: '[A-Za-z ]' });


    var $radios = $('input:radio[name=onlyOne]');
    if ($radios.is(':checked') === false) {
        $radios.filter('[value=true]').prop('checked', true);
    }

    $("input[type='radio']").change(function () {
        var selection = $(this).val();
    });
    setInitialWeek();
});
//------------------------- end


//-----------viewmodel--------------
var commDashboardViewModel = function () {
    var self = this;
    self.dateFrom = ko.observable();
    self.dateTo = ko.observable();
    self.dtWeeklyFrom = ko.observable();
    self.dtWeeklyTo = ko.observable();
    
    self.searchName = ko.observable();
    self.summaryList = ko.observableArray();
    self.detailList = ko.observableArray();
    self.isSummary = ko.observable(true);
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
    self.channelList = ko.observableArray([]);
    self.selectedChannel = ko.observable();
    self.enableChannel = ko.observable();

    self.regionList = ko.observableArray([]);
    self.selectedRegion = ko.observable();
    self.enableRegion = ko.observable();

    //self.areaList = ko.observableArray([]);
    //self.selectedArea = ko.observable();
    //self.enableArea = ko.observable();

    self.districtList = ko.observableArray([]);
    self.selectedDistrict = ko.observable();
    self.enableDistrict = ko.observable();

    self.branchList = ko.observableArray([]);
    self.selectedBranch = ko.observable();
    self.enableBranch = ko.observable();

    self.totalCountSummary = ko.observable();
    self.totalCountDetails = ko.observable();
    self.totalCommAmoutSummary = ko.observable();
    self.totalCommAmoutDetails = ko.observable();
    //------------------------- end

    
    //-----------Initialize--------------
    self.init = function () {
        self.allowSearch();
    };
    //------------------------- end

    //-----------Search--------------
    self.searchCommDashboard = function () {
        setDateRange();
        if (isWhitespaceNotEmpty(self.selectedChannel())) {
            $.ambiance({
                message: "Channel is required!",
                type: "error",
                fade: false
            });
            return false;
        } else {
            $.ajax({
                url: '/CommissionDashboard/GetCommissionDashboardResults',
                type: 'GET',
                data: {
                    IsReferror: self.isUser(),
                    IsSummary: self.isSummary(),
                    DateFrom: self.dateFrom(),
                    DateTo: self.dateTo(),
                    ChannelCode: self.selectedChannel(),
                    RegionCode: self.selectedRegion(),
                    //AreaCode: self.selectedArea(),
                    AreaCode: '',
                    DistrictCode: self.selectedDistrict(),
                    BranchCode: self.selectedBranch(),
                    Keyword: self.searchName()
                },
                beforeSend: function () {
                    $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });

                },
                complete: function () {
                    $.unblockUI();
                },
                success: function (data) {
                    var isSummary = self.isSummary();
                    var isUser = self.isUser();
                    if (isSummary == true || isSummary == "true") {
                        summarySources = data;
                        self.summaryList(data);
                    } else {
                        detailSources = data;
                        self.detailList(data);
                    }
                    //total count
                    var param = {
                        url: '/CommissionDashboard/GetTotalCount',
                        type: 'GET',
                        data: {},
                    };

                    bt.ajax.exec(param, function (data) {
                        if (isSummary == true || isSummary == "true") {
                            self.totalCountSummary(data.totalCount);
                            self.totalCommAmoutSummary(data.totalAmount);
                        } else {
                            self.totalCountDetails(data.totalCount);
                            self.totalCommAmoutDetails(data.totalAmount);
                        }
                    });

                  
                    showHidePrint();
                },
                error: function () {
                    $.unblockUI();
                }
            });
        }
     };
    //------------------------- end

    //-----------Radio button change ------------
    self.isSummary.subscribe(function (newValue) {
        hideShowDiv(self.isSummary());
        var isSummary = self.isSummary();
        if (summarySources.length > 0 || detailSources.length > 0) {
            if (isSummary == true || isSummary == "true") {
                self.summaryList(summarySources);
            } else {
                self.detailList(detailSources);
            }
        } else {
            self.searchCommDashboard();
        }
        showHidePrint();
    });
    //------------------------- end

    //----------Seach Validation ---------
    self.allowSearch = function () {
        var param = {
            url: '/ApplicationStatus/IsReferror',
            type: 'GET',
            data: {},
        };

        bt.ajax.exec(param, function (data) {
            self.isUser(data.isValid);
            commDashboardInit();
        });
    };
    //------------------------- end

    //----------Hide show div---------------
    var hideShowDiv = function (selection) {
        var summaryTable = $('#tblSummary');
        var detailTable = $('#tblDetail');
        var txtName = $('#divName');
        if (selection == true || selection == "true") {
            summaryTable.show();
            detailTable.hide();
            txtName.hide();
            self.searchName('');

        } else {
            summaryTable.hide();
            detailTable.show();
            txtName.show();
        }
    };
    //------------------------- end

    //----------Report module list-----
    self.getReportModes = function () {
        var reportModes = [{ Code: "D", Name: "DAILY" },
                  { Code: "W", Name: "WEEKLY" },
                  { Code: "M", Name: "MONTHLY" },
                  { Code: "Q", Name: "QUARTERLY" },
                  { Code: "Y", Name: "YEARLY" },
                  { Code: "O", Name: "OTHERS" }
        ];
        self.reportList(reportModes);
    };
    //------------------------- end

    //--------------Month list-----------
    self.getMonths = function () {
        var d = new Date();
        n = d.getMonth();
        y = d.getFullYear();

        var months = [{ Code: "1", Name: "January" },
                  { Code: "2", Name: "February" },
                  { Code: "3", Name: "March" },
                  { Code: "4", Name: "April" },
                  { Code: "5", Name: "May" },
                  { Code: "6", Name: "June" },
                  { Code: "7", Name: "July" },
                  { Code: "8", Name: "August" },
                  { Code: "9", Name: "September" },
                  { Code: "10", Name: "October" },
                  { Code: "11", Name: "November" },
                  { Code: "12", Name: "December" },
        ];
        self.monthsList(months);
        self.selectedMonth(n + 1);
    };
    //------------------------- end

    //--------------Quarter list-----------
    self.getQuarterly = function () {
        var d = new Date();
        n = d.getMonth();
        y = d.getFullYear();

        var quarter = [{ Code: "1", Name: "[Q1] Jan-Mar" },
                  { Code: "4", Name: "[Q2] Apr-Jun" },
                  { Code: "7", Name: "[Q3] Jul-Sep" },
                  { Code: "10", Name: "[Q4] Oct-Dec" },
        ];

        var quarterMonth = (Math.floor((n - 1) / 3) * 3) + 1;

        self.quarterList(quarter);
        self.selectedQuarter(quarterMonth);
    };
    //------------------------- end

    //--------------Year list-----------
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
    //------------------------- end

    //-------------Selected Report Change------------
    self.selectedReport.subscribe(function (val) {
        switch (val) {
            case 'D':
                showDaily();
                break;
            case 'W':
                showWeekly();
                break;
            case 'M':
                showMonthly();
                break;
            case 'Q':
                showQuarterly();
                break;
            case 'Y':
                showYearly();
                break;
            case 'O':
                showOthers();
                break;
            default:
                showDaily();
                break;
        }
    });
    //------------------------- end

    //-------------Div visibility------------
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
    //------------------------- end

    //-------------Initialize Controls--------------
    var commDashboardInit = function () {
        var isAllowed = self.isUser();
        if (isAllowed == true || isAllowed == "true") {
            self.getReportModes([]);
            self.getMonths([]);
            self.getYears([]);
            self.getChannels([]);
            //self.getRegions([]);
            hideShowDiv(true);
            showDaily();
        } else {
            self.isSummary(false);
            hideShowDiv(false);  
        }
            //self.searchCommDashboard({});
    };
    //------------------------- end

    //---- Show Daily --------------------
    var showDaily = function () {
        $("#dtDaily").datepicker("setDate", -1);
        visibilityDiv('.daily', true);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', false);
    };
    //------------------------- end

    //---- Show Weekly --------------------
    var showWeekly = function () {
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', true);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', false);
    };
    //------------------------- end

    //---- Show Monthly --------------------
    var showMonthly = function () {
        self.getMonths();
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', true);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', false);
    };
    //------------------------- end

    //---- Show Quarterly --------------------
    var showQuarterly = function () {
        self.getQuarterly();
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', true);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', false);
    };
    //------------------------- end

    //---- Show Yearly --------------------
    var showYearly = function () {
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', true);
        visibilityDiv('.others', false);
    };
    //------------------------- end

    //---- Show Others --------------------
    var showOthers = function () {
        $("#dtOthersFrom").datepicker("setDate", -1);
        $("#dtOthersTo").datepicker("setDate", -1);
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', true);
    };
    //------------------------- end

    //-----Print button --------------------
    self.getReportData = function () {
        var  url = '/CommissionDashboard/ASPXView';
        var isSummary = self.isSummary();
        reportHelper.getReportDataWithDetails(url, isSummary);
    };
    //------------------------- end

    //---- Set Date range --------------------
    var setDateRange = function () {
        var type = self.selectedReport();
        switch (type) {
            case 'D':
                var dtFrom = $('#dtDaily').val();
                self.dateFrom(dtFrom);
                self.dateTo(self.dateFrom());
                break;
            case 'W':
                var dtFrom = $('#dtWeeklyFrom').val();
                var dtTo = $('#dtWeeklyTo').val();
                self.dateFrom(dtFrom);
                self.dateTo(dtTo);
                break;
            case 'M':
                var selMonth = self.selectedMonth();
                var selYear = self.selectedYear();
                var firstDay = new Date(selYear, selMonth - 1, 1);
                var lastDay = new Date(selYear, selMonth, 0);
                var startDate = formatDateMMddYYYY(firstDay.getDate(), firstDay.getMonth() + 1, firstDay.getFullYear());
                var endtDate = formatDateMMddYYYY(lastDay.getDate(), lastDay.getMonth() + 1, lastDay.getFullYear());
                self.dateFrom(startDate);
                self.dateTo(endtDate);
                break;
            case 'Q':
                var firstDay = new Date(self.selectedYear(), self.selectedQuarter() - 1, 1);
                var startDate = formatDateMMddYYYY(firstDay.getDate(), firstDay.getMonth() + 1, firstDay.getFullYear());
                var quaterMonth = new Date(firstDay.setMonth(firstDay.getMonth() + 2));

                var lastDay = new Date(quaterMonth.getFullYear(), quaterMonth.getMonth() + 1, 0);
                var endtDate = formatDateMMddYYYY(lastDay.getDate(), lastDay.getMonth() + 1, lastDay.getFullYear());
                self.dateFrom(startDate);
                self.dateTo(endtDate);
                break;
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
    //------------------------- end

    //---- Hide/show print button --------------------
    var showHidePrint = function () {
        var isSummary = self.isSummary();
        var isUser = self.isUser();

        if ((isSummary == true || isSummary == "true") && (isUser == true || isUser == "true")) {
            if (typeof summarySources !== 'undefined' && summarySources.length > 0) {
                self.showPrint(true);
            } else {
                self.showPrint(false);
            }
        } else {
            if ((typeof detailSources !== 'undefined' && detailSources.length > 0) && (isUser == true || isUser == "true")) {
                self.showPrint(true);
            } else {
                self.showPrint(false);
            }
        }
    };
    //------------------------- end

    //---- Hide columns --------------------
    var hideShowDatatableColumn = function (iCol) {
        /* Get the DataTables object again - this is not a recreation, just a get of the object */
        var oTable = $('#commListDetail').dataTable();
        oTable.fnSetColumnVis(iCol, false);
    };
    //------------------------- end

   //-- check if string if null, empty or whitespace
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //------------------------- end

    //---------------get Channel List---------
    self.getChannels = function () {      
        var param = {
            url: '/CommissionDashboard/GetChannelList',
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

    //------------ get Area List ------
    //self.getAreas = function () {      
    //    var regionCode = (typeof self.selectedRegion() !== 'undefined' ? self.selectedRegion() : '');
    //    var param = {
    //        url: '/CommissionDashboard/GetAreaList',
    //        type: 'GET',
    //        data: {
    //                regionCode: regionCode
    //              }
    //    };

    //    bt.ajax.exec(param, function (data) {
    //        self.areaList(data);
    //        if (data.length == 1) {
    //            self.selectedArea(data[0].Code);
    //            self.enableArea(false);
    //        } else {
    //            self.enableArea(true);
    //        }
    //    });
    //};
    //------------------------- end

    //------------ get District List ------
    self.getDistricts = function () {
        //var areaCode = (typeof self.selectedArea() !== 'undefined' ? self.selectedArea() : '');
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
            //self.enableDistrict(true);
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
            //self.enableBranch(true);
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
        //if (typeof ch == 'undefined' || ch == '') {
        //    self.clearChannels();
        //} else {
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
            //self.clearArea();        
            self.clearDistrict();
        } else {
            //self.getAreas();
            self.getDistricts();
        }
        
    });
    //---- end ----------------------   

    //---- onchange selected district -----
    //self.selectedArea.subscribe(function () {
    //    var selectedArea = self.selectedArea();
    //    if (typeof selectedArea == 'undefined' || selectedArea == '') {
    //        self.clearDistrict();
    //    } else {
    //        self.getDistricts();
    //    }       
    //});
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
        //self.regionList([]);
        //self.selectedRegion('');
        //self.enableRegion(false);
        self.clearDistrict();
       
    };
    //---- end ----------------------   

    //----Clear Area -----
    //self.clearArea = function () {
    //    self.areaList([]);
    //    self.selectedArea('');
    //    self.enableArea(false);     
    //    self.clearDistrict();
    //};
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



