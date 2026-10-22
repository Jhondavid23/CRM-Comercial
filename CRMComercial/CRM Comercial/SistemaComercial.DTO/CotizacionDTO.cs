using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.DTO
{
    public class CotizacionDTO
    {
        public int IdCotizacion { get; set; }

        [Required]
        public int? IdCliente { get; set; }

        [Required]
        public int? IdUsuario { get; set; }

        [Required]
        public string? Nombre { get; set; }

        public int? EditadoPor { get; set; }

        public string? Descripcion { get; set; }

        public int? Estado { get; set; }

        public decimal? ValorIva { get; set; }

        public decimal? Total { get; set; }

        public string? CreatedAt { get; set; }

        public string? UpdatedAt { get; set; }

        public string? NumeroDocumento { get; set; }

        public DateTime? Expiracion { get; set; }

        public string?  Comentarios { get; set; }

        [Required]
        public virtual ICollection<DetalleCotizacionDTO> DetalleCotizacions { get; set; } = new List<DetalleCotizacionDTO>();

        public virtual ClienteDTO? IdClienteNavigation { get; set; }

        public virtual UsuarioDTO? IdUsuarioNavigation { get; set; }
    }
}
