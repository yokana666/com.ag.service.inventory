using Com.Danliris.Service.Inventory.Lib.ViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace Com.Danliris.Service.Inventory.Lib.PDFTemplates
{
    public class FpReturProInvDocsPdfTemplate
    {
        public MemoryStream GeneratePdfTemplate(FpReturProInvDocsViewModel viewModel)
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

            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PT DANLIRIS", 50, 378, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "SUKOHARJO", 50, 368, 0);

            cb.SetFontAndSize(bf_bold, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FM.FP-GB-15-005 / R2", 450, 378, 0);

            cb.SetFontAndSize(bf_bold, 14);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "BON RETUR BARANG", 297, 336, 0);

            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Dari Bagian", 50, 316, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 110, 316, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Produksi", 120, 316, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Untuk Bagian", 50, 306, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 110, 306, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Gudang Material", 120, 306, 0);


            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Bon Pengantar", 50, 296, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 110, 296, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Bon.No, 120, 296, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "No", 450, 316, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ":", 465, 316, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Code, 475, 316, 0);


            cb.EndText();

            #region CreateTable
            PdfPTable table = new PdfPTable(5);
            PdfPCell cell;
            table.TotalWidth = 500f;

            float[] widths = new float[] { 2f, 10f, 4f, 4f, 4f };
            table.SetWidths(widths);

            cell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            var rightCell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            var leftCell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            //Create cells headers.


            cell.Phrase = new Phrase("No", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Nama Barang", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Panjang (Meter)", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Jumlah", bold_font);
            table.AddCell(cell);

            cell.Phrase = new Phrase("Panjang Total", bold_font);
            table.AddCell(cell);

            int index = 1;

            foreach (var detail in viewModel.Details)
            {
                cell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
                cell.Phrase = new Phrase(index.ToString(), normal_font);
                table.AddCell(cell);


                cell.Phrase = new Phrase(detail.Product.Name, normal_font);
                table.AddCell(cell);


                cell.Phrase = new Phrase(string.Format("{0:n}", detail.Length), normal_font);
                table.AddCell(cell);


                cell.Phrase = new Phrase(string.Format("{0:n}", detail.Quantity), normal_font);
                table.AddCell(cell);


                cell.Phrase = new Phrase(string.Format("{0:n}", detail.Quantity * detail.Length), normal_font);
                table.AddCell(cell);
                index++;
            }

            var footerCell = new PdfPCell(new Phrase("Sukoharjo : " + viewModel._CreatedUtc, normal_font));
            footerCell.Colspan = 5;
            footerCell.Border = Rectangle.NO_BORDER;

            table.AddCell(footerCell);
            //Save tables.

            table.WriteSelectedRows(0, -1, 50, 285, cb);
            #endregion


            cb.BeginText();
            cb.SetTextMatrix(15, 23);

            cb.SetFontAndSize(bf, 8);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Yang Menerima,", 130, 110, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(.................................)", 130, 50, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Bag. Gudang Material", 130, 35, 0);

            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Yang Menyerahkan,", 460, 110, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, $"{viewModel._CreatedBy}", 460, 55, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(.................................)", 460, 50, 0);


            cb.EndText();

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;
            return stream;
        }
    }
}
