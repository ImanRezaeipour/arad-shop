using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using Aspose.Cells;


namespace Shopping.Infrastructure.Excel
{
    public class ExcelReader
    {
        public static void Read(string fileName, string sheet, out ArrayList data, out DataTable dataTable)
        {
            bool isExcel = Path.GetExtension(fileName).ToLower() == ".xls";
            data = new ArrayList();
            dataTable = new DataTable();

            OleDbConnection connection;
            if (isExcel)
                connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;IMEX=1\"");
            else
                connection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";");
            connection.Open();

            OleDbCommand command = new OleDbCommand();
            command.Connection = connection;
            try
            {
                if (isExcel)
                    command.CommandText = "select * from [" + sheet + "$]";
                else
                    command.CommandText = "select * from [" + sheet + "]";
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                adapter.Fill(dataTable);

                data = CollectData(dataTable);
            }
            finally
            {
                connection.Close();
            }
        }

        internal static ArrayList CollectData(DataTable dataTable)
        {
            ArrayList data = new ArrayList();
            foreach (DataRow row in dataTable.Rows)
            {
                object[] o = new object[dataTable.Columns.Count];
                for (int i = 0; i < dataTable.Columns.Count; i++)
                    o[i] = row[i];
                data.Add(o);
            }

            return data;
        }

        public static string ReadCell(Stream stream, string sheetName, int cellRow, int cellColumn)
        {
            Workbook workbook = new Workbook();
            workbook.Open(stream);
            Worksheet sheet = workbook.Worksheets[sheetName];
            if (sheet == null)
                throw new Exception(string.Format("شیت [{0}] در فایل اکسل وجود ندارد.", sheetName));

            return sheet.Cells[cellRow, cellColumn].Value + "";
        }
        public static string ReadCell(Stream stream, string sheetName, string cellName)
        {
            Workbook workbook = new Workbook();
            workbook.Open(stream);
            Worksheet sheet = workbook.Worksheets[sheetName];
            if (sheet == null)
                throw new Exception(string.Format("شیت [{0}] در فایل اکسل وجود ندارد.", sheetName));

            return sheet.Cells[cellName].Value + "";
        }
        public static DataTable ReadExcel(Stream stream, ref string sheetName)
        {
            DataTable table = new DataTable();
            Workbook workbook = new Workbook();
            workbook.Open(stream);
            Worksheet sheet;
            if (!string.IsNullOrEmpty(sheetName))
                sheet = workbook.Worksheets[sheetName];
            else
                sheet = workbook.Worksheets[0];

            if (sheet == null)
                throw new Exception(string.Format("شیت [{0}] در فایل اکسل وجود ندارد.", sheetName));


            if (string.IsNullOrEmpty(sheetName))
                sheetName = sheet.Name;


            for (int c = 0; c <= sheet.Cells.MaxColumn; c++)
            {
                string baseColumnName = CorrectString(sheet.Cells[0, c].Value + "");
                int index = 2;
                string columnName = baseColumnName;
                while (table.Columns.Contains(columnName))
                {
                    columnName = baseColumnName + "_" + index;
                    index++;
                }
                table.Columns.Add(columnName, typeof(string));
            }
            for (int r = 1; r <= sheet.Cells.MaxRow; r++)
            {
                object[] data = new object[sheet.Cells.MaxColumn + 1];
                for (int c = 0; c <= sheet.Cells.MaxColumn; c++)
                    data[c] = CorrectString(sheet.Cells[r, c].Value + "");
                table.Rows.Add(data);
            }
            return table;
        }
        public static DataTable ReadExcel(Stream stream)
        {
            var sheetName = "Sheet1";
            return ReadExcel(stream, ref sheetName);
            //            DataTable table = new DataTable();
            //            Workbook workbook = new Workbook();
            //            workbook.Open(stream);
            //            Worksheet sheet = workbook.Worksheets["Sheet1"];
            //            if (sheet == null)
            //                throw new Exception("Sheet1 در فایل اکسل وجود ندارد.");
            //            for (int c = 0; c <= sheet.Cells.MaxColumn; c++)
            //                table.Columns.Add(CorrectString(sheet.Cells[0, c].Value + ""), typeof(string));
            //            for (int r = 1; r <= sheet.Cells.MaxRow; r++)
            //            {
            //                object[] data = new object[sheet.Cells.MaxColumn + 1];
            //                for (int c = 0; c <= sheet.Cells.MaxColumn; c++)
            //                    data[c] = CorrectString(sheet.Cells[r, c].Value + "");
            //                table.Rows.Add(data);
            //            }
            //            return table;
        }

        public static List<string> GetSheetNames(Stream stream)
        {
            Workbook workbook = new Workbook();
            workbook.Open(stream);

            List<string> ret = new List<string>();
            foreach (Worksheet worksheet in workbook.Worksheets)
            {
                ret.Add(worksheet.Name);
            }
            return ret;
        }
        private static string CorrectString(string str)
        {
            return str.Replace("ي", "ی").Replace("ك", "ک").Trim();
        }
        public static void WriteExcel(string fileName, string sheetName, DataTable dataTable)
        {
            Workbook workbook = new Workbook();
            workbook.Open(fileName);


            int row = 0;
            int column = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                foreach (object item in dataRow.ItemArray)
                {
                    workbook.Worksheets[sheetName].Cells[row, column].Value = dataRow[column];
                    column++;
                }
                column = 0;
                row++;
            }

            workbook.Save(fileName);
        }
        public static void WriteExcel(string fileName, string sheetName, DataTable dataTable, bool createFileAndSheet, bool exportColumnName)
        {
            WriteExcel(fileName, sheetName, dataTable, createFileAndSheet, exportColumnName, FileFormatType.Excel2007Xlsx);
        }
        public static void WriteExcel(string fileName, string sheetName, DataTable dataTable,
            bool createFileAndSheet, bool exportColumnName, FileFormatType fileFormatType)
        {
            Workbook workbook = new Workbook();
            if (!File.Exists(fileName))
            {
                if (createFileAndSheet)
                {
                    if (!workbook.Worksheets.OfType<Worksheet>().Select(w => w.Name).Contains(sheetName))
                        workbook.Worksheets.Add(sheetName);

                    workbook.Save(fileName);
                }
            }

            workbook = new Workbook();
            workbook.Open(fileName);

            int column = 0;
            foreach (DataColumn dataColumn in dataTable.Columns)
                workbook.Worksheets[sheetName].Cells[0, column++].Value = dataColumn.Caption;

            Style style = workbook.CreateStyle();
            style.Number = 49; // TEXT format 

            int row = 1;
            column = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                foreach (object item in dataRow.ItemArray)
                {
                    Cell cell = workbook.Worksheets[sheetName].Cells[row, column];

                    cell.SetStyle(style);
                    cell.Value = dataRow[column] + "";
                    column++;
                }
                column = 0;
                row++;
            }

            workbook.Save(fileName, fileFormatType);
        }

        /// <summary>
        /// write only in first sheet: Sheet1
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="dataTable"></param>
        public static void WriteExcel(MemoryStream stream, DataTable dataTable)
        {
            Workbook workbook = new Workbook();
            workbook.Open(stream);


            Worksheet sheet0 = workbook.Worksheets[0];

            sheet0.DisplayRightToLeft = true;

            Style style = sheet0.Workbook.CreateStyle();

            style.Font.Name = "Tahoma";
            style.Font.Size = 12;
            style.Font.IsBold = true;
            style.HorizontalAlignment = TextAlignmentType.Center;

            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            style.HorizontalAlignment = TextAlignmentType.Center;

            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                DataColumn column = dataTable.Columns[i];
                sheet0.Cells[0, i].Value = column.Caption;
                if (column.DataType == typeof(DateTime))
                {
                    style.Custom = "mm/dd/yyyy";
                }
                else
                {
                    style.Custom = "";
                }
                sheet0.Cells[0, i].SetStyle(style);
            }

            style.Custom = "";
            style.Font.Size = 10;
            style.Font.IsBold = false;
            style.HorizontalAlignment = TextAlignmentType.Right;

            int rowIndex = 1;
            int columnIndex = 0;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                foreach (object item in dataRow.ItemArray)
                {
                    Cell cell = sheet0.Cells[rowIndex, columnIndex];
                    cell.Value = dataRow[columnIndex];
                    cell.SetStyle(style);
                    columnIndex++;
                }
                columnIndex = 0;
                rowIndex++;
            }

            workbook.Save(stream, FileFormatType.Excel2007Xlsx);
        }
    }
}