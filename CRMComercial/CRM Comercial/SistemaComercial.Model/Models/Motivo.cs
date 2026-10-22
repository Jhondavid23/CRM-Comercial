using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Motivo
{
    public int IdMotivo { get; set; }

    public string? Nombre { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Cotizacion> Cotizacions { get; set; } = new List<Cotizacion>();
}
