var COREJS = {
    ServerUrl: function (actionName, controllerName, areaName) {
        //debugger
        var protocol = document.location.protocol;
        var hostname = document.location.host;
        var pathname = document.location.pathname;

        if (pathname.indexOf("/", 1) < 0) {
            pathname = pathname + "/";
        }
        pathname = pathname.substring(0, pathname.indexOf("/", 1));
        if (areaName == "" || areaName == null || areaName == undefined) {
            return protocol + "//" + hostname + pathname + "/" + controllerName + "/" + actionName;
        }
        return protocol + "//" + hostname + pathname + "/" + areaName + "/" + controllerName + "/" + actionName;
    },
}