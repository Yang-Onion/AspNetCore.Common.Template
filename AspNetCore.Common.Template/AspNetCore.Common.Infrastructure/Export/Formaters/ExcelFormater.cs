using AspNetCore.Common.Infrastructure.Export.Attributes;
using Npoi.Core.HPSF;
using Npoi.Core.HSSF.UserModel;
using Npoi.Core.HSSF.Util;
using Npoi.Core.OpenXml4Net.OPC;
using Npoi.Core.SS.UserModel;
using Npoi.Core.SS.Util;
using Npoi.Core.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AspNetCore.Common.Infrastructure.Export.Formaters
{
    /// <summary>
    /// Excel格式化 EPPLUS暂时不支持不.NETCore
    /// </summary>
    public class ExcelFormater : BaseDataFormater<Stream>
    {
        private readonly string _template_file_name;
        private readonly string _template_file_directory = "downloadTemp";

        public ExcelFormater() {
            _template_file_name = string.Format("{0}.xls", Guid.NewGuid().ToString());
            if (!Directory.Exists(_template_file_directory))
                try {
                    Directory.CreateDirectory(_template_file_directory);
                }
                catch { }
        }

        public bool HasHeader { get; set; } = true;

        public override Stream Format(IList<IEnumerable<ExportProperty>> entities) {
            if (entities != null) {
                XSSFWorkbook workbook = null;
                try {
                    workbook = new XSSFWorkbook();
                    ISheet sheet1 = workbook.CreateSheet("Sheet1");
                    int rowIndex = 0;
                    int cellIndex = 0;
                    bool buildHeader = false;
                    foreach (var entity in entities) {
                        if (!buildHeader && HasHeader) {
                            var headerRow = CreateRow(sheet1, rowIndex);
                            var headers = entity.Select(g => g.Header);
                            foreach (var header in headers) {
                                var cell = CreateCell(workbook, headerRow, cellIndex);
                                cell.SetCellValue(header);
                                cellIndex++;
                            }
                            buildHeader = true;
                            rowIndex++;
                        }
                        cellIndex = 0;
                        var row = CreateRow(sheet1, rowIndex);
                        var values = entity.Select(g => g.Value);
                        foreach (var val in values) {
                            var cell = CreateCell(workbook, row, cellIndex);
                            if (val is int || val is float || val is decimal || val is double || val is long) {
                                cell.SetCellType(CellType.Numeric);
                                cell.SetCellValue(ToFixed(Convert.ToDouble(val), 2));
                            }
                            else
                                cell.SetCellValue(val?.ToString());
                            cellIndex++;
                        }
                        rowIndex++;
                    }

                    Stream outputStream = new MemoryStream();
                    string tempFile = Path.Combine(_template_file_directory, _template_file_name);
                    using (FileStream fs = new FileStream(tempFile, FileMode.Create, FileAccess.ReadWrite)) {
                        workbook.Write(fs);
                    }
                    using (var file = new FileStream(tempFile, FileMode.Open, FileAccess.Read)) {
                        file.CopyTo(outputStream);
                        outputStream.Seek(0, SeekOrigin.Begin);
                    }
                    File.Delete(tempFile);
                    return outputStream;
                }
                catch {
                    if (workbook != null)
                        try {
                            workbook.Close();
                        }
                        catch { }
                }
            }
            return null;
        }

        public override Stream Format(object[,] twoDimensArray) {
            if (twoDimensArray.Rank == 2) {
                if (twoDimensArray != null) {
                    XSSFWorkbook workbook = null;
                    try {
                        workbook = new XSSFWorkbook();
                        ISheet sheet1 = workbook.CreateSheet("Sheet1");
                        object val = null;
                        int hL = twoDimensArray.GetUpperBound(0) + 1;
                        int vL = twoDimensArray.GetUpperBound(1) + 1;
                        for (var i = 0; i < hL; i++) {
                            var row = CreateRow(sheet1, i);
                            for (var j = 0; j < vL; j++) {
                                val = twoDimensArray[i, j];
                                var cell = CreateCell(workbook, row, j);
                                if (val is int || val is float || val is decimal || val is double || val is long) {
                                    cell.SetCellType(CellType.Numeric);
                                    cell.SetCellValue(ToFixed(Convert.ToDouble(val), 2));
                                }
                                else
                                    cell.SetCellValue(val?.ToString());
                            }
                        }

                        Stream outputStream = new MemoryStream();
                        string tempFile = Path.Combine(_template_file_directory, _template_file_name);
                        using (FileStream fs = new FileStream(tempFile, FileMode.Create, FileAccess.ReadWrite)) {
                            workbook.Write(fs);
                        }
                        using (var file = new FileStream(tempFile, FileMode.Open, FileAccess.Read)) {
                            file.CopyTo(outputStream);
                            outputStream.Seek(0, SeekOrigin.Begin);
                        }
                        File.Delete(tempFile);
                        return outputStream;
                    }
                    catch {
                        if (workbook != null)
                            try {
                                workbook.Close();
                            }
                            catch { }
                    }
                }
            }
            return null;
        }

        public IRow CreateRow(ISheet sheet, int rowIndex) {
            IRow row = sheet.CreateRow(rowIndex);
            return row;
        }

        public ICell CreateCell(IWorkbook workbook, IRow row, int cellIndex, CellType type = CellType.Blank) {
            var cell = row.CreateCell(cellIndex, type);
            var font = workbook.CreateFont();
            font.IsBold = true;
            font.Color = HSSFColor.DarkBlue.Index2;
            cell.CellStyle.SetFont(font);
            return cell;
        }

        private double ToFixed(double d, int s) {
            decimal sp = Convert.ToDecimal(Math.Pow(10, s));
            double spd = Convert.ToDouble(sp);
            if (d < 0)
                return Math.Truncate(d) + Math.Ceiling((d - Math.Truncate(d)) * spd) / spd;
            return Math.Truncate(d) + Math.Floor((d - Math.Truncate(d)) * spd) / spd;
        }
    }
}