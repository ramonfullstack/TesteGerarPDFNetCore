using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace TesteGeracaoPDF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PDFODSController : ControllerBase
    {
        private readonly ILogger<PDFODSController> _logger;

        public PDFODSController(ILogger<PDFODSController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetPDF(string cliente, string data, string caminho = "")
        {
            try
            {
                var pdfService = new GeracaoPDFService();
                byte[] pdfBytes;
                if (string.IsNullOrEmpty(caminho))
                {
                    pdfBytes = pdfService.GerarPDFODS(cliente, data);
                }
                else
                {
                    pdfBytes = pdfService.GerarPDFODSDeArquivo(caminho);
                }

                return File(pdfBytes, "application/pdf", "meu_arquivo.pdf");
            }
            catch (Exception ex)
            {
                return NoContent();
            }

        }
    }
}
