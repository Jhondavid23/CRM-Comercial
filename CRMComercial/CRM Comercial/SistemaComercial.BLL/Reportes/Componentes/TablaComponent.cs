using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Reportes.Componentes
{
    public class TablaComponent : IComponent
    {
        private CotizacionDTO cotizacion;
        private string minutosInicioServicio = "";
        private string minutosFinServicio = "";
        private CultureInfo culture = new CultureInfo("es-CO");
        public TablaComponent(CotizacionDTO cotizacion)
        {
            this.cotizacion = cotizacion;
        }

        public void Compose(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Table(tabla =>
                {
                    tabla.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(60);
                        columns.RelativeColumn(2);
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    tabla.Header(header =>
                    {
                        header.Cell().Background("#003057").AlignCenter()
                        .Padding(2).Text("Cantidad").FontSize(10).FontColor("#fff");
                        header.Cell().Background("#003057").AlignCenter()
                        .Padding(2).Text("Descripción").FontSize(10).FontColor("#fff");
                        header.Cell().Background("#003057").AlignCenter()
                        .Padding(2).Text("Horario").FontSize(10).FontColor("#fff");
                        header.Cell().Background("#003057").AlignCenter()
                        .Padding(2).Text("Días").FontSize(10).FontColor("#fff");
                        header.Cell().Background("#003057").AlignCenter()
                        .Padding(2).AlignCenter().Text("Ubicación del servicio").FontSize(10).FontColor("#fff");
                        header.Cell().Background("#003057").AlignRight()
                        .Padding(2).Text("V. Unitario").FontSize(10).FontColor("#fff"); 
                        header.Cell().Background("#003057").AlignRight()
                        .Padding(2).Text("Subtotal").FontSize(10).FontColor("#fff");
                    });

                    foreach(var detalle in cotizacion.DetalleCotizacions)
                    {
                        var horas = this.RetornarHoras(detalle);
                        tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").AlignCenter().Padding(2).Text(detalle.CantidadServicios).FontSize(7).LineHeight(5);
                        tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").AlignCenter().Padding(2).Text(detalle.DetalleServicio!.Length == 0 ? detalle.IdServicioNavigation!.Nombre : detalle.DetalleServicio ).FontSize(7).LineHeight(5);
                        tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").AlignCenter().Padding(2).Text(RetornarHoras(detalle)).FontSize(7).LineHeight(5);
                        tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").AlignCenter().Padding(2).Text(detalle.DetalleCotizacionVariables!.Where(d => d.IdVariablesEconomicasNavigation!.Nombre == "diasRequeridoServicio").First().Valor).FontSize(7).LineHeight(5);
                        tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").AlignCenter().Padding(2).Text(detalle.IdSucursalNavigation.Nombre).FontSize(7).LineHeight(5);
                        tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").AlignRight().Padding(2).Text((detalle.Total / detalle.CantidadServicios).ToString("C2", culture)).FontSize(7).LineHeight(5);
                        tabla.Cell().BorderBottom(0.5f).BorderColor("#D9D9D9").AlignRight().Padding(2).Text(detalle.Total.ToString("C2", culture)).FontSize(7).LineHeight(5);
                    }
                });
            });
        }

        private void setearPropiedadesHora(DetalleCotizacionDTO detalle)
        {
            List<DetalleCotizacionVariableDTO> horaInicioFin = detalle.DetalleCotizacionVariables.Where(detallev => detallev.IdVariablesEconomicasNavigation.Nombre == "minutosInicioServicio" || detallev.IdVariablesEconomicasNavigation.Nombre == "minutosFinServicio").ToList();
            foreach (var detallev in horaInicioFin)
            {
                Type propiedades = typeof(TablaComponent);
                var element = propiedades.GetField(detallev.IdVariablesEconomicasNavigation.Nombre, BindingFlags.NonPublic | BindingFlags.Instance);
                if (element != null)
                {
                    element.SetValue(this, detallev.Valor);
                }
            }
        }

        public string convertirMinutosAHora(string minuto)
        {
            var minutos = float.Parse(minuto);
            //Convertir minutos a horas 
            var horas = Math.Floor(minutos / 60);
            var minutosRestantes = minutos % 60;

            // Formatear las horas y los minutos
            var horasFormateadas = horas < 10 ? "0" + horas: horas.ToString();
            var minutosFormateados = minutosRestantes < 10 ? "0" + minutosRestantes : minutosRestantes.ToString();

            // Retornar el resultado
            return $"{horasFormateadas}:{minutosFormateados}";
        }

        public string RetornarHoras(DetalleCotizacionDTO detalle)
        {
            this.setearPropiedadesHora(detalle);
            string minInicio = this.convertirMinutosAHora(this.minutosInicioServicio);
            string minFinServicio = this.convertirMinutosAHora(this.minutosFinServicio);
            return $"{minInicio} a {minFinServicio}";
        }

        
    }
}
