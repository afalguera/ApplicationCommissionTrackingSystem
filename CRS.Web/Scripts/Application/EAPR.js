var vm = {};

$(function () {
	vm = new eaprViewModel();
	vm.init();   
	ko.applyBindings(vm);
	$('#eapTableList').dataTable();
	$('#txtExpenseAmount').autoNumeric('init', { vMax: '999999999999.99' });
	$('#txtBudgetAllocation').autoNumeric('init', { vMax: '999999999999.99' });
	$('#eaprdialog-confirm').hide();
	$("#eaprdialog-form").dialog({
		autoOpen: false,
		height: 2500,
		width: 1200,
		position: "fixed",
        draggable: false,
		modal: true  
	});
	//$(".ui-dialog").css("position", "fixed");
	//$(".ui-dialog").css("overflow", "auto");
	//dtExpense
	$("#dtExpense").datepicker({
		showOn: "button",
		buttonImage: "../Content/images/calendar-icon.png",
		buttonImageOnly: true,
		dateFormat: 'mm/dd/yy',
		maxDate: '0d'
	});
	$("#dtExpense").mask("99/99/9999");
	$(".ui-dialog").css("position", "fixed");
	//$('#tblSearchPopup').dataTable();
	//$("#eaprdialog-search").dialog({
	//	autoOpen: false,
	//	height: 500,
	//	width: 500,
	//	position: "fixed",
	//	modal: true
	//});
});

	//--viewmodel
    var eaprViewModel = function () {
    //---------------------------------- viewmodel
		var self = this;
		self.mode = ko.observable();
		self.eaprId = ko.observable();
		self.controlNo = ko.observable();
		self.expenseDate = ko.observable();
		self.expenseAmount = ko.observable();
		self.amountInWords = ko.observable();
		self.payeeName = ko.observable();
		self.payeeTin = ko.observable();
		self.originatingDepartment = ko.observable();
		self.departmentCode = ko.observable();
		self.remarks = ko.observable();
		self.budgetAllocation = ko.observable();
		//self.isWithinBudget = ko.observable(true);
		//self.isExceedsBudget = ko.observable(false);
		//self.isNotApprBudget = ko.observable(false);
		self.userBy = ko.observable();

		self.selectedExpenseCategory = ko.observable();
		self.expenseCategoryCode = ko.observable();
		self.expenseCategoryName = ko.observable();
		self.expenseCategoryList = ko.observableArray([]);

		self.selectedItems = ko.observableArray([]);
		self.requestedTitle = ko.observable("");
		self.requestedItems = ko.observableArray([]);
		self.requestedName = ko.observable("");

		self.checkerTitle = ko.observable("");
		self.checkerItems = ko.observableArray([]);
		self.checkerName = ko.observable("");

		self.notedByTitle = ko.observable("");
		self.notedByItems = ko.observableArray([]);
		self.notedByName = ko.observable("");

		self.approverName = ko.observable("");
		self.approverTitle = ko.observable("");
		self.approverItems = ko.observableArray([]);

		self.additionalWithinApprovers = ko.observableArray([]);
		self.additionalNotOrExceedsApprovers = ko.observableArray([]);
		self.additionalBudgetApprovers = ko.observableArray([]);
		
		//search params
		self.dateFrom = ko.observable();
		self.dateTo = ko.observable();
		self.searchControlNo = ko.observable();
		self.searchPayeeName = ko.observable();
		self.searchPayeeTin = ko.observable();
		self.searchOriginatingDepartment = ko.observable();
		self.searchDepartmentCode = ko.observable();
		self.searchExpenseCategoryCode = ko.observable();
		self.eaprList = ko.observableArray([]);
		self.showPrint = ko.observable();

		self.paymentList = ko.observableArray();
		self.selectedPayee = ko.observable();
		self.departmentList = ko.observableArray();
		self.selectedDepartment = ko.observable();
		self.signatoryList = ko.observableArray();

		self.requestedByOptionList = ko.observableArray();
		self.requestedByTitleList = ko.observableArray();
		self.requestedByNameList = ko.observableArray();
		self.selectedRequestedByTitle = ko.observable();
		self.selectedRequestedByName = ko.observable();

		self.checkedByOptionList = ko.observableArray();
		self.checkedByTitleList = ko.observableArray();
		self.checkedByNameList = ko.observableArray();
		self.selectedCheckByTitle = ko.observable();
		self.selectedCheckByName = ko.observable();
		
		self.notedByOptionList = ko.observableArray();
		self.notedByTitleList = ko.observableArray();
		self.notedByNameList = ko.observableArray();
		self.selectedNotedByTitle = ko.observable();
		self.selectedNotedByName = ko.observable();

		self.approvedByOptionList = ko.observableArray();
		self.approvedByTitleList = ko.observableArray();
		self.approvedByNameList = ko.observableArray();
		self.selectedApprovedByTitle = ko.observable();
		self.selectedApprovedByName = ko.observable();
		self.totalCountSummary = ko.observable();
	    //---------------------------------- end


		//--add requestedby item
		self.addRequestedByItem = function () {
		    self.addSignatoryItem('R',
                                  self.selectedRequestedByTitle(),
                                  self.selectedRequestedByName(),
                                  $("#ddReqTitle option:selected").text(),
                                  self.requestedByOptionList()
                                  );
		};
        //---------------------------------- end

		//--add checker item
		self.addCheckerItem = function () {
		    self.addSignatoryItem('C',
                                self.selectedCheckByTitle(),
                                self.selectedCheckByName(),
                                $("#ddCheckTitle option:selected").text(),
                                self.checkedByOptionList()
                                );
		};
        //---------------------------------- end

		//--add notedby item
		self.addNotedByItem = function () {
		    self.addSignatoryItem('N',
                                self.selectedNotedByTitle(),
                                self.selectedNotedByName(),
                                $("#ddNotedTitle option:selected").text(),
                                self.notedByOptionList()
                                );			
		};
        //---------------------------------- end

		//--add approver item
		self.addApproverItem = function () {
			
		    self.addSignatoryItem('A',
                                self.selectedApprovedByTitle(),
                                self.selectedApprovedByName(),
                                $("#ddApproveTitle option:selected").text(),
                                self.approvedByOptionList()
                                );
		};
        //---------------------------------- end

		//self.removeSelected = function () {
		//    self.allItems.removeAll(self.selectedItems());
		//    self.selectedItems([]); // Clear selection
		//};

		//self.sortItems = function () {
		//    self.allItems.sort();
		//};

		//--remove requestedby item
		self.removeRequestedByItem = function (itemDeleted) {
		    //self.requestedItems.remove(itemDeleted);
		    self.removeSignatoryItem('R', itemDeleted);
		};
        //---------------------------------- end


		//--remove checker item
		self.removeCheckerItem = function (itemDeleted) {
		    //self.checkerItems.remove(itemDeleted);
		    self.removeSignatoryItem('C', itemDeleted);
		};
        //---------------------------------- end

		//--remove notedby item
		self.removeNotedByItem = function (itemDeleted) {
		    //self.notedByItems.remove(itemDeleted);
		    self.removeSignatoryItem('N', itemDeleted);
		};
        //---------------------------------- end

		//--remove approver item
		self.removeApproverItem = function (itemDeleted) {
		    //self.approverItems.remove(itemDeleted);
		    self.removeSignatoryItem('A', itemDeleted);
		};
        //---------------------------------- end

		//--clear page
		self.clearEAPR = function () {
			self.initializeCreate();
		};
        //---------------------------------- end


		//-- Initialize page
		self.init = function () {
			self.initializeSearchParams();
			self.getBudgetAllocationApprover('W');
		    //self.getBudgetAllocationApprover('NE');
			self.getExpenseCategories();
			self.showPrint(false);
			self.getPaymentList();
			self.getDepartmentList();
			self.getSignatoriesList();		  
		};
        //---------------------------------- end

		//--initialize insert
		self.initializeCreate = function () {
			self.controlNo('');
			self.setFocusToControlNo();
			self.initializeExpenseDate();
			self.expenseAmount('');
			self.amountInWords('');
			//self.payeeName('');
			//self.payeeTin('');
			//self.originatingDepartment('');
			//self.departmentCode('');
			self.remarks('');
			self.getExpenseCategories();
			self.budgetAllocation('');
			//self.initializeExpenseCheckbox();
			//self.requestedItems([]);
			//self.getInitialCheckers();
			//self.getInitialNotedBy();
			//self.getInitialApprovers();
			self.getInitialExpenseCategory();
			self.requestedByOptionList([]);
			self.checkedByOptionList([]);
			self.notedByOptionList([]);
			self.approvedByOptionList([]);
			self.additionalBudgetApprovers([]);
		};
        //---------------------------------- end

		//-- check if string if null, empty or whitespace
		var isWhitespaceNotEmpty = function (str) {
			return (!str || 0 === str.length) && (!str || /^\s*$/.test(str));
		}
        //---------------------------------- end

		//--populate initial checker values
		self.getInitialCheckers = function () {
			var checkers = [{ checkerTitle: "Sales Admin & Control", checkerName: "HEVergara" }
			];
			self.checkerItems(checkers);
		};
        //---------------------------------- end

		//--populate initial checker values
		self.getInitialNotedBy = function () {
			var notedby = [{ notedByTitle: "Sales Support Service Head", notedByName: "MBParpan" }
			];
			self.notedByItems(notedby);
		};
        //---------------------------------- end

		//--populate initial approver values
		self.getInitialApprovers = function () {
			var approvers = [{ approverTitle: "Card Sales & Dist Head", approverName: "ECSantiago" }
			];
			self.approverItems(approvers);
		};
        //---------------------------------- end

		//--on enter event amount in words
		self.searchKeyboardCmd = function (data, event) {
			event.preventDefault();
			var keyCode = (event.which ? event.which : event.keyCode);
			if (keyCode === 13) {
				if (!isWhitespaceNotEmpty(self.expenseAmount())) {
					convertAmountToWords();
				} else {
					self.amountInWords("");
				}
				return false;
			}
			return true;
		};
        //---------------------------------- end

		//--on blur event amount in words
		self.blurConvert = function () {
			if (!isWhitespaceNotEmpty(self.expenseAmount())) {
				convertAmountToWords();
			} else {
				self.amountInWords("");
			}
		};
        //---------------------------------- end

		//--on enter event budget allocation
		self.budgetAllocationCmd = function (data, event) {
			event.preventDefault();
			var keyCode = (event.which ? event.which : event.keyCode);
			if (keyCode === 13) {
				if (!isWhitespaceNotEmpty(self.budgetAllocation())) {
					self.getAdditionalBudgetApprovers();
				} 
				return false;
			}
			return true;
		};
        //---------------------------------- end

		//--on blur event budget allocation
		self.blurBudgetAllocation = function () {
			if (!isWhitespaceNotEmpty(self.budgetAllocation())) {
				self.getAdditionalBudgetApprovers();
			} else {
				self.additionalBudgetApprovers([]);
			}
		};
        //---------------------------------- end

		//--converting amount to words
		var convertAmountToWords = function () {
		    var amt = self.expenseAmount();

		    if (amt == '0') {
		        self.expenseAmount('');
		        self.amountInWords('');
		    }else{
		        var param = {
		            url: '/EAPR/GetAmountInWords',
		            type: 'GET',
		            data: { strAmount: amt },
		        };

		        bt.ajax.exec(param, function (data) {
		            self.amountInWords(data.amount);
		            self.getAdditionalBudgetApprovers();
		        });
		    }

			
		};
        //---------------------------------- end

		//--create EAPR item
		self.crudEAPR = function () {
			var bAllow = false;

			if (self.mode() == 'DELETE') {
				bAllow = true;
			} else {
				bAllow = self.validateEAPR();
			}

			if (bAllow == true || bAllow == "true") {
				var eparam = getEAPRParams();
				$.ajax({
					url: '/EAPR/EAPRCRUD',
					type: 'POST',
					data: JSON.stringify(eparam),
					contentType: 'application/json',
					beforeSend: function () {
						$.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Saving...</h4>' });

					},
					complete: function () {
						$.unblockUI();
					},
					success: function (data) {
						var bSuccess = data.success;
						if (bSuccess == true || bSuccess == "true") {
							var strMessage = '';
							if (self.mode() == 'DELETE') {
								strMessage = "Record successfully deleted!";
							} else {
								strMessage =  "Record successfully saved!";
							}

							$.ambiance({
								message: strMessage,
								title: "Success!",
								type: "success"
							});
							self.init();
							self.cancelEAPR();
						} else {
							$.ambiance({
								message: "Error saving record!",
								type: "error",
								fade: false
							});
						}
					},
					error: function () {
						$.unblockUI();
					}
				});

			} else {
				return false;
			}
		};
        //---------------------------------- end

		//--get eapr parameters
		var getEAPRParams = function () {
			var expenseCategoryCode = self.selectedExpenseCategory();
			//var isWithinBudget = $("#winCheck").is(':checked');
			//var isExceedsBudget = $("#exceedCheck").is(':checked');
			//var isNotApprBudget = $("#notinCheck").is(':checked');
			var mode = self.mode();
			var eaprParam =
			{
				Mode: mode,
				EaprId: (mode != 'Insert' ? self.eaprId() : 0),
				ControlNo: self.controlNo(),
				PaymentDate: self.expenseDate(),
				ExpenseAmount: self.expenseAmount(),
				PayeeName: $("#ddPayee option:selected").text(),
				PayeeTin: self.payeeTin(),
				OriginatingDepartment: $("#ddDepartment option:selected").text(),
				DepartmentCode: self.departmentCode(),
				Description: self.remarks(),
				ExpenseCategoryCode: expenseCategoryCode,
				BudgetAllocation: self.budgetAllocation(),
				//WithApprBudget: self.isWithinBudget(),
				//ExceedsApprBudget: self.isExceedsBudget(),
				//NotInApprBudget: self.isNotApprBudget(),
				RequestedBy: self.requestedByOptionList(),
				CheckedBy: self.checkedByOptionList(),
				NotedBy: self.notedByOptionList(),
				ApprovedBy: self.approvedByOptionList(),
				AdditionalApprovedBy: self.additionalBudgetApprovers()
			};

			return eaprParam;
		};
        //---------------------------------- end

		//--get expense categories
		self.getExpenseCategories = function () {
			var param = {
				url: '/EAPR/GetExpenseCategory',
				type: 'GET',
				data: {}
			};
			bt.ajax.exec(param, function (data) {
				self.expenseCategoryList(data);
			});
		};
		//-- Get Additional Budget Approver based on budget
		self.getBudgetAllocationApprover = function (budgetType) {
			var param = {
				url: '/EAPR/GetBudgetAllocationApprover',
				type: 'GET',
				data: { budgetType: budgetType}
			};

			bt.ajax.exec(param, function (data) {
				if (budgetType == 'W') {
					self.additionalWithinApprovers(data);
				} else {
					self.additionalNotOrExceedsApprovers(data);
				}
			});
		};
        //---------------------------------- end

		//-- Set focus on the first element
		self.setFocusToControlNo = function () {
			$('#txtControlNo').focus();
		}
        //---------------------------------- end

		//-- initialize datepickers
		self.initializeExpenseDate = function () {
			$("#dtExpense").datepicker("setDate", new Date());
			var d = new Date();
			var getCurrDate = formatDateMMddYYYY(d.getDate(), d.getMonth()+1, d.getFullYear());
			self.expenseDate(getCurrDate);
		};
        //---------------------------------- end

		//-- Validate before saving
		self.validateEAPR = function () {
			var strMessage = '';
			var ctr = 0;

			if (isWhitespaceNotEmpty(self.expenseDate())) {
				ctr++;
				strMessage += ctr.toString() + '. Date is required.' + '<br/>';
			}

			if (isWhitespaceNotEmpty(self.expenseAmount())) {
				ctr++;
				strMessage += ctr.toString() + '. Expense Amount is required.' + '<br/>';
			}

			if (isWhitespaceNotEmpty(self.selectedPayee())) {
				ctr++;
				strMessage += ctr.toString() + '. Payee Name is required.' + '<br/>';
			}

			if (isWhitespaceNotEmpty(self.payeeTin())) {
				ctr++;
				strMessage += ctr.toString() + '. Tin Of Payee is required.' + '<br/>';
			}

			if (isWhitespaceNotEmpty(self.selectedDepartment())) {
				ctr++;
				strMessage += ctr.toString() + '. Originating Department is required.' + '<br/>';
			}

			if (isWhitespaceNotEmpty(self.departmentCode())) {
				ctr++;
				strMessage += ctr.toString() + '. Department Code is required.' + '<br/>';
			}

			if (isWhitespaceNotEmpty(self.remarks())) {
				ctr++;
				strMessage += ctr.toString() + '. Description is required.' + '<br/>';
			}

			if (isWhitespaceNotEmpty(self.selectedExpenseCategory())) {
				ctr++;
				strMessage += ctr.toString() + '. Expense Category is required.' + '<br/>';
			}

			//if (isWhitespaceNotEmpty(self.budgetAllocation())) {
			//	ctr++;
			//	strMessage += ctr.toString() + '. Budget Allocation is required.' + '<br/>';
			//}

			if (self.requestedByOptionList().length < 1) {
				ctr++;
				strMessage += ctr.toString() + '. Requested By is required.' + '<br/>';
			}

			if (self.checkedByOptionList().length < 1) {
				ctr++;
				strMessage += ctr.toString() + '. Checked By is required.' + '<br/>';
			}

			if (self.notedByOptionList().length < 1) {
				ctr++;
				strMessage += ctr.toString() + '. Noted By is required.' + '<br/>';
			}

			//if (self.approvedByOptionList().length < 1) {
			//	ctr++;
			//	strMessage += ctr.toString() + '. Approved By is required.' + '<br/>';
			//}

			if (!isWhitespaceNotEmpty(strMessage)) {
				var iTimeOut = parseInt(ctr) / 2;
				$.ambiance({
					message: strMessage,
					type: "custom",
					title: "Error Notification!",
					timeout: iTimeOut
				});
				return false;
			}
			return true;
		};
        //---------------------------------- end

		//--Format date (mm/dd/yyyy)
		var formatDateMMddYYYY = function (dd, mm, yyyy) {
			var formattedDate = '';
			if (parseInt(dd) < 10) { dd = '0' + dd } if (parseInt(mm) < 10) { mm = '0' + mm }
			formattedDate = mm + '/' + dd + '/' + yyyy;
			return formattedDate;
		};
        //---------------------------------- end

		//--Print EAPR
		self.viewEAPRForm = function (item) {
			var eaprId = item.EaprId;
			self.eaprId(eaprId);
			var param = {
				url: '/EAPR/ViewEAPRForm',
				type: 'GET',
				data: {eaprId: eaprId }
			};

			bt.ajax.exec(param, function (data) {
				var url = "../Reports/ReportViewer.aspx";
				window.open(url);
			});
		};
        //---------------------------------- end

		//--Get additional approvers
		self.getAdditionalBudgetApprovers = function () {
			//var budget = self.budgetAllocation() != '' ? parseFloat(removeCommas(self.budgetAllocation())) : 0;
			var budget = self.expenseAmount() != '' ? parseFloat(removeCommas(self.expenseAmount())) : 0;
			//var isWithinBudget = $("#winCheck").is(':checked');
			self.additionalBudgetApprovers([]);
			var minAmount;
			var arrAddlApprovers;

			//if (isWithinBudget == true || isWithinBudget == "true") {
				arrAddlApprovers = self.additionalWithinApprovers();
			//}
			//else{
			//	arrAddlApprovers = self.additionalNotOrExceedsApprovers();
			//}
			ko.utils.arrayForEach(arrAddlApprovers, function (feature) {
				minAmount = parseFloat(feature.ApproverAmountLower);
				if (budget >= minAmount) {
					self.additionalBudgetApprovers.push({
						ApproverTitle: feature.ApproverTitle,
						ApproverName: feature.ApproverName,
						Remarks: feature.Remarks
					});
				}
			});
		};
        //---------------------------------- end

		//-- remove commas
		var removeCommas = function (str) {
			return (str.replace(/,/g, ''));
		};

		//--search EARP List
		self.searchEAPRList = function () {

			$.ajax({
				url: '/EAPR/GetEAPRResults',
				type: 'GET',
				data: {
					DateFrom: self.dateFrom(),
					DateTo: self.dateTo(),
					ControlNo: self.searchControlNo(),
					PayeeName: self.searchPayeeName(),
					PayeeTin: self.searchPayeeTin(),
					OriginatingDepartment: self.searchOriginatingDepartment(),
					DepartmentCode: self.searchDepartmentCode(),
					ExpenseCategoryCode: self.searchExpenseCategoryCode()
				},
				beforeSend: function () {
					$.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });

				},
				complete: function () {
					$.unblockUI();
				},
				success: function (data) {
				    self.eaprList(data);

				    //total
				    var param = {
				        url: '/EAPR/GetTotalCount',
				        type: 'GET',
				        data: {},
				    };

				    bt.ajax.exec(param, function (data) {
			            self.totalCountSummary(data.totalCount);
				    });

					if (data.length > 0) {
						self.showPrint(true);
					} else {
						self.showPrint(false);
					}
				},
				error: function () {
					$.unblockUI();
				}
			});
		};
        //---------------------------------- end

		//--initialize search params
		self.initializeSearchParams = function () {
			//dateFrom
			$("#dtDateFrom").datepicker({
				showOn: "button",
				buttonImage: "../Content/images/calendar-icon.png",
				buttonImageOnly: true,
				dateFormat: 'mm/dd/yy',
				maxDate: '0d'
			});
			$("#dtDateFrom").mask("99/99/9999");
			$("#dtDateFrom").datepicker("setDate", new Date());

			$("#dtDateTo").datepicker({
				showOn: "button",
				buttonImage: "../Content/images/calendar-icon.png",
				buttonImageOnly: true,
				dateFormat: 'mm/dd/yy',
				maxDate: '0d'
			});
			$("#dtDateTo").mask("99/99/9999");
			$("#dtDateTo").datepicker("setDate", new Date());
			var d = new Date();
			var getCurrDate = formatDateMMddYYYY(d.getDate(), d.getMonth() + 1, d.getFullYear());
	
			self.dateFrom(getCurrDate);
			self.dateTo(getCurrDate);
			self.searchControlNo('');
			self.searchPayeeName('');
			self.searchPayeeTin('');
			self.searchOriginatingDepartment('');
			self.searchDepartmentCode('');
			self.searchExpenseCategoryCode('');
			self.searchEAPRList();
		};
        //---------------------------------- end


		//get initial expense category
		self.getInitialExpenseCategory = function () {
			var selCategory = $('#ddExpenseCategory').val();
			self.selectedExpenseCategory(selCategory);
		}

        //--get report data
		self.getReportData = function () {
		    var url = '/EAPR/ASPXView';
		    reportHelper.getReportDataWithDetails(url);
		}		
        //---------------------------------- end

		//-- show eapr form
		self.showEAPRForm = function (item) {
			self.initializeCreate();
			self.mode('INSERT');
			$("#eaprdialog-form").dialog("open");
		};
        //---------------------------------- end

		//--cancel eapr
		self.cancelEAPR = function () {
			$('#eaprdialog-form').dialog("close");
		};

		//--edit eapr
		self.updateEAPR = function (item) {
			self.mode('UPDATE');
			var eaprId = item.EaprId;
			self.eaprId(eaprId);
			$("#eaprdialog-form").dialog("open");
			self.getEAPRItem(eaprId); 
		};
        //---------------------------------- end

		//--delete eapr
		self.deleteEAPR = function (item) {
			self.mode('DELETE');
			var eaprId = item.EaprId;
			self.eaprId(eaprId);
			$("#eaprdialog-confirm").dialog({
				autoOpen: false,
				resizable: false,
				height: 140,
				modal: true,
				buttons: {
					"Delete": function () {
						self.crudEAPR();
						$(this).dialog("close");
					},
					Cancel: function () {
						$(this).dialog("close");
					}
				}
			});
			$("#eaprdialog-confirm").dialog("open");
		};
        //---------------------------------- end

        //------------------ get eapr item by Id
		self.getEAPRItem = function (eaprId) {
			$.ajax({
				url: '/EAPR/GetEAPRItem',
				type: 'GET',
				data: {
					eaprId: eaprId
				},
				beforeSend: function () {
					$.blockUI({ message: '<h4><img src="../Content/images/ajax-loader.gif" /><br/> Just a moment...</h4>' });

				},
				complete: function () {
					$.unblockUI();
				},
				success: function (data) {
					self.getExpenseCategories();
					//var within = data[0].WithApprBudget;
					//var exceed = data[0].ExceedsApprBudget;
					//var notin = data[0].NotInApprBudget;
					var selExpCat = data[0].ExpenseCategoryCode;
					var requestedItem = data[0].RequestedBy;
					var checkItem = data[0].CheckedBy;
					var notedItem = data[0].NotedBy;
					var approveItem = data[0].ApprovedBy;
					var additionalItem = data[0].AdditionalApprovedBy;
					self.controlNo(data[0].ControlNo);
					self.expenseDate(data[0].PaymentDateString);
					self.expenseAmount(data[0].ExpenseAmountString);
					self.amountInWords(data[0].ExpenseAmountInWords);
					self.payeeName(data[0].PayeeName);
					self.payeeTin(data[0].PayeeTin);
					self.originatingDepartment(data[0].OriginatingDepartment);
					self.departmentCode(data[0].DepartmentCode);
					self.remarks(data[0].Description);
					self.budgetAllocation(data[0].BudgetAllocationString);
					self.selectedExpenseCategory(selExpCat);
					//$("#winCheck").prop("checked", within);
					//$("#exceedCheck").prop("checked", exceed);
					//$("#notinCheck").prop("checked", notin);
					$('#term').val(self.remarks().replace(/<br\s*[\/]?>/gi, "\n"));
					self.requestedByOptionList(requestedItem == null ? [] : requestedItem);
				    self.checkedByOptionList(checkItem == null ? [] : checkItem);
				    self.notedByOptionList(notedItem == null ? [] : notedItem);
				    self.approvedByOptionList(approveItem == null ? [] : approveItem);
					//self.requestedItems(requestedItem);
					//self.checkerItems(checkItem == null ? [] : checkItem);
					//self.notedByItems(notedItem == null ? [] : notedItem);
					//self.approverItems(approveItem == null ? [] : approveItem);
					//self.additionalBudgetApprovers(additionalItem == null ? [] : additionalItem);
					self.getAdditionalBudgetApprovers();

				},
				error: function () {
					$.unblockUI();
				}
			});
		};
        //---------------------------------- end

		//-- get popuplist
		//self.getPopupList = function () {
		//    var list = [
		//                    { Code: "FI", Name: "Finance" },
		//                    { Code: "ACCT", Name: "Accounting" },
		//                    { Code: "HR", Name: "Human Resources" },
		//    ];
		//    self.popupList(list);
		//};
		////-- get selected row
		//self.getSelectedEAPRSearchRow = function (item, event) {
		//    var oTable = $('#tblSearchPopup').dataTable();
		//    self.addedPopList.push({ Id: item.Id, Code: item.Code, Name: item.Name });
		//    var row = oTable.fnGetData(event.target.parentNode);
		//    //var rowIndex = oTable.fnGetPosition($(this).closest('tr')[0]);
		//    self.popupList.remove(item);
		  
		//    oTable.fnDraw();
		//    $("#tblSearchPopup").dataTable().fnDraw();
		//};
		////--remove from added list and return to poplist
		//self.removeSelectedEAPRSearchRow = function (itemDeleted) {
		//    self.popupList.push({ Id: itemDeleted.Id, Code: itemDeleted.Code, Name: itemDeleted.Name });
		//    self.addedPopList.remove(itemDeleted);
			
		//};

		//------ get payment list -------------
		self.getPaymentList = function () {
			var param = {
				url: '/EAPR/GetPaymentList',
				type: 'GET',
				data: {},
			};

			bt.ajax.exec(param, function (data) {
				self.paymentList(data);
			});
		};
		//---------------------------------- end
		
		//------ get department list --------
		self.getDepartmentList = function () {
			var param = {
				url: '/EAPR/GetDepartmentList',
				type: 'GET',
				data: {},
			};

			bt.ajax.exec(param, function (data) {
				self.departmentList(data);
			});
		};
		//---------------------------------- end

		//------ get department list --------
		self.getSignatoriesList = function () {
			var param = {
				url: '/EAPR/GetSignatoriesList',
				type: 'GET',
				data: {},
			};

			bt.ajax.exec(param, function (data) {
				self.signatoryList(data);
				var arrSignatories = self.signatoryList();
				self.getSignatoriesByTypeOrCode('R', arrSignatories);
				self.getSignatoriesByTypeOrCode('C', arrSignatories);
				self.getSignatoriesByTypeOrCode('N', arrSignatories);
				self.getSignatoriesByTypeOrCode('A', arrSignatories);
			});
		};
		//---------------------------------- end

		//---- onchange selected payment ---
		self.selectedPayee.subscribe(function () {
			var search = self.selectedPayee();
			var pTin = ko.utils.arrayFirst(self.paymentList(), function (item) {
				return item.Code == search;
			});

			self.payeeTin(pTin.Tin);
		});
		//---------------------------------- end

		//------ onchange selected department
		self.selectedDepartment.subscribe(function () {
			var search = self.selectedDepartment();
			var dept = ko.utils.arrayFirst(self.departmentList(), function (item) {
				return item.Code == search;
			});

			self.departmentCode(dept.Code);
		});
		//---------------------------------- end

		//------ get signatories by code or type
		self.getSignatoriesByTypeOrCode = function (pTypeOrCode, arrSignatories) {
			var arrSignatories = ko.utils.arrayFilter(arrSignatories, function (item) {
				return item.PositionType == pTypeOrCode;
			});

			var uniqueVal = bt.util.getUniqueList(arrSignatories, 'Code');

			switch (pTypeOrCode) {
				case 'R':
					self.requestedByTitleList(uniqueVal);
					break;
				case 'C':
					self.checkedByTitleList(uniqueVal);
					break;
				case 'N':
					self.notedByTitleList(uniqueVal);
					break;
				case 'A':
					self.approvedByTitleList(uniqueVal);
					break;
				default:
					break;
			}
		};
		//---------------------------------- end

		//------ onchange selected requested by
		self.selectedRequestedByTitle.subscribe(function () {
			var search = self.selectedRequestedByTitle();
			var req = ko.utils.arrayFilter(self.signatoryList(), function (item) {
				return item.Code == search;
			});

			self.requestedByNameList(req);
		});
		//---------------------------------- end

		//------ onchange selected checked by
		self.selectedCheckByTitle.subscribe(function () {
			var search = self.selectedCheckByTitle();
			var chk = ko.utils.arrayFilter(self.signatoryList(), function (item) {
				return item.Code == search;
			});

			self.checkedByNameList(chk);
		});
		//---------------------------------- end

		//------ onchange selected noted by
		self.selectedNotedByTitle.subscribe(function () {
			var search = self.selectedNotedByTitle();
			var noted = ko.utils.arrayFilter(self.signatoryList(), function (item) {
				return item.Code == search;
			});

			self.notedByNameList(noted);
		});
		//---------------------------------- end

		//------ onchange selected noted by
		self.selectedApprovedByTitle.subscribe(function () {
			var search = self.selectedApprovedByTitle();
			var appr = ko.utils.arrayFilter(self.signatoryList(), function (item) {
				return item.Code == search;
			});

			self.approvedByNameList(appr);
		});
	    //---------------------------------- end

	    //------------------------ add Signatory
		self.addSignatoryItem = function (itemType, itemCode, itemName, itemPositionName, arrItems) {
		    var match = ko.utils.arrayFirst(arrItems, function (item) {
		        return item.PositionName == itemPositionName && item.Name == itemName;
		    });

		    if (!isWhitespaceNotEmpty(itemCode) && !isWhitespaceNotEmpty(itemName)
				&& (match == null)) {

		        switch (itemType) {
		            case 'R':
		               // var ddText =   $("#ddReqTitle option:selected").text();
		                self.requestedByOptionList.push({ Code: itemCode, Name: itemName, PositionName: itemPositionName });
		                break;
		            case 'C':
		               // var ddText = $("#ddCheckTitle option:selected").text();
		                self.checkedByOptionList.push({ Code: itemCode, Name: itemName, PositionName: itemPositionName });
		                break;
		            case 'N':
		               // var ddText = $("#ddNotedTitle option:selected").text();
		                self.notedByOptionList.push({ Code: itemCode, Name: itemName, PositionName: itemPositionName });
		                break;
		            case 'A':
		                //var ddText = $("#ddApproveTitle option:selected").text();
		                self.approvedByOptionList.push({ Code: itemCode, Name: itemName, PositionName: itemPositionName });
		                break;
		            default:
		                break;
		        }

		        //self.requestedItems.push({ requestedName: self.requestedName(), requestedTitle: self.requestedTitle() });
		        //self.requestedName("");
		        //self.requestedTitle("");
		    } else {
		        var strMessage = "Title: " + itemCode + " & Name: " + itemName + "already exists!";
		        $.ambiance({
		            message: strMessage,
		            type: "custom",
		            title: "Error Notification!",
		            timeout: 1
		        });
		        return false;
		    }
		};
	    //---------------------------------- end

	    //------------------------ remove Signatory
		self.removeSignatoryItem = function (itemType,itemDeleted) {
		    switch (itemType) {
		        case 'R':
		            self.requestedByOptionList.remove(itemDeleted);
		            break;
		        case 'C':
		            self.checkedByOptionList.remove(itemDeleted);
		            break;
		        case 'N':
		            self.notedByOptionList.remove(itemDeleted);
		            break;
		        case 'A':
		            self.approvedByOptionList.remove(itemDeleted);
		            break;
		        default:
		            break;
		    }
		};
	    //---------------------------------- end
};




