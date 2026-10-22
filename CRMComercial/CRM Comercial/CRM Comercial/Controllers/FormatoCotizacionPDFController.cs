using Microsoft.AspNetCore.Mvc;
using SistemaComercial.BLL.Reportes.Servicios.Contrato;
using SistemaComercial.DTO;
using SistemaComercial.Utility;

namespace CRM_Comercial.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormatoCotizacionPDFController : ControllerBase
    {
        private readonly IFormatCotizacionPDFService formatoCotizacion;
        private readonly IWebHostEnvironment _host;
        public FormatoCotizacionPDFController(IFormatCotizacionPDFService _formatoCotizacion, IWebHostEnvironment host) 
        {
            formatoCotizacion = _formatoCotizacion;
            _host = host;
        }

        [HttpPost]
        public IActionResult ObtenerCotizacionPDF([FromBody] CotizacionDTO cotizacion)
        {
            Response response = new Response();
            try
            {
                byte[] formato =  formatoCotizacion.GenerateCotizacionPDF(cotizacion);
                return File(formato, contentType:"cotizacion/pdf", "cotizacion.pdf");
            }
            catch (Exception ex) 
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }


}
