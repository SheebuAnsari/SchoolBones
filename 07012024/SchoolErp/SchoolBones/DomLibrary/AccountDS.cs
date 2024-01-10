using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomLibrary
{
    public class FeeDetails
    {
        public int Wallet { get; set; }
        public int StudentFeesId { get; set; }
        public StudentDetails StdInfo { get; set; }
        public IdValuePair[] MonthsAndFee { get; set; }
        public FeesInfo[] ArrFeesInfo { get; set; }
        public int Settlement { get; set; }
        public int MaxFee { get; set; }
        public string Status { get; set; }

    }
    public class FeeInfo
    {

        public int RegistrationId { get; set; }
        public int StdId { get; set; }
        public string StdName { get; set; }
        public int Class { get; set; }
        public int ForMonth { get; set; }
        public int Year { get; set; }
        public int Date { get; set; }
        public int MonthlyFee { get; set; }
        public int Pending { get; set; }
        public int Settlement { get; set; }
        public int YearlyFee { get; set; }
        public int Concession { get; set; }
    }
    public class FeesInfo
    {
        public int MonthlyFeeId { get; set; }
        public string Status { get; set; }
        public int Class { get; set; }
        public int ForMonth { get; set; }
        public int Year { get; set; }
        public int Date { get; set; }
        public int MonthlyFee { get; set; }
        public int YearlyFee { get; set; }
        public int Concession { get; set; }
        public int Settlement { get; set; }
        
    }

    public class RowData
    {
        public CellData[] CellData { get; set; }

    }
    public class CellData
    {
        public object[] Cell { get; set; }
    }

    public class FeeCollection
    {
        public string Status { get; set; }
        public int Class { get; set; }
        public int ForMonth { get; set; }
        public int Year { get; set; }
        public int Date { get; set; }
        public int MonthlyFee { get; set; }
        public int YearlyFee { get; set; }
        public int Concession { get; set; }
        public int Settlement { get; set; }

    }
}
