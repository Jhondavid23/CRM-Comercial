using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Detallecotizacion
{
    public int IdDetalleCotizacion { get; set; }

    public int IdCotizacion { get; set; }

    public int IdServicio { get; set; }

    public int IdSucursal { get; set; }

    public int? CantidadServicios { get; set; }

    public string? DetalleServicio { get; set; }

    public decimal Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Detallecotizacioninventario> Detallecotizacioninventarios { get; set; } = new List<Detallecotizacioninventario>();

    public virtual ICollection<Detallecotizacionvariable> Detallecotizacionvariables { get; set; } = new List<Detallecotizacionvariable>();

    public virtual Cotizacion IdCotizacionNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
