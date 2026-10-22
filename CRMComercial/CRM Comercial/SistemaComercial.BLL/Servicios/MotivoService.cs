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

namespace SistemaComercial.BLL.Servicios
{
    public class MotivoService : IMotivoService
    {
        private readonly IGenericRepository<Motivo> _motivoRepositorio;
        private readonly IMapper _mapper;

        public MotivoService(IGenericRepository<Motivo> motivoRepositorio, IMapper mapper)
        {
            _mapper = mapper;
            _motivoRepositorio = motivoRepositorio;
        }

        public async Task<List<MotivoDTO>> ListarMotivos()
        {
            try
            {
                var listarMotivos = await _motivoRepositorio.Consultar();
                return _mapper.Map<List<MotivoDTO>>(listarMotivos);
            }
            catch
            {
                throw;
            }
        }
        public async Task<MotivoDTO> CrearMotivo(MotivoDTO motivo)
        {
            try
            {
                var motivoCreado = await _motivoRepositorio.Crear(_mapper.Map<Motivo>(motivo));
                return _mapper.Map<MotivoDTO>(motivoCreado);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EditarMotivo(MotivoDTO motivo)
        {
            try
            {

                var motivoEditado = await _motivoRepositorio.Editar(_mapper.Map<Motivo>(motivo));
                return motivoEditado;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> EliminarMotivo(int id)
        {
            try
            {
                var motivo = await _motivoRepositorio.Obtener(m => m.IdMotivo == id);
                var motivoEliminado = await _motivoRepositorio.Eliminar(motivo);
                return motivoEliminado;

            }
            catch
            {
                throw;
            }
        }

        
    }
}
