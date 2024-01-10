var COMMONINPUT = {
    Test: function () {
        debugger
        alert("Calling test from Commonmethod.js file");
    },
    UpdateCalendar: function(){
        debugger;
        var minDate = 0;
        var maxDate = 0;
        //var ddlClass = 0;

        //if (document.getElementById("ddlClass") != null) {
        //    ddlClass = parseInt(document.getElementById("ddlClass").value);
        //}

        var ddlMonth = parseInt(document.getElementById("ddlMonth").value)
        var ddlYear = parseInt(document.getElementById("ddlYear").value)

        if (ddlMonth > 0 && ddlYear > 0) {
            minDate = COMMONINPUT.GetMinMaxDate(ddlMonth, ddlYear, 0);
            maxDate = COMMONINPUT.GetMinMaxDate(ddlMonth, ddlYear, 1);
            var dtAttendaneDate = document.getElementById("dtAttendaneDate");  //yyyy-mm-dd
            dtAttendaneDate.min = minDate;
            dtAttendaneDate.max = maxDate;
            document.getElementById("dvCalendar").style.display = 'block';
        }
    },
    GetMinMaxDate: function(iMonth, iYear, bMin) {
        var sMonth = '';
        if (iMonth >= 1 && iMonth <= 9) {
            sMonth = '0' + iMonth;
        } else {
            sMonth = iMonth;
        }
        if (bMin == 0) {
            return iYear + '-' + sMonth + '-' + '01';
        } else {
            return iYear + '-' + sMonth + '-' + GREGOREAN.maxDays(iMonth, iYear);
        }
    }
}