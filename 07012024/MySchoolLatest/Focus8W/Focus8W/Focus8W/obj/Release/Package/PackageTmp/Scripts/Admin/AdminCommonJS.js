var url = '';
var arrTeachersData = [];
var arrTeacherIds = [];
var iScreen = 0;
//var arrUpdateStatus = [];

var ADMINCOMMONJS = {
    Test: function () {
        debugger
        alert("Calling test from ADMINCOMMONJS.js file");
    },

    SaveTeacherDetails: function() {
        debugger;
        
        var iRegId = document.getElementById("hdnRegId").value;
        var iMobile = document.getElementById("txtMobile").value;
        //var sEmail = document.getElementById("txtEmail").value;
        //var sPassword = document.getElementById("txtPassword").value;
        //var sName = document.getElementById("txtName").value;
        var sQualification = document.getElementById("txtQualification").value;
        var sJoinDate = document.getElementById("txtJoinDate").value;
        var iJoinDate = FDate.convertDateToInt(sJoinDate, "yyyy-mm-dd", 0);
        //var iPackage = document.getElementById("txtPackage").value;
        //var iMonthlySalary = document.getElementById("txtMonthlySalary").value;
        var iExp = document.getElementById("txtExp").value;
        var chkActive = document.getElementById("chkActive");
        

        var oTeacher = {};
        oTeacher.RegistrationId = iRegId;
        oTeacher.Mobile = iMobile;
        //oTeacher.Email = sEmail;
        //oTeacher.Password = sPassword;
        //oTeacher.Name = sName;
        oTeacher.Qualification = sQualification;
        oTeacher.JoiningDate = iJoinDate;
        //oTeacher.Package = parseInt(iPackage);
        //oTeacher.MonthlySalary = parseInt(iMonthlySalary);
        oTeacher.Experiance = parseInt(iExp);
        oTeacher.Active = chkActive.checked;

        var url = COREJS.ServerUrl("SaveTeacher", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                oTeacherDetails: oTeacher
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },


    SetTeacherDetail: function (lstAllTeachers, tblId) {
        debugger
        if (lstAllTeachers != null && lstAllTeachers.length > 0) {
            arrTeachersData = lstAllTeachers; //Hold global object
            ADMINCOMMONJS.SetTeacherDetailData(lstAllTeachers, tblId)

        }
    },
    SetTeacherDetailData: function (lstTeachers, tblId) {
        debugger;
        document.getElementById(tblId).innerHTML = '';
        for (var i = 0; i < lstTeachers.length; i++) {
            ADMINCOMMONJS.SetTeacherBodyData(lstTeachers[i], tblId);
        }
    },
    SetTeacherBodyData: function (rowData, tblId) {
        debugger;
        var td = undefined;
        var tBody = document.getElementById(tblId);
        var tr = document.createElement('tr');
        if (iScreen != 16 && iScreen != 17) {
            tr.className = rowData.Active != true ? "DisableElement" : "";
        }
        {
            //0
            td = document.createElement('td');
            td.id = rowData.TeacherId;
            var div = document.createElement('div');
            div.className = "form-check";
            var chkId = document.createElement('input');
            chkId.id = rowData.TeacherId;
            chkId.type = 'checkbox';
            chkId.className = 'form-check-input';
            chkId.setAttribute("onclick", "ADMINCOMMONJS.GetTeacherIds(this, " + rowData.TeacherId + ")");
            div.appendChild(chkId);
            td.appendChild(div)
            tr.appendChild(td);


           

            //td = document.createElement('td');
            //td.textContent = rowData.Email;
            //tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowData.RegistrationId;
            tr.appendChild(td);


            td = document.createElement('td');
            td.textContent = rowData.Name;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowData.Mobile;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowData.Qualification;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowData.JoiningDate;
            tr.appendChild(td);

            //td = document.createElement('td');
            //td.textContent = rowData.Package;
            //tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowData.Experiance;
            tr.appendChild(td);

            //6
            if (iScreen != 16 && iScreen != 17) {
                td = document.createElement('td');
                var div = document.createElement('div');
                div.className = "form-check";
                var chkActive = document.createElement('input');
                chkActive.type = 'checkbox';
                chkActive.className = 'form-check-input';
                chkActive.setAttribute("onclick", "ActDeactTeachers(this)");
                chkActive.checked = rowData.Active;
                div.appendChild(chkActive);
                td.appendChild(div)
                tr.appendChild(td);
            }

            tBody.appendChild(tr);
        }
    },
    RefreshTeacherData: function (tblId) {
        debugger;
        TEACHERCOMMONJS.SetStudentDetailData(lstAllStudents, tblId);
    },

    DeleteTeacher: function (bIsPermanent) {
        debugger;
        var arrSelectedIds = COMMONMETHODS.GetSelectedIds("tblTeacherDetail");
        url = COREJS.ServerUrl("DeleteTeachers", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: { 
                arrSelectedTechers: arrSelectedIds,
                bDeletePermanent: bIsPermanent
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },
    EditTeacher:function(){
        debugger;
        url = COREJS.ServerUrl("CallCommonPopupAdmin", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                oTeacherDetails: ADMINCOMMONJS.GetEditTeacher(),
                iPopupName: COMMONENUM.ePopupName.EditTeacherDetails,
            },
            success: function (data) {
                debugger;
                ADMINCOMMONJS.successToOpenEditTeacherPopup(true, data);
            }
        });
    },
    successToOpenEditTeacherPopup: function (bSuccess, data) {
        debugger;
        $('#popupEditTeacherDetails').appendTo("body").remove();  //Note: before rendering close the existing modal from Body tag.
        $("#dvContainerEditTeacher").html(data);
        $('#popupEditTeacherDetails').appendTo("body").modal('show');
    },
    GetEditTeacher: function () {
        debugger;
        if (arrTeacherIds != null && arrTeacherIds.length > 0) {
            if (lstAllTeachers != null) {
                for (var iRow = 0; iRow < lstAllTeachers.length; iRow++) {
                    if (lstAllTeachers[iRow].TeacherId == arrTeacherIds[0]) {
                        return lstAllTeachers[iRow];
                    }
                }
            }
        }
    },
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
    searchTeacher: function (tblId) {
        debugger
        alert("Pending")
        var arrTeachers = [];
        arrTeachers  = ADMINCOMMONJS.GetTeachersData();
        var arrFilterData = [];
        var sSearchValue = document.getElementById("txtSearchTeacher").value;
        if (sSearchValue != undefined && sSearchValue != null) {
            for (var i = 0; i < arrTeachers.length; i++) {
                let iIndex = arrTeachers[i].Name.toUpperCase().indexOf(sSearchValue.toUpperCase());
                if (iIndex > -1) {
                    arrFilterData.push(arrTeachers[i]);
                }
            }
        }
        if (arrFilterData != null) {
            ADMINCOMMONJS.SetTeacherDetail(arrFilterData, tblId);
        } else {
            ADMINCOMMONJS.SetTeacherDetail(arrTeachers, tblId);
        }
    },
    GetTeachersData: function () {
        return arrTeachersData; //It will return Original Teachers data
    },
    
}