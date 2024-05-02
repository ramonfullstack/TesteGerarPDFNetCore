using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Net.Http.Headers;


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
         
            // Configurações de autenticação
            string username = "rabodevam\\SantosRS1";
            string password = "0UA+^~0VO2ywhv";
            //string domain = "seu_domínio"; // Se estiver usando autenticação Windowstffg

            // Criar uma instância do HttpClient
            using (var httpClient = new HttpClient())
            {
                // Configurar a autenticação (se necessário)
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var byteArray = System.Text.Encoding.ASCII.GetBytes($"{username}:{password}");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                }

                // Configurar o endpoint do serviço de relatório
                string reportUrl = "https://sqlrpsdev01.am.rabodev.com/ReportServer_RPS_DEV_01/Pages/ReportViewer.aspx?%2fEOP%2fEOP0004&rs:Format=PDF&CodCliente=1015699&DataReferencia=2024-02-23&ParcelasAberto=N&ParcelasAbertoLiq=N";

                try
                {
                    // Fazer a solicitação HTTP GET para obter o relatório
                    var resposta = await httpClient.GetAsync(reportUrl);

                    // Verificar se a solicitação foi bem-sucedida (código de status 200)
                    if (resposta.IsSuccessStatusCode)
                    {
                        // Ler e processar o conteúdo do relatório (se necessário)
                        var conteudoRelatorio = await resposta.Content.ReadAsStringAsync();
                        Console.WriteLine("Conteúdo do relatório:");
                        Console.WriteLine(conteudoRelatorio);
                    }
                    else
                    {
                        // Se a solicitação não foi bem-sucedida, exibir o código de status
                        Console.WriteLine("Erro ao chamar o serviço de relatório. Código de status: " + resposta.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    // Lidar com exceções
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                }
            }
        }


        public static async Task ChamandoApi()
        {
            string apiUrl = $"https://example.com/api";
            // Criar uma instância do HttpClient
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var resposta = await httpClient.GetAsync(apiUrl);

                    // Verificar se a solicitação foi bem-sucedida (código de status 200)
                    if (resposta.IsSuccessStatusCode)
                        // Ler e processar o conteúdo do relatório (se necessário)
                        Console.WriteLine(resposta.Content.ReadAsStringAsync());
                    else
                        // Se a solicitação não foi bem-sucedida, exibir o código de status
                        Console.WriteLine("Erro ao chamar o serviço de relatório. Código de status: " + resposta.StatusCode);
                }
                catch (Exception ex)
                {
                    // Lidar com exceções
                    Console.WriteLine("Ocorreu um erro: " + ex.Message);
                }
            }
        }
    }
}
