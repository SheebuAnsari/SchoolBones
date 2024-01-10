const ePopupName = '';
const eModule = '';


var COMMONENUM = {
    eModule: new function () {
        Object.defineProperties(this, {
            "None": {
                get: function () {
                    return 0;
                }
            },
            "Admin": {
                get: function () {
                    return 1;
                }
            },
            "Teacher": {
                get: function () {
                    return 2;
                }
            },
            "Student": {
                get: function () {
                    return 3;
                }
            },
            "Accounts": {
                get: function () {
                    return 4;
                }
            },
           
        });
    },
    ePopupName: new function () {
        Object.defineProperties(this, {
            "None": {
                get: function () {
                    return 0;
                }
            },
            "EditStudent": {
                get: function () {
                    return 1;
                }
            },
            "AddSubject": {
                get: function () {
                    return 2;
                }
            },
            "TakeStudentAttendance": {
                get: function () {
                    return 3;
                }
            },
            "TakeTeacherAttendance": {
                get: function () {
                    return 4;
                }
            },
            "EditTeacherDetails": {
                get: function () {
                    return 5;
                }
            },

            //Accounts
            "SaveFeeStructure": {
                get: function () {
                    return 150;
                }
            },
            "UpdateFeeStructure": {
                get: function () {
                    return 151;
                }
            },
            "DeleteFeeStructure": {
                get: function () {
                    return 152;
                }
            },
            "SaveSalary": {
                get: function () {
                    return 153;
                }
            },
            "UpdateSalary": {
                get: function () {
                    return 154;
                }
            },
            "DeleteSalary": {
                get: function () {
                    return 155;
                }
            },
        });
    },
}
