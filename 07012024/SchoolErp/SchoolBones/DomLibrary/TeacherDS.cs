namespace DomLibrary
{
    public class TeacherSalary
    {
        public Inputs Inputs { get; set; }
        public int SalaryId { get; set; }
        public int RegId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public int Date { get; set; }
        public int MonthlySalary { get; set; }
        public int YearlySalary { get; set; }


        public int DayOfMonths { get; set; }
        public int NoOfWorkingDays { get; set; }
        public int DeductionDays { get; set; }
        public int PerDaySalary { get; set; }
        public int NetMonthlySalary { get; set; }
        public int MaxMonthlySalary { get; set; }

    }

    public class TeacherAttendance
    {
        public int RegistrationId { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public int Date { get; set; }
        public int Status { get; set; }
    }
    public class TeachersAttendance
    {
        public Inputs Inputs { get; set; }
        public IdNamePair[] Teachers { get; set; }
        public Attendance[] Attendance { get; set; }
    }

    public class Attendance
    {
        public int TeacherId { get; set; }
        public int Date { get; set; }
        public int Status { get; set; }
    }
    //public class Subject
    //{
    //    public int SubjectId { get; set; }
    //    public int Class { get; set; }
    //    public string SubjectName { get; set; }
    //    public int SubjectMark { get; set; }
    //    public bool IsDefaultSubject { get; set; }
    //}

    //public class AdmissionDetails
    //{
    //    public int iEnrollmentNo { get; set; }
    //    public string StdName { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Address { get; set; }
    //    public string City { get; set; }
    //    public long Contact { get; set; }
    //    public int AdmissionInClass { get; set; }
    //    public int AdmissionDate { get; set; }
    //    public int DOB { get; set; }
    //    public bool IsActive { get; set; }
    //}
    //public class StudentAttandence
    //{
    //    public int StdId { get; set; }
    //    public string StdName { get; set; }
    //    public int Class { get; set; }
    //    public int Year { get; set; }
    //    public int Date { get; set; }
    //    public int Status { get; set; }
    //}
    //public class Marks
    //{
    //    public int StdId { get; set; }
    //    public string StdName { get; set; }
    //    public int Class { get; set; }
    //    public int Year { get; set; }
    //    public int Physics { get; set; }
    //    public int Chemistry { get; set; }
    //    public int Mathmatics { get; set; }
    //    public bool ResultPrepared { get; set; }
    //}
    //public class PassFailStatus
    //{
    //    public int StdId { get; set; }
    //    public int ObtainedMarks { get; set; }
    //    public string PassingStatus { get; set; }

    //}

    //    //public class TeacherDS
    //    //{
    //    //}
    //    //namespace EmployeeDL
    //    //{

    //    //}

    public class TeacherDetails
    {
        public int RegistrationId { get; set; }
        public int TeacherId { get; set; }
        public long Mobile { get; set; }
        //public string Email { get; set; }
        //public string Password { get; set; }
        public string Name { get; set; }
        public string Qualification { get; set; }
        public int JoiningDate { get; set; }
        //public int Package { get; set; }
        //public int MonthlySalary { get; set; }
        public int Experiance { get; set; }
        public bool Active { get; set; }

    }
    //    public class AttendanceRegister
    //    {
    //        public int Id { get; set; }
    //        public int TeacherId { get; set; }
    //        public string Date { get; set; }
    //        public bool Status { get; set; }
    //        public bool AdminApproval { get; set; }
    //    }
    //    public class SaveStatus
    //    {
    //        public int iStatus { get; set; }
    //        public object Tag { get; set; }
    //    }

    //    public class StudentAddmission
    //    {
    //        public int StdId { get; set; }
    //        public string StdName { get; set; }
    //        public int AdmissionInClass { get; set; }
    //        public int Year { get; set; }
    //    }
    

    //    public class Marks
    //    {
    //        public int StdId { get; set; }
    //        public int Class { get; set; }
    //        public int Year { get; set; }
    //        public int Physics { get; set; }
    //        public int Chemistry { get; set; }
    //        public int Mathmatics { get; set; }
    //        public bool ResultPrepared { get; set; }
    //    }


    //    //public class IdNamePair
    //    //{
    //    //    public IdNamePair() { }
    //    //    public IdNamePair(int ID, string Name)
    //    //    {
    //    //        this.ID = ID;
    //    //        this.Name = Name;
    //    //    }
    //    //    public int ID { get; set; }
    //    //    public string Name { get; set; }
    //    //}

    //    //public class IdStatusPair
    //    //{
    //    //    public IdStatusPair() { }
    //    //    public IdStatusPair(int ID, bool Status)
    //    //    {
    //    //        this.ID = ID;
    //    //        this.Status = Status;
    //    //    }
    //    //    public int ID { get; set; }
    //    //    public bool Status { get; set; }
    //    //}

}
