using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolBones.Enums
{
    public enum Modules
    {
        Developer = 0,
        Admin = 1,
        Teacher = 2,
        Student = 3,
        Account = 4,
        Admission = 5,
    }
    public enum SubModule
    {
        None = 0,
        //Teacher
        FeeSubmission = 1,
        ClasswiseStudents = 2,
        Student = 3,


        AddStudent = 9,
        ActiveStudent = 10,
        InActiveStudent = 11,
        PerDeleteStudent = 12,
        AllStudent = 13,
        CreateMarksheet = 5,
        CreateDMarksheet = 6,
        AddSubjects = 4,
        //TakeAttandenceInputs = 7,
        TakeAttendanceInputs = 7,
        //LoadAttandenceInputs = 8,
        LoadAttendanceInputs = 8,

        //Admin
        AddTeacher = 14,
        AllTeachers = 15,
        ActiveTeachers = 16,
        InActiveTeachers = 17,
        TakeTeacherAttendanceInputs = 18,
        LoadTeacherAttendanceInputs = 19,
        IDCardOfTeachers = 20,
        IDCardOfStudents = 21,
        AttendanceApproval = 22,
        AddMenu = 23,
        TimeTableNew = 98,
        TimeTableLayout = 99,
        TimeTable = 100,

        //Teacher
        AllStudents = 24,
        DefinedMaximumMarks = 25,
        DisplayMarksheet = 26,
        Registration = 27,
        FeesStructure = 28,
        FeesStructureOperations = 29,
        LoadSemesterMarksheet = 30,

        //Accounts
        DefineMontlySalary = 299,
        CreateTimeTable = 300,
        LoadSalary = 301,
        LoadFeeCollection=302,

    }
    public enum CRUD
    {
        None = 0,
        Insert = 1,
        Update = 2,
        Delete = 3,
    }

    public enum PopupName
    {
        None = 0,
        EditStudent = 1,
        AddSubject = 2,
        TakeStudentAttendance = 3,
        TakeTeacherAttendance = 4,
        EditTeacherDetails = 5,

        //Admin 0-50

        //Teacher 50-100
        //Student 100-150

        //Accounts 150-100
        SaveFeeStructure = 150,
        UpdateFeeStructure = 151,
        DeleteFeeStructure = 152,

        SaveSalary = 153,
        UpdateSalary = 154,
        DeleteSalary = 155,
    }
}
