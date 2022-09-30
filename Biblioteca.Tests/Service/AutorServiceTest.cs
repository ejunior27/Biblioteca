using Core;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Biblioteca.Tests.Service
{
    public class AutorServiceTest
    {
        private readonly AutorService _subject;
        //private readonly Mock<BibliotecaContext> _mockBibliotecaContext;
        private readonly BibliotecaContext _mockBibliotecaContext;

        public AutorServiceTest()
        {
            //_mockBibliotecaContext = new Mock<BibliotecaContext>();
            _mockBibliotecaContext = Substitute.For<BibliotecaContext>();            
            //_subject = new AutorService(_mockBibliotecaContext.Object);
            _subject = new AutorService(_mockBibliotecaContext);
        }

        [Fact]
        public void Inserir_HappyDay_RetornaIdAutor()
        {
            //Arrange
            //_mockBibliotecaContext.Setup(mock => mock.GetAll())
            //    .Returns(GetAutoresFake());

            _mockBibliotecaContext.GetAll().Returns(GetAutoresFake());

            //Act
            var idRetorno = _subject.Inserir(new Autor
            {
                AnoNascimento = new DateTime(1997,1,6),
                IdAutor = 3,
                Nome = "J.K. Rowling"                
            });

            var autor = _subject.Obter(3);

            //Assert
            idRetorno.Should()
                .Be(3);

            autor.Should()
                .NotBeNull();

        }

        [Fact]
        public void Inserir_IdMaiorQue5_RetornaIdZero()
        {
            //Arrange
            //_mockBibliotecaContext.Setup(mock => mock.GetAll())
            //    .Returns(GetAutoresFake());
            
            _mockBibliotecaContext.GetAll().Returns(GetAutoresFake());

            //Act
            var idRetorno = _subject.Inserir(new Autor
            {
                AnoNascimento = new DateTime(1997, 1, 6),
                IdAutor = 8,
                Nome = "J.K. Rowling"
            });

            var autor = _subject.Obter(3);

            //Assert
            idRetorno.Should()
                .Be(0);

            autor.Should()
                .NotBeNull();
        }

        private List<Autor> GetAutoresFake()
        {
            return new List<Autor>
            {
                new Autor { IdAutor = 1, Nome = "Machado de Assis", AnoNascimento = DateTime.Parse("1917-12-31")},
                new Autor { IdAutor = 2, Nome = "Ian S. Sommervile", AnoNascimento = DateTime.Parse("1935-12-31")},
                new Autor { IdAutor = 3, Nome = "J.K. Rowling", AnoNascimento = DateTime.Parse("1997-1-6")},
            };
        }

        private Autor GetAutorIdGreaterThan5()
        {
            return new Autor
            {
                IdAutor = 10,
                Nome = "C.S. Lewis"
            };
        }
    }
}
