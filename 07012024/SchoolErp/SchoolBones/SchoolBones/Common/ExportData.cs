using DomLibrary;
using System;
using System.Collections.Generic;
using System.IO;

namespace SchoolBones.Common
{
    public class ExportData
    {
        #region Admin
        public void ToEXCEL(string sProductName, List<Marks> lstCurrentRate)
        {
            int iRow = 0;
            string fileName = "";
            DateTime dt;

            NPOI.SS.UserModel.IRow row = null;
            NPOI.SS.UserModel.ICell cell = null;
            NPOI.SS.UserModel.ISheet sheet = null;
            NPOI.HSSF.UserModel.HSSFWorkbook workBook = null;
            //FDateTime objDateTime = new FDateTime();

            MemoryStream ms = null;
            FileStream fs = null;
            try
            {
                //Create workbook & style
                workBook = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.SS.UserModel.IDataFormat ICustomDataFormat = workBook.CreateDataFormat();
                sheet = workBook.CreateSheet("StandardRate");

                //create first row
                row = sheet.CreateRow(iRow);
                row.CreateCell(0, NPOI.SS.UserModel.CellType.String).SetCellValue("Product");
                row.CreateCell(1, NPOI.SS.UserModel.CellType.String).SetCellValue(sProductName);
                iRow++;
                row = sheet.CreateRow(iRow);
                row.CreateCell(0, NPOI.SS.UserModel.CellType.String).SetCellValue("Effective Date");
                row.CreateCell(1, NPOI.SS.UserModel.CellType.String).SetCellValue("Rate");

                foreach (var item in lstCurrentRate)
                {
                    iRow++;
                    row = sheet.CreateRow(iRow);
                    row.CreateCell(0, NPOI.SS.UserModel.CellType.String).SetCellValue(item.StdId);
                    row.CreateCell(1, NPOI.SS.UserModel.CellType.String).SetCellValue(item.Class);
                }

                dt = DateTime.Now;
                //Write the workbook to a Memory stream
                ms = new MemoryStream();
                workBook.Write(ms);
                fileName = Path.Combine(Path.GetTempPath() + $"{"StandardRate"}{dt.Year}{dt.Month}{dt.Day}{dt.Hour}{dt.Minute}{dt.Second}{dt.Millisecond}" + ".xls");
                fs = new FileStream(fileName, FileMode.CreateNew);
                ms.WriteTo(fs);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                ms.Dispose();
                ms.Close();
                fs.Close();
            }
        }
        #endregion


        #region Admin

        #endregion

        #region Admin

        #endregion
        #region Admin

        #endregion

    }
}
