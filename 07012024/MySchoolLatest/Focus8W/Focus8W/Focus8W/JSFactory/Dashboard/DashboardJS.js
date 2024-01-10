var DASHBOARDJS = {
    Test: function () {
        debugger
        alert("Calling test from DASHBOARDJS.js file");
    },

    LoadModuleScreen: function (bSuccess, data, eModule) {
        debugger
        if (eModule == COMMONENUM.eModule.None) {
            $("#dvDeveloper").html(data);
        } else if (eModule == COMMONENUM.eModule.Admin) {
            $("#dvAdmin").html(data); // HTML DOM replace
        } else if (eModule == COMMONENUM.eModule.Teacher) {
            $("#dvTeacher").html(data);
        } else if (eModule == COMMONENUM.eModule.Account) {
            $("#dvAccount").html(data);
        } else if (eModule == COMMONENUM.eModule.Student) {
            $("#dvStudent").html(data);
        } else if (eModule == COMMONENUM.eModule.Admission) {
            $("#dvAdmission").html(data);
        }
    },
    ActiveMenu: function (eleli) {
        //var menuAccountTab = document.getElementsByClassName("subA");
        var sModuleName = COMMONMETHODS.GetModuleName();
        var id = 'ul_' + sModuleName;
        var menuAccountTab = document.getElementById(id);
        if (menuAccountTab != null) {
            for (var iMenu = 0; iMenu < menuAccountTab.children.length; iMenu++) {
                if (menuAccountTab.children[iMenu].children[0].className == 'nav-link SelectedMenu')
                    menuAccountTab.children[iMenu].children[0].className = 'nav-link';
            }
        }
        eleli.children[0].className = "nav-link SelectedMenu";
    },
}