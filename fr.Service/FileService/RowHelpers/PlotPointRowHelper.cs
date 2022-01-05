using fr.Core.Exceptions;
using fr.Service.Model.Plots;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;

namespace fr.Service.FileService.RowHelpers
{
    public static class PlotPointRowHelper
    {
        private static readonly List<string> _headers = new() { "Sub", "Code", "Common Name", "Science Name", "Family", "Diameter", "Height (Max)", "X", "Y" };
        private static readonly Dictionary<string, int> headerDict = new();

        public static void LoadHeaderDict(List<string> headers)
        {
            headerDict.Clear();
            foreach (var header in _headers)
            {
                headerDict.Add(header, headers.IndexOf(header));
            }
        }

        public static PlotPointFromFileModel ReadFromRow(ExcelRange cells, int row)
        {
            if (!headerDict.Any())
            {
                throw new AppException("Header Dict must not be empty");
            }

            if (!double.TryParse(cells[row, headerDict["Diameter"] + 1].Text, out var diametorResult))
            {
                return null;
            }

            if (!double.TryParse(cells[row, headerDict["Height (Max)"] + 1].Text, out var heightResult))
            {
                return null;
            }

            if (!double.TryParse(cells[row, headerDict["X"] + 1].Text, out var xResult))
            {
                return null;
            }

            if (!double.TryParse(cells[row, headerDict["Y"] + 1].Text, out var yResult))
            {
                return null;
            }

            return new PlotPointFromFileModel
            {
                Sub = cells[row, headerDict["Sub"] + 1].Text,
                Code = cells[row, headerDict["Code"] + 1].Text,
                Diameter = diametorResult,
                Height = heightResult,
                X = xResult,
                Y = yResult,
                CommonName = cells[row, headerDict["Common Name"] + 1].Text,
                ScienceName = cells[row, headerDict["Science Name"] + 1].Text,
                Family = cells[row, headerDict["Family"] + 1].Text
            };
        }
    }
}
