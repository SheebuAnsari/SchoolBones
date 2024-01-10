var FDate = {
    today: function(iCalendarType) {
        let iDate = 0;
        let today = null;

        try {
            today = FOCUSDATETIME.getTodayDate(iCalendarType);
            iDate = DATE.prototype.convertIntoFocusDate(today[0], today[1], today[2]);
        }
        catch (err) {
            alert("Exception: {FDate.today} " + err.message);
        }

        return (iDate);
    },

    convertDateToInt: function(sDate, sFormat, iCalendarType) {
        let sValue = "";
        let sMonthText = "";
        let sYearText = "";
        let iDay = 0;
        let iMonth = 0;
        let iYear = 0;
        let iDate = 0;

        try {
            sValue = FDate.extractDayText(sDate, sFormat);
            iDay = FConvert.toInt(sValue);

            sValue = FDate.extractMonthText(sDate, sFormat);
            iMonth = FConvert.toInt(sValue);

            sValue = FDate.extractYearText(sDate, sFormat);
            iYear = FConvert.toInt(sValue);

            if(iDay == 0 || iMonth == 0 || iYear == 0) {
                return(0);
            }

            if(FDate.isValidDatePart(iCalendarType, iDay, iMonth, iYear) == false) {
                return(0);
            }

            iDate = FDate.convertToInt(iDay, iMonth, iYear);
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.convertDateToInt} " + err.message, "red");
        }

        return(iDate);
    },

    convertToInt: function (iDay, iMonth, iYear) {
        let iDate = 0;

        iDate = iYear << 16 | iMonth << 8 | iDay;

        return (iDate);
    },

    getDayFromInt: function(iDate) {
        let value = 0;

        value = (iDate & 0xFF);

        return(value);
    },

    getMonthFromInt: function(iDate) {
        let value = 0;

        value = ((iDate >> 8) & 0xFF);

        return(value);
    },

    getYearFromInt: function(iDate) {
        let value = 0;

        value = (iDate >> 16);

        return(value);
    },

    getMinYear: function (iCalendarType) {
        let iValue = 0;

        switch (iCalendarType) {
            case eCalendarType.Hijri:
                iValue = HIJRI.minYear();
                break;
            case eCalendarType.Shamsi:
                iValue = PERSIAN.minYear();
                break;
            default:
                iValue = GREGOREAN.minYear();
                break;
        }

        return (iValue);
    },

    getMaxYear: function (iCalendarType) {
        let iValue = 0;

        switch (iCalendarType) {
            case eCalendarType.Hijri:
                iValue = HIJRI.maxYear();
                break;
            case eCalendarType.Shamsi:
                iValue = PERSIAN.maxYear();
                break;
            default:
                iValue = GREGOREAN.maxYear();
                break;
        }

        return (iValue);
    },

    maxDays: function (iMonth, iYear, iCalendarType) {
        let iMax = 0;

        try {
            iCalendarType = FConvert.toInt(iCalendarType);
            switch (iCalendarType) {
                case eCalendarType.Hijri:
                    iMax = HIJRI.maxDays(iMonth, iYear);
                    break;
                case eCalendarType.Shamsi:
                    iMax = PERSIAN.maxDays(iMonth, iYear);
                    break;
                default:
                    iMax = GREGOREAN.maxDays(iMonth, iYear);
                    break;
            }
        }
        catch (err) {
            WriteConsoleLog("Exception: {FDate.maxDays} " + err.message, "red");
        }

        return (iMax);
    },

    addDays: function (iFocusDate, iNoOfDays, iCalendarType) {
        let date = null;
        let iDay = 0;
        let iMonth = 0;
        let iYear = 0;

        try {
            iDay = FDate.getDayFromInt(iFocusDate);
            iMonth = FDate.getMonthFromInt(iFocusDate);
            iYear = FDate.getYearFromInt(iFocusDate);

            switch (iCalendarType) {
                case eCalendarType.Hijri:
                    date = HIJRI.addDays(iDay, iMonth, iYear, iNoOfDays);
                    date = HIJRI.dateToArray(date);
                    iFocusDate = FDate.convertToInt(date[0], date[1], date[2]);
                    break;
                case eCalendarType.Shamsi:
                    date = PERSIAN.addDays(iDay, iMonth, iYear, iNoOfDays);
                    date = PERSIAN.dateToArray(date);
                    iFocusDate = FDate.convertToInt(date[0], date[1], date[2]);
                    break;
                default:
                    date = GREGOREAN.addDays(iDay, iMonth, iYear, iNoOfDays);
                    date = GREGOREAN.dateToArray(date);
                    iFocusDate = FDate.convertToInt(date[0], date[1], date[2]);
                    break;
            }
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.addDays} " + err.message, "red");
        }

        return (iFocusDate);
    },

    subtractDays: function (iFocusDate, iNoOfDays, iCalendarType) {
        let date = null;
        let iDay = 0;
        let iMonth = 0;
        let iYear = 0;

        try {
            iDay = FDate.getDayFromInt(iFocusDate);
            iMonth = FDate.getMonthFromInt(iFocusDate);
            iYear = FDate.getYearFromInt(iFocusDate);

            switch (iCalendarType) {
                case eCalendarType.Hijri:
                    date = HIJRI.subtractDays(iDay, iMonth, iYear, iNoOfDays);
                    date = HIJRI.dateToArray(date);
                    iFocusDate = FDate.convertToInt(date[0], date[1], date[2]);
                    break;
                case eCalendarType.Shamsi:
                    date = PERSIAN.subtractDays(iDay, iMonth, iYear, iNoOfDays);
                    date = PERSIAN.dateToArray(date);
                    iFocusDate = FDate.convertToInt(date[0], date[1], date[2]);
                    break;
                default:
                    date = GREGOREAN.subtractDays(iDay, iMonth, iYear, iNoOfDays);
                    date = GREGOREAN.dateToArray(date);
                    iFocusDate = FDate.convertToInt(date[0], date[1], date[2]);
                    break;
            }
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.subtractDays} " + err.message, "red");
        }

        return (iFocusDate);
    },

    addMonths: function (iFocusDate, iNoOfMonths, iCalendarType) {
        let iDay = 0;
        let iMonth = 0;
        let iYear = 0;
        let date = null;

        try {
            iDay = FDate.getDayFromInt(iFocusDate);
            iMonth = FDate.getMonthFromInt(iFocusDate);
            iYear = FDate.getYearFromInt(iFocusDate);

            switch (iCalendarType) {
                case eCalendarType.Hijri:
                    break;
                case eCalendarType.Shamsi:
                    date = PERSIAN.addMonths(iDay, iMonth, iYear, iNoOfMonths);
                    date = PERSIAN.dateToArray(date);
                    iFocusDate = FDate.convertToInt(date[0], date[1], date[2]);
                    break;
                default:
                    date = GREGOREAN.addMonths(iDay, iMonth, iYear, iNoOfMonths);
                    date = GREGOREAN.dateToArray(date);
                    iFocusDate = FDate.convertToInt(date[0], date[1], date[2]);
                    break;
            }
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.addMonths} " + err.message, "red");
        }

        return (iFocusDate);
    },

    getDateDiffInDays: function (iFocusDate1, iFocusDate2) {
        let _MS_PER_DAY = 1000 * 60 * 60 * 24;
        let utc1 = null;
        let utc2 = null;

        try {
            // Discard the time and time-zone information.
            utc1 = Date.UTC(FDate.getYearFromInt(iFocusDate1),
                            FDate.getMonthFromInt(iFocusDate1) - 1,
                            FDate.getDayFromInt(iFocusDate1));

            utc2 = Date.UTC(FDate.getYearFromInt(iFocusDate2),
                            FDate.getMonthFromInt(iFocusDate2) - 1,
                            FDate.getDayFromInt(iFocusDate2));

            return Math.floor((utc2 - utc1) / _MS_PER_DAY);
        }
        catch (err) {
            WriteConsoleLog("Exception: {FDate.getDateDiffInDays} " + err.message, "red");
            err.message = "Exception: {getDateDiffInDays} " + err.message;
            throw err;
        }
    },

    convertDateIntoString: function (iDate, sFormat, iCalendarType, iLanguageId) {
        let sResult = "";
        let iDay = 0;
        let iMonth = 0;
        let iYear = 0;
        let datePart = null;

        iDate = FConvert.toInt(iDate);
        if (iDate <= 0) {
            return ("");
        }

        iDay = FDate.getDayFromInt(iDate);
        iMonth = FDate.getMonthFromInt(iDate);
        iYear = FDate.getYearFromInt(iDate);

        iCalendarType = FConvert.toInt(iCalendarType);
        iLanguageId = FConvert.toInt(iLanguageId);


        datePart = FDate.dateToArray(iCalendarType, iYear, iMonth, iDay);
        if (FCommon.UI.isValidObject(datePart) == false || datePart.length == 0) {
            return ("");
        }

        sResult = FDate.formatDate(datePart, sFormat, iCalendarType, iLanguageId);

        return (sResult);
    },

    dateToArray: function (iCalendarType, iYear, iMonth, iDay) {
        let date = [];
        let today = null;

        try {
            switch (iCalendarType) {
                case eCalendarType.Hijri:
                    today = HIJRI.getDate(iDay, iMonth, iYear);
                    date = HIJRI.dateToArray(today);
                    break;
                case eCalendarType.Shamsi:
                    today = PERSIAN.getDate(iDay, iMonth, iYear);
                    date = PERSIAN.dateToArray(today);
                    break;
                default:
                    today = GREGOREAN.getDate(iDay, iMonth, iYear);
                    date = GREGOREAN.dateToArray(today);
                    break;
            }
        }
        catch (err) {
            WriteConsoleLog("Exception: {FDate.dateToArray} " + err.message, "red");
        }

        return (date);
    },

    formatDate: function (datePart, format, iCalendarType, iLanguageId) { // http://www.codeproject.com/Articles/11011/JavaScript-Date-Format
        let arrMonthName = null;
        let arrDayName = null;
        let value = null;
        let dt = null;

        try {
            if (FCommon.UI.isValidObject(iCalendarType) == false) {
                iCalendarType = eCalendarType.Gregorean;
            }

            arrMonthName = FDate.getMonthNames(iCalendarType, iLanguageId);
            arrDayName = FDate.getWeekDaysName(iLanguageId);

            switch(iCalendarType)
            {
                case eCalendarType.Shamsi:
                    dt = PERSIAN.getDate(datePart[0], datePart[1] - 1, datePart[2]);
                    break;
                default:
                    dt = new Date(datePart[2], datePart[1] - 1, datePart[0]);
                    break;
            }

            value = format.replace(/(yyyy|mmmm|mmm|mm|m|dddd|ddd|dd|d|hh|nn|ss|a\/p)/gi,
                function ($1) {
                    switch ($1.toLowerCase()) {
                        case 'yyyy':
                            return datePart[2];
                        case 'mmmm':
                            return arrMonthName[datePart[1] - 1];
                        case 'mmm':
                            return arrMonthName[datePart[1] - 1].substr(0, 3);
                        case 'mm':
                            return COMMON.prototype.zeroPadLeft(datePart[1], 2);
                        case 'm':
                            return datePart[1];
                        case 'dddd':
                            return arrDayName[d.getDay()];
                        case 'ddd':
                            return arrDayName[d.getDay()].substr(0, 3);
                        case 'dd':
                            return COMMON.prototype.zeroPadLeft(datePart[0], 2);
                        case 'd':
                            return datePart[0];
                            //case 'hh': return ((h = d.getHours() % 12) ? h : 12).zf(2);
                            //case 'nn': return d.getMinutes().zf(2);
                            //case 'ss': return d.getSeconds().zf(2);
                            //case 'a/p': return d.getHours() < 12 ? 'a' : 'p';
                    }
                }
            );
        }
        catch (err) {
            WriteConsoleLog("Exception: {FDate.formatDate} " + err.message, "red");
        }

        return (value);
    },

    isValidDatePart: function (iCalendarType, iDay, iMonth, iYear) {
        let iMinYear = 0;
        let iMaxYear = 0;
        let bResult = false;

        try {
            if (iMonth < 1 || iMonth > 12) {
                return (false);
            }
            iMinYear = FDate.getMinYear(iCalendarType);
            iMaxYear = FDate.getMaxYear(iCalendarType);

            if (iYear < iMinYear || iYear > iMaxYear) {
                return (false);
            }

            if (iDay < 0 || iDay > FDate.maxDays(iMonth, iYear, iCalendarType)) {
                return (false);
            }

            bResult = true;
        }
        catch (err) {
            WriteConsoleLog("Exception: {FDate.isValidDatePart} " + err.message, "red");
        }

        return (bResult);
    },

    extractDayText: function(sDate, sFormat) {
        let sValue = "";
        let arrPos = null;

        try {
            arrPos = FDate.getDayIndex(sFormat);
            if (arrPos.length > 0) {
                sValue = sDate.substr(arrPos[0], arrPos[1] - arrPos[0]);
            }            
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.extractDayText} " + err.message, "red");
        }

        return(sValue);
    },

    extractMonthText: function(sDate, sFormat) {
        let sValue = "";
        let arrPos = null;

        try {
            arrPos = FDate.getMonthIndex(sFormat);
            if (arrPos.length > 0) {
                sValue = sDate.substr(arrPos[0], arrPos[1] - arrPos[0]);
            }            
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.extractMonthText} " + err.message, "red");
        }

        return(sValue);
    },

    extractYearText: function(sDate, sFormat) {
        let sValue = "";
        let arrPos = null;

        try {
            arrPos = FDate.getYearIndex(sFormat);
            if (arrPos.length > 0) {
                sValue = sDate.substr(arrPos[0], arrPos[1] - arrPos[0]);
            }            
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.extractYearText} " + err.message, "red");
        }

        return(sValue);
    },

    getDayIndex: function (sFormat) { // Returns day start and end index from format
        let result = null;
        let arrPos = [];

        try {
            result = sFormat.match(/d+/i);
            if (result == null) {
                return ([]);
            }

            arrPos.push(result.index);
            arrPos.push(result.index + result[0].length);
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.getDayIndex} " + err.message, "red");
        }

        return (arrPos);
    },

    getMonthIndex: function (sFormat) { // Returns month start and end index from format
        let result = null;
        let arrPos = [];

        try {
            result = sFormat.match(/m+/i);
            if (result == null) {
                return ([]);
            }

            arrPos.push(result.index);
            arrPos.push(result.index + result[0].length);
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.getMonthIndex} " + err.message, "red");
        }

        return (arrPos);
    },

    getYearIndex: function (sFormat) { // Returns year start and end index form format
        let result = null;
        let arrPos = [];

        try {
            result = sFormat.match(/y+/i);
            if (result == null) {
                return ([]);
            }

            arrPos.push(result.index);
            arrPos.push(result.index + result[0].length);
        }
        catch(err) {
            WriteConsoleLog("Exception: {FDate.getYearIndex} " + err.message, "red");
        }

        return (arrPos);
    },

    getTodayArray: function(iCalendarType) {
        let date = [];

        try {
            switch (iCalendarType) {
                case eCalendarType.Hijri:
                    date = HIJRI.today();
                    break;
                case eCalendarType.Shamsi:
                    date = PERSIAN.today();
                    break;
                default:
                    date = GREGOREAN.today();
                    break;
            }
        }
        catch (err) {
            WriteConsoleLog("Exception: {FDate.getTodayArray} " + err.message, "red");
        }

        return (date);
    },

    getDayOfWeek: function (iFocusDate, iCalendarType) {
        let iDay = 0;
        let iMonth = 0;
        let iYear = 0;
        let iValue = 0;

        iDay = FDate.getDayFromInt(iFocusDate);
        iMonth = FDate.getMonthFromInt(iFocusDate);
        iYear = FDate.getYearFromInt(iFocusDate);

        switch (iCalendarType) {
            case eCalendarType.Hijri:
                iValue = HIJRI.dayOfWeek(iDay, iMonth, iYear);
                break;
            case eCalendarType.Shamsi:
                iValue = PERSIAN.dayOfWeek(iDay, iMonth, iYear);
                break;
            default:
                iValue = GREGOREAN.dayOfWeek(iDay, iMonth, iYear);
                break;
        }

        return (iValue);
    },

    getMonthNames: function (iCalendarType, iLanguageId) {
        let arrData = [];

        iLanguageId = FConvert.toInt(iLanguageId);
        switch (iCalendarType) {
            case eCalendarType.Hijri:
                arrData = ["مُحَرَّم", "صَفَر", "رَبيع الأوّل", "رَبيع الثاني", "جُمادى الأولى", "جُمادى الثانية", "رَجَب", "شَعْبان", "رَمَضان", "شَوّال", "ذو القَعْدة", "ذو الحِجّة"];
                break;
            case eCalendarType.Shamsi:
                arrData = ["فروردین", "اردیبهشت", "خرداد", "تیر", "امرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند"];
                break;
            default: // Gregorean
                if(iLanguageId == eLanguage.Arabic) {
                    arrData = ["يناير", "فبراير", "مارس", "أبريل", "مايو", "يونيو", "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"];
                }
                else {
                    arrData = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                }
                break;
        }

        return (arrData);
    },

    getWeekDaysName: function (iLanguageId) {
        let arrData = [];

        switch (iLanguageId) {
            case eLanguage.Arabic:
                arrData = ["الأحد", "الاثنين", "الثلاثاء", "الأربعاء", "الخميس", "الجمعة", "السبت"];
                break;
            case eLanguage.Farsi:
                arrData = ["یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه"];
                break;
            default:
                arrData = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
                break;
        }

        return (arrData);
    }
};

var FConvert = {
    toInt: function (value) {
        value = parseInt(value);
        if (isNaN(value) == true) {
            return 0;
        }
        return value;
    }
};


const eCalendarType = new function () {
    Object.defineProperties(this, {
        "None": {
            get: function () {
                return 0;
            }
        },
        "Gregorean": {
            get: function () {
                return 1;
            }
        },
        "Hijri": {
            get: function () {
                return 2; // Islamic Calendar (Hijri Calendar)
            }
        },
        "Shamsi": {
            get: function () {
                return 3; // Iranian Calendar (Jalali Calendar)
            }
        }
    });
};

var GREGOREAN = {
    minYear: function () {
        return 1900;
    },
    maxYear: function () {
        return 2050;
    },
    maxDays: function (iMonth, iYear) {
        var iMax = 0;

        try {
            switch (iMonth) {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    iMax = 31;
                    break;
                case 4:
                case 6:
                case 9:
                case 11:
                    iMax = 30;
                    break;
                case 2:
                    if (GREGOREAN.isLeapYear(iYear)) {
                        iMax = 29;
                    } else {
                        iMax = 28;
                    }

            }

            //iMax = new persianDate([iYear, iMonth, 1]).daysInMonth();

            //if (iMonth >= 1 && iMonth <= 6) {
            //    iMax = 31;
            //}
            //else if (iMonth >= 7 && iMonth <= 11) {
            //    iMax = 30;
            //}
            //else if (iMonth == 12) {
            //    if (DATE.prototype.isLeapYear(iYear) == true) {
            //        iMax = 29;
            //    }
            //    else {
            //        iMax = 30;
            //    }
            //}
        }
        catch (err) {
            alert("Exception: {GREGOREAN.maxDays} " + err.message);
        }

        return (iMax);
    },
    isLeapYear: function (iYear) {
        try {
            if (iYear <= 0) {
                return false;
            }

            if (iYear % 400 == 0 || (iYear % 4 == 0 && iYear % 100 != 0)) {
                return true;
            }
        }
        catch (err) {
            alert("Exception: {GREGOREAN.isLeapYear} " + err.message);
        }
        return false;
    }
}
