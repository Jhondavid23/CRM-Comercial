using System;
using System.Collections.Generic;

namespace SistemaComercial.Model.Models;

public partial class Numerodocumento
{
    public int IdNumeroDocumento { get; set; }

    public int UltimoNumero { get; set; }

    public DateTime? CreatedAt { get; set; }
}
