using SistemaComercial.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.BLL.Servicios.Contrato
{
    public interface IMotivoService
    {
        public Task<List<MotivoDTO>> ListarMotivos();
        public Task<MotivoDTO> CrearMotivo(MotivoDTO inventario);
        public Task<bool> EditarMotivo(MotivoDTO inventario);
        public Task<bool> EliminarMotivo(int id);
    }
}
