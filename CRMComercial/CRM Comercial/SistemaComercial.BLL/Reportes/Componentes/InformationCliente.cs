using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Reportes.Componentes
{
    public class InformationCliente :  IComponent
    {
        private CotizacionDTO cotizacion;

        public InformationCliente(CotizacionDTO _cotizacion)
        {
            cotizacion = _cotizacion;
        }
        
        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Background("#EAEAEA").Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Padding(3).Text("Información cliente");
                    });
                });

                column.Item().PaddingVertical(5).Row(row =>
                {
                    //Reemplazar esto por componentes
                    row.RelativeItem().Border(1).BorderColor("#EAEAEA").Background(Colors.White).Padding(10).Column(column1 =>
                    {
                        //Agregar el texto en línea
                        column1.Item().Text(txt =>
                        {
                            txt.Span("Cliente: ").SemiBold().FontSize(10);
                            txt.Span(cotizacion.IdClienteNavigation!.NombreCompleto).FontSize(10);
                        });
                        //Definir la sucursal del primer detalle
                        column1.Item().Text(txt =>
                        {
                            txt.Span("Sucursal: ").SemiBold().FontSize(10);
                            txt.Span(ObtenerSucursal()).FontSize(10);
                        });
                        column1.Item().Text(txt =>
                        {
                            txt.Span("Contacto: ").SemiBold().FontSize(10);
                            txt.Span(cotizacion.IdClienteNavigation!.NombreContacto).FontSize(10);
                        });
                    });

                    row.RelativeItem().Border(1).BorderColor("#EAEAEA").Background(Colors.White).Padding(10).Column(column1 =>
                    {
                        //Agregar el texto en línea
                        column1.Item().Text(txt =>
                        {
                            txt.Span("Correo electrónico: ").SemiBold().FontSize(10);
                            txt.Span(cotizacion.IdClienteNavigation!.Email).FontSize(10);
                        });
                        column1.Item().Text(txt =>
                        {
                            txt.Span("Teléfono: ").SemiBold().FontSize(10);
                            txt.Span(cotizacion.IdClienteNavigation!.Telefono).FontSize(10);
                        });
                    });


                    row.RelativeItem().Border(1).BorderColor("#EAEAEA").Background(Colors.White).Padding(10).Column(column1 =>
                    {
                        //Agregar el texto en línea
                        column1.Item().Text(txt =>
                        {
                            txt.Span("Fecha: ").SemiBold().FontSize(10);
                            txt.Span(cotizacion.CreatedAt).FontSize(10);
                        });
                        column1.Item().Text(txt =>
                        {
                            txt.Span("Tipo servicio: ").SemiBold().FontSize(10);
                            txt.Span(ObtenerNombreServicio()).FontSize(10);
                        });
                        column1.Item().Text(txt =>
                        {
                            txt.Span("Ejecutivo comercial: ").SemiBold().FontSize(10);
                            txt.Span(cotizacion.IdUsuarioNavigation!.NombreCompleto).FontSize(10);
                        });
                    });
                });
            });
        }

        public string ObtenerSucursal()
        {
            var sucursal = cotizacion.DetalleCotizacions.First().IdSucursalNavigation!.Nombre;
            return sucursal;
        }
        public string ObtenerNombreServicio()
        {
            return cotizacion.DetalleCotizacions.FirstOrDefault().IdServicioNavigation.Nombre;
        }
    }
}
