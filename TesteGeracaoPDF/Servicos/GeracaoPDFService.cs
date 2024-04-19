using iTextSharp.text;
using iTextSharp.text.pdf;
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
    }
}




