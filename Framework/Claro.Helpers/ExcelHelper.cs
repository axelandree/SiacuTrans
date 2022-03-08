using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Claro.Helpers
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
        private static DataTable ToDataTable(object data, List<int> lstHeader, ExcelNamedRange ExcelValue, ExcelWorksheet sheet, string keys = null)
        {
            PropertyInfo[] properties = data.GetType().GenericTypeArguments[0].GetProperties();

            if (keys != null)
            {
                string strkeys = keys;
                string[] arrkeys = strkeys.Split(',');
                List<PropertyInfo> list = new List<PropertyInfo>();

                for (int i = 0; i < properties.Length; i++)
                {
                    if (i < arrkeys.Length)
                    {
                    if (arrkeys[i].ToLower(CultureInfo.InvariantCulture) == "true")
                        list.Add(properties[i]);
                }

                }

                properties = list.ToArray();
            }

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

                        if (lstHeader != null && ValidationHeader(headerAttribute.Order, lstHeader))
                        {
                            continue;
                        }

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

                            if (lstHeader != null && lstHeader.Count > 0 && ValidationHeader(headerAttribute.Order, lstHeader))
                            {
                                continue;
                            }

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
        /// <param name="book"></param>
        /// <param name="lstHeaders"></param>
        /// <returns></returns>
        private ExcelWorksheet CreateObj(object objTGeneric, ExcelWorksheet sheet, ExcelWorkbook book, List<int> lstHeaders, string keys = null)
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
                                DataTable dt = ToDataTable(o, lstHeaders, ExcelValue, sheet, keys);


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
                                    ExcelValue.AutoFitColumns();
                                }

                            }
                            else if (o is IExcel)
                            {
                                CreateObj(o, sheet, book, lstHeaders, keys);
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
        public string ExportExcel<T>(T objTGeneric, string fileReportName, string keys = null)
        {
            return ExportExcel(objTGeneric, fileReportName, null, keys);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objTGeneric"></param>
        /// <param name="fileReportName"></param>
        /// <param name="lstHeaders"></param>
        /// <returns></returns>
        public string ExportExcel<T>(T objTGeneric, string fileReportName, List<int> lstHeaders, string keys = null)
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
                worksheet = CreateObj(objTGeneric, worksheet, book, lstHeaders, keys);
                ExcelTable tbl;
                string AddressPrintArea = "";
                if (worksheet.Tables.Count > 0)
                {
                    for (int i = 0; i < worksheet.Tables.Count; i++)
                    {
                        tbl = worksheet.Tables[i];
                        tbl.StyleName = "ClaroTable";
                        tbl.WorkSheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        tbl.ShowFilter = false;
                        AddressPrintArea = "A1:" + tbl.Address.Address.Split(':')[1];
                    }
                }

                worksheet.PrinterSettings.PrintArea = worksheet.Cells[AddressPrintArea];
                worksheet.PrinterSettings.LeftMargin = new decimal(0.25);
                worksheet.PrinterSettings.RightMargin = new decimal(0.25);
                worksheet.PrinterSettings.TopMargin = new decimal(0.75);
                worksheet.PrinterSettings.BottomMargin = new decimal(0.75);
                worksheet.PrinterSettings.HeaderMargin = new decimal(0.3);
                worksheet.PrinterSettings.FooterMargin = new decimal(0.3);
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.PrinterSettings.ShowHeaders = false;
                worksheet.PrinterSettings.ShowGridLines = false;
                worksheet.PrinterSettings.PaperSize = ePaperSize.A4;
                worksheet.PrinterSettings.PageOrder = ePageOrder.DownThenOver;
                worksheet.PrinterSettings.HorizontalCentered = false;
                worksheet.PrinterSettings.Draft = false;
                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.VerticalCentered = false;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;
               
               
              

                package.Save();
            }

            return path;
        }

    }
}
