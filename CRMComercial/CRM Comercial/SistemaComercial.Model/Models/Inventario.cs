using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Inventario
{
    public int IdInventario { get; set; }

    public int? IdCategoriaInventario { get; set; }

    public int? CreatedBy { get; set; }

    public string Nombre { get; set; } = null!;

    public int CantidadDisponible { get; set; }

    public int CantidadAsignada { get; set; }

    public int CantidadTotal { get; set; }

    public bool? Estado { get; set; }

    public decimal? Valor { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UptadedAt { get; set; }

    public virtual Usuario? CreatedByNavigation { get; set; }

    public virtual ICollection<Detallecotizacioninventario> Detallecotizacioninventarios { get; set; } = new List<Detallecotizacioninventario>();

    public virtual Categoriainventario? IdCategoriaInventarioNavigation { get; set; }
}
