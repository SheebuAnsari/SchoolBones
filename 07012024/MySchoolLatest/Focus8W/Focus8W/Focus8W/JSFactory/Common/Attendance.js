var url = '';
var arrTeacherIds = [];
var arrStdIds=[];


var ATTENDANCE = {
    OpenAttendanceRegTeacher: function () {
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
                COMMONMETHODS.ShowCommonPopup(true, data, sPopupId);
            }
        });
    },

    SaveTeacherAttendance: function () {
        debugger;
        var iTeacherId = 0;
        var iAttDate = 0;
        lstAttandence = [];
        var sSelectedDate = 0;
        lstAttendance = [];

        sSelectedDate = document.getElementById("dtAttendaneDate").value;
        for (var iRow = 0; iRow < lstTeacher.length; iRow++) {
            iTeacherId = lstTeacher[iRow].RegistrationId;
            for (var iId = 0; iId < arrTeacherIds.length; iId++) {
                if (iTeacherId == arrTeacherIds[iId]) {
                    lstAttendance.push({
                        RegistrationId: lstTeacher[iRow].RegistrationId,
                        TeacherId: lstTeacher[iRow].RegistrationId,
                        Date: lstTeacher[iRow].Date,
                        Status: 1
                    });
                }
            }
        }

        url = COREJS.ServerUrl("SaveTeacherAttendance", "Admin", "AdminArea")
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                arrTeacherAttendance: lstAttendance,
                iDate: FDate.convertDateToInt(sSelectedDate, "yyyy-mm-dd", 0),
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    LoadTeacherAttendance: function () {
        debugger
        var ddlMonth = parseInt(document.getElementById("ddlMonth").value)
        var ddlYear = parseInt(document.getElementById("ddlYear").value)

        url = COREJS.ServerUrl("LoadTeacherAttendance", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "html",
            data: {
                iMonth: ddlMonth,
                iYear: ddlYear
            },
            success: function (data) {
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Admin);
            }
        });
    },

    OpenAttendanceRegStudent: function () {
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
                COMMONMETHODS.ShowCommonPopup(true, data, sPopupId);
            }
        });
    },

    LoadStudentAttendance: function () {
        debugger
        var ddlClass = parseInt(document.getElementById("ddlClass").value)
        var ddlMonth = parseInt(document.getElementById("ddlMonth").value)
        var ddlYear = parseInt(document.getElementById("ddlYear").value)

        url = COREJS.ServerUrl("LoadStudentAttendance", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                iClass: ddlClass,
                iMonth: ddlMonth,
                iYear: ddlYear
            },
            success: function (data) {
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Teacher);
            }
        });
    },

    SaveStudentAttendance: function () {
        debugger;
        var iStdId = 0;
        var iAttDate = 0;
        lstAttendance = [];

        var sSelectedDate = document.getElementById("dtAttendaneDate").value;

        //if(lstAttendance.length == 0)
        {
            for (var iRow = 0; iRow < lstStudent.length; iRow++) {
                iStdId = lstStudent[iRow].StdId;
                for (var iId = 0; iId < arrStdIds.length; iId++) {
                    if (iStdId == arrStdIds[iId]) {
                        lstAttendance.push({
                            StdId: lstStudent[iRow].StdId,
                            StdName: lstStudent[iRow].StdName,
                            Class: lstStudent[iRow].Class,
                            Date: lstStudent[iRow].Date,
                            Status: 1
                        });
                    }
                }
            }
        }

        //var url = "@Url.Action("SaveAttendance", "Teacher")";
        url = COREJS.ServerUrl("SaveStudentAttendance", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                arrStudentAttendance: lstAttendance,
                iDate: FDate.convertDateToInt(sSelectedDate, "yyyy-mm-dd", 0),
            },
            success: function (data) {
                successSaveAttendance(true, data);
            }
        });
    },

    AttendanceOfTeachers: function (ele) {
        debugger;
        var iTeacherId = ele.id;
        if (ele.checked) {
            arrTeacherIds.push(iTeacherId);
        } else {
            var iIndex = arrStdIds.indexOf(iTeacherId);
            arrTeacherIds.splice(iIndex);
        }
    },

    AttendanceOfStudents: function (ele) {
        debugger;
        var iStdId = ele.id;
        if (ele.checked) {
            arrStdIds.push(iStdId);
        } else {
            var iIndex = arrStdIds.indexOf(iStdId);
            arrStdIds.splice(iIndex);
        }
    },
}