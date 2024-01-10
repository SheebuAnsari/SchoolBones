var url = '';
var lstTeacherData = [];
var lstAllTeachers = [];


var ADMIN = {
    Test: function () {
        debugger
        alert("Calling test from ADMIN.js file");
    },

    DoActiveInActiveTeacher: function (bActive) {
        debugger
        url = COREJS.ServerUrl("DoActiveInActiveTeacher", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                //arrSelectedTechers: arrTeacherIds,
                arrSelectedTechers: [iRowId],
                bActive: bActive
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    RefreshTeacherData: function (tblId, bIsActive) {
        debugger;
        alert("Pending")
        //url = COREJS.ServerUrl("LoadActiveInActiveTeachers", "Admin", "AdminArea");
        //$.ajax({
        //    url: url,
        //    cache: false,
        //    type: "GET",
        //    dataType: "HTML",
        //    data: {
        //        bActive: bIsActive
        //    },
        //    success: function (data) {
        //        COMMONMETHODS.SaveStatus(true, data);
        //    }
        //});


        //ADMIN.DoActiveInActiveTeacher(bIsActive);
        //ADMIN.SetStudentDetailData(lstAllStudents, tblId);
        //ADMIN.SetTeacherDetail(lstTeacherData, tblId);
    },

    //searchTeacher: function (tblId) {
    //    debugger
    //    var arrTeachers = [];
    //    arrTeachers = ADMIN.GetTeachersData();
    //    var arrFilterData = [];
    //    var sSearchValue = document.getElementById("txtSearchTeacher").value;
    //    if (sSearchValue != undefined && sSearchValue != null) {
    //        for (var i = 0; i < arrTeachers.length; i++) {
    //            let iIndex = arrTeachers[i].Name.toUpperCase().indexOf(sSearchValue.toUpperCase());
    //            if (iIndex > -1) {
    //                arrFilterData.push(arrTeachers[i]);
    //            }
    //        }
    //    }
    //    if (arrFilterData != null) {
    //        ADMIN.SetTeacherDetail(arrFilterData, tblId);
    //    } else {
    //        ADMIN.SetTeacherDetail(arrTeachers, tblId);
    //    }
    //},

    searchTeacher: function (tblId) {
        debugger
        var arrFilterData = [];
        var sSearchValue = document.getElementById("txtSearchTeacher").value;
        if (sSearchValue != undefined && sSearchValue != null) {
            for (var i = 0; i < lstAllTeachers.length; i++) {
                let iIndex = lstAllTeachers[i].Name.toUpperCase().indexOf(sSearchValue.toUpperCase());
                if (iIndex > -1) {
                    arrFilterData.push(lstAllTeachers[i]);
                }
            }
        }
        if (arrFilterData != null) {
            ADMIN.SetTeacherDetail(arrFilterData, tblId);
        } else {
            TEACHER.SetStudentDetailData(lstAllTeachers, tblId);
        }
    },

    GetTeachersData: function () {
        return arrTeachersData; //It will return Original Teachers data
    },

    SetTeacherDetail: function (lstAllTeachers, tblId) {
        debugger
        if (lstAllTeachers != null && lstAllTeachers.length > 0) {
            arrTeachersData = lstAllTeachers; //Hold global object
            ADMIN.SetTeacherDetailData(lstAllTeachers, tblId)
        }
    },

    SetTeacherDetailData: function (lstTeachers, tblId) {
        debugger;
        document.getElementById(tblId).innerHTML = '';
        for (var i = 0; i < lstTeachers.length; i++) {
            ADMIN.SetTeacherBodyData(lstTeachers[i], tblId);
        }
    },

    SetTeacherBodyData: function (rowData, tblId) {
        debugger;
        var td = undefined;
        var tBody = document.getElementById(tblId);
        //var tBodyId = document.getElementById(tBody.id).id;
        var tr = document.createElement('tr');
        //onclick="COMMONMETHODS.SelectRow(this, '@tBodyId')"
        //id="@arrRegisteredUsers[iUser].RegistrationId"
        tr.setAttribute("id", rowData.TeacherId)
        tr.setAttribute("onclick", "COMMONMETHODS.SelectRow(this, '" + tBody.id + "')");
        if (iSubModule != 16 && iSubModule != 17)  //here 16 : ActiveTeachers 17 : InActiveTeachers 
        {
            tr.className = rowData.Active != true ? "DisableElement" : "";
        }
        {
            //0
            //td = document.createElement('td');
            //td.id = rowData.TeacherId;
            //var div = document.createElement('div');
            //div.className = "form-check";
            //var chkId = document.createElement('input');
            //chkId.id = rowData.TeacherId;
            //chkId.type = 'checkbox';
            //chkId.className = 'form-check-input';
            //chkId.setAttribute("onclick", "ADMIN.GetTeacherIds(this, " + rowData.TeacherId + ")");
            //div.appendChild(chkId);
            //td.appendChild(div)
            //tr.appendChild(td);




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
            //if (iSubModule != 16 && iSubModule != 17)
            //{
            //    td = document.createElement('td');
            //    var div = document.createElement('div');
            //    div.className = "form-check";
            //    var chkActive = document.createElement('input');
            //    chkActive.type = 'checkbox';
            //    chkActive.className = 'form-check-input';
            //    chkActive.setAttribute("onclick", "ActDeactTeachers(this)");
            //    chkActive.checked = rowData.Active;
            //    div.appendChild(chkActive);
            //    td.appendChild(div)
            //    tr.appendChild(td);
            //}

            tBody.appendChild(tr);
        }
    },

    DeleteTeacher: function (bIsPermanent) {
        debugger;
        //var arrSelectedIds = COMMONMETHODS.GetSelectedIds("tblTeacherDetail");
        url = COREJS.ServerUrl("DeleteTeachers", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                //arrSelectedTechers: arrSelectedIds,
                arrSelectedTechers: [{ ID: iRowId, Status: true }],
                bDeletePermanent: bIsPermanent
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    EditTeacher: function () {
        debugger;
        url = COREJS.ServerUrl("CallCommonPopupAdmin", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                oTeacherDetails: ADMIN.GetEditTeacher(),
                iPopupName: COMMONENUM.ePopupName.EditTeacherDetails,
            },
            success: function (data) {
                debugger;
                ADMIN.successToOpenEditTeacherPopup(true, data);
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

    Close: function () {
        debugger;
        COMMONMETHODS.ShowToastAsAlert("Not implemented.");
    },
}