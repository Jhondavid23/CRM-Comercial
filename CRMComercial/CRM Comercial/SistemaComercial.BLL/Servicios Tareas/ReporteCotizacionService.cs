using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SistemaComercial.BLL.Servicios_Tareas.Contrato;
using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios_Tareas
{
    public class ReporteCotizacionService : IReporteCotizacion
    {
        private readonly IGenericRepository<Cotizacion> _cotizacionRepository;
        private readonly IMapper _mapper;

        private ReporteCotizacionService(IGenericRepository<Cotizacion> cotizacionRepository, IMapper mapper)
        {
            _cotizacionRepository = cotizacionRepository;
            _mapper = mapper;
        }

        public async Task<List<CotizacionDTO>> ObtenerReporteCotizaciones(int estado)
        {
            try
            {
                IQueryable<Cotizacion> cotizacionesGanadas = await _cotizacionRepository.Consultar(c => c.Estado == estado) ?? throw new TaskCanceledException("No hay cotizaciones ganadas");
                return _mapper.Map<List<CotizacionDTO>>(cotizacionesGanadas);
            }
            catch
            {
                throw;
            }
        }

        private IQueryable<Cotizacion> ObtenerReporteVentasPorFecha(IQueryable<Cotizacion> tablaCotizacion, int restarCantidadDias)
        {
            try
            {
                DateTime? ultimaFecha = tablaCotizacion.OrderByDescending(v => v.CreatedAt).Select(v => v.CreatedAt).First();
                ultimaFecha = ultimaFecha.Value.AddDays(restarCantidadDias);
                return tablaCotizacion.Where(v => v.CreatedAt.Value.Date >= ultimaFecha.Value.Date);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CotizacionDTO>> ObtenerReporteVentasPorFecha(DateTime fechaInicial, DateTime fechaFinal)
        {
            try
            {
                IQueryable<Cotizacion> cotizaciones = await _cotizacionRepository.Consultar();
                IQueryable<Cotizacion> cotizacionFiltrada = cotizaciones.Where(c => c.CreatedAt.Value.Date >= fechaInicial.Date && c.CreatedAt.Value.Date <= fechaFinal.Date);
                return _mapper.Map<List<CotizacionDTO>>(cotizacionFiltrada);
            }
            catch
            {
                throw;
            }
        }

        private async Task<int> TotalCotizacionesUltimaSemana()
        {
            try
            {
                int total = 0;
                IQueryable<Cotizacion> cotizaciones = await _cotizacionRepository.Consultar();
                if(cotizaciones.Count() > 0)
                {
                    this.ObtenerReporteVentasPorFecha(cotizaciones, -7);
                    total = cotizaciones.Count();
                }
                return total;
            }
            catch
            {
                throw;
            }
        }

        private async Task<string> TotalIngresosUltimaSemana()
        {
            try
            {
                decimal resultado = 0;
                IQueryable<Cotizacion> _cotizacionQuery = await _cotizacionRepository.Consultar();

                if(_cotizacionQuery.Count() > 0)
                {
                    var tablaVenta = this.ObtenerReporteVentasPorFecha(_cotizacionQuery, -7);
                    resultado = tablaVenta.Select(c => c.Total).Sum(c => c.Value);
                }
                return Convert.ToString(resultado, new CultureInfo("es-COL"));
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<CotizacionDTO>> ObtenerCotizacionesUltimaSemana()
        {
            try
            {
                IQueryable<Cotizacion> resultado;
                IQueryable<Cotizacion> cotizaciones = await _cotizacionRepository.Consultar();
                if (cotizaciones.Count() > 0)
                {
                    resultado = this.ObtenerReporteVentasPorFecha(cotizaciones, -7);
                    return _mapper.Map<List<CotizacionDTO>>(resultado);
                   
                }
                return _mapper.Map<List<CotizacionDTO>>(cotizaciones);
            }
            catch
            {
                throw;
            }
        }


        public async Task<DashboardDTO> Resumen()
        {
            DashboardDTO vmDahsBoard = new DashboardDTO();
            try
            {
                vmDahsBoard.TotalCotizaciones = await TotalCotizacionesUltimaSemana();
                vmDahsBoard.TotalIngresos = await TotalIngresosUltimaSemana();
                vmDahsBoard.CotizacionesUltimaSemana = await ObtenerCotizacionesUltimaSemana();
                
            }
            catch
            {
                throw;
            }
            return vmDahsBoard;
        }
    }
}
