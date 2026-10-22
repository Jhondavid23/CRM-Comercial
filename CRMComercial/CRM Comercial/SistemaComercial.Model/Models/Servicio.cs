using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public int? TipoServicio { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Detallecotizacion> Detallecotizacions { get; set; } = new List<Detallecotizacion>();

    public virtual Tiposervicio? TipoServicioNavigation { get; set; }
}
