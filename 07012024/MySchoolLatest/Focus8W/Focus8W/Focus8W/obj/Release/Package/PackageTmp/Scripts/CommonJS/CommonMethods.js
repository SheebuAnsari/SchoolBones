var url = '';
var lstTeacherData = [];

var COMMONMETHODS = {
    Test: function () {
        debugger
        alert("Calling test from Commonmethod.js file");
    },


    FillUserDetails: function () {
        debugger;
        url = COREJS.ServerUrl("FillUserDetails", "Common", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            data: {
                iRegId: iRegUserId,
            },
            datatype: "html",
            success: function (data) {
                if (data == 'Alert : No record available.') {
                    return COMMONMETHODS.ShowToastAsAlert(data);
                }
                COMMONMETHODS.successLoadModuleView(true, data, iUserType == COMMONENUM.eModule.Student ? COMMONENUM.eModule.Teacher : COMMONENUM.eModule.Admin);
            }
        })
    },
    GetModuleId: function () {
        debugger
        var MenuTabs = document.getElementById("MenuTabs");
        if (MenuTabs != null && MenuTabs != undefined) {
            var MenuTabChildren = MenuTabs.children;
            for (var i = 0; i < MenuTabChildren.length; i++) {
                if (MenuTabChildren[i].className == 'nav-link active' || MenuTabChildren[i].getAttribute("aria-selected") == true) {
                    return parseInt(MenuTabChildren[i].getAttribute("data-module"));
                }
            }
        }
    },
     

    LoadSubjects: function() {
        debugger;
        url = COREJS.ServerUrl("LoadSubjects", "Teacher", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                iClass: parseInt(document.getElementById("ddlClass").value)
            },
            success: function (data) {
                debugger
                COMMONMETHODS.successLoadModuleView(true, data, COMMONENUM.eModule.Teacher);
            }
        });
    },

    LoadStudentsForMarks: function() {
        debugger
        var iClass = document.getElementById("ddlClass").value;
        var iYear = document.getElementById("ddlYear").value;
        var iExamType = document.getElementById("ddlExamType").value;

        url = COREJS.ServerUrl("LoadStudentsForMarks", "Teacher", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            data: {
                iClass: iClass,
                iYear: iYear,
                iExamType: iExamType
            },
            datatype: "json",
            success: function (data) {
                COMMONMETHODS.successLoadModuleView(true, data, COMMONENUM.eModule.Teacher);
            }
        })
    },

    LoadPreparedResults: function() {
        debugger;
        var iClass = document.getElementById("ddlClass").value;
        var iYear = document.getElementById("ddlYear").value;

        url = COREJS.ServerUrl("CreatedMarksheet", "Teacher", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            data: {
                iClass: iClass,
                iYear: iYear
            },
            datatype: "json",
            success: function (data) {
                COMMONMETHODS.successLoadModuleView(true, data, COMMONENUM.eModule.Teacher);
            }
        })
    },

    LoadAttendance: function() {
        debugger
        var ddlClass = parseInt(document.getElementById("ddlClass").value)
        var ddlMonth = parseInt(document.getElementById("ddlMonth").value)
        var ddlYear = parseInt(document.getElementById("ddlYear").value)

        url = COREJS.ServerUrl("LoadAttendance", "Teacher", "");
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
                COMMONMETHODS.successLoadModuleView(true, data, COMMONENUM.eModule.Teacher);
            }
        });
    },

    LoadFeeDetails: function() {
        debugger
        var txtEnrollmentNo = parseInt(document.getElementById("txtEno").value);
        var txtRollNo = parseInt(document.getElementById("txtRno").value);

        url = COREJS.ServerUrl("LoadFeeDetails", "Accounts", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            data: {
                iEnrollmentno: txtEnrollmentNo,
                iRollno: txtRollNo
            },
            datatype: "html",
            success: function (data) {
                COMMONMETHODS.successLoadModuleView(true, data, COMMONENUM.eModule.Accounts);
            }
        })
    },

    LoadTeacherAttendance: function() {
        debugger
        var ddlMonth = parseInt(document.getElementById("ddlMonth").value)
        var ddlYear = parseInt(document.getElementById("ddlYear").value)

        url = COREJS.ServerUrl("LoadTeacherAttendance", "Admin", "");
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
                COMMONMETHODS.successLoadModuleView(true, data, COMMONENUM.eModule.Admin);
            }
        });
    },

    LoadDefinedMaximumMarks: function() {
        debugger;
        var ddlClass = parseInt(document.getElementById("ddlClass").value)
        url = COREJS.ServerUrl("DefineMaximumMarks", "Teacher", "");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "html",
            data: {
                iClass: ddlClass,
            },
            success: function (data) {
                COMMONMETHODS.successLoadModuleView(true, data, COMMONENUM.eModule.Teacher);
            }
        });
    },

    ShowMarksheet: function() {
        debugger
        var iStdId = -1;
        //document.getElementById("dvDisplayMarksheet").style.display = "";
        url = COREJS.ServerUrl("LoadMarksheetInputs", "Teacher", "");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "html",
            data: {
            },
            success: function (data) {
                debugger
                //document.getElementById("dvDisplayMarksheet").style.display = "block";
                COMMONMETHODS.successLoadModuleView(true, data, COMMONENUM.eModule.Teacher);
            }
        });
    },
    SaveStatus: function (bSuccess, data) {
        var sMsg = "";
        if(bSuccess){
            if (data.iStatus > 0) {
                COMMONMETHODS.ShowToastAsAlert("Successfully.");
            } else {
                COMMONMETHODS.ShowToastAsAlert("Something is wrong!");
            }
        }
    },
    GetSelectedIds: function (tblId) {
        debugger;
        var tr = undefined;
        var IdValue = {};
        arrUpdateStatus = [];

        var tBody = document.getElementById(tblId);
        var tableRows = tBody.children;
        for (var iRow = 0; iRow < tableRows.length; iRow++) {
            IdValue = {}
            tr = tableRows[iRow];
            //Check Row is selected or not
            var chkActive = tr.children[0].children[0].children[0];
            IdValue.ID = parseInt(tr.children[0].id);//TeacherId
            IdValue.Status = true;
            //IdValue.Status = tr.children[9].children[0].children[0].checked;//chkActive
            if (chkActive.checked) {
                arrUpdateStatus.push(IdValue);
            }
        }
        return arrUpdateStatus;
    },
    
    ShowToastAsAlert: function (sMessage) {
        //debugger;
        Date.now()
        var toastElList = document.getElementById("liveToast");
        var toastBody = document.getElementById("toastBody");
        var lstTime = document.getElementById("lstTime");
        
        toastBody.innerText = sMessage;
        toastElList.className = "toast show";
        lstTime.textContent = new Date().toDateString();
        setTimeout(function () {
            toastElList.className = "toast hide";
        }, 5000)
    },
    SaveUserRegistration: function (ele) {
        debugger;
        var oRegistration = {};
        var sUserName = document.getElementById("txtUserName").value;
        var sLoginName = document.getElementById("txtLoginName").value;
        var sUserPassword = document.getElementById("txtUserPassword").value;
        var iUserType = parseInt(document.getElementById("ddlUserType").value)

        oRegistration.UserName = sUserName;
        oRegistration.LoginName = sLoginName;
        oRegistration.UserPassword = sUserPassword;
        oRegistration.UserType = iUserType;

        url = COREJS.ServerUrl("SaveRegistration", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                oRegistration: oRegistration
            },
            success: function (returnedRegId) {
                var sStatus = '';
                sStatus = returnedRegId > 0 ? "Registration id : " + returnedRegId : "Failed";
                COMMONMETHODS.ShowToastAsAlert(sStatus);
                document.getElementById("lblRegId").innerText = sStatus;
            }
        });
    },
    successLoadModuleView: function (bSuccess, data, eModule) {
        debugger
        if (bSuccess) {
            if (eModule == COMMONENUM.eModule.Admin) {
                $("#dvAdmin").html(data);
            } else if (eModule == COMMONENUM.eModule.Teacher) {
                $("#dvTeacher").html(data);
            } else if (eModule == COMMONENUM.eModule.Accounts) {
                $("#dvAccounts").html(data);
            }
        }
    },
   
}