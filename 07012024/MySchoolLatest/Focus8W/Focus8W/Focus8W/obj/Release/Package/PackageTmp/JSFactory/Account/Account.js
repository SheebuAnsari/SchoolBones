var arrTeacherSalary = [];


var ACCOUNT = {
    Test: function () {
        debugger
        alert("Calling test from ACCOUNT.js file");
    },
    GetSalaryData: function() {
        debugger;
        var arrSalary = [];
        var tblDefineSalary = document.getElementById("tblDefineSalary");
        var arrTr = tblDefineSalary.children;

        for (var i = 0; i < arrTr.length; i++) {
            var oSalary = {};
            oSalary.SalaryId = parseInt(arrTr[i].id);
            oSalary.RegId = parseInt(arrTr[i].getAttribute("data-regid"));
            oSalary.Year = parseInt(document.getElementById("ddlYear").value);
            oSalary.MonthlySalary = parseInt(arrTr[i].children[3].children[0].value);
            arrSalary.push(oSalary)
        }
        return arrSalary;
    }
}