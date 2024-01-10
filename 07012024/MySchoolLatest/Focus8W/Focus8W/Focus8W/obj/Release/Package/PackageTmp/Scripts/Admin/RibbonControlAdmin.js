var url = '';

var RIBBONCONTROLADMIN = {
    Test: function () {
        debugger
        alert("Calling test from RIBBONCONTROLADMIN.js file");
    },

    ToExcel: function (ele) {
        debugger
        COMMONMETHODS.ShowToastAsAlert("Not Implemented")
        url = COREJS.ServerUrl("ToExcel", "Teacher", "");
        //$.ajax({
        //    url: url,
        //    cache: false,
        //    datatype: "json",
        //    type: "Get",
        //    data: {
        //    },
        //    success: function (data) {
        //        COMMONMETHODS.SaveStatus(true,data)
        //    }
        //})
    },
    ToggleSubModule: function () {
        debugger;
        var ulModule = document.getElementById("ulModule");
        if (ulModule.style.display == "none") {
            ulModule.style.display = "block";
        } else {
            ulModule.style.display = "none";
        }
    },
    FilterMenu:function(ele){
        debugger;
        var eModule = '';

        eModule = ele.id;
        url = COREJS.ServerUrl("FilterMenus", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "Html",
            data: {
                eModule: eModule
            },
            success: function (data) {
                RIBBONCONTROLADMIN.successFilterMenu(true, data);
            }
        });
    },
    successFilterMenu: function (bSuccess, data) {
        $("#dvLoadMenus").html(data);
    },

    DoActiveInActiveTeacher: function (bActive) {
        debugger
        url = COREJS.ServerUrl("DoActiveInActiveTeacher", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                arrSelectedTechers: arrTeacherIds,
                bActive: bActive
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },
    UpdateTimeTable: function () {
        debugger;
        var iCreatedDate = 0;
        var iClass = 0;

        iCreatedDate = FDate.convertDateToInt(document.getElementById("txtCreatedDate").value, "yyyy-mm-dd", 0);
        iClass = parseInt(document.getElementById("ddlClass").value);
        var oTimeTable = {};
        oTimeTable.Class = iClass;
        oTimeTable.CreatedDate = iCreatedDate;
        oTimeTable.RowData = GetRowData();
        url = COREJS.ServerUrl("SaveTimeTable", "Admin", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                oTimeTable: oTimeTable
            },
            success: function (data) {
                COMMONMETHODS.ShowToastAsAlert("Data saved successfully");
            }
        });
    }

}