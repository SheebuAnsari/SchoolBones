var url = '';
var arrIdStatusPair = [];
//var arrUpdateStatus = [];
var lstAllStudents = [];
var arrStudentIds = [];


var TEACHERCOMMONJS = {
    SelectAll: function (ele) {
        debugger;
        var iRow = 0;
        arrStudentIds = [];
        var tBody = document.getElementById("tblStudentDetail");
        var tableRows = tBody.children;

        arrStudentIds = [];
        for (; iRow < tableRows.length; iRow++) {
            tr = tableRows[iRow];
            tr.children[0].children[0].children[0].checked = ele.checked;

            if (ele.checked) {
                arrStudentIds.push(tr.children[0].id);
            }
        }
    },

    GetStudentIds: function (ele, iRegistrationId) {
        debugger;
        if (ele.checked) {
            arrStudentIds.push(iRegistrationId);
        } else {
            for (var i = 0; i < arrStudentIds.length; i++) {
                if (iRegistrationId == arrStudentIds[i])
                    break;
            }
            arrStudentIds.splice(i, 1);
        }
    },

    GetStdIds: function (ele) {
        debugger;
        var IdStatusPair = {};
        var iStdId = parseInt(ele.id);
        var bChecked = ele.checked;


        if (ele.checked) {
            IdStatusPair.ID = iStdId;
            IdStatusPair.Status = bChecked;
            arrIdStatusPair.push(IdStatusPair);
        } else {
            var i = 0;
            for (; i < arrIdStatusPair.length; i++) {
                if (iStdId == arrIdStatusPair[i].ID)
                    break;
            }
            arrIdStatusPair.splice(i, 1);
        }
    },
    
    GetUpdatedValueOfActivation: function (tblId) {
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
            IdValue.Status = tr.children[9].children[0].children[0].checked;//chkActive
            if (chkActive.checked) {
                arrUpdateStatus.push(IdValue);
            }
        }
    },
    
    
    
    successToOpenEditStudent: function (bSuccess, data) {
        debugger;
        $('#popupEditStudent').appendTo("body").remove();  //Note: before rendering close the existing modal from Body tag.
        $("#dvContainerEditStd").html(data);
        $('#popupEditStudent').appendTo("body").modal('show');
    },
  
    
    
}