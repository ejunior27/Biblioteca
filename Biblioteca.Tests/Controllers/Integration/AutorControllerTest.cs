using AutoMapper;
using BibliotecaAPI;
using BibliotecaAPI.Controllers;
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

        private readonly Mock<IAutorService> _mockAutorService;
        private readonly Mock<IMapper> _mockMapper;
        private AutorController _subject;

        public AutorControllerTest()
        {
            _mockAutorService = new();
            _mockMapper = new();

            _subject = new AutorController(_mockAutorService.Object, _mockMapper.Object);
        }

        [Fact]
        public void IntegrationAutorController_HappyDay_ListAutorModel()
        {
            //Arrange
            _mockAutorService.Setup(x => x.ObterTodos())
                .Returns(new List<Autor>
                {
                    new Autor{IdAutor = 1, Nome = "Machado de Assis"},
                    new Autor{IdAutor = 2, Nome = "Graciliano Ramos"},
                });

            _mockMapper.Setup(x => x.Map<List<AutorModel>>(It.IsAny<IEnumerable<Autor>>()))
                .Returns(new List<AutorModel>
                {
                    new AutorModel{IdAutor = 1, Nome = "Machado de Assis"},
                    new AutorModel{IdAutor = 2, Nome = "Graciliano Ramos"},
                });

            //Act
            var retorno = _subject.Get();

            //Assert
            retorno.Value.Should()
                .BeEquivalentTo(new List<AutorModel>
                {
                    new AutorModel{IdAutor = 1, Nome = "Machado de Assis"},
                    new AutorModel{IdAutor = 2, Nome = "Graciliano Ramos"},
                });
        }
    }
}
