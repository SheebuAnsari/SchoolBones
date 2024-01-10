var url = '';
var DAYS = 6; //or rows
var dvINFOINDEX = 0;
var eleListItem = undefined;
var targetId = '';
var iTeacherId = 0;
var sTeacherName = 0;
var sSelectedSubName = 0;


var TIMETABLE = {
    Test: function () {
        debugger
        alert("Calling test from TIMETABLE.js file");
    },
    DeleteTimeTableLayout: function () {
        debugger;
        url = COREJS.ServerUrl("DeleteTimeTableLayout", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "JSON",
            data: {
                iLayoutId: iRowId,
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },
    SaveTimeTableLayout: function () {
        debugger
        //var ddlLayout = document.getElementById("ddlLayout");
        //var ddlClass = document.getElementById("ddlClass");
        var txtAlias = document.getElementById("txtAlias");
        var txtNoOfPeriod = document.getElementById("txtNoOfPeriod");
        var txtLunchBreak = document.getElementById("txtLunchBreak");

        var oTimeTableLayout = {};
        oTimeTableLayout.Class = parseInt(ddlClass.value);
        oTimeTableLayout.LayoutName = txtAlias.value;
        oTimeTableLayout.CreatedDate = 0;
        oTimeTableLayout.Duration = TIMETABLE.TimeToInt('txtDuration');
        oTimeTableLayout.FirstPeriod = TIMETABLE.TimeToInt('txtStartingPeriod');
        oTimeTableLayout.NoOfPeriod = parseInt(txtNoOfPeriod.value);
        //if (txtAlias != undefined && txtAlias.value != '') {
        //    oTimeTableLayout.LayoutName = txtAlias.value;
        //} else {
        //    //oTimeTableLayout.LayoutName = ddlLayout.value;
        //}


        oTimeTableLayout.LunchBreak = parseInt(txtLunchBreak.value);

        url = COREJS.ServerUrl("SaveTimeTableLayout", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "JSON",
            data: {
                oLayout: oTimeTableLayout,
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    

    LoadTimeTableAsPerLayoutAndClass: function () {
        debugger;
        var ddlLayout = document.getElementById("ddlLayout");
        var ddlClass = document.getElementById("ddlClass");
        url = COREJS.ServerUrl("LoadTimeTableAsPerLayoutAndClass", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                iLayoutId: ddlLayout.value,
                //iClass: parseInt(ddlClass.value),

            },
            success: function (data) {
                if (data.includes('!')) {
                    COMMONMETHODS.ShowToastAsAlert(data);
                } else {
                    //document.getElementById("dvTimeTableDataNew").innerHTML = data;
                    //document.getElementById("dvTimeTableLayouts").innerHTML = data;
                    $("#dvTimeTableDataNew").html(data);
                }
            }
        });
    },
    SaveTimeTable: function () {
        debugger;
        var iCreatedDate = 0;
        var iClass = 0;
        var ddlLayout = '';
        ddlLayout = document.getElementById("ddlLayout");

        //iCreatedDate = FDate.convertDateToInt(document.getElementById("txtCreatedDate").value, "yyyy-mm-dd", 0);
        //iClass = parseInt(document.getElementById("ddlClass").value);
        var oTimeTable = {};
        oTimeTable.Class = 0;//iClass;
        oTimeTable.CreatedDate = 0;//iCreatedDate;
        oTimeTable.RowData = TIMETABLE.GetRowData();
        url = COREJS.ServerUrl("SaveTimeTable", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "html",
            data: {
                oTimeTable: oTimeTable,
                iLayoutId: ddlLayout.value,
            },
            success: function (data) {
                COMMONMETHODS.ShowToastAsAlert("Data saved successfully");
            }
        });
    },

    DeleteTimeTable: function () {
        debugger
        //var iClass = parseInt(document.getElementById("ddlClass").value);
        var ddlLayout = document.getElementById("ddlLayout");
        url = COREJS.ServerUrl("DeleteTimeTable", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "POST",
            dataType: "JSON",
            data: {
                iLayoutId: ddlLayout.value,
            },
            success: function (data) {
                COMMONMETHODS.SaveStatus(true, data);
            }
        });
    },

    DragStartLi: function (eleLi, evt) {
        //debugger         
        //COMMONMETHODS.ShowToastAsAlert("Drag Start Li");         
        srcListItem = eleLi;
        TIMETABLE.GetTeacherAttributes(eleLi, evt);  //Take attribute of ORIGIN     
    },

    OndragendLi: function (eleLi, evt) {
        //debugger         
        COMMONMETHODS.ShowToastAsAlert("On drag end Li and target id : " + targetId);
        if (TIMETABLE.IsTeacherAvailable() == true) {
            let list = document.getElementById("lstSubjects");
            list.style = "list-style: none;";
            list.innerHTML = '';
            for (var iSub = 0; iSub < arrSubjects.length; iSub++) {
                let li = document.createElement('li');

                let dvCheckbox = document.createElement('div');
                dvCheckbox.className = 'form-check';
                var rdo = document.createElement("INPUT");
                rdo.setAttribute("type", "radio");
                rdo.setAttribute("onclick", "TIMETABLE.GetSubject(this)");
                rdo.id = 'rdoSubject' + iSub;
                rdo.name = 'rdoSubject';
                rdo.value = arrSubjects[iSub].SubjectName;
                rdo.className = 'form-check-input';

                var lblCaption = document.createElement('label');
                lblCaption.className = 'form-check-label';
                lblCaption.htmlFor = 'rdoSubject' + iSub;
                lblCaption.innerText = arrSubjects[iSub].SubjectName;

                dvCheckbox.appendChild(rdo);
                dvCheckbox.appendChild(lblCaption);
                li.appendChild(dvCheckbox);
                list.appendChild(li);
            }
            document.getElementById("lblAssignTeacher").textContent = " Assign " + sTeacherName + " fol all days";
            //Open TeacherExpertSubject popup
            $('#popupTeacherSubject').appendTo("body").modal('show');

            //Drop Teacher in cell if Subject are selected and click on OK
            //Drop(eleLi, evt)
        } else {
            alert("Stop")
        }
    },


    IsTeacherAvailable: function () {
        debugger;
        var RowId = targetId.split('_')[0];
        var eleCols = document.getElementById("tbodyTimeTable").children[0].children; //Get 1 Row's TDs
        for (var iCol = 1; iCol <= eleCols.length; iCol++) {
            var cellId = RowId + "_" + "C" + iCol;
            if (cellId != targetId && document.getElementById(cellId) != null) {
                if (document.getElementById(cellId).children[dvINFOINDEX] != undefined) {
                    var iRegId = document.getElementById(cellId).children[dvINFOINDEX].getAttribute("data-regid");
                    if (iRegId == iTeacherId) {
                        return false;
                    }
                }
            }
        }
        return true;
    },

    OndragoverCell: function (eleLi, evt) {
        //debugger
        //targetId = evt.target.id;
        targetId = evt.currentTarget.id;
    },

    Drop: function (targetId) {
        debugger;
        //var targetEleTd = document.getElementById(targetId);
        //targetEleTd.innerHTML='';
        //var dvTdInfo = document.createElement('div');
        //dvTdInfo.innerHTML = '';
        //dvTdInfo.className='row text-center';
        //dvTdInfo.setAttribute("data-regid", iTeacherId);
        //dvTdInfo.setAttribute("data-name", sTeacherName);
        //dvTdInfo.setAttribute("data-subjectname", sSelectedSubName);
        ////SetAttribute
        //var span = '';
        //span = document.createElement('span');
        //span.textContent = sTeacherName;
        //dvTdInfo.appendChild(span);

        ////AddSubject in cell
        //span = '';
        //span = document.createElement('span');
        //span.textContent = '['+sSelectedSubName+']';
        //dvTdInfo.appendChild(span);
        //targetEleTd.appendChild(dvTdInfo);
        TIMETABLE.CreateCellData(targetId);
    },

    CreateCellData: function (targetId) {
        debugger
        var targetEleTd = document.getElementById(targetId);
        targetEleTd.innerHTML = '';
        var dvTdInfo = document.createElement('div');
        dvTdInfo.innerHTML = '';
        dvTdInfo.className = 'row text-center';
        dvTdInfo.setAttribute("data-regid", iTeacherId);
        dvTdInfo.setAttribute("data-name", sTeacherName);
        dvTdInfo.setAttribute("data-subjectname", sSelectedSubName);
        //SetAttribute
        var span = '';
        span = document.createElement('span');
        span.textContent = sTeacherName;
        dvTdInfo.appendChild(span);

        //AddSubject in cell
        span = '';
        span = document.createElement('span');
        span.textContent = '[' + sSelectedSubName + ']';
        dvTdInfo.appendChild(span);
        targetEleTd.appendChild(dvTdInfo);
    },

    AssignForWeek: function (eleChk) {
        debugger;
        if (eleChk.checked) {
            //alert("check")

        }
        else {
            alert("uncheck")
        }
    },
    GetTeacherAttributes: function (eleLi, evt) {
        iTeacherId = parseInt(eleLi.id);
        sTeacherName = eleLi.getAttribute("data-name");
    },
    LoadClasswiseTimeTable: function () {
        debugger;
        var iClass = parseInt(document.getElementById("ddlClass").value);
        url = COREJS.ServerUrl("LoadClasswiseTimeTable", "Admin", "AdminArea");
        $.ajax({
            url: url,
            cache: false,
            type: "GET",
            dataType: "html",
            data: {
                iClass: iClass,
            },
            success: function (data) {
                //document.getElementById("dvTimeTableData").innerHTML = data;
                $("#dvClasswiseTimeTable").html(data);
            }
        });
    },

    GetRowData: function () {
        debugger;
        var arrRowWiseData = [];
        var eleRows = document.getElementById("tbodyTimeTable").children;
        //Note skip Row:0 becoz this is header, and also skip Col:0 that is for Days
        for (var iRow = 0; iRow < eleRows.length; iRow++) {
            var cellData = [];
            var oRowData = {};
            var eleCols = eleRows[iRow].children;
            for (var iCol = 1; iCol < eleCols.length; iCol++) {
                var oCell = {};
                if (eleCols[iCol].children[dvINFOINDEX] != undefined) {
                    if (parseInt(eleCols[iCol].children[dvINFOINDEX].getAttribute("data-regid")) > 0 && eleCols[iCol].children[dvINFOINDEX].getAttribute("data-name") != null) {
                        oCell.RowId = (iRow + 1);
                        oCell.ColId = iCol;
                        oCell.RegId = parseInt(eleCols[iCol].children[dvINFOINDEX].getAttribute("data-regid"));
                        oCell.Name = eleCols[iCol].children[dvINFOINDEX].getAttribute("data-name");
                        oCell.SubjectName = eleCols[iCol].children[dvINFOINDEX].getAttribute("data-subjectname");
                        cellData.push(oCell);
                    }
                }
            }
            oRowData.Day = (iRow + 1);
            oRowData.CellData = cellData;
            arrRowWiseData.push(oRowData);
        }
        return arrRowWiseData;
    },

    GetSubject: function (eleRdo) {
        debugger;
        sSelectedSubName = eleRdo.value
    },

    SelectSubject: function () {
        debugger;
        TIMETABLE.Drop(targetId);

        if (document.getElementById("chkAssignTeacher").checked) {
            TIMETABLE.AssignTeacherWeekly();
        }
        else {
            //alert("!c")
        }
    },

    AssignTeacherWeekly: function () {
        debugger;
        var ColId = targetId.split('_')[1];

        for (var iRow = 1; iRow <= DAYS; iRow++) {
            ///Read all cell of column ColId
            ///Create cellId using ColId and loop
            var cellId = "R" + iRow + "_" + ColId;
            if (cellId != targetId) {
                TIMETABLE.CreateCellData(cellId);
            }
        }
    },


    TimeToInt: function (iDateControl) {
        debugger;
        var Time1 = '';
        var iTimeValue = 0;
        var arTime = [];
        
        Time1 = document.getElementById(iDateControl).value
        arTime = Time1.split(':');
        for (var iCounter = 0; iCounter < arTime.length; iCounter++) {
            switch (iCounter) {
                case 0://Hour            
                    iTimeValue += arTime[iCounter] * 60 * 60;
                    break;
                case 1://Min                
                    iTimeValue += arTime[iCounter] * 60;
                    break;
                case 2://Sec                 
                    iTimeValue += arTime[iCounter];
                    break;
            }
        }
        return iTimeValue;
    },

    IntToTime: function (iDate, iDateControl) {
        iDate = Number(iDate);
        var h = Math.floor(iDate / 3600);
        var m = Math.floor(iDate % 3600 / 60);
        var s = Math.floor(iDate % 3600 % 60);
        h = h > 9 ? h : '0' + h;
        m = m > 9 ? m : '0' + m;
        var sTime = h + ":" + m;
        //document.getElementById(iDateControl).value = sTime;
        return sTime;
    },

    CalculateNoOfPeriods: function (strTimeCtrlId, endTimeCtrlId, durationOfPeriodsCtrlId) {
        debugger;
        var iTotalNoOfPeriods = 0; //if fraction time is coming then assusme it as 1.
        var iTotalTime = 0;
        var sValue = 0;
        var eValue = 0;
        var iDuration = 0;

        sValue = TIMETABLE.TimeToInt(strTimeCtrlId);
        eValue = TIMETABLE.TimeToInt(endTimeCtrlId);
        iDuration = TIMETABLE.TimeToInt(durationOfPeriodsCtrlId);
        iTotalTime = eValue - sValue;
        document.getElementById("noofperiods").textContent = "No of Periods : " + parseInt(iTotalTime / iDuration) + " and Fraction Time : " + parseInt((iTotalTime % iDuration) / 60);

        iTotalNoOfPeriods = parseInt(iTotalTime / iDuration) + (parseInt(iTotalTime % iDuration) > 0 ? 1 : 0)
        if (document.getElementById("txtNoOfPeriod") != null) {
            document.getElementById("txtNoOfPeriod").value = iTotalNoOfPeriods;
        }
        //return iTotalNoOfPeriods;
    },
}