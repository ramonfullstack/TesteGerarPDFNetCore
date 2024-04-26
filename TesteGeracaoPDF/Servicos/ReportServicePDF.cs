using System.Net.Http;
using System;
using System.Threading.Tasks;


namespace TesteGeracaoPDF.Servicos
{
    public static class ReportServicePDF
    {
        public static async Task GerarReportServiceAsync(
            string reportName = "EOP/EOP0004", 
            string format = "PDF", 
            string codCliente = "1015699", 
            string dataReferencia = "2024-04-24", 
            string parcelasAberto = "N", 
            string parcelasAbertoLiq = "N")
        {
            // Parâmetros da URL
            //string reportName = "EOP/EOP0004";
            //string format = "PDF";
            //string dataReferencia = "2024-02-23";
            //string parcelasAberto = "N";
            //string parcelasAbertoLiq = "N";6
            
            string apiUrl = $"https://sqlrpsdev01.am.rabodev.com/ReportServer_RPS_DEV_01/Pages/ReportViewer.aspx?%2f{reportName}&rs:Format={format}&CodCliente={codCliente}&DataReferencia={dataReferencia}&ParcelasAberto={parcelasAberto}&ParcelasAbertoLiq={parcelasAbertoLiq}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Faz a chamada GET
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    // Verifica se a resposta é bem-sucedida
                    if (response.IsSuccessStatusCode)
                    {
                        // Lê o conteúdo da resposta como uma string
                        string content = await response.Content.ReadAsStringAsync();

                        // Aqui você pode manipular o conteúdo da resposta conforme necessário
                        Console.WriteLine(content);
                    }
                    else
                    {
                        Console.WriteLine("A solicitação falhou com o código de status: " + response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ocorreu um erro ao fazer a solicitação: " + ex.Message);
                }
            }
        }
    }
}
