using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cotizacion.Moneda.Util
{
    public static class MonedaServicio
    {
        // url
        static string URL_COTIZACION = "https://www.bancoprovincia.com.ar/Principal/Dolar";
        
        /// <summary>
        /// Metodo para Cortizar el Dolar 
        /// </summary>
        /// <returns>List<string></returns>
        public static async Task<List<string>> CotizarMoneda()
        {
            using var httClient = new HttpClient();
            using var response = await httClient.GetAsync(URL_COTIZACION);
            var apiResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<string>>(apiResponse);
        }
    }
}
