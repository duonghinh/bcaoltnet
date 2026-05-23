using ClosedXML.Excel;
using System.Data;

namespace WarehouseManagementSystem.Presenation.Helpers;

public static class DataGridExcelExporter
{
    public static string ExportDataGridView(DataGridView grid, string folder, string fileName)
    {
        Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, fileName);

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Dữ liệu");

        for (int c = 0; c < grid.Columns.Count; c++)
        {
            if (!grid.Columns[c].Visible) continue;
            ws.Cell(1, c + 1).Value = grid.Columns[c].HeaderText;
            ws.Cell(1, c + 1).Style.Font.SetBold();
        }

        for (int r = 0; r < grid.Rows.Count; r++)
        {
            if (grid.Rows[r].IsNewRow) continue;
            for (int c = 0; c < grid.Columns.Count; c++)
            {
                if (!grid.Columns[c].Visible) continue;
                var val = grid.Rows[r].Cells[c].Value;
                ws.Cell(r + 2, c + 1).Value = val?.ToString() ?? "";
            }
        }

        ws.Columns().AdjustToContents();
        wb.SaveAs(path);
        return path;
    }

    public static string ExportDataTable(DataTable table, string folder, string fileName)
    {
        Directory.CreateDirectory(folder);
        var path = Path.Combine(folder, fileName);

        using var wb = new XLWorkbook();
        var ws = wb.Worksheets.Add("Dữ liệu");

        for (int c = 0; c < table.Columns.Count; c++)
            ws.Cell(1, c + 1).Value = table.Columns[c].ColumnName;

        ws.Range(1, 1, 1, table.Columns.Count).Style.Font.SetBold();

        for (int r = 0; r < table.Rows.Count; r++)
        {
            for (int c = 0; c < table.Columns.Count; c++)
                ws.Cell(r + 2, c + 1).Value = table.Rows[r][c]?.ToString() ?? "";
        }

        ws.Columns().AdjustToContents();
        wb.SaveAs(path);
        return path;
    }
}
