using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;
namespace logico.paquete_1
{
	public class Convertidor
	{
        private const string BcrUrl = "https://www.bccr.fi.cr/";
        private const string UsdToCrcXPath = "//label[@id='D317']";
        private double tipoCambio { get; set; }

        public Convertidor()
		{
            obtenerTipoCambio();
		}

		public async void obtenerTipoCambio()
		{
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync(BcrUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var htmlDocument = new HtmlDocument();
                        htmlDocument.LoadHtml(content);

                        var usdToCrcNode = htmlDocument.DocumentNode.SelectSingleNode(UsdToCrcXPath);
                        if (usdToCrcNode != null)
                        {
                            var exchangeRateText = usdToCrcNode.InnerText.Trim();
                            exchangeRateText = exchangeRateText.Replace("₡", "").Trim();
                            exchangeRateText = exchangeRateText.Replace(",", ".").Trim();
                            Console.WriteLine(exchangeRateText);
                            if (double.TryParse(exchangeRateText, NumberStyles.Any, CultureInfo.InvariantCulture, out double exchangeRate))
                            {
                                Console.WriteLine("El exchange es: " + exchangeRate);
                               
                                tipoCambio = exchangeRate;
                                return;


                            }
                            else
                            {
                                Console.WriteLine("No se pudo convertir el tipo de cambio a double.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontró el nodo del tipo de cambio en la página.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Error al obtener el tipo de cambio: {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener el tipo de cambio: {ex.Message}");
                }
            }

            tipoCambio= 0;
        }



		public double realizarTipoCambio(double monto)
		{
            Console.WriteLine(tipoCambio);
			return monto / tipoCambio ;
		}
	}
}

