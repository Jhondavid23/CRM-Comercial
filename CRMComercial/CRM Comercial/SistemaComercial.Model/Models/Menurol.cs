using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Menurol
{
    public int IdMenuRol { get; set; }

    public int? IdMenu { get; set; }

    public int? IdRol { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UptadeAt { get; set; }

    public virtual Menu? IdMenuNavigation { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
