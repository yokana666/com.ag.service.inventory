using Com.Danliris.Service.Inventory.Lib.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace Com.Danliris.Service.Inventory.Lib.PDFTemplates
{
    public class FpRegradingResultDocsPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(FpRegradingResultDocsViewModel viewModel)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            BaseFont bf_bold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            var normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);
            var bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 8);

            Document document = new Document(PageSize.A5);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.CloseStream = false;
            document.Open();
            PdfContentByte cb = writer.DirectContent;

            cb.BeginText();

            //cb.SetFontAndSize(bf, 8);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PT DANLIRIS", 50, 378, 0);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "SUKOHARJO", 50, 368, 0);

            cb.SetFontAndSize(bf_bold, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FM.FP-01-PR.01-09.1-02", 290, 570, 0);

            cb.SetFontAndSize(bf_bold, 14);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "BON HASIL RE-GRADING KAIN GREIGE", 210, 540, 0);

            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "No Bon", 20, 500, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 90, 500, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Code, 100, 500, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Unit", 20, 490, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 90, 490, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Bon.unitName, 100, 490, 0);


            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nama Barang", 20, 480, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 90, 480, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Product.Name, 100, 480, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Panjang Datang", 20, 470, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 90, 470, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, string.Format("{0:n}", viewModel.TotalLength), 100, 470, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Grade Datang", 20, 460, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 90, 460, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.OriginalGrade, 100, 460, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tanggal", 240, 500, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 290, 500, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel._CreatedUtc.ToString("MM-dd-yyyy"), 300, 500, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nama Mesin", 240, 490, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 290, 490, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Machine.name, 300, 490, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Operator", 240, 480, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 290, 480, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Operator, 300, 480, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Shift", 240, 470, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 290, 470, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Shift, 300, 470, 0);


            cb.EndText();

            #region CreateTable
            PdfPTable table = new PdfPTable(3);
            PdfPCell cell;
            table.TotalWidth = 380f;

            float[] widths = new float[] { 4f, 4f, 7f };
            table.SetWidths(widths);

            cell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            var rightCell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            var leftCell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            //Create cells headers.


            //cell.Phrase = new Phrase("Grade Asli", bold_font);
            //table.AddCell(cell);

            cell.Phrase = new Phrase("Hasil Regrade", bold_font);
            table.AddCell(cell);

            //cell.Phrase = new Phrase("Panjang Before", bold_font);
            //table.AddCell(cell);

            cell.Phrase = new Phrase("Panjang Re-grade", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Keterangan", bold_font);
            table.AddCell(cell);

            //int index = 1;

            foreach (var detail in viewModel.Details)
            {
                cell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
                //cell.Phrase = new Phrase(detail.GradeBefore, normal_font);
                //table.AddCell(cell);


                cell.Phrase = new Phrase(detail.Grade, normal_font);
                table.AddCell(cell);


                //cell.Phrase = new Phrase(string.Format("{0:n}", detail.LengthBeforeReGrade), normal_font);
                //table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("{0:n}", detail.Length), normal_font);
                table.AddCell(cell);

                cell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
                cell.Phrase = new Phrase(detail.Remark, normal_font);
                table.AddCell(cell);

                //index++;
            }

            //var footerCell = new PdfPCell(new Phrase("Sukoharjo : " + viewModel._CreatedUtc.ToString("MM-dd-yyyy"), normal_font));
            //footerCell.Colspan = 5;
            //footerCell.Border = Rectangle.NO_BORDER;
            //footerCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //table.AddCell(footerCell);
            //Save tables.

            table.WriteSelectedRows(0, -1, 20, 450, cb);
            #endregion


            cb.BeginText();
            cb.SetTextMatrix(15, 23);

            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Penerima,", 100, 110, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(.................................)", 100, 50, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Operator", 100, 35, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Mengetahui,", 320, 110, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(.................................)", 320, 50, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Kasubsie", 320, 35, 0);

            cb.EndText();

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;
            return stream;
        }
    }
}
