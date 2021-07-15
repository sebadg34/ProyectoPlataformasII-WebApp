﻿using System;
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
        public static PdfVoucherWriter instancia { get; private set; }

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
            if (instancia == null)
            {
                instancia = new PdfVoucherWriter();
            }

            return instancia;
        }

        /// <summary>
        /// Método encargado de escribir cada aspecto del archivo Pdf de la reserva.
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public FileStreamResult WriteVoucher(Customer cliente, Flight vuelo)
        {
            MemoryStream flujoMemoria = new MemoryStream();

            PdfWriter escritorPdf = new PdfWriter(flujoMemoria);
            PdfDocument documentoPdf = new PdfDocument(escritorPdf);
            Document documento = new Document(documentoPdf,PageSize.LETTER);

            PdfFont fuente = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            Style estiloTitulo = new Style()
                .SetFont(fuente)
                .SetFontSize(28)
                .SetBold()
                .SetTextAlignment(TextAlignment.JUSTIFIED);

            Style estiloParrafo = new Style()
                .SetFont(fuente)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.JUSTIFIED);

            Style estiloCelda = new Style()
                .SetFont(fuente)
                .SetFontSize(12)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);

            string rutaLogo = HttpContext.Current.Server.MapPath("~/Images/LogoAirlinePlus.png");
            Image logo = new Image(ImageDataFactory.Create(rutaLogo));

            documentoPdf.AddEventHandler(PdfDocumentEvent.START_PAGE, new HeaderEventHandler(logo));

            documento.SetMargins(25,25,25,25);

            documento.Add(new Paragraph("Información de tu Reserva").AddStyle(estiloTitulo));
            documento.Add(new Paragraph("Este documento contiene el detalle y condiciones del servicio que adquiriste.").AddStyle(estiloParrafo));
            documento.Add(new Paragraph("No es necesario que lo lleves el día de tu viaje.").AddStyle(estiloParrafo));
            documento.Add(new LineSeparator(new SolidLine()));

            //Salto de línea
            documento.Add(new Paragraph());

            //Tabla de datos pasajero
            Table tabla = new Table(2).UseAllAvailableWidth();
            //Datos Pasajero
            Cell celda = new Cell(1,2).Add(new Paragraph("Datos Pasajero"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddCell(celda);
            //Nombre y Apellido
            celda = new Cell(1,1).Add(new Paragraph("Nombre del pasajero"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddCell(celda);
            celda = new Cell(1,1).Add(new Paragraph(cliente.Nombres + " " + cliente.Apellidos))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            //Rut o Num. Pasaporte
            celda = new Cell(1, 1).Add(new Paragraph("Documento de identificación"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddCell(celda);
            if(cliente.Rut != null)
            {
                celda = new Cell(1, 1).Add(new Paragraph(cliente.Rut))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
                tabla.AddCell(celda);
            }
            else
            {
                celda = new Cell(1, 1).Add(new Paragraph(cliente.Numero_Pasaporte))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
                tabla.AddCell(celda);
            }
            //Dirección y Num. Dirección
            celda = new Cell(1, 1).Add(new Paragraph("Dirección del pasajero"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddCell(celda);
            celda = new Cell(1, 1).Add(new Paragraph(cliente.Direccion + " " + cliente.Numero_Direccion))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            //N° Teléfono
            celda = new Cell(1, 1).Add(new Paragraph("Número Telefónico"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddCell(celda);
            celda = new Cell(1, 1).Add(new Paragraph("+56" + cliente.Numero_Telefono.ToString()))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            //Datos Emergencia
            celda = new Cell(1, 2).Add(new Paragraph("Datos de Emergencia"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddCell(celda);
            //Nombre y Apellido de Emergencia
            celda = new Cell(1, 1).Add(new Paragraph("Nombre de Emergencia"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddCell(celda);
            celda = new Cell(1, 1).Add(new Paragraph(cliente.Nombres_Emergencia + " " + cliente.Apellidos_Emergencia ))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            //N° Teléfono Emergencia
            celda = new Cell(1, 1).Add(new Paragraph("Número Telefónico Emergencia"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddCell(celda);
            celda = new Cell(1, 1).Add(new Paragraph("+56" + cliente.Numero_Telefono_Emergencia.ToString()))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);

            documento.Add(tabla);

            //Salto de línea
            documento.Add(new Paragraph());
            documento.Add(new Paragraph());

            //Datos de Vuelo
            tabla = new Table(6).UseAllAvailableWidth();
            //N° de Vuelo
            celda = new Cell(2, 1).Add(new Paragraph("N° de Vuelo"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddHeaderCell(celda);
            //Origen
            celda = new Cell(1, 2).Add(new Paragraph("Origen"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddHeaderCell(celda);
            //Destino
            celda = new Cell(1, 2).Add(new Paragraph("Destino"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddHeaderCell(celda);
            //Categoría
            celda = new Cell(2, 1).Add(new Paragraph("Categoría"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddHeaderCell(celda);
            //Ciudad y Fecha Origen
            celda = new Cell().Add(new Paragraph("Fecha"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddHeaderCell(celda);
            celda = new Cell().Add(new Paragraph("Ciudad Origen"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddHeaderCell(celda);
            //Ciudad y Fecha Destino
            celda = new Cell().Add(new Paragraph("Fecha"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddHeaderCell(celda);
            celda = new Cell().Add(new Paragraph("Ciudad Destino"))
                .AddStyle(estiloCelda)
                .SetBold()
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY);
            tabla.AddHeaderCell(celda);

            //Datos
            //Dato N° de Vuelo
            celda = new Cell().Add(new Paragraph(vuelo.ID_Vuelo))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            //Dato Ciudad y Fecha Origen
            celda = new Cell().Add(new Paragraph(vuelo.Fecha_Salida.ToString()))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            celda = new Cell().Add(new Paragraph(vuelo.Ciudad_Origen))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            //Dato Ciudad y Fecha Destino
            celda = new Cell().Add(new Paragraph(vuelo.Fecha_Llegada.ToString()))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            celda = new Cell().Add(new Paragraph(vuelo.Ciudad_Destino))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);
            //Dato Categoría
            celda = new Cell().Add(new Paragraph(vuelo.Categoria))
                .AddStyle(estiloCelda)
                .SetBackgroundColor(ColorConstants.LIGHT_GRAY, 0.5f);
            tabla.AddCell(celda);

            documento.Add(tabla);

            //Salto de línea
            documento.Add(new Paragraph());
            documento.Add(new LineSeparator(new SolidLine()));
            documento.Add(new Paragraph());
            documento.Add(new Paragraph("NOTA:").SetBold()).Add(new Paragraph("No olvide llegar al menos dos horas antes al aeropuerto."));

            documento.Close();

            byte[] flujoBytes = flujoMemoria.ToArray();
            flujoMemoria = new MemoryStream();
            flujoMemoria.Write(flujoBytes, 0,flujoBytes.Length);
            flujoMemoria.Position = 0;

            return new FileStreamResult(flujoMemoria, "application/pdf");
        }
    }

    public class HeaderEventHandler : IEventHandler
    {
        Image logo;

        public HeaderEventHandler(Image imagen)
        {
            this.logo = imagen;
        }
        public void HandleEvent(Event evento)
        {
            PdfDocumentEvent eventoDocumento = (PdfDocumentEvent)evento;
            PdfDocument documentoPdf = eventoDocumento.GetDocument();
            PdfPage paginaPdf = eventoDocumento.GetPage();

            PdfCanvas canvasPdf = new PdfCanvas(paginaPdf.NewContentStreamBefore(), paginaPdf.GetResources(), documentoPdf);
            Rectangle areaRaiz = new Rectangle(25, paginaPdf.GetPageSize().GetTop() - 25, paginaPdf.GetPageSize().GetRight() - 25, 25);
            new Canvas(canvasPdf, areaRaiz).Add(getTable());

        }

        public Table getTable()
        {
            float[] anchuraCelda = { 40f, 80f };
            Table tablaEvento = new Table(UnitValue.CreatePercentArray(anchuraCelda)).UseAllAvailableWidth();

            Style estiloCelda = new Style()
                .SetBorder(Border.NO_BORDER);
            Style estiloTexto = new Style()
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(10f);

            Cell celda = new Cell().Add(logo.SetAutoScale(true));

            tablaEvento.AddCell(celda
                .AddStyle(estiloCelda)
                .SetTextAlignment(TextAlignment.RIGHT));

            celda = new Cell()
                .Add(new Paragraph("AIRLINE PLUS S.A."))
                .AddStyle(estiloTexto).AddStyle(estiloCelda);

            tablaEvento.AddCell(celda);

            return tablaEvento;

        }
    }
}