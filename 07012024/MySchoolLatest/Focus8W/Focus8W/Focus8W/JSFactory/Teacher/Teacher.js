var url = '';
var lstAllStudents = [];

var TEACHER = {
    SetStudentDetail: function (tblId) {
        debugger;
        if (lstAllStudents != null && lstAllStudents.length > 0) {
            TEACHER.SetStudentDetailData(lstAllStudents, tblId)
        }
    },
    SetStudentDetailData: function (lstStudents, tblId) {
        debugger;
        document.getElementById(tblId).innerHTML = '';
        for (var i = 0; i < lstStudents.length; i++) {
            TEACHER.SetStudentBodyData(lstStudents[i], tblId);
        }
    },

    SetStudentBodyData: function (rowStudentData, tblId) {
        debugger;
        var td = undefined;
        var tBody = document.getElementById(tblId);
        var tr = document.createElement('tr');
        tr.setAttribute("id", rowStudentData.RegistrationId)
        tr.setAttribute("onclick", "COMMONMETHODS.SelectRow(this, '" + tBody.id + "')");

        tr.className = rowStudentData.IsActive != true ? "DisableElement" : "";
        {
            //0
            //td = document.createElement('td');
            //td.id = rowStudentData.RegistrationId;
            //var div = document.createElement('div');
            //div.className = "form-check";
            //var chkId = document.createElement('input');
            //chkId.id = "chk_" + rowStudentData.RegistrationId;
            //chkId.type = 'checkbox';
            //chkId.className = 'form-check-input';
            //chkId.setAttribute("onclick", "TEACHER.GetStudentIds(this, " + rowStudentData.RegistrationId + ")");
            //div.appendChild(chkId);
            //td.appendChild(div)
            //tr.appendChild(td);


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
                img.src = "data:image/png;base64," + COMMONMETHODS.ToBase64String(rowStudentData.StudentImage); // "~/Image/QR.png";
                td.appendChild(img);
            } else {
                td.textContent = "StudentImage";
            }
            tr.appendChild(td);

            //6
            //td = document.createElement('td');
            //var div = document.createElement('div');
            //div.className = "form-check";
            //var chkActive = document.createElement('input');
            //chkActive.type = 'checkbox';
            //chkActive.className = 'form-check-input';
            //chkActive.setAttribute("onclick", "ActDeactTeachers(this)");
            //chkActive.checked = rowStudentData.IsActive;
            //div.appendChild(chkActive);
            //td.appendChild(div)
            //tr.appendChild(td);

            tBody.appendChild(tr);
        }
    },

    DoActiveInActiveStudent: function (bActive) {
        debugger
        url = COREJS.ServerUrl("DoActiveInActiveStudent", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                //arrStudnetIds: arrStudnetIds,
                arrStudnetIds: [iRowId],
                bActive: bActive
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    DeleteStudent: function (bIsPermanent) {
        debugger;
        //var arrSelectedIds = COMMONMETHODS.GetSelectedIds("tblStudentDetail");
        //url = "Teacher/DeleteStudents";
        url = COREJS.ServerUrl("DeleteStudents", "Teacher", "TeacherArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                //arrSelectedStudents: arrSelectedIds,
                arrSelectedStudents: [{ ID: iRowId, Status: true }],
                bDeletePermanent: bIsPermanent
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    EditStudent: function () {
        debugger;
        var objStudent = {};
        objStudent = TEACHER.GetEditStudent();
        objStudent.StudentImage = [];

        url = COREJS.ServerUrl("CallCommonPopups", "Teacher", "TeacherArea");
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
    GetEditStudent: function () {
        debugger;
        if (lstAllStudents != null) {
            for (var i = 0; i < lstAllStudents.length; i++) {
                if (iRowId == lstAllStudents[i].RegistrationId) {
                    return lstAllStudents[i];
                }
            }
        }
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

    SelectRowAddSubject: function (eleTr, iSubId, tbodyId) {
        debugger;
        iSubjectId = iSubId;
        RIBBONCONTROLTEACHER.ResetRowColor(tbodyId);
        eleTr.className = 'SelectRow';
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
            TEACHER.SetStudentDetailData(arrFilterData, tblId);
        } else {
            TEACHER.SetStudentDetailData(lstAllStudents, tblId);
        }
    },
    
    RefreshStudentData: function (tblId) {
        debugger
        TEACHER.SetStudentDetailData(lstAllStudents, tblId);
    },

}