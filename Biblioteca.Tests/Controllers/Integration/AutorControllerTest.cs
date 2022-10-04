using AutoMapper;
using BibliotecaAPI;
using Castle.Core.Resource;
using Core;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Models;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Biblioteca.Tests.Controllers.Integration
{
    public class AutorControllerTest
    {
        //api/autor/

        private readonly HttpClient _httpClientTest;
        private readonly Mock<IAutorService> _mockAutorService;
        private readonly Mock<IMapper> _mockMapper;

        public AutorControllerTest()
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

            _mockAutorService = new();
            _mockMapper = new();
        }

        [Fact]
        public async Task IntegrationAutorController_HappyDay_ListAutorModel()
        {
            //Arrange
            _mockAutorService.Setup(x => x.ObterTodos())
                .Returns(new List<Autor>
                {
                    new Autor{IdAutor = 1, Nome = "Machado de Assis"},
                    new Autor{IdAutor = 2, Nome = "Graciliano Ramos"},
                });

            _mockMapper.Setup(x => x.Map<List<AutorModel>>(It.IsAny<List<Autor>>()))
                .Returns(new List<AutorModel>
                {
                    new AutorModel{IdAutor = 1, Nome = "Machado de Assis"},
                    new AutorModel{IdAutor = 2, Nome = "Graciliano Ramos"},
                });

            //Act
            var response = await _httpClientTest.GetAsync("/api/autor");

            //Assert
            response.IsSuccessStatusCode.Should()
                .BeTrue();

            var autorJsonString = await response.Content.ReadAsStringAsync();
            var listaAutoresRetorno = JsonConvert.DeserializeObject<List<AutorModel>>(autorJsonString);

            listaAutoresRetorno.Should()
                .BeEquivalentTo(new List<AutorModel>
                {
                    new AutorModel{IdAutor = 1, Nome = "Machado de Assis"},
                    new AutorModel{IdAutor = 2, Nome = "Graciliano Ramos"},
                });
        }
    }
}
