var url = '';



var DASHBOARDJSERVER = {
    Test: function () {
        debugger
        alert("Calling test from DASHBOARDJSERVER.js file");
    },

    LoadMenuScreen: function (eleli) {
        //NOTE : ReservedMenu 1:Admin, 2:Teacher, 3:Accounts, 4:Student,5:AddMenu.
        var sActionMethod = '';
        var iMenuId = parseInt(eleli.id);
        var eModule = eleli.getAttribute("data-emodule");
        var sController = eleli.getAttribute("data-scontroller");
        var sActionMethod = eleli.getAttribute("data-sactionmethod");

        if (sController == null || sActionMethod == null)
            return;

        //DASHBOARDJS.ActiveMenu(eleli);

        debugger;
        //NOTE : NOTE : NOTE : NOTE : NOTE : NOTE : NOTE : NOTE : NOTE
        //var jsUrl = '@Url.Action("action", "controller")';
        //url = jsUrl.replace('action', sActionMethod).replace('controller', sController)

        debugger;
        $.ajax({
            url: COREJS.ServerUrl(sActionMethod,sController, ""),
            cache: false,
            type: "Get",
            dataType: "html",
            data: {},
            success: function (data) {
                DASHBOARDJS.successLoadMenuScreen(data, eModule);
            }
        });
    },
}