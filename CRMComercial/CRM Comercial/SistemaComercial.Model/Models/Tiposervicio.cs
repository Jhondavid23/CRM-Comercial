using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Tiposervicio
{
    public int IdTipoServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Formula { get; set; }

    public string? Descripcion { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
