var url = '';
var arrIdStatusPair = [];
//var arrUpdateStatus = [];
var lstAllStudents = [];
var arrStudentIds = [];


var TEACHERCOMMONJS = {
    SetStudentDetail: function(tblId) {
        debugger;
        if (lstAllStudents != null && lstAllStudents.length > 0) {
            TEACHERCOMMONJS.SetStudentDetailData(lstAllStudents, tblId)
         }
    },
    SetStudentDetailData: function (lstStudents, tblId) {
        debugger;
        document.getElementById(tblId).innerHTML = '';
        for (var i = 0; i < lstStudents.length; i++) {
            TEACHERCOMMONJS.SetStudentBodyData(lstStudents[i], tblId);
        }
    },
    SetStudentBodyData: function (rowStudentData, tblId) {
        debugger;
        var td = undefined;
        var tBody = document.getElementById(tblId);
        var tr = document.createElement('tr');
        tr.className = rowStudentData.IsActive != true ? "DisableElement" : "";
        {
            //0
            td = document.createElement('td');
            td.id = rowStudentData.RegistrationId;
            var div = document.createElement('div');
            div.className = "form-check";
            var chkId = document.createElement('input');
            chkId.id = "chk_" + rowStudentData.RegistrationId;
            chkId.type = 'checkbox';
            chkId.className = 'form-check-input';
            chkId.setAttribute("onclick", "TEACHERCOMMONJS.GetStudentIds(this, " + rowStudentData.RegistrationId + ")");
            div.appendChild(chkId);
            td.appendChild(div)
            tr.appendChild(td);


            td = document.createElement('td');
            td.textContent = rowStudentData.RegistrationId;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowStudentData.StdName;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowStudentData.Address;
            tr.appendChild(td);


            td = document.createElement('td');
            td.textContent = rowStudentData.City;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowStudentData.Contact;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowStudentData.AdmissionInClass;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowStudentData.DOB;
            tr.appendChild(td);

            td = document.createElement('td');
            td.textContent = rowStudentData.AdmissionDate;
            tr.appendChild(td);

            td = document.createElement('td');
            if (rowStudentData.StudentImage != null) {
                var img = document.createElement('img');
                img.style = "height: 40px; width: 100px;";
                img.src = "data:image/png;base64," + TEACHERCOMMONJS.ToBase64String(rowStudentData.StudentImage); // "~/Image/QR.png";
                td.appendChild(img);
            } else {
                td.textContent = "StudentImage";
            }
            tr.appendChild(td);

            //6
            td = document.createElement('td');
            var div = document.createElement('div');
            div.className = "form-check";
            var chkActive = document.createElement('input');
            chkActive.type = 'checkbox';
            chkActive.className = 'form-check-input';
            chkActive.setAttribute("onclick", "ActDeactTeachers(this)");
            chkActive.checked = rowStudentData.IsActive;
            div.appendChild(chkActive);
            td.appendChild(div)
            tr.appendChild(td);

            tBody.appendChild(tr);
        }
    },

    //NOTE : btoa and atob work with strings, which they treat as arrays of bytes, so to use these two functions, you should first convert your array of integers (provided they fall in the range 0-255) into a string.
    ToBase64String: function (x) {
        debugger;
        return btoa(x.map(function(v){return String.fromCharCode(v)}).join(''))
    },
    //OR
    ToBase64StringUsing_atob: function (x) {
        debugger;
        return atob(x).split('').map(function (v) { return v.codePointAt(0) });
    },

    

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
    searchStudent: function (tblId) {
        debugger
        var arrFilterData = [];
        var sSearchValue = document.getElementById("txtSearchStudent").value;
        if (sSearchValue != undefined && sSearchValue != null) {
            for (var i = 0; i < lstAllStudents.length; i++) {
                let iIndex = lstAllStudents[i].StdName.toUpperCase().indexOf(sSearchValue.toUpperCase());
                if (iIndex > -1) {
                    arrFilterData.push(lstAllStudents[i]);
                }
            }
        }
        if (arrFilterData != null) {
            TEACHERCOMMONJS.SetStudentDetailData(arrFilterData, tblId);
        } else {
            TEACHERCOMMONJS.SetStudentDetailData(lstAllStudents, tblId);
        }
    },
    RefreshStudentData: function (tblId) {
        debugger
        TEACHERCOMMONJS.SetStudentDetailData(lstAllStudents, tblId);
    },
    
    successToOpenEditStudent: function (bSuccess, data) {
        debugger;
        $('#popupEditStudent').appendTo("body").remove();  //Note: before rendering close the existing modal from Body tag.
        $("#dvContainerEditStd").html(data);
        $('#popupEditStudent').appendTo("body").modal('show');
    },
    GetEditStudent: function(){
        debugger;
        if (arrStudentIds != null && arrStudentIds.length > 0) {
            if(lstAllStudents !=null){
                for (var iRow = 0; iRow < lstAllStudents.length; iRow++) {
                    if (lstAllStudents[iRow].RegistrationId == arrStudentIds[0]) {
                        return lstAllStudents[iRow];
                    }
                }
            }
        }
    },
    
    Close: function () {
        debugger;
        COMMONMETHODS.ShowToastAsAlert("Not implemented.");
    }
}