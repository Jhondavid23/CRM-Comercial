using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SistemaComercial.DTO;
using SistemaComercial.Model;

namespace SistemaComercial.BLL.Reportes.Servicios.Contrato
{
    public interface IFormatCotizacionPDFService
    {
        public byte[] GenerateCotizacionPDF(CotizacionDTO cotizacion);
    }
}
