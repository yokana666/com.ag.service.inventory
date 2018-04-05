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

            Document document = new Document(PageSize.A5.Rotate());
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
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FM.FP-01-PR.01-09.1-02", 450, 336, 0);

            cb.SetFontAndSize(bf_bold, 14);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "BON HASIL RE-GRADING KAIN GREIGE", 297, 378, 0);

            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "No Bon", 50, 316, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 110, 316, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Code, 120, 316, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Unit", 50, 306, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 110, 306, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Supplier.name, 120, 306, 0);


            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nama Barang", 50, 296, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 110, 296, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Product.Name, 120, 296, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tanggal", 410, 316, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 465, 316, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel._CreatedUtc.ToString("MM-dd-yyyy"), 475, 316, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nama Mesin", 410, 306, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 465, 306, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Machine.name, 475, 306, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Operator", 410, 296, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 465, 296, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Operator, 475, 296, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Shift", 410, 286, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 465, 286, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Shift, 475, 286, 0);


            cb.EndText();

            #region CreateTable
            PdfPTable table = new PdfPTable(5);
            PdfPCell cell;
            table.TotalWidth = 500f;

            float[] widths = new float[] { 4f, 4f, 4f, 4f, 7f };
            table.SetWidths(widths);

            cell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            var rightCell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            var leftCell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            //Create cells headers.


            cell.Phrase = new Phrase("Grade Asli", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Hasil Regrade", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Panjang Before", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Panjang After", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Keterangan", bold_font);
            table.AddCell(cell);

            //int index = 1;

            foreach (var detail in viewModel.Details)
            {
                cell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
                cell.Phrase = new Phrase(detail.GradeBefore, normal_font);
                table.AddCell(cell);


                cell.Phrase = new Phrase(detail.Grade, normal_font);
                table.AddCell(cell);


                cell.Phrase = new Phrase(string.Format("{0:n}", detail.LengthBeforeReGrade), normal_font);
                table.AddCell(cell);

                cell.Phrase = new Phrase(string.Format("{0:n}", detail.Length), normal_font);
                table.AddCell(cell);

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

            table.WriteSelectedRows(0, -1, 50, 270, cb);
            #endregion


            cb.BeginText();
            cb.SetTextMatrix(15, 23);

            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Penerima,", 130, 110, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(.................................)", 130, 50, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Operator", 130, 35, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Mengetahui,", 460, 110, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(.................................)", 460, 50, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Kasubsie", 460, 35, 0);

            cb.EndText();

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;
            return stream;
        }
    }
}
