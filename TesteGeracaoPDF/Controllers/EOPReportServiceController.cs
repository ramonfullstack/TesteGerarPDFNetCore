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
        public async Task<IActionResult> GetPDFAsync()
        {
            // URL da API
            string apiUrl = "https://sqlrpsdev01.am.rabodev.com/ReportServer_RPS_DEV_01?%2fEOP%2fEOP0004&rs:Format=PDF&CodCliente=6785&DataReferencia=2023-11-22&ParcelasAberto=N&ParcelasAbertoLiq=N";

            // Credenciais para autenticação NTLM
            var credentials = new NetworkCredential("santosrs1", "0UA+^~0VO2ywhv", "rabodevam");

            // Configuração do HttpClientHandler com autenticação NTLM
            var handler = new HttpClientHandler()
            {
                Credentials = credentials,
                UseDefaultCredentials = false
            };

            // Criação do HttpClient com o HttpClientHandler configurado
            using (var httpClient = new HttpClient(handler))
            {
                try
                {
                    // Faz a requisição GET para a API
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    // Verifica se a requisição foi bem-sucedida (código de status 200)
                    if (response.IsSuccessStatusCode)
                    {
                        // Se sim, pode processar a resposta aqui
                        Console.WriteLine("Requisição bem-sucedida!");
                        // Exemplo: Ler o conteúdo da resposta
                        string content = await response.Content.ReadAsStringAsync();
                        Console.WriteLine(content);
                    }
                    else
                    {
                        // Se não, trata o erro de acordo com o código de status
                        Console.WriteLine($"Erro: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }


            var pdfBytes = new byte[] { };
            return File(pdfBytes, "application/pdf", "meu_arquivo.pdf");
        }

       

      
    }
}
