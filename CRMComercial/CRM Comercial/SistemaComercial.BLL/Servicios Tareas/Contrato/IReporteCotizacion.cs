using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios_Tareas.Contrato
{
    public interface IReporteCotizacion
    {
        public Task<List<CotizacionDTO>> ObtenerReporteCotizaciones(int estado);

        public Task<List<CotizacionDTO>> ObtenerCotizacionesUltimaSemana();
    }
}
