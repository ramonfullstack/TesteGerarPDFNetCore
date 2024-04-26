using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TesteGeracaoPDF.Servicos;

namespace TesteGeracaoPDF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        private readonly ILogger<PdfController> _logger;

        public PdfController(ILogger<PdfController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPDFAsync(string reportName = "EOP/EOP0004",
            string format = "PDF",
            string codCliente = "1015699",
            string dataReferencia = "2024-04-24",
            string parcelasAberto = "N",
            string parcelasAbertoLiq = "N")
        { 
            var pdfService = new GeracaoPDFService();

            byte[] pdfBytes = await pdfService.GerarPDFReportServiceAsync(
                reportName, 
                format, 
                codCliente, 
                dataReferencia, 
                parcelasAberto, 
                parcelasAbertoLiq);

            return File(pdfBytes, "application/pdf", "meu_arquivo.pdf");
        }

       

      
    }
}
