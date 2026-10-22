using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaComercial.Model
{
    public partial class Motivo
    {
        public int IdMotivo { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }
        public virtual ICollection<Cotizacion> Cotizacions { get; set; } = new List<Cotizacion>();
    }
}
