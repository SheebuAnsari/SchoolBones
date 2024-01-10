var url = '';
var lstTeacherData = [];
var iUserType = -1;
var iRowId = -1;

var COMMONMETHODS = {
    Test: function () {
        debugger
        alert("Calling test from Commonmethod.js file");
    },


    LoadMenuScreen: function (eleli) {
        //NOTE : ReservedMenu 1:Admin, 2:Teacher, 3:Accounts, 4:Student,5:AddMenu.
        var sActionMethod = '';
        var iMenuId = parseInt(eleli.id);
        var eModule = eleli.getAttribute("data-emodule");
        var sController = eleli.getAttribute("data-scontroller");
        var sActionMethod = eleli.getAttribute("data-sactionmethod");
        var sArea = eleli.getAttribute("data-area");

        if (sController == null || sActionMethod == null)
            return;

        DASHBOARDJS.ActiveMenu(eleli);

        //NOTE : NOTE : NOTE : NOTE : NOTE : NOTE : NOTE : NOTE : NOTE
        //var jsUrl = '@Url.Action("action", "controller")';
        //url = jsUrl.replace('action', sActionMethod).replace('controller', sController)

        debugger;
        url = COREJS.ServerUrl(sActionMethod, sController, sArea),
        $.ajax({
            url: url,
            cache: false,
            type: "Get",
            dataType: "html",
            data: {},
            success: function (data) {
                DASHBOARDJS.LoadModuleScreen(true, data, eModule);
            }
        });
    },

    SelectRow: function (eleTr, tbodyId) {
        debugger;
        iRowId = parseInt(eleTr.id);
        iUserType = parseInt(eleTr.getAttribute("data-usertype"));
        COMMONMETHODS.ResetRowColor(tbodyId);
        eleTr.className = 'SelectRow';
    },
    ResetRowColor: function (tbodyId) {
        debugger
        var tableRows = document.getElementById(tbodyId).children;
        for (var iRow = 0; iRow < tableRows.length; iRow++) {
            tr = tableRows[iRow];
            if (tr.className != 'DisableElement')
                tr.className = '';
        }
    },

    GetModuleId: function () {
        debugger
        var MenuTabs = document.getElementById("MenuTabs");
        if (MenuTabs != null && MenuTabs != undefined) {
            var MenuTabChildren = MenuTabs.children;
            for (var i = 0; i < MenuTabChildren.length; i++) {
                if (MenuTabChildren[i].className == 'nav-link active' || MenuTabChildren[i].getAttribute("aria-selected") == true) {
                    return parseInt(MenuTabChildren[i].getAttribute("data-module"));
                }
            }
        }
    },
    GetModuleName: function () {
        //debugger
        var MenuTabs = document.getElementById("MenuTabs");
        if (MenuTabs != null && MenuTabs != undefined) {
            var MenuTabChildren = MenuTabs.children;
            for (var i = 0; i < MenuTabChildren.length; i++) {
                if (MenuTabChildren[i].className == 'nav-link active' || MenuTabChildren[i].getAttribute("aria-selected") == true) {
                    return MenuTabChildren[i].getAttribute("data-modulename");
                }
            }
        }
    },

    

    LoadStudentsForMarks: function() {
        debugger
        var iClass = document.getElementById("ddlClass").value;
        var iYear = document.getElementById("ddlYear").value;
        var iExamType = document.getElementById("ddlExamType").value;

        url = COREJS.ServerUrl("LoadStudentsForMarks", "Teacher", "");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            data: {
                iClass: iClass,
                iYear: iYear,
                iExamType: iExamType
            },
            datatype: "json",
            success: function (data) {
                DASHBOARDJS.LoadModuleScreen(true, data, COMMONENUM.eModule.Teacher);
            }
        })
    },

    

    

    

   

    
    SaveStatus: function (bSuccess, data) {
        var sMsg = "";
        if(bSuccess){
            if (data.iStatus > 0) {
                COMMONMETHODS.ShowToastAsAlert("Successfully.");
            } else {
                COMMONMETHODS.ShowToastAsAlert("Something is wrong!");
            }
        }
    },
    GetSelectedIds: function (tblId) {
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
            IdValue.Status = true;
            //IdValue.Status = tr.children[9].children[0].children[0].checked;//chkActive
            if (chkActive.checked) {
                arrUpdateStatus.push(IdValue);
            }
        }
        return arrUpdateStatus;
    },
    
    ShowToastAsAlert: function (sMessage) {
        //debugger;
        Date.now()
        var toastElList = document.getElementById("liveToast");
        var toastBody = document.getElementById("toastBody");
        var lstTime = document.getElementById("lstTime");
        
        toastBody.innerText = sMessage;
        toastElList.className = "toast show";
        lstTime.textContent = new Date().toDateString();
        setTimeout(function () {
            toastElList.className = "toast hide";
        }, 5000)
    },
    
   
    
    //NOTE : btoa and atob work with strings, which they treat as arrays of bytes, so to use these two functions, 
    //you should first convert your array of integers (provided they fall in the range 0-255) into a string.
    ToBase64String: function (x) {
        debugger;
        return btoa(x.map(function (v) { return String.fromCharCode(v) }).join(''))
    },
    //OR
    ToBase64StringUsing_atob: function (x) {
        debugger;
        return atob(x).split('').map(function (v) { return v.codePointAt(0) });
    },

    ShowCommonPopup: function (bSuccess, data, sPopupId) {
        debugger;
        //$("#dvPopupContainer").html('');
        $('#' + sPopupId + '').appendTo("body").remove();  //NOTE: before rendering close the existing modal from Body tag.
        $("#dvPopupContainer").html(data);
        $('#' + sPopupId + '').appendTo("body").modal('show');
    },
    Close: function (sPopupId) {
        debugger;
        $('#' + sPopupId.id + '').appendTo("body").remove();  //Note: before rendering close the existing modal from Body tag.
    },
}