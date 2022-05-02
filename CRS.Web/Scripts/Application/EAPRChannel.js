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

//--------------Set Initial Week-------------------
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
//---------------------------------- end

//--------------Get Next Week-------------------
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
    //$("#txtWeeklyDateTo").val(nFullDate);
    $("#dtWeeklyTo").val(nFullDate);
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

    vm = new eaprChannelViewModel();
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
//---------------------------------- end

//--------------EAPR Channel ViewModel-------------------
var eaprChannelViewModel = function () {
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
    self.channelList = ko.observableArray([]);
    self.selectedChannel = ko.observable();

    self.isBranchName = ko.observable();
    self.isDistrict = ko.observable();
    //---------------------------------- end

    //---------------Initialize Page-------------------
    self.init = function () {
        self.allowSearch();
    };
    //---------------------------------- end
   

    //self.searchEAPRChannel = function () {
    //    setDateRange();
    //    $.ajax({
    //        url: '/EAPRChannel/GetEAPRChannelResults',
    //        type: 'GET',
    //        data: {
    //            DateFrom: self.dateFrom(),
    //            DateTo: self.dateTo(),
    //            SourceCode: self.sourceCode(),
    //            ChannelCode: self.selectedChannel(),
    //            ProgramCode: self.selectedProgram(),
    //            IsReferror: self.isUser(),
    //        },
    //        beforeSend: function () {
    //            $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });

    //        },
    //        complete: function () {
    //            $.unblockUI();
    //        },
    //        success: function (data) {
    //            var isUser = self.isUser();
    //                summarySources = data;
    //                self.summaryList(data);
    //            showHidePrint();
    //        },
    //        error: function () {
    //            $.unblockUI();
    //        }
    //    });

    //};

    //---------------Allow Search-------------------
    self.allowSearch = function () {
        var param = {
            url: '/ApplicationStatus/IsReferror',
            type: 'GET',
            data: {},
        };

        bt.ajax.exec(param, function (data) {
            self.isUser(data.isValid);
            eaprChannelInit();
        });       
    };
    //---------------------------------- end

    //---------------Report Types-------------------
    self.getReportModes = function () {
        var reportModes = [
                  { Code: "D", Name: "DAILY" },
                  { Code: "W", Name: "WEEKLY" },
                  { Code: "M", Name: "MONTHLY" },
                  { Code: "Q", Name: "QUARTERLY" },
                  { Code: "Y", Name: "YEARLY" },
                  { Code: "O", Name: "OTHERS" }
        ];
        self.reportList(reportModes);
    };
    //---------------------------------- end

    //---------------Months-------------------
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
        //self.selectedMonth(n + 1); //current month
        self.selectedMonth(n); //prev month
    };
    //---------------------------------- end

    //---------------Quarterly-------------------
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
    //---------------------------------- end

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
                showMonthly();
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
    var eaprChannelInit = function () {
        var isAllowed = self.isUser();      
        if (isAllowed == true || isAllowed == "true") {            
            self.getReportModes([]);
            //self.getMonths([]);
            self.getYears([]);
            self.getChannels([]);
            //self.selectedReport('M');
            showDaily();
            //showMonthly();
            //setDateRange();
        } else {
            self.isSummary(false);
            hideShowDiv(false);
        }

        //self.searchEAPRChannel({});
    };
    //---------------------------------- end

    //---------------showDaily------------------- 
    var showDaily = function () {
        $("#dtDaily").datepicker("setDate", -1);
        visibilityDiv('.daily', true);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', false);
    };
    //---------------------------------- end

    //---------------showWeekly------------------- 
    var showWeekly = function () {
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', true);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', false);
    };
    //---------------------------------- end

    //---------------showMonthly------------------- 
    var showMonthly = function () {
        self.getMonths();
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', true);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', false);
    };
    //---------------------------------- end

    var showQuarterly = function () {
        self.getQuarterly();
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', true);
        visibilityDiv('.yearly', false);
        visibilityDiv('.others', false);
    };
    //------------------showYearly---------------- 

    var showYearly = function () {
        visibilityDiv('.daily', false);
        visibilityDiv('.weekly', false);
        visibilityDiv('.monthly', false);
        visibilityDiv('.quarterly', false);
        visibilityDiv('.yearly', true);
        visibilityDiv('.others', false);
    };
    //---------------------------------- end

    //--------------showOthers-------------------- 
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
    //---------------------------------- end

   //--get report data
    self.getReportData = function () {
        var url = '/EAPRChannel/ASPXView';
        reportHelper.getReportDataWithDetails(url);
    }
    //---------------------------------- end

    //-----------------set Date Range----------------- 
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
    //---------------------------------- end

    //-- check if string if null, empty or whitespace ----------
    var isWhitespaceNotEmpty = function (str) {
        return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
    }
    //---------------------------------- end

    //-- get Channel List --------------------------------------
    self.getChannels = function () {
        var param = {
            url: '/CommissionDashboard/GetChannelList',
            type: 'GET',
            data: {}
        };

        bt.ajax.exec(param, function (data) {
            self.channelList(data);
            if (data.length == 1) {
                self.selectedChannel(data[0].Code);
                $("#ddChannel").prop("disabled", true);
            }
        });
    };
    //---------------------------------- end
   
    //---View EAPR Channel-------------------------
    self.viewEAPRChannelForm = function () {
        setDateRange();
        var eaprChannel = self.selectedChannel();
        var dateFrom = self.dateFrom();
        var dateTo = self.dateTo();
        var reportType = self.selectedReport();
        var isPerBranch = self.isBranchName();
        var isDistrict = self.isDistrict();

        if (isWhitespaceNotEmpty(self.selectedChannel())) {
            $.ambiance({
                message: "Channel is required!",
                type: "error",
                fade: false
            });
            return false;
        } else {
            $.ajax({
                url: '/EAPRChannel/ViewEAPRChannelForm',
                type: 'GET',
                data: {
                    dateFrom: dateFrom,
                    dateTo: dateTo,
                    eaprChannel: eaprChannel,
                    reportType: reportType,
                    isPerBranch: isPerBranch,
                    isDistrict: isDistrict
                },
                beforeSend: function () {
                    $.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });

                },
                complete: function () {
                    $.unblockUI();
                },
                success: function (data) {
                    if (!data || data == '') {
                        $.ambiance({
                            message: 'No record found!',
                            title: "Notification!",
                            type: "success",
                            fade: false
                        });
                    } else {
                        var url = "../Reports/ReportViewer.aspx";
                        window.open(url);
                    }
                },
                error: function () {
                    $.unblockUI();
                }
            });
        }

       

          
    };
    //---------------------------------- end

    //---View EAPR Channel-------------------------
    self.selectedChannel.subscribe(function (val) {
        var search = val;

        if (typeof val === 'undefined' || val == 'undefined') {
            return false;
        }

        var selChannel = ko.utils.arrayFirst(self.channelList(), function (item) {
            return item.Code == search;
        });      
        self.isBranchName(selChannel.IsBranchName);
        self.isDistrict(selChannel.IsDistrict);
    });
    //---------------------------------- end

};



