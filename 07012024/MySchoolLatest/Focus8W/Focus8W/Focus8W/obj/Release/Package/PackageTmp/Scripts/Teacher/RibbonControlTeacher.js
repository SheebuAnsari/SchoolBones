var url = '';
var lstSubject = [];
var iSubjectId = -1;
var iUserType =-1;
var iRegUserId = -1;

var arrStudnetIds = [];

var RIBBONCONTROLTEACHER = {
   

    DoActiveInActiveStudent: function (bActive) {
        debugger
        url = COREJS.ServerUrl("DoActiveInActiveTeacher", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                arrStudnetIds: arrStudnetIds,
                bActive: bActive
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    DeleteStudent: function (bIsPermanent) {
        debugger;
        var arrSelectedIds = COMMONMETHODS.GetSelectedIds("tblStudentDetail");
        //url = "Teacher/DeleteStudents";
        url = COREJS.ServerUrl("DeleteStudents", "Teacher", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                arrSelectedStudents: arrSelectedIds,
                bDeletePermanent: bIsPermanent
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    EditStudent: function () {
        debugger;
        var objStudent = '';
        objStudent = TEACHERCOMMONJS.GetEditStudent();
        objStudent.StudentImage = [];

        url = COREJS.ServerUrl("CallCommonPopups", "Teacher", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                objStudent: objStudent,
                iPopupName: COMMONENUM.ePopupName.EditStudent,
            },
            success: function (data) {
                debugger;
                TEACHERCOMMONJS.successToOpenEditStudent(true, data);
            },
            error: function (xhr, ajaxOptions, throwError) {
                alert(xhr.responseText);
            }
        });
    },



    GetStudentIds: function (ele, iStudentId) {
        debugger;
        if (ele.checked) {
            arrStudnetIds.push(iStudentId);
        } else {
            for (var i = 0; i < arrStudnetIds.length; i++) {
                if (iStudentId == arrStudnetIds[i])
                    break;
            }
            arrStudnetIds.splice(i, 1);
        }
    },

    AddSubject: function (ele) {
        debugger
        var oSubject = {};
        oSubject.Class = parseInt(document.getElementById("hdnClass").value);

        url = COREJS.ServerUrl("CallCommonPopups", "Teacher", "");
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
                RIBBONCONTROLTEACHER.successAddSubject(true, data);
            }
        });
    },

    DeleteSubject: function (ele) {
        debugger
        var oSubject = {};
        oSubject.SubjectId = iSubjectId;

        url = COREJS.ServerUrl("DoAction", "Teacher", "");
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
    UpdateSubject: function (ele) {
        debugger;
        var oSubject = RIBBONCONTROLTEACHER.GetSelectedSubject(iSubjectId);

        url = COREJS.ServerUrl("CallCommonPopups", "Teacher", "");
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
                RIBBONCONTROLTEACHER.successAddSubject(true, data);
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
    SelectRowAddSubject: function (eleTr, iSubId, tbodyId) {
        debugger;
        iSubjectId = iSubId;
        RIBBONCONTROLTEACHER.ResetRowColor(tbodyId);
        eleTr.className = 'SelectRow';
    },
    SelectRowAddRegUserDetails: function (eleTr, tbodyId) {
        debugger;
        iUserType = parseInt(eleTr.id);
        iRegUserId = parseInt(eleTr.getAttribute("data-regid"));
        RIBBONCONTROLTEACHER.ResetRowColor(tbodyId);
        eleTr.className = 'SelectRow';
    },
    ResetRowColor: function (tbodyId) {
        debugger
        var tableRows = document.getElementById(tbodyId).children;
        for (var iRow = 0; iRow < tableRows.length; iRow++) {
            tr = tableRows[iRow];
            tr.className = '';
        }
    },

    successAddSubject: function (bSuccess, data) {
        debugger;
        $('#popupAddSubject').appendTo("body").remove();  //Note: before rendering close the existing modal from Body tag.
        $("#dvContainerAddSubject").html(data);
        $('#popupAddSubject').appendTo("body").modal('show');
    },
    
    successLoadFeeDetails: function (bSuccess, data, eModule) {
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
    }
}