using BibliotecaAPI;
using Core;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Biblioteca.Tests.Controllers.Integration
{
    public class EditoraControllerTest
    {
        private readonly HttpClient _httpClientTest;

        public EditoraControllerTest()
        {
            var server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(BibliotecaContext));
                    services.AddDbContext<BibliotecaContext>(options => options.UseInMemoryDatabase("Biblioteca"));
                })
                );

            _httpClientTest = server.CreateClient();
        }

        [Fact]
        public async Task IntegrationEditoraController_HappyDay_ListAutorModel()
        {
            //Arrange
            string[] retornoEsperado = { "value1", "value2" };

            //Act
            var response = await _httpClientTest.GetAsync("/api/editora");

            //Assert
            response.IsSuccessStatusCode.Should()
                .BeTrue();

            var editoraJsonString = await response.Content.ReadAsStringAsync();
            var listaEditorasRetorno = JsonConvert.DeserializeObject<IEnumerable<string>>(editoraJsonString);

            listaEditorasRetorno.Should()
                .BeEquivalentTo(retornoEsperado);
        }

        [Fact]
        public async Task IntegrationEditoraController_EndpointNaoExiste_Retorna404()
        {
            //Arrange
            var retornoEsperado = HttpStatusCode.NotFound;

            //Act
            var response = await _httpClientTest.GetAsync("/api/patch");

            //Assert
            response.IsSuccessStatusCode.Should()
                .BeFalse();

            response.StatusCode.Should()
                .Be(retornoEsperado);
        }
    }
}
