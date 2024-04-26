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

        //[HttpGet("GetPDFDefault")]
        //public string GetPDFDefault(string cliente, string data)
        //{
        //    try
        //    {
        //        Document doc = new Document(PageSize.A4);
        //        doc.SetMargins(40, 40, 40, 80);
        //        doc.AddCreationDate();

        //        //caminho onde sera criado o pdf + nome desejado
        //        //OBS: o nome sempre deve ser terminado com .pdf
        //        string caminho = @"C:\temp\teste.pdf";

        //        PdfWriter writer = PdfWriter.GetInstance(doc, new
        //        FileStream(caminho, FileMode.Create));
        //        doc.Open();

        //        //criando uma string vazia
        //        string dados = "";

        //        //criando a variavel para paragrafo
        //        Paragraph paragrafo = new Paragraph(dados,
        //        new Font(Font.NORMAL, 14));
               
        //        paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
           
        //        paragrafo.Add("TESTE TESTE TESTE");
        //        //acidionado paragrafo ao documento
        //        doc.Add(paragrafo);
        //        //fechando documento para que seja salva as alteraçoes.
        //        doc.Close();

        //        return $"PDF GERADO COM SUCESSO EM {caminho}";
        //    }
        //    catch (Exception ex)
        //    {
        //        return $"Erro na geração de pdf Erro: {ex.GetBaseException().Message}";
        //    }
            
        //}

      
    }
}
