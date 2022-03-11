using System;
using OfficeOpenXml;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Data;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Drawing;
using OfficeOpenXml.Style;

namespace Claro.Helpers.Transac.Service
{

    public class ExcelHelper
    {
        public static readonly Type HeaderAttributeType = typeof(HeaderAttribute);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="lstHeader"></param>
        /// <returns></returns>
        private static bool ValidationHeader(int Order, List<int> lstHeader)
        {
            bool est = false;

            if (lstHeader != null && lstHeader.Count > 0)
            {
                foreach (var str in lstHeader)
                {
                    if (Order == str)
                    {
                        est = true;
                        break;
                    }
                }
            }

            return est;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="lstHeader"></param>
        /// <returns></returns>
        private static DataTable ToDataTable(object data, List<int> lstHeader, ExcelNamedRange ExcelValue, ExcelWorksheet sheet)
        {
            PropertyInfo[] properties = data.GetType().GenericTypeArguments[0].GetProperties();
            DataTable table = new DataTable();
            HeaderAttribute headerAttribute;
            DataTable Data = new DataTable();
            Data.Columns.Add("item", typeof(int));
            Data.Columns.Add("Name", typeof(string));
            Data.Columns.Add("Group", typeof(string));
            DataRow rowdata;
            int groupCount = 0;

            foreach (PropertyInfo prop in properties)
            {
                foreach (object attribute in prop.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == HeaderAttributeType)
                    {

                        headerAttribute = attribute as HeaderAttribute;

                        rowdata = Data.NewRow();
                        rowdata["item"] = headerAttribute.Order;
                        rowdata["Name"] = headerAttribute.Title;
                        rowdata["Group"] = headerAttribute.Group;

                        if (headerAttribute.Group != null)
                        {
                            groupCount++;
                        }

                        Data.Rows.Add(rowdata);
                        table.Columns.Add(headerAttribute.Title, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

                    }
                }
            }
            if (lstHeader != null && lstHeader.Count > 0)
            {

                DataView dv = Data.DefaultView;
                dv.Sort = "item asc";
                Data = dv.ToTable();

                for (int r = 0; r <= Data.Rows.Count - 1; r++)
                {
                    Data.Rows[r]["item"] = r;
                }
            }

            string name;
            int num = 1;
            int fila;
            int filaOld = -num;
            for (int i = 0; i <= Data.Rows.Count - 1; i++)
            {
                name = Data.Rows[i][1].ToString();
                fila = Convert.ToInt32(Data.Rows[i][0].ToString());

                if (filaOld == (fila - 1))
                {
                    table.Columns[name].SetOrdinal(fila);
                    filaOld = fila;
                }
                else
                {
                    table.Columns[name].SetOrdinal((filaOld + 1));
                    filaOld = filaOld + 1;
                }

            }


            if (groupCount > 0)
            {
                int x = ExcelValue.Start.Row - 1;
                int y = ExcelValue.Start.Column;
                string nameGroup = "x";
                int startGroup = 0;
                int start = 0;
                int end = 0;
                int yv;
                sheet.Cells[1, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                sheet.Cells[1, 1].Style.Fill.BackgroundColor.SetColor(Color.MidnightBlue);
                sheet.Cells[1, 1].Style.Font.Color.SetColor(Color.White);
                sheet.Cells[1, 1].Style.Font.Bold = true;
                for (int i = 0; i <= Data.Rows.Count - 1; i++)
                {
                    yv = y++;
                    sheet.Cells[x, yv].Value = Data.Rows[i][2];
                    sheet.Cells[x, yv].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    sheet.Cells[x, yv].Style.Fill.BackgroundColor.SetColor(Color.MidnightBlue);
                    sheet.Cells[x, yv].Style.Font.Color.SetColor(Color.White);
                    sheet.Cells[x, yv].Style.Font.Bold = true;

                    if (!string.IsNullOrEmpty(Data.Rows[i][2].ToString()))
                    {
                        startGroup++;

                        if (startGroup == 1)
                        {
                            start = yv;
                        }
                        if (nameGroup.Equals(Data.Rows[i][2].ToString()))
                        {
                            end = yv;
                            sheet.Cells[x, start, x, end].Merge = true;
                            sheet.Cells[x, start, x, end].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                            sheet.Cells[x, start, x, end].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                            sheet.Cells[x, start, x, end].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                            sheet.Cells[x, start, x, end].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                            sheet.Cells[x, start, x, end].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        }
                    }
                    else
                    {
                        start = 0;
                        end = 0;
                        startGroup = 0;
                    }


                    nameGroup = Data.Rows[i][2].ToString();
                }

            }


            foreach (var item in (IEnumerable)data)
            {
                DataRow row = table.NewRow();

                foreach (PropertyInfo prop in properties)
                {
                    foreach (object attribute in prop.GetCustomAttributes(false))
                    {
                        if (attribute.GetType() == HeaderAttributeType)
                        {
                            headerAttribute = attribute as HeaderAttribute;


                            row[headerAttribute.Title] = prop.GetValue(item) ?? DBNull.Value;
                        }
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="book"></param>
        /// <param name="NameKey"></param>
        /// <param name="NameValue"></param>
        /// <returns></returns>
        private ExcelNamedRange KeyCell(ExcelWorkbook book, string NameKey, string NameValue)
        {
            ExcelNamedRange namedRange = null;

            foreach (ExcelNamedRange named in book.Names)
            {
                if (named.Name == NameKey)
                {
                    namedRange = named;
                    namedRange.Value = NameValue;
                    break;
                }
            }

            return namedRange;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTGeneric"></param>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private ExcelWorksheet CreateObj(object objTGeneric, ExcelWorksheet sheet)
        {
            return CreateObj(objTGeneric, sheet);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objTGeneric"></param>
        /// <param name="sheet"></param>
        /// <param name="book"></param>
        /// <param name="lstHeaders"></param>
        /// <returns></returns>
        private ExcelWorksheet CreateObj(object objTGeneric, ExcelWorksheet sheet, ExcelWorkbook book, List<int> lstHeaders)
        {
            PropertyInfo[] properties = objTGeneric.GetType().GetProperties();
            HeaderAttribute headerAttribute;

            foreach (PropertyInfo prop in properties)
            {
                foreach (object attribute in prop.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == HeaderAttributeType)
                    {
                        headerAttribute = attribute as HeaderAttribute;

                        if (lstHeaders != null && ValidationHeader(headerAttribute.Order, lstHeaders))
                        {
                            continue;
                        }

                        object o = prop.GetValue(objTGeneric, null);

                        if (!object.Equals(o, null))
                        {
                            if (prop.PropertyType.GenericTypeArguments != null && prop.PropertyType.GenericTypeArguments.Length > 0
                                && !(prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            {
                                string name = prop.Name;
                                ExcelNamedRange ExcelValue = KeyCell(book, name, "");
                                DataTable dt = ToDataTable(o, lstHeaders, ExcelValue, sheet);


                                if (ExcelValue != null)
                                {

                                    bool hasRows = (dt.Rows.Count > 0);

                                    if (!hasRows)
                                    {
                                        DataRow dr = dt.NewRow();

                                        dt.Rows.Add(dr);
                                    }
                                    sheet.InsertRow(ExcelValue.Start.Row + 1, dt.Rows.Count + 1);
                                    ExcelValue.LoadFromDataTable(dt, true, TableStyles.Custom);
                                }

                            }
                            else if (o is IExcel)
                            {
                                CreateObj(o, sheet, book, lstHeaders);
                            }
                            else
                            {
                                KeyCell(book, headerAttribute.Title, o.ToString());
                            }
                        }

                    }
                }
            }

            return sheet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objTGeneric"></param>
        /// <param name="fileReportName"></param>
        /// <returns></returns>
        public string ExportExcel<T>(T objTGeneric, string fileReportName)
        {
            return ExportExcel(objTGeneric, fileReportName, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objTGeneric"></param>
        /// <param name="fileReportName"></param>
        /// <param name="lstHeaders"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public string ExportExcel<T>(T objTGeneric, string fileReportName, List<int> lstHeaders, int? size = 0)
        {
            string path = System.IO.Path.GetTempFileName();
            string FileReport = (System.Web.HttpContext.Current.Server.MapPath(fileReportName));
            var FileTemplate = new FileInfo(FileReport);

            FileInfo FileNew = new FileInfo(path);
            if (FileNew.Exists)
                FileNew.Delete();

            using (ExcelPackage package = new ExcelPackage(FileNew, FileTemplate))
            {
                ExcelWorkbook book = package.Workbook;
                ExcelWorksheet worksheet = book.Worksheets[1];

                worksheet = CreateObj(objTGeneric, worksheet, book, lstHeaders);

                if (worksheet.Tables.Count > 0)
                {
                    for (int i = 0; i < worksheet.Tables.Count; i++)
                    {
                        var tbl = worksheet.Tables[i];
                        tbl.StyleName = "ClaroTable";
                        tbl.ShowFilter = false;
                    }
                }

                if (size == 15)
                {
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(2).Width = 5;
                    worksheet.Column(3).Width = 11;
                    worksheet.Column(4).Width = 12;
                    worksheet.Column(5).Width = 17;
                    worksheet.Column(6).Width = 12;
                    worksheet.Column(9).Width = 18;
                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                }
                else if (size == 20)
                {
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(2).Width = 5;//NRO
                    worksheet.Column(3).Width = 10;//FECHA
                    worksheet.Column(4).Width = 10;//HORA
                    worksheet.Column(5).Width = 10;//TEF ORIGEN
                    worksheet.Column(6).Width = 10;//TEF DESTINO
                    worksheet.Column(7).Width = 5;//CANTIDAD
                    worksheet.Column(8).Width = 5;//COSTO
                    worksheet.Column(9).Width = 15;//PLAN
                    worksheet.Column(10).Width = 8;//TARIFA
                    worksheet.Column(11).Width = 5;//TIPO
                    worksheet.Column(12).Width = 5;//ZONA HORARIA
                    worksheet.Column(13).Width = 7;//OPERADOR
                    worksheet.Column(14).Width = 10;//HORARIO
                    worksheet.Column(15).Width = 5;//TIPO LLAMADA
                    worksheet.Column(16).Width = 5;//CONSUMO
                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                }
                else
                {                    
                    worksheet.Cells.AutoFitColumns();
                    worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                }
                                             

                
                package.Save();
            }

            return path;
        }

        public string ExportExcelVisib<T>(T objTGeneric, string fileReportName, List<int> lstHeaders, List<string> lstRemove, List<string> lstParam)
        {
            string path = System.IO.Path.GetTempFileName();
            string FileReport = (System.Web.HttpContext.Current.Server.MapPath(fileReportName));
            var FileTemplate = new FileInfo(FileReport);

            FileInfo FileNew = new FileInfo(path);
            if (FileNew.Exists)
                FileNew.Delete();

            using (ExcelPackage package = new ExcelPackage(FileNew, FileTemplate))
            {
                ExcelWorkbook book = package.Workbook;
                ExcelWorksheet worksheet = book.Worksheets[1];


                worksheet = CreateObjVisib(objTGeneric, worksheet, book, lstHeaders, lstRemove);

                worksheet.Select("B11");
                if (worksheet.Tables.Count > 0)
                {
                    for (int i = 0; i < worksheet.Tables.Count; i++)
                    {
                        var tbl = worksheet.Tables[i];
                        tbl.StyleName = "ClaroTable";
                        tbl.ShowFilter = false;
                    }
                }

                worksheet.Cells.AutoFitColumns();
                package.Save();
            }

            return path;
        }

        private ExcelWorksheet CreateObjVisib(object objTGeneric, ExcelWorksheet sheet, ExcelWorkbook book, List<int> lstHeaders, List<string> lstRemove)
        {
            PropertyInfo[] properties = objTGeneric.GetType().GetProperties();
            HeaderAttribute headerAttribute;

            foreach (PropertyInfo prop in properties)
            {
                foreach (object attribute in prop.GetCustomAttributes(false))
                {
                    if (attribute.GetType() == HeaderAttributeType)
                    {
                        headerAttribute = attribute as HeaderAttribute;


                        object o = prop.GetValue(objTGeneric, null);

                        if (!object.Equals(o, null))
                        {
                            if (prop.PropertyType.GenericTypeArguments != null && prop.PropertyType.GenericTypeArguments.Length > 0
                                && !(prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                            {
                                string name = prop.Name;
                                ExcelNamedRange ExcelValue = KeyCell(book, name, "");
                                DataTable dt = ToDataTable(o, lstHeaders, ExcelValue, sheet);

                                foreach (string item in lstRemove)
                                {
                                    dt.Columns.Remove(item);
                                }

                                if (ExcelValue != null)
                                {

                                    bool hasRows = (dt.Rows.Count > 0);

                                    if (!hasRows)
                                    {
                                        DataRow dr = dt.NewRow();

                                        dt.Rows.Add(dr);
                                    }
                                    sheet.InsertRow(ExcelValue.Start.Row + 1, dt.Rows.Count);
                                    ExcelValue.LoadFromDataTable(dt, true, TableStyles.Custom);

                                }

                            }
                            else if (o is IExcel)
                            {
                                CreateObjVisib(o, sheet, book, lstHeaders, lstRemove);
                            }
                            else
                            {
                                KeyCell(book, headerAttribute.Title, o.ToString());
                            }
                        }

                    }
                }
            }

            return sheet;
        }


    }
}
