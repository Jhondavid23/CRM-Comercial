using System;
using System.Collections.Generic;

namespace SistemaComercial.Model;

public partial class Cotizacion
{
    public int IdCotizacion { get; set; }

    public int? IdCliente { get; set; }

    public int? IdUsuario { get; set; }

    public int? IdMotivo { get; set; }

    public string? Nombre { get; set; }

    public int? EditadoPor { get; set; }

    public int? Estado { get; set; }

    public string? Descripcion { get; set; }

    public decimal? Total { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? NumeroDocumento { get; set; }

    public decimal? ValorIva { get; set; }

    public DateTime? Expiracion { get; set; }

    public string? Comentarios { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual ICollection<DetalleCotizacion> DetalleCotizacions { get; set; } = new List<DetalleCotizacion>();

    public virtual Usuario? EditadoPorNavigation { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Motivo? IdMotivoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
