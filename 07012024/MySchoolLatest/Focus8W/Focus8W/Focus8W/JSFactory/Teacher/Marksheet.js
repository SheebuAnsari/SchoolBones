var url = '';
var lstSubject = [];


var MARKSHEET = {
    LoadPreparedResults: function () {
        debugger;
        var iClass = document.getElementById("ddlClass").value;
        var iYear = document.getElementById("ddlYear").value;

        url = COREJS.ServerUrl("CreatedMarksheet", "Teacher", "TeacherArea");
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
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Teacher);
            }
        })
    },

    LoadSubjects: function () {
        debugger;
        url = COREJS.ServerUrl("LoadSubjects", "Teacher", "TeacherArea");
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
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Teacher);
            }
        });
    },

    ShowMarksheet: function () {
        debugger
        var iStdId = -1;
        //document.getElementById("dvDisplayMarksheet").style.display = "";
        url = COREJS.ServerUrl("LoadMarksheetInputs", "Teacher", "TeacherArea");
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
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Teacher);
            }
        });
    },

    LoadSemesterMarksheet: function () {
        debugger;
        var iClass = document.getElementById("ddlClass").value;
        var iYear = document.getElementById("ddlYear").value;
        var iExamType = document.getElementById("ddlExamType").value;

        url = COREJS.ServerUrl("LoadSemesterWiseMarksheet", "Teacher", "TeacherArea");
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
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Teacher);
            }
        })
    },

    LoadDefinedMaximumMarks: function () {
        debugger;
        var ddlClass = parseInt(document.getElementById("ddlClass").value)
        url = COREJS.ServerUrl("DefineMaximumMarks", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "html",
            data: {
                iClass: ddlClass,
            },
            success: function (data) {
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Teacher);
            }
        });
    },

    AddSubject: function (ele) {
        debugger
        var oSubject = {};
        oSubject.Class = parseInt(document.getElementById("hdnClass").value);

        url = COREJS.ServerUrl("CallCommonPopups", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                oSubject: oSubject,
                iPopupName: COMMONENUM.ePopupName.AddSubject,
            },
            success: function (data) {
                MARKSHEET.successAddSubject(true, data);
            }
        });
    },

    DeleteSubject: function (ele) {
        debugger
        var oSubject = {};
        //oSubject.SubjectId = iSubjectId;
        oSubject.SubjectId = iRowId;

        url = COREJS.ServerUrl("DoAction", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                oSubject: oSubject,
                iActionType: 3,  //iActionType : 3 for Delete
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data)
            }
        });
    },

    UpdateSubjectPopup: function (ele) {
        debugger;
        //var oSubject = RIBBONCONTROLTEACHER.GetSelectedSubject(iSubjectId);
        var oSubject = MARKSHEET.GetSelectedSubject(iRowId);

        url = COREJS.ServerUrl("CallCommonPopups", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                oSubject: oSubject,
                iPopupName: COMMONENUM.ePopupName.AddSubject,
            },
            success: function (data) {
                MARKSHEET.successAddSubject(true, data);
            }
        });
    },

    successAddSubject: function (bSuccess, data) {
        debugger;
        $('#popupAddSubject').appendTo("body").remove();  //Note: before rendering close the existing modal from Body tag.
        $("#dvContainerAddSubject").html(data);
        $('#popupAddSubject').appendTo("body").modal('show');
    },

    UpdateSubject: function (iSubjectId, iActionType) {
        debugger;
        var oSubject = {};
        url = COREJS.ServerUrl("DoAction", "Teacher", "TeacherArea");
        oSubject.SubjectId = iSubjectId
        oSubject.Class = parseInt(document.getElementById("hdnClass").value);
        oSubject.SubjectName = document.getElementById("txtSubjectName").value;

        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                oSubject: oSubject,
                iActionType: iActionType
            },
            success: function (data) {
                debugger;
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    SaveSubject: function () {
        debugger
        var oSubject = {};
        oSubject.Class = parseInt(document.getElementById("hdnClass").value);
        oSubject.SubjectName = document.getElementById("txtSubjectName").value;
        oSubject.MaximumMarks = parseInt(document.getElementById("txtMaxMark").value);
        oSubject.IsDefaultSubject = document.getElementById("chkIsDefaultSubject").checked;


        url = COREJS.ServerUrl("AddSubject", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                oSubject: oSubject
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    GetSelectedSubject: function (iSubjectId) {
        if (lstSubject != null && lstSubject.length > 0) {
            for (var i = 0; i < lstSubject.length; i++) {
                if (lstSubject[i].SubjectId == iSubjectId) {
                    return lstSubject[i];
                }
            }
        }
    },

    SaveMaxMarks: function () {
        debugger;
        url = COREJS.ServerUrl("SaveDefinedMaximumMarks", "Teacher", "TeacherArea");

        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "JSON",
            data: {
                arrSubjectMaxMarks: GetDefinedMarks()
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    GetDefinedMarks: function () {
        debugger
        var arrSubjectMaxMarks = [];
        SubjectMaxMarks = {};

        var iClass = parseInt(document.getElementById("hdnClass").value);
        var tBody = document.getElementById("tbodyMaxMarks");
        var tableRows = tBody.children;

        for (var iRow = 0; iRow < tableRows.length; iRow++) {
            SubjectMaxMarks = {}
            tr = tableRows[iRow];
            SubjectMaxMarks.SubjectId = parseInt(tr.children[0].id);
            SubjectMaxMarks.Class = iClass;
            SubjectMaxMarks.MaxMark = parseInt(tr.children[1].children[0].value);
            arrSubjectMaxMarks.push(SubjectMaxMarks);
        }
        return arrSubjectMaxMarks;
    },
    DisplayMarksheet: function () {
        debugger
        var iStdId = -1;
        var txtRollNo = parseInt(document.getElementById("txtRno").value);

        iStdId = txtRollNo;
        //document.getElementById("dvDisplayMarksheet").style.display = "";
        url = COREJS.ServerUrl("DisplayMarksheet", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "html",
            data: {
                iStdId: iStdId
            },
            success: function (data) {
                debugger
                document.getElementById("dvDisplayMarksheet").innerHTML = data;
            }
        });
    },

    RefreshMaximumMarks: function () {
        debugger
        alert("Pending")
    },
}