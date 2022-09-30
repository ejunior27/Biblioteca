using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Tests
{
    public class BibliotecaContextFake
    {
        public List<Autor> Autor { get; set; }

        public BibliotecaContextFake()
        {
            Autor = new List<Autor>
            {
                new Autor { IdAutor = 1, Nome = "Machado de Assis", AnoNascimento = DateTime.Parse("1917-12-31")},
                new Autor { IdAutor = 2, Nome = "Ian S. Sommervile", AnoNascimento = DateTime.Parse("1935-12-31")},
                new Autor { IdAutor = 3, Nome = "J.K. Rowling", AnoNascimento = DateTime.Parse("1997-1-6")},
            };
        }


    }
}
