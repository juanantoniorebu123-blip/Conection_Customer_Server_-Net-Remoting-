using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
{
    [Serializable]
    public class TDatosRepositorio
    {
        public String nombre;
        public String direccion;
        public int NoLibros;

        public TDatosRepositorio(String nombre, String direccion, int NoLibros)
        {
            this.nombre = nombre;
            this.direccion = direccion;
            this.NoLibros = NoLibros;
        }

        public TDatosRepositorio()
        {
            this.nombre = "";
            this.direccion = "";
            this.NoLibros = 0;
        }
    }
}
