using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using SistemaComercial.BLL.Servicios.Contrato;
using SistemaComercial.BLL.Servicios_Tareas.Contrato;
using SistemaComercial.DAL;
using SistemaComercial.DAL.Repositorios.Contratos;
using SistemaComercial.DTO;
using SistemaComercial.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios_Tareas
{
    public class DetalleCotizacionLogicaService : IDetalleCotizacionLogica
    {
        public ICotizacionService _cotizacionService;
        public DbcomercialContext _dbComercialContext;
        public IDetalleCotizacionService _detalleCotizacionService;
        public IVariablesEconomicasService _variablesEconomicasService;
        public IDetalleCotizacionVariableService _detalleCotizacionVariableService;
        public IMapper _mapper;
        public ICotizacionLogica _cotizacionLogica;


        public DetalleCotizacionLogicaService(
            DbcomercialContext dbcomercialContext,
            ICotizacionService cotizacionService,
            IVariablesEconomicasService variablesEconomicasService,
            IDetalleCotizacionVariableService detalleCotizacionVariableService,
            IMapper mapper,
            IGenericRepository<DetalleCotizacion> _detalleCotizacionRepository,
            IDetalleCotizacionService detalleCotizacionService,
            ICotizacionLogica cotizacionLogica
            )
        {
            _dbComercialContext = dbcomercialContext;
            _variablesEconomicasService = variablesEconomicasService;
            _detalleCotizacionVariableService = detalleCotizacionVariableService;
            _variablesEconomicasService = variablesEconomicasService;
            _detalleCotizacionService = detalleCotizacionService;
            _cotizacionService = cotizacionService;
            _cotizacionLogica = cotizacionLogica;
            _mapper = mapper;
        }

        public async Task<CotizacionDTO> ActualizarTotal(int idCotizacion)
        {
            try
            {
                CotizacionDTO cotizacion = await this._cotizacionService.ListarCotizacion(idCotizacion);
                if (cotizacion == null) { throw new TaskCanceledException("Cotización no encontrada"); }
                List<DetalleCotizacionDTO> detalles = await _detalleCotizacionService.ListarDetallesCotizacion(idCotizacion);
                var ivaLocal = (int)await _variablesEconomicasService.ListarVariable("iva");
                decimal ivaPlano = (decimal)ivaLocal / 100;
                decimal totalDetalles = 0;
                decimal totalIva = 0;
                foreach (DetalleCotizacionDTO detalle in detalles)
                {
                    totalDetalles += detalle.Total;
                }
                decimal diezPorcientoTotal = totalDetalles * (decimal)0.1;
                decimal valorEnPesosImpuestos = diezPorcientoTotal * ivaPlano;
                cotizacion.ValorIva = valorEnPesosImpuestos;
                decimal total = totalDetalles + valorEnPesosImpuestos;
                decimal totalRedondeada = Math.Ceiling(total);
                cotizacion.Total = totalRedondeada;
                CotizacionDTO cotizacionEditada = await this._cotizacionService.EditarCotizacion(cotizacion, true);
                return cotizacionEditada;

            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleCotizacionDTO> CreateDetalleCotizacion(DetalleCotizacionDTO detalleCotizacion)
        {
            try
            {
                // Validar si existe
                bool listaDetalles = await  this._detalleCotizacionService.ListarDetalleCotizacion(detalleCotizacion.IdDetalleCotizacion);

                //En caso de que exista actualizarlo
                //if (listaDetalles)
                //{
                //    DetalleCotizacionDTO detalleEditado = await this.EditarDetalleCotizacion(detalleCotizacion);
                //    return detalleEditado;
                //}

                // En caso que no exista crearla y actualizar el total
                if (!listaDetalles)
                {
                    var valorDetalle = this._cotizacionLogica.CalculoDetalleCotizacion(detalleCotizacion);

                    detalleCotizacion.Total = valorDetalle.Result;
                
                    DetalleCotizacionDTO detalleCotizacionCreada = await _detalleCotizacionService.CrearDetalleCotizacion(detalleCotizacion);
                
                     // Crear el detalle cotización
                    CotizacionDTO cotizacionActualizado = await this.ActualizarTotal(detalleCotizacion.IdCotizacion);
                    return detalleCotizacionCreada;
                }
                return detalleCotizacion;
            }
            catch
            {
                throw;
            }
        }

        public async Task<DetalleCotizacionDTO> EditarDetalleCotizacion(DetalleCotizacionDTO detalle)
        {
            DetalleCotizacionDTO detalleEditado = new DetalleCotizacionDTO();
            using (var transaction = _dbComercialContext.Database.BeginTransaction())
            {
                try
                {
                    var valorDetalle = this._cotizacionLogica.CalculoDetalleCotizacion(detalle);
                   
                    if (detalle.Total == 0)
                    {
                        throw new TaskCanceledException("El detalle cotización no cuenta con un valor válido");
                    }
                    detalle.Total = valorDetalle.Result;
                    // Editar Detalle cotización
                    detalleEditado = await this._detalleCotizacionService.EditarDetalleCotizacion(detalle);

                    //En vez de elimiar, editar los detallesVariablesEConomicos por medio del id
                    // Eliminar detalle variables 
                    bool detalleVariables = await _detalleCotizacionVariableService.EliminarDetallesVariables(detalle.IdDetalleCotizacion);
                    //Crear detalle variables
                    foreach (DetalleCotizacionVariableDTO detalleVariable in detalle.DetalleCotizacionVariables)
                    {
                        detalleVariable.IdDetalleCotizacionVariables = 0;
                        detalleVariable.IdDetalleCotizacion = detalle.IdDetalleCotizacion;
                        DetalleCotizacionVariableDTO detalleCotizacion = await _detalleCotizacionVariableService.CrearDetalleVariable(detalleVariable);
                    }
                    CotizacionDTO cotizacionActualizado = await this.ActualizarTotal(detalle.IdCotizacion);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback(); 
                    throw;
                }
                    
                return detalleEditado;

            }
        }

        public async Task<bool> EliminarDetalleCotizacionLogic(int id)
        {
            try
            {
                DetalleCotizacionDTO detalleCotizacionEliminado = await this._detalleCotizacionService.EliminarDetalleCotizacion(id);
                CotizacionDTO cotizacionActualizado = await this.ActualizarTotal(detalleCotizacionEliminado.IdCotizacion);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
