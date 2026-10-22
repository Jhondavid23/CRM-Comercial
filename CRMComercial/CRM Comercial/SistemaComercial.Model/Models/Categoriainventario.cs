using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Categoriainventario
{
    public int IdCategoria { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
