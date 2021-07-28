using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Cotizacion.Moneda.Test
{
    public class MonedaControllerTest : IClassFixture<ClientProvider<Startup>>
    {
        private HttpClient Client;
        
        // Constructor 
        public MonedaControllerTest(ClientProvider<Startup> fixture)
        {
            Client = fixture.Client;
        }
        
        [Fact]
        public async Task Get_Cotizar_Moneda_Dolar_Test()
        {
            // Arrange
            var request = new
            {
                Url = "/cotizar/DOLAR"
            };
            
            // Act
            var response = await Client.GetAsync(request.Url);
            
            // Assert
            response.EnsureSuccessStatusCode();
            //Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        
        [Fact]
        public async Task Get_Cotizar_Moneda_Dolar_Assertion_Test()
        {
            // Arrange
            var request = new
            {
                Url = "/cotizar/REAL"
            };

            // Act 
            var response = await Client.GetAsync(request.Url);
            
            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_Cotizar_Moneda_Dolar_Assertion_Test()
        {
            // Arrange
            var request = new
            {
                Url = "/api/Moneda/ComprarMoneda",
                CompraMoneda = new 
                {
                    IdUsuario = "200",
                    MontoComprar = 4000.00m,
                    TipoMoneda = "DOLAR",
                    FechaCompra = DateTime.Now
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url,ContentHelper.GetStringContent(request.CompraMoneda));
                
            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
