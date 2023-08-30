using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using ExcelDataReader;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Adv.Tools.UI.Common
{

    public class ExcelFilesHelper
    {

        //File Path
        public string GetExcelFilePath()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "xlsx files (*.xlsx)|*.xlsx";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
                return string.Empty;
            }
        }
        public string GetSaveFolderPath()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    return dialog.SelectedPath;
                }

                return string.Empty;
            }

        }

        public FileStream GetExcelFileAsStream(string filePath)
        {
            if (File.Exists(filePath))
            {
                return File.Open(filePath, FileMode.Open, FileAccess.Read);
            }

            return null;
        }
        public List<T> GetExcelTableAsList<T>(DataSet ds, string name) where T : new()
        {
            var dt = ds?.Tables[name];
            var result = new List<T>();

            if (dt is null)
            {
                return result;
            }

            foreach (DataRow row in dt.Rows)
            {
                var expected = new T();
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (row.Table.Columns.Contains(prop.Name))
                    {
                        var value = row[prop.Name]?.ToString() ?? null;
                        if (value != null)
                        {
                            prop.SetValue(expected, Convert.ChangeType(value, prop.PropertyType));
                        }
                    }
                }
                result.Add(expected);
            }
            return result;
        }
        public DataSet GetExcelFileAsDataSet(Stream stream)
        {
            var ds = new DataSet();

            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                ds = reader.AsDataSet(new ExcelDataSetConfiguration()
                {
                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                    {
                        UseHeaderRow = true
                    }
                });
            }

            return ds;
        }
        public DataTable ConvertListToDataTable<T>(List<T> list)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            // Get the properties of the type T
            PropertyInfo[] properties = typeof(T).GetProperties();

            // Create columns in the DataTable based on the property names
            foreach (PropertyInfo property in properties)
            {
                dataTable.Columns.Add(property.Name, typeof(string));
            }

            // Add rows to the DataTable with the string values of the properties
            foreach (T item in list)
            {
                DataRow row = dataTable.NewRow();

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(item);
                    string stringValue = value?.ToString() ?? string.Empty;
                    row[property.Name] = stringValue;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
        public void ExportDataTableAsExcelFile(DataTable dt, string folderPath)
        {
            XLWorkbook workBook = new XLWorkbook();
            workBook.AddWorksheet(dt);
            workBook.SaveAs($@"{folderPath}\{dt.TableName}.xlsx");
        }
    }
}
