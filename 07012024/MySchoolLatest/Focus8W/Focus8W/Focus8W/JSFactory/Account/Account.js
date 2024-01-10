var url = '';
var arrTeacherSalary = [];


var ACCOUNT = {
    Test: function () {
        debugger
        alert("Calling test from ACCOUNT.js file");
    },

    LoadFeeDetails: function () {
        debugger
        var txtEnrollmentNo = parseInt(document.getElementById("txtEno").value);
        var txtRollNo = parseInt(document.getElementById("txtRno").value);

        url = COREJS.ServerUrl("LoadFeeDetails", "Account", "AccountArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            data: {
                iRegistrationId: txtEnrollmentNo,
                iRollno: txtRollNo
            },
            datatype: "HTML",
            success: function (data) {
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Account);
            }
        })
    },

    LoadClasswiseStudents: function () {
        var ddlClass = parseInt(document.getElementById("ddlClass").value)
        var ddlYear = parseInt(document.getElementById("ddlYear").value)

        url = COREJS.ServerUrl("LoadClasswiseStudents", "Account", "AccountArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "HTML",
            data: {
                iClass: ddlClass,
                iYear: ddlYear
            },
            success: function (data) {
                $('#dvLoadStudents').html(data);
            }
        });
    },

    CallCommonPopup: function (iAction) {
        debugger
        var sPopupId = "";
        var oFS = {};
        var oFeesStructure = {};
        var oPopupData = {};
        oPopupData.Value = {};

        oFS = JSON.stringify(ACCOUNT.GetSelectedFeeStruct());
        if ((iAction == 2 || iAction == 3) && oFS == undefined) {
            COMMONMETHODS.ShowToastAsAlert("Please select row for updation.")
            return;
        }
        if (iAction == 1) {
            sPopupId = "popupSaveFeeStructure";
            oPopupData.Id = COMMONENUM.ePopupName.SaveFeeStructure;
            oPopupData.Value = {};
        } else if (iAction == 2) {

            sPopupId = "popupUpdateFeeStructure";
            oPopupData.Id = COMMONENUM.ePopupName.UpdateFeeStructure;
            oPopupData.Value = oFS;
        } else if (iAction == 3) {
            sPopupId = "popupDeleteFeeStructure";
            oPopupData.Id = COMMONENUM.ePopupName.DeleteFeeStructure;
            oPopupData.Value = JSON.stringify(iFeeStructureId);
        }
        var url = COREJS.ServerUrl("CallCommonPopup", "Shared", "");
        $.ajax({
            url: url,
            cache: false,
            type: "Post",
            dataType: "HTML",
            data: {
                oPopupData: oPopupData
            },
            success: function (data) {
                debugger
                COMMONMETHODS.ShowCommonPopup(true, data, sPopupId);
            }
        });
    },

    SaveSalary: function () {
        debugger
        url = COREJS.ServerUrl("SaveSalary", "Account", "AccountArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                arrTeacherSalary: ACCOUNT.GetSalaryData(),
            },
            success: function (data) {
                COMMONMETHODS.ShowToastAsAlert("Saved");
            }
        });
    },

    LoadSalaryDetails: function () {
        debugger;
        url = COREJS.ServerUrl("LoadTeachersSalary", "Account", "AccountArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            data: {
                iMonth: parseInt(document.getElementById("ddlMonth").value),
                iYear: parseInt(document.getElementById("ddlYear").value)
            },
            datatype: "HTML",
            success: function (data) {
                $('#dvSalaryDetails').html(data);
            }
        })
    },

    LoadFeeCollection: function () {
        debugger;
        url = COREJS.ServerUrl("LoadFeeCollection", "Account", "AccountArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            data: {
                iClass: parseInt(document.getElementById("ddlClass").value),
                iMonth: parseInt(document.getElementById("ddlMonth").value),
                iYear: parseInt(document.getElementById("ddlYear").value)
            },
            datatype: "HTML",
            success: function (data) {
                $('#dvLoadFeeCollection').html(data);
            }
        })
    },

    LoadFeesStructure: function (eleDdl) {
        debugger;
        //var ddlYear = parseInt(document.getElementById("ddlYear").value)
        url = COREJS.ServerUrl("LoadFeesStructure", "Account", "AccountArea");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            data: {
                iYear: parseInt(eleDdl.value)
            },
            datatype: "html",
            success: function (data) {
                $('#dvFeesStructure').html(data);
            }
        })
    },

    DefineMontlySalary: function (eleDdl) {
        debugger;
        url = COREJS.ServerUrl("LoadMonthlySalary", "Account", "AccountArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            data: {
                iYear: parseInt(eleDdl.value)
            },
            datatype: "HTML",
            success: function (data) {
                $('#dvDefineSalary').html(data);
            }
        })
    },
    GetSalaryData: function() {
        debugger;
        var arrSalary = [];
        var tblDefineSalary = document.getElementById("tblDefineSalary");
        var arrTr = tblDefineSalary.children;

        for (var i = 0; i < arrTr.length; i++) {
            var oSalary = {};
            oSalary.SalaryId = parseInt(arrTr[i].id);
            oSalary.RegId = parseInt(arrTr[i].getAttribute("data-regid"));
            oSalary.Year = parseInt(document.getElementById("ddlYear").value);
            oSalary.MonthlySalary = parseInt(arrTr[i].children[3].children[0].value);
            arrSalary.push(oSalary)
        }
        return arrSalary;
    },
    BackToFeeSubmission: function(iSubModule) {
        debugger
        //here 24 : SubModule.AllStudents
        if (iSubModule == 24) {
            COMMONMETHODS.ShowToastAsAlert("Not implemented.");
            return;
        }
        url = COREJS.ServerUrl("FeeSubmission", "Account", "AccountArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "html",
            data: {

            },
            success: function (data) {
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Account);
            }
        });
    },












    GetSelectedFeeStruct: function () {
        debugger
        if (iRowId > 0) {
            for (var i = 0; i < arrFeesStructure.length; i++) {
                if (iRowId == arrFeesStructure[i].FeeStructureId) {
                    return arrFeesStructure[i];
                }
            }
        }
    },
}