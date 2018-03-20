using Com.Danliris.Service.Inventory.Lib.ViewModels.MaterialsRequestNoteViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Com.Danliris.Service.Inventory.Lib.PDFTemplates
{
    public class MaterialsRequestNotePdfTemplate
    {
        public string[] Bulan = { "Januari", "Februari", "Maret", "April", "Mei", "Juni", "Juli", "Agustus", "September", "Oktober", "Nopember", "Desember" };
        public MemoryStream GeneratePdfTemplate(MaterialsRequestNoteViewModel viewModel)
        {
            //Declaring fonts.

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            BaseFont bf_bold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            var normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);
            var bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 9);

            //Creating page.

            Document document = new Document(PageSize.A5);
            MemoryStream stream = new MemoryStream();



            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.CloseStream = false;

            document.Open();


            PdfContentByte cb = writer.DirectContent;

            //Set Header
            #region SetHeader

            cb.BeginText();
            cb.SetTextMatrix(15, 23);

            //LEFT
            cb.SetFontAndSize(bf_bold, 14);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "PT DAN LIRIS", 100, 550, 0);
            cb.SetFontAndSize(bf, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "INDUSTRIAL & TRADING CO.LTD", 100, 535, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Kel. Banaran - Sukoharjo", 100, 520, 0);

            cb.SetFontAndSize(bf, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "UNIT", 20, 495, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $": {viewModel.Unit.name}", 55, 495, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "NO", 20, 480, 0);
            cb.SetFontAndSize(bf_bold, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $": {viewModel.Code}", 55, 480, 0);
            cb.SetFontAndSize(bf, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "TIPE", 20, 465, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $": {viewModel.RequestType}", 55, 465, 0);

            //RIGHT
            cb.SetFontAndSize(bf_bold, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FM.FP-00-PC-19-008", 275, 550, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, $"Sukoharjo, {viewModel._CreatedUtc.Day} {Bulan[viewModel._CreatedUtc.Month - 1]} {viewModel._CreatedUtc.Year}", 275, 535, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Kepada Yth.", 275, 495, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "GUDANG GREIGE", 275, 480, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "PT DAN LIRIS - SUKOHARJO", 275, 465, 0);

            cb.SetFontAndSize(bf_bold, 12);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "SURAT PERMINTAAN BARANG", 220, 435, 0);

            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Untuk memenuhi permintaan order kami mohon bantuan Saudara mengusahakan / menyiapkan", 20, 405, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "barang tersebut dibawah:", 20, 395, 0);

            cb.EndText();

            #endregion



            //Creating new table.
            #region CreateTable
            PdfPTable table = string.Equals(viewModel.RequestType.ToUpper(), "TEST") ? new PdfPTable(4) : new PdfPTable(5);
            table.TotalWidth = 380f;

            //535 pixels width distribute into 8 rows.
            //string.Equals(viewModel.RequestType.ToUpper(), "TEST") ? new float[] { 2f, 2f, 15f, 5f, 5f, 5f, 5f, 5f } :
            float[] widths = string.Equals(viewModel.RequestType.ToUpper(), "TEST") ? new float[] { 3f, 10f, 5f, 10f } : new float[] { 3f, 5f, 10f, 5f, 10f };
            table.SetWidths(widths);

            var cell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            var rightCell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            var leftCell = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            //Create cells headers.
            if (string.Equals(viewModel.RequestType.ToUpper(), "TEST"))
            {
                cell.Phrase = new Phrase("No", bold_font);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Nama Barang", bold_font);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Grade", bold_font);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Panjang (Meter)", bold_font);
                table.AddCell(cell);
            }
            else
            {
                cell.Phrase = new Phrase("No", bold_font);
                table.AddCell(cell);

                cell.Phrase = new Phrase("No. SPP", bold_font);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Nama Barang", bold_font);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Grade", bold_font);
                table.AddCell(cell);

                cell.Phrase = new Phrase("Panjang (Meter)", bold_font);
                table.AddCell(cell);
            }


            //Add all items.
            int index = 1;
            foreach (var item in viewModel.MaterialsRequestNote_Items)
            {
                if (string.Equals(viewModel.RequestType.ToUpper(), "TEST"))
                {
                    cell.Phrase = new Phrase(index.ToString(), normal_font);
                    table.AddCell(cell);

                    leftCell.Phrase = new Phrase(item.Product.name, normal_font);
                    table.AddCell(leftCell);

                    cell.Phrase = new Phrase(item.Grade, normal_font);
                    table.AddCell(cell);

                    rightCell.Phrase = new Phrase(item.Length.ToString(), normal_font);
                    table.AddCell(rightCell);
                }
                else
                {
                    cell.Phrase = new Phrase(index.ToString(), normal_font);
                    table.AddCell(cell);

                    cell.Phrase = new Phrase(item.ProductionOrder.orderNo, normal_font);
                    table.AddCell(cell);

                    leftCell.Phrase = new Phrase(item.Product.name, normal_font);
                    table.AddCell(leftCell);

                    cell.Phrase = new Phrase(item.Grade, normal_font);
                    table.AddCell(cell);

                    rightCell.Phrase = new Phrase(item.Length.ToString(), normal_font);
                    table.AddCell(rightCell);
                }
                index++;
            }

            var footerCell = new PdfPCell(new Phrase("Atas bantuan Saudara, kami ucapkan terima kasih.", normal_font));
            footerCell.Colspan = string.Equals(viewModel.RequestType.ToUpper(), "TEST") ? 4 : 5;
            footerCell.Border = Rectangle.NO_BORDER;

            table.AddCell(footerCell);
            //Save tables.

            table.WriteSelectedRows(0, -1, 20, 385, cb);
            #endregion

            //Set footer
            #region Footer

            cb.BeginText();
            cb.SetTextMatrix(15, 23);

            //LEFT
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Penerima", 60, 100, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(..........................)", 60, 50, 0);

            //CENTER
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Mengetahui", 210, 100, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(..........................)", 210, 50, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, $"Kabag {viewModel.Unit.name}", 210, 35, 0);

            //RIGHT
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Hormat Kami", 360, 100, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, $"{viewModel._CreatedBy}", 360, 55, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "(..........................)", 360, 50, 0);
            cb.SetFontAndSize(bf, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Bag. PPIC", 360, 35, 0);

            cb.EndText();

            #endregion

            document.Close();

            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
