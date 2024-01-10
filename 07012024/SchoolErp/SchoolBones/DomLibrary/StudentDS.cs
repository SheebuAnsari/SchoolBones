namespace DomLibrary
{
    public class StudentDetails
    {
        public int EnrollmentNo { get; set; }
        public int StdId { get; set; }
        public string StdName { get; set; }
        public int Class { get; set; }
        public int Year { get; set; }
        //public long Mobile { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        //public string AdmissionDate { get; set; }
        //public string Address { get; set; }
        //public int AdmissionInClass { get; set; }
        //public string DOB { get; set; }

    }
    public class AdmissionDetails
    {
        public int RegistrationId { get; set; }
        //public int iEnrollmentNo { get; set; }
        public string StdName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public long Contact { get; set; }
        public int AdmissionInClass { get; set; }
        public int AdmissionDate { get; set; }
        public int DOB { get; set; }
        public bool IsActive { get; set; }
        public byte[] StudentImage { get; set; }
    }
    public class StudentCurrentInfo
    {
        public int StdId { get; set; }
        public string StdName { get; set; }
        public int Class { get; set; }
        public int Year { get; set; }
        public int MarkSheetId { get; set; }
        public int ResultCreated { get; set; }
    }
    public class StudentAttendance
    {
        public int StdId { get; set; }
        public string StdName { get; set; }
        public int Class { get; set; }
        public int Year { get; set; }
        public int Date { get; set; }
        public int Status { get; set; }
        public Subject[] Subjects { get; set; }
    }
    public class StudentMarksheet
    {
        public Inputs Inputs { get; set; }
        public SemesterData[] ExamTypeSubjects { get; set; }
        public Marks[] StudentsMarks { get; set; }
    }
    public class Marks
    {
        public int MarkSheetId { get; set; }
        public StudentCurrentInfo StdCurrentInfo { get; set; }
        public int StdId { get; set; }
        public string StdName { get; set; }
        public int Class { get; set; }
        public int Year { get; set; }
        public Subject[] Subjects { get; set; }
        public string[] SubjectColumns { get; set; }
        public SemesterData[] SemesterData { get; set; }
        public bool ResultPrepared { get; set; }
        public int ExamType { get; set; }
    }
    public class Subject
    {
        public int SubjectId { get; set; }
        public int Class { get; set; }
        public string SubjectName { get; set; }
        public int SubjectMark { get; set; }
        public int MaximumMarks { get; set; }
        public int ExamType { get; set; }
        public bool IsDefaultSubject { get; set; }
    }
    public class SemesterData //Not in use but for future
    {
        //public int MarkSheetId { get; set; } //No need of this
        public int ExamType { get; set; }
        public IdValuePair[] arrSubjectMarks { get; set; }
    }
    public class MarkSheet
    {
        public StudentAttendance[] StudentAttendance { get; set; }
        public Subject[] Subjects { get; set; }
        public Inputs Inputs { get; set; }
    }
    public class SubjectOLD
    {
        public int SubjectId { get; set; }
        public int Class { get; set; }
        public string SubjectName { get; set; }
        public int SubjectMark { get; set; }
        public bool IsDefaultSubject { get; set; }
    }
    public class PassFailStatus
    {
        public int StdId { get; set; }
        public int ObtainedMarks { get; set; }
        public string PassingStatus { get; set; }

    }

    public class SubjectMaxMarks
    {
        public int SubjectId { get; set; }
        public int Class { get; set; }
        public int MaxMark { get; set; }
    }
    public class SubjectDetails
    {
        public Inputs Inputs { get; set; }
        public Subject[] arrSub { get; set; }
    }
}
