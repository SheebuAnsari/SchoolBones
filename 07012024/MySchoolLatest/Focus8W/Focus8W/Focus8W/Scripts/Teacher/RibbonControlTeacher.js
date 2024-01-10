//var url = '';

//var iSubjectId = -1;

//var arrStudnetIds = [];

//var RIBBONCONTROLTEACHER = {
   

//    DoActiveInActiveStudent: function (bActive) {
//        debugger
//        url = COREJS.ServerUrl("DoActiveInActiveStudent", "Teacher", "");
//        $.ajax({
//            url: url,
//            cache: false,
//            type: "POST",
//            dataType: "json",
//            data: {
//                //arrStudnetIds: arrStudnetIds,
//                arrStudnetIds: [iRowId],
//                bActive: bActive
//            },
//            success: function (data) {
//                COMMONMETHODS.SaveStatus(true, data);
//            }
//        });
//    },

//    DeleteStudent: function (bIsPermanent) {
//        debugger;
//        //var arrSelectedIds = COMMONMETHODS.GetSelectedIds("tblStudentDetail");
//        //url = "Teacher/DeleteStudents";
//        url = COREJS.ServerUrl("DeleteStudents", "Teacher", "");
//        $.ajax({
//            url: url,
//            cache: false,
//            type: "POST",
//            dataType: "json",
//            data: {
//                //arrSelectedStudents: arrSelectedIds,
//                arrSelectedStudents: [{ ID: iRowId, Status: true }],
//                bDeletePermanent: bIsPermanent
//            },
//            success: function (data) {
//                COMMONMETHODS.SaveStatus(true, data);
//            }
//        });
//    },

//    EditStudent: function () {
//        debugger;
//        var objStudent = {};
//        objStudent = RIBBONCONTROLTEACHER.GetEditStudent();
//        objStudent.StudentImage = [];

//        url = COREJS.ServerUrl("CallCommonPopups", "Teacher", "");
//        $.ajax({
//            url: url,
//            cache: false,
//            type: "POST",
//            dataType: "html",
//            data: {
//                objStudent: objStudent,
//                iPopupName: COMMONENUM.ePopupName.EditStudent,
//            },
//            success: function (data) {
//                debugger;
//                TEACHERCOMMONJS.successToOpenEditStudent(true, data);
//            },
//            error: function (xhr, ajaxOptions, throwError) {
//                alert(xhr.responseText);
//            }
//        });
//    },
//    GetEditStudent: function(){
//        debugger;
//        if (lstAllStudents != null) {
//            for (var i = 0; i < lstAllStudents.length; i++) {
//                if (iRowId == lstAllStudents[i].RegistrationId) {
//                    return lstAllStudents[i];
//                }
//            }
//        }
//    },

//    SelectRowAddSubject: function (eleTr, iSubId, tbodyId) {
//        debugger;
//        iSubjectId = iSubId;
//        RIBBONCONTROLTEACHER.ResetRowColor(tbodyId);
//        eleTr.className = 'SelectRow';
//    },
  
//}