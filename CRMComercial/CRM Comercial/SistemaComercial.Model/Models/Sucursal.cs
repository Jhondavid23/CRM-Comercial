using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public int? IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Detallecotizacion> Detallecotizacions { get; set; } = new List<Detallecotizacion>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
