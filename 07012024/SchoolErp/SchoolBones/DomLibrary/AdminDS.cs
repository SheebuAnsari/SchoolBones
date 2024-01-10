using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomLibrary
{
    //internal class AdminDS
    //{
    //}
    public class FeesStructure
    {
        public Inputs Inputs { get; set; }
        public int FeeStructureId { get; set; }
        public int Class { get; set; }
        public int Year { get; set; }
        public int Date { get; set; }
        public int MonthlyFee { get; set; }
        public int YearlyFee { get; set; }
    }
    public class TimeTableLayout
    {
        public int iLayoutId { get; set; }
        public string LayoutName { get; set; }
        public int NoOfPeriod { get; set; }
        public int FirstPeriod { get; set; }
        public int Duration { get; set; }
        public int Class { get; set; }
        public int LunchBreak { get; set; }
        public int CreatedDate { get; set; }
    }
    public class TimeTable
    {
        public Registration[] Teacher { get; set; }
        public TeacherDetails[] Teachers { get; set; }
        public Subject[] Subjects { get; set; }
        public RowWiseData[] RowData { get; set; }
        public int Class { get; set; }
        public int CreatedDate { get; set; }
        public Cell[] CellData { get; set; }
    }
    public class RowWiseData
    {
        public Cell[] CellData { get; set; }
        public int Day { get; set; }
    }
    public class Cell
    {
        public int RowId { get; set; }
        public int ColId { get; set; }
        public int RegId { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }

    }

    public class ReportData
    {
        public LineData[] LineData { get; set; }
        public ColumnInfo[] Columns { get; set; }
    }
    public class ColumnInfo
    {
        public ColumnInfo(int CollId, string CollName)
        {
            this.CollId = CollId;
            this.ColumnName = CollName;
        }
        public int CollId { get; set; }
        public string ColumnName { get; set; }
    }
    public class LineData
    {
        public int RowNo { get; set; }
        public object[] CellData { get; set; }
    }
}
