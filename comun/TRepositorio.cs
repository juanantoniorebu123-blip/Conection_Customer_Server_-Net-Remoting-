using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
{
    [Serializable]
    public class TRepositorio
    {
        public TDatosRepositorio repositorio;
        public List<TLibro> libros;
        public String nombreFich;

        public TRepositorio()
        {
            libros = new List<TLibro>();
        }
    }
}
