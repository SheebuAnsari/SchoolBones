var DASHBOARDJS = {
    Test: function () {
        debugger
        alert("Calling test from DASHBOARDJS.js file");
    },

    successLoadMenuScreen: function (data, eModule) {
        debugger
        if (eModule == COMMONENUM.eModule.None) {
            $("#dvDeveloper").html(data);
        } else if (eModule == COMMONENUM.eModule.Admin) {
            $("#dvAdmin").html(data); // HTML DOM replace
        } else if (eModule == COMMONENUM.eModule.Teacher) {
            $("#dvTeacher").html(data);
        } else if (eModule == COMMONENUM.eModule.Accounts) {
            $("#dvAccounts").html(data);
        } else if (eModule == COMMONENUM.eModule.Student) {
            $("#dvStudent").html(data);
        }
    },
    ActiveMenu: function (eleli) {
        var menuAccountTab = document.getElementsByClassName("subA");
        if (menuAccountTab != null) {
            for (var iMenu = 0; iMenu < menuAccountTab.length; iMenu++) {
                if (menuAccountTab[iMenu].children[0].className == 'nav-link SelectedMenu')
                    menuAccountTab[iMenu].children[0].className = 'nav-link';
            }
        }
        eleli.children[0].className = "nav-link SelectedMenu";
    },
}