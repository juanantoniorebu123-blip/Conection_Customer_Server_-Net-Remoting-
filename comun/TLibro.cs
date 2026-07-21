using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
{
    [Serializable]
    public class TLibro
    {
        public String Isbn;
        public String Titulo;
        public String Autor;
        public String Pais;
        public String Idioma;
        public int anio;
        public int NoLibros;
        public int NoPrestados;
        public int NoListaEspera;

        public TLibro(String Isbn, String Titulo, String Autor, String Pais, String Idioma, int anio, int NoLibros, int NoPrestados, int NoListaEspera)
        {
            this.Isbn = Isbn;
            this.Titulo = Titulo;
            this.Autor = Autor;
            this.Pais = Pais;
            this.Idioma = Idioma;
            this.anio = anio;
            this.NoLibros = NoLibros;
            this.NoPrestados = NoPrestados;
            this.NoListaEspera = NoListaEspera;
        }

        public TLibro()
        {
            this.Isbn = "";
            this.Titulo = "";
            this.Autor = "";
            this.Pais = "";
            this.Idioma = "";
            this.anio = 0;
            this.NoLibros = 0;
            this.NoPrestados = 0;
            this.NoListaEspera = 0;
        }

        public TLibro(TLibro l)
        {
            this.Isbn = l.Isbn;
            this.Titulo = l.Titulo;
            this.Autor = l.Autor;
            this.Pais = l.Pais;
            this.Idioma = l.Idioma;
            this.anio = l.anio;
            this.NoLibros = l.NoLibros;
            this.NoPrestados = l.NoPrestados;
            this.NoListaEspera = l.NoListaEspera;
        }
    }
}
