﻿using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TesteGeracaoPDF.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PdfController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PdfController> _logger;

        public PdfController(ILogger<PdfController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string GetPDF()
        {
            try
            {
                Document doc = new Document(PageSize.A4);
                doc.SetMargins(40, 40, 40, 80);
                doc.AddCreationDate();

                //caminho onde sera criado o pdf + nome desejado
                //OBS: o nome sempre deve ser terminado com .pdf
                string caminho = @"C:\temp\teste.pdf";

                PdfWriter writer = PdfWriter.GetInstance(doc, new
                FileStream(caminho, FileMode.Create));

                doc.Open();

                //criando uma string vazia
                string dados = "";

                //criando a variavel para paragrafo
                Paragraph paragrafo = new Paragraph(dados,
                new Font(Font.NORMAL, 14));
                //etipulando o alinhamneto
                paragrafo.Alignment = Element.ALIGN_JUSTIFIED;
                //Alinhamento Justificado
                //adicioando texto
                paragrafo.Add("TESTE TESTE TESTE");
                //acidionado paragrafo ao documento
                doc.Add(paragrafo);
                //fechando documento para que seja salva as alteraçoes.
                doc.Close();

                return "Pdf gerado com sucesso";
            }
            catch (Exception ex)
            {

                return "Erro na geração de pdf: " + $"Erro: {ex.GetBaseException().Message}";
            }
            
        }
    }
}
