using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Detallecotizacionvariable
{
    public int IdDetalleCotizacionVariables { get; set; }

    public int? IdDetalleCotizacion { get; set; }

    public int? IdVariablesEconomicas { get; set; }

    public long? Valor { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Detallecotizacion? IdDetalleCotizacionNavigation { get; set; }

    public virtual Variableseconomica? IdVariablesEconomicasNavigation { get; set; }
}
