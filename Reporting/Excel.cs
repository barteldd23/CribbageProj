using ClosedXML.Excel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Reporting
{
    public static class Excel
    {
        public static void Export(string filename, string[,] data)
        {
            try
            {
                IXLWorkbook xlWorkbook = new XLWorkbook();
                IXLWorksheet xlWorksheet = xlWorkbook.AddWorksheet("Data");

                int rows = data.GetLength(0);
                int cols = data.GetLength(1);

                PdfWriter writer = new PdfWriter(filename.Substring(0, filename.Length - 4) + "pdf");
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                Paragraph header = new Paragraph("Data")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(20);
                document.Add(header);

                Paragraph subheader = new Paragraph("Information")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(15);
                document.Add(subheader);

                Table table = new Table(cols, false);

                for (int iRow = 1; iRow <= rows; iRow++)
                {
                    for (int iCol = 1; iCol <= cols; iCol++)
                    {
                        // Excel
                        xlWorksheet.Cell(iRow, iCol).Value = data[iRow - 1, iCol - 1];

                        // PDF
                        Cell cell = new Cell(1, 1);
                        cell.Add(new Paragraph(data[iRow - 1, iCol - 1]));
                        table.AddCell(cell);
                    }
                }

                document.Add(table);
                document.Close(); // you can run into sharing violations when closing a workbook
                xlWorkbook.SaveAs(filename); // different ways to save (paragraph, worksheet, workbook)
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
