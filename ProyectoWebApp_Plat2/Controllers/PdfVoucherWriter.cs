using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iText.IO.Font;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using ProyectoWebApp_Plat2.Models;

namespace ProyectoWebApp_Plat2.Controllers
{
    /// <summary>
    /// Clase que se encarga de la escritura del archivo Pdf de cada Reserva
    /// </summary>
    public class PdfVoucherWriter
    {
        //instancia de la clase.
        public static PdfVoucherWriter instance { get; private set; }

        /// <summary>
        /// Constructor de la clase. Es de caracter privado para seguir el patrón de sisenio Singleton.
        /// </summary>
        private PdfVoucherWriter()
        {

        }

        /// <summary>
        /// Metodo que verifica si existe ya una instancia de la clase, de no existir una, entonces se crea la instancia única. En cualquier caso se retorna la instancia de la clase.
        /// </summary>
        /// <returns>La instancia única de la clase</returns>
        public static PdfVoucherWriter GetInstance()
        {
            if (instance == null)
            {
                instance = new PdfVoucherWriter();
            }

            return instance;
        }

        /// <summary>
        /// Método encargado de escribir cada aspecto del archivo Pdf de la reserva.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public FileStreamResult WriteVoucher(Customer customer)
        {
            MemoryStream memoryStream = new MemoryStream();

            PdfWriter pdfWriter = new PdfWriter(memoryStream);
            PdfDocument pdfDocument = new PdfDocument(pdfWriter);
            Document document = new Document(pdfDocument,PageSize.LETTER);

            PdfFont font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            Style styleTitles = new Style()
                .SetFont(font)
                .SetFontSize(28)
                .SetBold()
                .SetTextAlignment(TextAlignment.JUSTIFIED);

            Style styleParagraph = new Style()
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.JUSTIFIED);

            Style styleCell = new Style()
                .SetFont(font)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);


            Image logo = new Image(ImageDataFactory.Create(@"C:\Users\tomas\source\repos\ProyectoReservaVuelos\ProyectoWebApp_Plat2\images\Logo AirlinePlus.jfif"));

            pdfDocument.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(logo));

            document.SetMargins(25,25,25,25);

            document.Add(new Paragraph("Información de tu Reserva").AddStyle(styleTitles));
            document.Add(new Paragraph("Este documento contiene el detalle y condiciones del servicio que adquiriste.").AddStyle(styleParagraph));
            document.Add(new Paragraph("No es necesario que lo lleves el día de tu viaje.").AddStyle(styleParagraph));
            document.Add(new LineSeparator(new SolidLine()));

            //Salto de línea
            document.Add(new Paragraph());

            //Tabla de datos pasajero
            Table table = new Table(2).UseAllAvailableWidth();
            //Datos Pasajero
            Cell cell = new Cell(1,2).Add(new Paragraph("Datos Pasajero"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddCell(cell);
            //Nombre y Apellido
            cell = new Cell(1,1).Add(new Paragraph("Nombre del pasajero"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddCell(cell);
            cell = new Cell(1,1).Add(new Paragraph(""))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            //Rut o Num. Pasaporte
            cell = new Cell(1, 1).Add(new Paragraph("Documento de identificación"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddCell(cell);
            cell = new Cell(1, 1).Add(new Paragraph(""))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            //Dirección y Num. Dirección
            cell = new Cell(1, 1).Add(new Paragraph("Dirección del pasajero"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddCell(cell);
            cell = new Cell(1, 1).Add(new Paragraph(""))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            //N° Teléfono
            cell = new Cell(1, 1).Add(new Paragraph("Número Telefónico"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddCell(cell);
            cell = new Cell(1, 1).Add(new Paragraph(""))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            //Datos Emergencia
            cell = new Cell(1, 2).Add(new Paragraph("Datos de Emergencia"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddCell(cell);
            //Nombre y Apellido de Emergencia
            cell = new Cell(1, 1).Add(new Paragraph("Nombre de Emergencia"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddCell(cell);
            cell = new Cell(1, 1).Add(new Paragraph(""))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            //N° Teléfono Emergencia
            cell = new Cell(1, 1).Add(new Paragraph("Número Telefónico Emergencia"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddCell(cell);
            cell = new Cell(1, 1).Add(new Paragraph(""))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);

            document.Add(table);

            //Salto de línea
            document.Add(new Paragraph());
            document.Add(new Paragraph());

            //Datos de Vuelo
            table = new Table(6).UseAllAvailableWidth();
            //N° de Vuelo
            cell = new Cell(2, 1).Add(new Paragraph("N° de Vuelo"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddHeaderCell(cell);
            //Origen
            cell = new Cell(1, 2).Add(new Paragraph("Origen"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddHeaderCell(cell);
            //Destino
            cell = new Cell(1, 2).Add(new Paragraph("Destino"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddHeaderCell(cell);
            //Categoría
            cell = new Cell(2, 1).Add(new Paragraph("Categoría"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddHeaderCell(cell);
            //Ciudad y Fecha Origen
            cell = new Cell().Add(new Paragraph("Fecha"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddHeaderCell(cell);
            cell = new Cell().Add(new Paragraph("Ciudad Origen"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddHeaderCell(cell);
            //Ciudad y Fecha Destino
            cell = new Cell().Add(new Paragraph("Fecha"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddHeaderCell(cell);
            cell = new Cell().Add(new Paragraph("Ciudad Destino"))
                .AddStyle(styleCell)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            table.AddHeaderCell(cell);

            //Datos
            //Dato N° de Vuelo
            cell = new Cell().Add(new Paragraph(" "))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            //Dato Ciudad y Fecha Origen
            cell = new Cell().Add(new Paragraph(" "))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            cell = new Cell().Add(new Paragraph(" "))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            //Dato Ciudad y Fecha Destino
            cell = new Cell().Add(new Paragraph(" "))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            cell = new Cell().Add(new Paragraph(" "))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);
            //Dato Categoría
            cell = new Cell().Add(new Paragraph(" "))
                .AddStyle(styleCell)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            table.AddCell(cell);

            document.Add(table);

            //Salto de línea
            document.Add(new Paragraph());
            document.Add(new LineSeparator(new SolidLine()));
            document.Add(new Paragraph());
            document.Add(new Paragraph("NOTA:").SetBold()).Add(new Paragraph("No olvide llegar al menos dos horas antes al aeropuerto."));

            document.Close();

            byte[] byteStream = memoryStream.ToArray();
            memoryStream = new MemoryStream();
            memoryStream.Write(byteStream, 0,byteStream.Length);
            memoryStream.Position = 0;

            return new FileStreamResult(memoryStream, "application/pdf");
        }
    }

    public class HeaderEventHandler : IEventHandler
    {
        Image logo;

        public HeaderEventHandler(Image image)
        {
            this.logo = image;
        }
        public void HandleEvent(Event @event)
        {
            PdfDocumentEvent docEvent = (PdfDocumentEvent)@event;
            PdfDocument pdfDocument = docEvent.GetDocument();
            PdfPage pdfPage = docEvent.GetPage();

            PdfCanvas pdfCanvas = new PdfCanvas(pdfPage.NewContentStreamBefore(), pdfPage.GetResources(), pdfDocument);
            Rectangle rootArea = new Rectangle(25, pdfPage.GetPageSize().GetTop() - 25, pdfPage.GetPageSize().GetRight() - 25, 25);
            new Canvas(pdfCanvas, rootArea).Add(getTable(docEvent));

        }

        public Table getTable(PdfDocumentEvent docEvent)
        {
            float[] cellWidht = { 40f, 80f };
            Table tableEvent = new Table(UnitValue.CreatePercentArray(cellWidht)).UseAllAvailableWidth();

            Style styleCell = new Style()
                .SetBorder(Border.NO_BORDER);
            Style styleText = new Style()
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10f);

            Cell cell = new Cell().Add(logo.SetAutoScale(true));

            tableEvent.AddCell(cell
                .AddStyle(styleCell)
                .SetTextAlignment(TextAlignment.RIGHT));

            cell = new Cell()
                .Add(new Paragraph("AIRLINE PLUS S.A."))
                .AddStyle(styleText).AddStyle(styleCell);

            tableEvent.AddCell(cell);

            return tableEvent;

        }
    }
}