var url = '';


var ADMISSION = {
    Test: function () {
        debugger
        alert("Calling test from Admission.js file");
    },
    AddUserDetails: function (iUserType) {
        debugger;
        if (iUserType == COMMONENUM.eModule.Teacher) {
            ADMISSION.SaveTeacherDetails();
        } else {
            ADMISSION.StudentAdmission();
        }
    },
    SaveTeacherDetails: function () {
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

        url = COREJS.ServerUrl("SaveTeacherDetails", "Admission", "AdmissionArea");
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

    StudentAdmission: function () {
        debugger;
        var photo = null;
        var iRegId = document.getElementById("hdnRegId").value;
        var txtFirstName = document.getElementById("txtFirstName");
        var txtLastName = document.getElementById("txtLastName");
        var txtAddress = document.getElementById("txtAddress");
        var txtCity = document.getElementById("txtCity");
        var txtContact = document.getElementById("txtContact");
        var txtAdmissionInClass = document.getElementById("txtAdmissionInClass");
        var chkIsActive = document.getElementById("chkIsActive");
        //var txtAdmissionDate = document.getElementById("txtAdmissionDate");
        var sSelectedDate = document.getElementById("txtAdmissionDate").value;
        var iAdmissionDate = FDate.convertDateToInt(sSelectedDate, "yyyy-mm-dd", 0);
        var stxtDOB = document.getElementById("txtDOB").value;
        var iDOB = FDate.convertDateToInt(stxtDOB, "yyyy-mm-dd", 0);
        if ($("#userImageInfo").attr("src").indexOf("base64") > 0) {
            photo = $("#userImageInfo").attr("src").substring($("#userImageInfo").attr("src").indexOf("base64,") + 7);
        }

        var oAdmissionDetails = {};
        oAdmissionDetails.RegistrationId = iRegId;
        oAdmissionDetails.StdName = txtLastName.value;
        oAdmissionDetails.FirstName = txtFirstName.value;
        oAdmissionDetails.LastName = txtLastName.value;
        oAdmissionDetails.Address = txtAddress.value;
        oAdmissionDetails.City = txtCity.value;
        oAdmissionDetails.Contact = parseInt(txtContact.value);
        oAdmissionDetails.AdmissionInClass = parseInt(txtAdmissionInClass.value);
        oAdmissionDetails.DOB = iDOB;
        oAdmissionDetails.IsActive = chkIsActive.checked;
        oAdmissionDetails.AdmissionDate = iAdmissionDate;
        oAdmissionDetails.StudentImage = photo;

        url = COREJS.ServerUrl("SaveStudentDetails", "Admission", "AdmissionArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                oAdmissionDetails: oAdmissionDetails
            },
            success: function (data) {
                debugger
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    SaveRegistration: function (iUserType) {
        debugger;
        var oRegistration = {};
        var sUserName = document.getElementById("txtUserName").value;
        var sLoginName = document.getElementById("txtFatherName").value;
        var sUserPassword = document.getElementById("txtDob").value;
        //var iUserType = parseInt(document.getElementById("ddlUserType").value)

        oRegistration.UserName = sUserName;
        oRegistration.LoginName = sLoginName;
        oRegistration.UserPassword = sUserPassword;
        oRegistration.UserType = iUserType;

        url = COREJS.ServerUrl("SaveRegistration", "Admin", "");
        url = COREJS.ServerUrl("SaveRegistration", "Admission", "AdmissionArea");
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

    DeleteRegistration: function () {
        debugger
        url = COREJS.ServerUrl("DeleteRegistration", "Admission", "AdmissionArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                iRegId: iRowId
            },
            success: function (data) {
                COMMONMETHODS.ShowToastAsAlert("Deleted successfully.");
                //sleep(1000)
                //COMMONRIBBON.LoadRegUser();
            }
        });
    },

    RefreshRegistration: function (iUserType) {
        debugger
        url = COREJS.ServerUrl("LoadRegistration", "Admission", "AdmissionArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "html",
            data: {
                iModuleId: iUserType,
            },
            success: function (data) {
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Admission);
            }
        });
    },

    LoadUserDetails: function () {
        debugger;
        var dvLoadRegUser = document.getElementById("dvLoadRegUser");
        var dvAddUserDetail = document.getElementById("dvAddUserDetail");
        url = COREJS.ServerUrl("AddUserDetails", "Admission", "AdmissionArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            data: {
                iRegId: iRowId,
            },
            datatype: "html",
            success: function (data) {
                if (data == 'Alert : No record available.') {
                    return COMMONMETHODS.ShowToastAsAlert(data);
                }
                //$("#dvAddUserDetail").html('');
                //dvLoadRegUser.style.display = 'none';
                //$("#dvAddUserDetail").html(data);

                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Admission);
            }
        })
    },

    BackToLoadUser: function (iUserType) {
        debugger;
        //var dvLoadRegUser = document.getElementById("dvLoadRegUser");
        //var dvAddUserDetail = document.getElementById("dvAddUserDetail");

        //dvAddUserDetail.innerHTML = '';
        //dvLoadRegUser.style.display = 'block';
        ADMISSION.RefreshRegistration(iUserType);
    },

    UpdateStudent: function () {
        debugger;

        var txtEnrollmentNo = document.getElementById("txtEnrollmentNo");
        var txtStdName = document.getElementById("txtStdName");
        var txtAddress = document.getElementById("txtAddress");
        var txtCity = document.getElementById("txtCity");
        var txtContact = document.getElementById("txtContact");
        var txtAdmissionInClass = document.getElementById("txtAdmissionInClass");
        var txtDOB = document.getElementById("txtDOB");
        var chkIsActive = document.getElementById("chkIsActive");
        //var txtAdmissionDate = document.getElementById("txtAdmissionDate");
        var sSelectedDate = document.getElementById("txtAdmissionDate").value;
        var iAdmissionDate = FDate.convertDateToInt(sSelectedDate, "yyyy-mm-dd", 0);



        var oAdmissionDetails = {};
        oAdmissionDetails.RegistrationId = txtEnrollmentNo.value;
        oAdmissionDetails.StdName = txtStdName.value;
        oAdmissionDetails.Address = txtAddress.value;
        oAdmissionDetails.City = txtCity.value;
        oAdmissionDetails.Contact = parseInt(txtContact.value);
        oAdmissionDetails.AdmissionInClass = parseInt(txtAdmissionInClass.value);
        oAdmissionDetails.DOB = parseInt(txtDOB.value);
        oAdmissionDetails.IsActive = chkIsActive.checked;
        oAdmissionDetails.AdmissionDate = iAdmissionDate;

        url = COREJS.ServerUrl("StudentAdmission", "Admission", "AdmissionArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                oAdmissionDetails: oAdmissionDetails
            },
            success: function (data) {
                debugger
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    triggerOpen: function () {
        //debugger;
        $("#userphoto").trigger('click');
    },
    uploadImage: function (ele) {
        debugger;
        if (ele.files && ele.files[0]) {
            var fileType = ele.files[0].type;
            if (fileType == "image/png" || fileType == "image/gif" || fileType == "image/jpg" || fileType == "image/jpeg") {
                if (ele.files[0].size < 100000) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        debugger
                        $('#userImageInfo').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(ele.files[0]);



                    //var reader = new FileReader();
                    //reader.onload = function (e) {
                    //    $('#' + id).attr('src', e.target.result);
                    //}
                    //reader.readAsDataURL(ele.files[0]);
                    //$(ele).css("color", "#000");
                }
                else {
                    $(ele).css("color", "red");
                    //COMMON.prototype.showMessage(msgs.msgFilesizeExceed, "Error");
                    alert("Failed due to file size.");
                }
            }
            else {
                $(ele).css("color", "red");
                //COMMON.prototype.showMessage(msgs.msgInvalidFileType, "Error");
                alert("Failed due to file type.");
            }
        }
    },
}