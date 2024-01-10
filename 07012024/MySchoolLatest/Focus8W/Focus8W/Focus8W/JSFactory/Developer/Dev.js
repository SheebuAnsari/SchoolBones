var url = '';

var DEV = {
    Test: function () {
        debugger
        alert("Calling test from DEV.js file");
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

    TestIndex: function () {
        debugger;
        //var eAction = -1;
        //var iMenuId = -1;
        //var oMenu = {};

        //eAction = 0;//document.getElementById("hdnAction").value;
        //iMenuId = iRowId;//document.getElementById("hdnMenuId").value;

        //oMenu.Id = iMenuId;
        //oMenu.Tag = {};

        url = COREJS.ServerUrl("TestIndex", "Developer", "DeveloperArea");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "HTML",
            data: {
            },
            success: function (data) {
                alert("this is test controller by area.");
            }
        });
    },

    SaveMenu: function () {
        debugger;
        var oMenu = {};
        var txtMenuName = document.getElementById("txtMenuName");
        var ddlModule = document.getElementById("ddlModule");
        //var txtSubModule = document.getElementById("txtSubModule");
        //var chkAIsGroup = document.getElementById("chkAIsGroup");

        oMenu.MenuName = txtMenuName.value;
        oMenu.Module = -1;
        oMenu.SubModule = parseInt(ddlModule.value);
        oMenu.IsGroup = 0;
        oMenu.IsActive = 1;

        url = COREJS.ServerUrl("SaveMenu", "Developer", "DeveloperArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                oMenu: oMenu
            },
            success: function (data) {
                debugger
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    ConfirmationDeleteMenu: function () {
        debugger
        //$('#popupMenuOpeations').appendTo("body").remove();  //Note: before rendering close the existing modal from Body tag.
        //$("#dvContainerMenuOpeations").html();
        //$('#popupMenuOpeations').appendTo("body").modal('show');

        //document.getElementById("hdnAction").value = eAction;
        //document.getElementById("hdnMenuId").value = iRowId;

        DEV.DeleteMenu();
    },

    DeleteMenu: function () {
        debugger;
        var eAction = -1;
        var iMenuId = -1;
        var oMenu = {};

        eAction = 0;//document.getElementById("hdnAction").value;
        iMenuId = iRowId;//document.getElementById("hdnMenuId").value;

        oMenu.Id = iMenuId;
        oMenu.Tag = {};

        url = COREJS.ServerUrl("MenuCRUD", "Developer", "DeveloperArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "json",
            data: {
                eAction: 3,
                oMenu: oMenu,
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },
    ToExcel: function (ele) {
        debugger
        //COMMONMETHODS.ShowToastAsAlert("Not Implemented")
        url = COREJS.ServerUrl("ToExcel", "Teacher", "DeveloperArea");
        $.ajax({
            url: url,
            cache: false,
            datatype: "json",
            type: "Get",
            data: {
            },
            success: function (data) {
                COMMONMETHODS.ShowToastAsAlert("Not implemented");
            }
        })
    },

    FilterMenu: function (ele) {
        debugger;
        var eModule = '';
        if (ele == undefined || ele == null) {
            eModule = "Admin";
        } else {
            eModule = ele.id;
        }

        url = COREJS.ServerUrl("FilterMenus", "Developer", "DeveloperArea");
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "Html",
            data: {
                eModule: eModule
            },
            success: function (data) {
                DEV.successFilterMenu(true, data);
            }
        });
    },
    successFilterMenu: function (bSuccess, data) {
        $("#dvLoadMenus").html(data);
    },
}