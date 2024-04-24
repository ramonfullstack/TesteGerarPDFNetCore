using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace TesteGeracaoPDF
{
    public class GeracaoPDFService
    {
        public byte[] GerarPDF()
        {
            // Criar um novo documento PDF
            Document doc = new Document();
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.Open();
            doc.Add(new Paragraph("Conteúdo do PDF"));

            doc.Close();
            writer.Close();

            return ms.ToArray();
        }

        public byte[] GerarPDFODS(string cod_cliente, string data)
        {
            // Criar um novo documento PDF
            Document doc = new Document();
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(doc, ms);

            doc.Open();
            doc.Add(new Paragraph("PDF da ODS"));
            doc.Add(new Paragraph($"Código cliente: {cod_cliente}"));
            doc.Add(new Paragraph($"Conteúdo do PDF: {data}"));

            doc.Close();
            writer.Close();

            return ms.ToArray();
        }

        public byte[] GerarPDFODSDeArquivo(string caminho_arquivo)
        {
            // Verifica se o arquivo existe
            if (!File.Exists(caminho_arquivo))
            {
                throw new FileNotFoundException("O arquivo PDF não foi encontrado.", caminho_arquivo);
            }

            byte[] pdfBytes;

            using (MemoryStream ms = new MemoryStream())
            {
               
                try
                {
                    Document doc = new();
                    PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    // Abra o arquivo PDF existente
                    using (PdfReader reader = new PdfReader(caminho_arquivo))
                    {
                        for (int i = 1; i <= reader.NumberOfPages; i++)
                        {
                            doc.NewPage();
                            PdfImportedPage page = writer.GetImportedPage(reader, i);
                            writer.DirectContent.AddTemplate(page, 0, 0);
                        }
                    } 
                    // O arquivo PDF será fechado automaticamente aqui
                }
                catch (Exception ex)
                {
                    // Trate qualquer exceção que possa ocorrer durante o processamento
                    Console.WriteLine($"Erro ao processar o arquivo PDF: {ex.Message}");
                    throw;
                }

                // Convertendo o MemoryStream para um array de bytes
                pdfBytes = ms.ToArray();

                SalvarPDF(pdfBytes, caminho_arquivo);
                
            }

            return pdfBytes;
        }

        public void SalvarPDF(byte[] pdfBytes, string caminho_destino)
        {
            caminho_destino = Path.GetDirectoryName(caminho_destino);
            // Salva o array de bytes como um arquivo PDF no caminho de destino
            using (FileStream fs = new FileStream(caminho_destino, FileMode.Create))
            {
                fs.Write(pdfBytes, 0, pdfBytes.Length);
            }
        }

    }
}




