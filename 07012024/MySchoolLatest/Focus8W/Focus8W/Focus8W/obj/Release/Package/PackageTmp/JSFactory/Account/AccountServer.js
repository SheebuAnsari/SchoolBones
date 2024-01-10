var url = '';


var ACCOUNTSERVER = {
    Test: function () {
        debugger
        alert("Calling test from ACCOUNTSERVER.js file");
    },
    LoadClasswiseStudents: function () {
        var ddlClass = parseInt(document.getElementById("ddlClass").value)
        var ddlYear = parseInt(document.getElementById("ddlYear").value)

        url = COREJS.ServerUrl("LoadClasswiseStudents", "Accounts", "")
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "HTML",
            data: {
                iClass: ddlClass,
                iYear: ddlYear
            },
            success: function (data) {
                $('#dvLoadStudents').html(data);
            }
        });
    },

    LoadFeesStructure: function (eleDdl) {
        debugger;
        //var ddlYear = parseInt(document.getElementById("ddlYear").value)
        url = COREJS.ServerUrl("LoadFeesStructure", "Accounts", "");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            data: {
                iYear: parseInt(eleDdl.value)
            },
            datatype: "html",
            success: function (data) {
                $('#dvFeesStructure').html(data);
            }
        })
    },

    DefineMontlySalary: function (eleDdl) {
        debugger;
        url = COREJS.ServerUrl("LoadMonthlySalary", "Accounts", "");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            data: {
                iYear: parseInt(eleDdl.value)
            },
            datatype: "HTML",
            success: function (data) {
                $('#dvDefineSalary').html(data);
            }
        })
    },
    SaveSalary: function () {
        debugger
        url = COREJS.ServerUrl("SaveSalary", "Accounts", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                arrTeacherSalary: ACCOUNT.GetSalaryData(),
            },
            success: function (data) {
                COMMONMETHODS.ShowToastAsAlert("Saved");
            }
        });
    },
    LoadSalaryDetails: function () {
        debugger;
        url = COREJS.ServerUrl("LoadTeachersSalary", "Accounts", "");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            data: {
                iMonth: parseInt(document.getElementById("ddlMonth").value),
                iYear: parseInt(document.getElementById("ddlYear").value)
            },
            datatype: "HTML",
            success: function (data) {
                $('#dvSalaryDetails').html(data);
            }
        })
    },
    LoadFeeCollection: function () {
        debugger;
        url = COREJS.ServerUrl("LoadFeeCollection", "Accounts", "");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            data: {
                iClass: parseInt(document.getElementById("ddlClass").value),
                iMonth: parseInt(document.getElementById("ddlMonth").value),
                iYear: parseInt(document.getElementById("ddlYear").value)
            },
            datatype: "HTML",
            success: function (data) {
                $('#dvLoadFeeCollection').html(data);
            }
        })
    }
}