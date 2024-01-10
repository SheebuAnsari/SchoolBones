var url = '';
var arrTeachersData = [];
var arrTeacherIds = [];
var iSubModule = 0;
//var arrUpdateStatus = [];

var ADMINCOMMONJS = {
    Test: function () {
        debugger
        alert("Calling test from ADMINCOMMONJS.js file");
    },

    //SaveTeacherDetails: function() {
    //    debugger;
        
    //    var iRegId = document.getElementById("hdnRegId").value;
    //    var iMobile = document.getElementById("txtMobile").value;
    //    //var sEmail = document.getElementById("txtEmail").value;
    //    //var sPassword = document.getElementById("txtPassword").value;
    //    //var sName = document.getElementById("txtName").value;
    //    var sQualification = document.getElementById("txtQualification").value;
    //    var sJoinDate = document.getElementById("txtJoinDate").value;
    //    var iJoinDate = FDate.convertDateToInt(sJoinDate, "yyyy-mm-dd", 0);
    //    //var iPackage = document.getElementById("txtPackage").value;
    //    //var iMonthlySalary = document.getElementById("txtMonthlySalary").value;
    //    var iExp = document.getElementById("txtExp").value;
    //    var chkActive = document.getElementById("chkActive");
        

    //    var oTeacher = {};
    //    oTeacher.RegistrationId = iRegId;
    //    oTeacher.Mobile = iMobile;
    //    //oTeacher.Email = sEmail;
    //    //oTeacher.Password = sPassword;
    //    //oTeacher.Name = sName;
    //    oTeacher.Qualification = sQualification;
    //    oTeacher.JoiningDate = iJoinDate;
    //    //oTeacher.Package = parseInt(iPackage);
    //    //oTeacher.MonthlySalary = parseInt(iMonthlySalary);
    //    oTeacher.Experiance = parseInt(iExp);
    //    oTeacher.Active = chkActive.checked;

    //    var url = COREJS.ServerUrl("SaveTeacher", "Admin", "");
    //    $.ajax({
    //        url: url,
    //        cache: false,
    //        type: "POST",
    //        dataType: "json",
    //        data: {
    //            oTeacherDetails: oTeacher
    //        },
    //        success: function (data) {
    //            COMMONMETHODS.SaveStatus(true, data);
    //        }
    //    });
    //},


    
    
    
    

    
    
    SelectAll: function(ele) {
        debugger;
        var iRow = 0;
        arrTeacherIds=[];
        var tBody = document.getElementById("tblTeacherDetail");
        var tableRows = tBody.children;

        arrTeacherIds=[];
        for (; iRow < tableRows.length; iRow++) {
            tr = tableRows[iRow];
            tr.children[0].children[0].children[0].checked = ele.checked;

            if (ele.checked) {
                arrTeacherIds.push(tr.children[0].id);
            }
        }
    },
    GetTeacherIds: function (ele, iTeacherId) {
        debugger;
        if (ele.checked) {
            arrTeacherIds.push(iTeacherId);
        } else {
            for (var i = 0; i < arrTeacherIds.length; i++) {
                if (iTeacherId == arrTeacherIds[i])
                    break;
            }
            arrTeacherIds.splice(i, 1);
        }
    },
    
    
    
}