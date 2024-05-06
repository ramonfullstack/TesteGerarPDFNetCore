using System;

namespace TesteGeracaoPDF.Models
{
    public class GetPdfModel
    {
        public string CodigoCliente { get; set; } = "6785";
        public string dataReferencia { get; set; } = DateTime.Now.ToString();
        public string ParcelasAberto { get; set; } = "N";
        public string parcelasAbertoLiq { get; set; } = "N";
        public string CapaDaCarta { get; set; } = "N";
        public string TemOperacoes { get; set; } = "N";
    }
}
