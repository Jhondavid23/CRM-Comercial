using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Variableseconomica
{
    public int IdVariablesEconomicas { get; set; }

    public int? CreatedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Valor { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UptatedAt { get; set; }

    public virtual Usuario? CreatedByNavigation { get; set; }

    public virtual ICollection<Detallecotizacionvariable> Detallecotizacionvariables { get; set; } = new List<Detallecotizacionvariable>();

    public virtual Usuario? UpdatedByNavigation { get; set; }
}
