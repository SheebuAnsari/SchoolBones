var iFeeStructureId = 0;
var iSalaryId = 0;
var arrFeesStructure = [];


var COMMONRIBBON = {
    Test: function () {
        debugger
        alert("Calling test from Commonmethod.js file");
    },

    DeleteRegistration: function () {
        debugger
        var url = COREJS.ServerUrl("DeleteRegistration", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                iRegId: iRegUserId
            },
            success: function (data) {
                COMMONMETHODS.ShowToastAsAlert("Deleted successfully.");
                //sleep(1000)
                //COMMONRIBBON.LoadRegUser();
            }
        });
    },
    LoadRegUserRefresh: function () {
        debugger
        var url = '';
        url = COREJS.ServerUrl("LoadRegUserRefresh", "Common", "");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "html",
            data: {
                iModuleId: COMMONMETHODS.GetModuleId()
            },
            success: function (data) {
                if (COMMONMETHODS.GetModuleId() == COMMONENUM.eModule.Admin) {
                    $("#dvAdmin").html(data);
                } else if (COMMONMETHODS.GetModuleId() == COMMONENUM.eModule.Teacher) {
                    $("#dvTeacher").html(data);
                }
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

        oFS = JSON.stringify(COMMONRIBBON.GetSelectedFeeStruct());
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
                COMMONRIBBON.successOpenPopup(true, data, sPopupId);
            }
        });
    },
    GetSelectedFeeStruct: function () {
        debugger
        if (iFeeStructureId > 0) {
            for (var i = 0; i < arrFeesStructure.length; i++) {
                if (iFeeStructureId == arrFeesStructure[i].FeeStructureId) {
                    return arrFeesStructure[i];
                }
            }
        }
    },

    TakeStudentAttandance: function() {
        debugger;
        var sPopupId = '';
        var iClass = 0;

        sPopupId = "popupTakeStudentAttendance";
        iClass = document.getElementById("ddlClass").value;

        var oInputs = {};
        oInputs.Class = iClass;

        var oPopupData = {};
        oPopupData.Id = COMMONENUM.ePopupName.TakeStudentAttendance;
        oPopupData.Value = {};


        //var iYear = document.getElementById("ddlYear").value;
        if (document.getElementById("dtAttendaneDate").value == '') {
            COMMONMETHODS.ShowToastAsAlert("Please select date for attendance.")
            return;
        }

        var url = COREJS.ServerUrl("CallCommonPopup", "Shared", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                oPopupData: oPopupData,
                oInputs: oInputs
            },
            success: function (data) {
                COMMONRIBBON.successOpenPopup(true, data, sPopupId);
            }
        });
    },
    TakeTeacherAttandance: function TakeTeacherAttandance() {
        debugger;
        var sPopupId = "popupTakeTeacherAttendance";
        var oPopupData = {};
        oPopupData.Id = COMMONENUM.ePopupName.TakeTeacherAttendance;
        oPopupData.Value = {};

        if (document.getElementById("dtAttendaneDate").value == '') {
            COMMONMETHODS.ShowToastAsAlert("Please select date for attendance.")
            return;
        }

        var url = COREJS.ServerUrl("CallCommonPopup", "Shared", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                oPopupData: oPopupData
            },
            success: function (data) {
                COMMONRIBBON.successOpenPopup(true, data, sPopupId);
            }
        });
    },

    successOpenPopup: function (bSuccess, data, sPopupId) {
        debugger;
        //$("#dvPopupContainer").html('');
        $('#' + sPopupId + '').appendTo("body").remove();  //NOTE: before rendering close the existing modal from Body tag.
        $("#dvPopupContainer").html(data);
        $('#' + sPopupId + '').appendTo("body").modal('show');
    }
}