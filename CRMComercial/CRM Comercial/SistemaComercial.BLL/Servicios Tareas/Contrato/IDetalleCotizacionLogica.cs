using AutoMapper;
using SistemaComercial.BLL.Servicios.Contrato;
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
    public interface IDetalleCotizacionLogica
    {
        public Task<CotizacionDTO> ActualizarTotal(int idCotizacion);
        public Task<DetalleCotizacionDTO> CreateDetalleCotizacion(DetalleCotizacionDTO detalleCotizacion);
        public Task<DetalleCotizacionDTO> EditarDetalleCotizacion(DetalleCotizacionDTO detalle);
        public Task<bool> EliminarDetalleCotizacionLogic(int idDetalleCotizacion);
    }
}
