using fr.Service.FileService.RowHelpers;
using fr.Service.Model.Plots;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace fr.Service.FileService
{
    public class FileService : IFileService
    {
        public IFormFile File { get; set; }

        public FileService()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public Task<bool> CalcSvgFile(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var rs = new StreamReader(stream);

            var svg = rs.ReadToEnd();

            return Task.FromResult(true);
        }

        public async Task<IEnumerable<PlotPointFromFileModel>> ReadPlotPointsAsync()
        {
            var result = new List<PlotPointFromFileModel>();

            using var ms = new MemoryStream();
            await File.CopyToAsync(ms);

            using var package = new ExcelPackage(ms);
            var sheet = package.Workbook.Worksheets[0];

            var colCount = sheet.Dimension.End.Column;
            var rowCount = sheet.Dimension.End.Row;

            var headerRow = sheet.Cells[1, 1];
            var headerStrings = new List<string>();

            for (int index = 1; index <= colCount; index++)
            {
                headerStrings.Add(headerRow[1, index].ToText());
            }

            PlotPointRowHelper.LoadHeaderDict(headerStrings);

            for (int row = 2; row <= rowCount; row++)
            {
                var cells = sheet.Cells[row, 1];
                result.Add(PlotPointRowHelper.ReadFromRow(cells, row));
            }

            return result.Where(r => r != null);
        }
    }
}
