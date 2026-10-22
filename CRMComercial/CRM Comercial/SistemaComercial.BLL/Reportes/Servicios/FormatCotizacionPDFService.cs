using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using SistemaComercial.BLL.Reportes.Componentes;
using SistemaComercial.BLL.Reportes.Servicios.Contrato;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Reportes.Servicios
{
    public class FormatCotizacionPDFService : IFormatCotizacionPDFService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IVariablesEconomicasService _variableEconomicaService;
        private decimal valorDiezSubtotal;
        private int iva;
        private decimal valorIva;
        private decimal valorTotal;
        private CultureInfo culture = new CultureInfo("es-CO");
        public FormatCotizacionPDFService(IWebHostEnvironment webHostEnvironment, IVariablesEconomicasService variableEconomicaService)
        {
            _webHostEnvironment = webHostEnvironment;
            _variableEconomicaService = variableEconomicaService;
        }

        public byte[] GenerateCotizacionPDF(CotizacionDTO cotizacion)
        {
            //Valores cotización
            this.valorDiezSubtotal = ObtenerSubtotal(cotizacion) * (decimal)0.1;
            this.iva = this.ObtenerImpuesto().Result;
            this.valorIva = this.valorDiezSubtotal * ((decimal)this.iva / 100);
            this.valorTotal = valorDiezSubtotal * valorIva;

            var rutaItem = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", "logoColvi.png");

            // Verificar si el archivo existe
            if (!File.Exists(rutaItem))
            {
                throw new FileNotFoundException("Logo no encontrado en la ruta especificada", rutaItem);
            }
            //var imageBytes = this.GetImageBytesFromDisk("./Images/logoColvi.png");
            var data = QuestPDF.Fluent.Document.Create(document =>
            {
                document.Page(page =>
                {
                    //Page content
                    page.Margin(30);
                    page.Header().ShowOnce().Row(row =>
                    {
                        // ContantItem = ancho fijo, RelativeItem = Ancho auto
                        //var rutaImagen = Path.Combine(_host.ContentRootPath, "images");

                        row.ConstantItem(100).AlignCenter().AlignMiddle().PaddingLeft(5).Image(rutaItem);

                        //Header información empresa
                        row.RelativeItem().AlignCenter().AlignMiddle().Column(col =>
                        {
                            col.Item().AlignCenter().Text("Colviseg del Caribe Ltda.").Bold().FontSize(14);
                            col.Item().AlignCenter().Text("Nit: 800.101.613 - 0").FontSize(9);
                            col.Item().AlignCenter().Text("Cra 42 N 75B-119").FontSize(9);
                            col.Item().AlignCenter().Text("Tel: 3114156455").FontSize(9);
                            col.Item().AlignCenter().Text("comercial@cvsc.com.co").FontSize(9);
                        });
                        row.ConstantItem(150).AlignMiddle().Column(col =>
                        {
                            col.Item().Background("#003057").Border(1).BorderColor("#003057").AlignCenter().Text("Número de cotización").FontColor("#ffff");
                            col.Item().Border(1).BorderColor("#003057").AlignCenter().Text("C - " + cotizacion.NumeroDocumento);
                        });
                    });
                    page.Content().PaddingVertical(10).Column(col1 =>
                    {
                        //Componente Información cliente
                        col1.Item().Row(row1 =>
                        {
                            row1.RelativeItem().Component(new InformationCliente(cotizacion));
                        });

                        col1.Item().PaddingVertical(10).LineHorizontal(0.5f);

                        //Tabla de items
                        col1.Item().Row(row2 =>
                        {
                            row2.RelativeItem().Component(new TablaComponent(cotizacion));
                        });

                        //Iva, Subtotal, Total
                        col1.Item().PaddingVertical(10).Row(row =>
                        {
                            row.RelativeItem().Column(column1 =>
                            {
                                //Agregar el texto en línea
                            });
                            row.RelativeItem().Column(column1 =>
                            {
                                //Agregar el texto en línea
                            });
                            row.RelativeItem().Column(column1 =>
                            {
                                //Agregar el texto en línea
                                column1.Item().Border(1).BorderColor("#003057").AlignCenter().Text("Subtotal");
                                column1.Item().Border(1).BorderColor("#003057").AlignCenter().Text("Base para IVA");
                                column1.Item().Border(1).BorderColor("#003057").AlignCenter().Text("IVA 19%");
                                column1.Item().Background("#003057").Border(1).BorderColor("#003057").AlignCenter().Text("Total").FontColor("#ffff");
                            });
                            row.RelativeItem().Column(column1 =>
                            {
                                //Agregar el texto en línea
                                column1.Item().Border(1).BorderColor("#003057").AlignCenter().Text(ObtenerSubtotal(cotizacion).ToString("C2", this.culture));
                                column1.Item().Border(1).BorderColor("#003057").AlignCenter().Text(this.valorDiezSubtotal.ToString("C2", this.culture));
                                column1.Item().Border(1).BorderColor("#003057").AlignCenter().Text(this.valorIva.ToString("C2", this.culture));
                                column1.Item().Background("#003057").Border(1).BorderColor("#003057").AlignCenter().Text(RoundCustom((ObtenerSubtotal(cotizacion) + valorIva)).ToString("C2", this.culture)).FontColor("#ffff");
                            });
                        });

                        col1.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
                        {
                            column.Item().Text("Comentarios").FontSize(14);
                            column.Item().Text(cotizacion.Comentarios ?? "");
                            column.Spacing(5);

                        });
                    });
                    page.Footer().AlignRight().Text(txt =>
                    {
                        txt.Span("Página ").FontSize(10);
                        txt.CurrentPageNumber().FontSize(10);
                        txt.Span(" de ").FontSize(10);
                        txt.TotalPages().FontSize(10);
                    });
                });
            });

            using (var memoryStream = new MemoryStream())
            {
                data.GeneratePdf(memoryStream);
                return memoryStream.ToArray();
            }

        }


        private decimal ObtenerSubtotal(CotizacionDTO cotizacion)
        {
           
            decimal contador = 0;
            foreach (var item in cotizacion.DetalleCotizacions)
            {
                contador += (decimal)item.Total;
            }
            return contador;
        }

        private async Task<int> ObtenerImpuesto()
        {
            try
            {
                int valorIva = (int)await this._variableEconomicaService.ListarVariable("iva");
                return valorIva;
            }
            catch
            {
                throw;
            }
        }

        public static int RoundCustom(decimal value)
        {
            // Obtener la parte entera del número
            int wholePart = (int)value;

            // Obtener la parte decimal del número
            decimal fractionalPart = value - wholePart;

            // Si la parte decimal es mayor o igual a 0.5, redondear hacia arriba
            if (fractionalPart >= (decimal)0.5)
            {
                return (int)Math.Ceiling(value);
            }
            else
            {
                // De lo contrario, redondear hacia abajo
                return (int)Math.Floor(value);
            }
        }
    }
}
