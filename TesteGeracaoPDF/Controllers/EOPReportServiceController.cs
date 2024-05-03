using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using TesteGeracaoPDF.Servicos;
using System.Text;

namespace TesteGeracaoPDF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EOPReportServiceController : ControllerBase
    {
        private readonly ILogger<EOPReportServiceController> _logger;

        public EOPReportServiceController(ILogger<EOPReportServiceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetPDFAsync(
            string codCliente = "6785",
            string dataReferencia = "2023-11-22",
            string parcelasAberto = "N",
            string parcelasAbertoLiq = "N",
            string capaDaCarta = "N",
            string temOperacoes = "N")
        {
            try
            {
                string relatorioReport;
                HttpClientHandler handler = GenerateHandlerAuth();
                if (capaDaCarta.Equals("S"))
                    if (temOperacoes.Equals("S"))
                        relatorioReport = "EOP00040";
                    else
                        relatorioReport = "EOP00039";
                else
                    relatorioReport = "EOP0004";

                string apiUrl = $"https://sqlrpsdev01.am.rabodev.com/ReportServer_RPS_DEV_01?%2fEOP%2f{relatorioReport}&rs:Format=PDF";
                apiUrl += $"&CodCliente={codCliente}&DataReferencia={dataReferencia}&ParcelasAberto={parcelasAberto}&ParcelasAbertoLiq={parcelasAbertoLiq}";

                //string apiUrl = "https://sqlrpsdev01.am.rabodev.com/ReportServer_RPS_DEV_01?%2fEOP%2fEOP0004&rs:Format=PDF&CodCliente=6785&DataReferencia=2023-11-22&ParcelasAberto=N&ParcelasAbertoLiq=N";


                string contentResponse = string.Empty;

                using (var httpClient = new HttpClient(handler))
                {

                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Requisição bem-sucedida!");

                        var pdfStream = await response.Content.ReadAsStreamAsync();
                        return new FileStreamResult(pdfStream, "application/pdf");

                        //var filePath = Path.Combine(Path.GetTempPath(), "EOP_ReportService.pdf");
                        //using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                        //{
                        //    await pdfStream.CopyToAsync(fileStream);
                        //}

                        //return File(filePath, "application/pdf", "EOP_ReportService.pdf");
                    }
                    else
                    {
                        // Se não, trata o erro de acordo com o código de status
                        Console.WriteLine($"Erro: {response.StatusCode}");
                        return StatusCode((int)response.StatusCode);
                    }
                }

                //var pdfBytes = Encoding.UTF8.GetBytes(contentResponse);
                //return File(pdfBytes, "application/pdf", "EOP_ReportService.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
                return StatusCode(500); // Internal Server Error
            }
        }

        private static HttpClientHandler GenerateHandlerAuth()
        {
            var credentials = new NetworkCredential("santosrs1", "0UA+^~0VO2ywhv", "rabodevam");

            var handler = new HttpClientHandler()
            {
                Credentials = credentials,
                UseDefaultCredentials = false
            };
            return handler;
        }
    }
}
