using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IDetalleCotizacionService
    {
        public Task<List<DetalleCotizacionDTO>> ListarDetallesCotizacion(int id);
        public Task<bool> ListarDetalleCotizacion(int id);
        public Task<DetalleCotizacionDTO> CrearDetalleCotizacion(DetalleCotizacionDTO detalleCotizacion);
        public Task<DetalleCotizacionDTO> EditarDetalleCotizacion(DetalleCotizacionDTO detalleCotizacion);
        public Task<DetalleCotizacionDTO> EliminarDetalleCotizacion(int id);
    }
}
