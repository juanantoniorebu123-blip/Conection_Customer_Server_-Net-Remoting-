using Practica_3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;




namespace Practica_3
{





    internal class MainCliente
    {



        public static bool comprobar(TLibro l, string texto, string campo)
        {
            bool encontrado;

            switch (campo.ToLower())
            {
                case "i":
                    encontrado = l.Isbn.Contains(texto);
                    break;

                case "t":
                    encontrado = l.Titulo.Contains(texto);
                    break;

                case "a":
                    encontrado = l.Autor.Contains(texto);
                    break;

                case "p":
                    encontrado = l.Pais.Contains(texto);
                    break;

                case "d":
                    encontrado = l.Idioma.Contains(texto);
                    break;

                case "*":
                    encontrado =
                        l.Isbn.Contains(texto) ||
                        l.Titulo.Contains(texto) ||
                        l.Autor.Contains(texto) ||
                        l.Pais.Contains(texto) ||
                        l.Idioma.Contains(texto);
                    break;

                default:
                    encontrado = false;
                    break;
            }

            return encontrado;
        }



        public static void Mostrar(int Pos, bool Cabecera, TLibro l)
        {
            if (Cabecera)
            {
                Console.WriteLine(String.Format("{0,-5}{1,-58}{2,-18}{3,-4}{4,-4}{5,-4}",
                                                "POS", "TÍTULO", "ISBN", "DIS", "PRE", "RES"));
                Console.WriteLine(String.Format("     {0,-30}{1,-28}{2,-12}",
                                                        "AUTOR", "PAIS (IDIOMA)", "AÑO"));
                for (int i = 0; i < 93; i++)
                {
                    Console.Write("*");
                }
                Console.Write("\n");
            }
            Console.WriteLine(String.Format("{0,-5}{1,-58}{2,-18}{3,-4}{4,-4}{5,-4}",
                                                Pos + 1, l.Titulo, l.Isbn, l.NoLibros, l.NoPrestados, l.NoListaEspera));
            Console.WriteLine(String.Format("     {0,-30}{1,-28}{2,-12}", l.Autor, l.Pais + " (" + l.Idioma + ")", l.anio));
        }

        public static int mostrarRepositorio(GestorBibliotecario_inf biblioteca, int idAdmin)
        {
            int n = biblioteca.NRepositorios(idAdmin);

            Console.WriteLine("Pos \t Nombre \t\tDireccion \t\t Nº Libros");
            TDatosRepositorio datos;
            for (int i = 1; i < n + 1; i++)
            {
                datos = biblioteca.DatosRepositorio(idAdmin, (i - 1));
                Console.WriteLine("{0,-3} {1,-15} {2,-20} {3}",i, datos.nombre, datos.direccion, datos.NoLibros);

            }
            Console.WriteLine("0 Todos los repositorios");
            Console.WriteLine("\nElige un repositorio: ");
            int opc22 = int.Parse(Console.ReadLine());

            return opc22;
        }
        public static int mostrarRepositorio2(GestorBibliotecario_inf biblioteca, int idAdmin)
        {
            int n = biblioteca.NRepositorios(idAdmin);

            Console.WriteLine("Pos \t Nombre \t\tDireccion \t\t Nº Libros");
            TDatosRepositorio datos;
            for (int i = 1; i < n + 1; i++)
            {
                datos = biblioteca.DatosRepositorio(idAdmin, (i - 1));
                Console.WriteLine("{0,-3} {1,-15} {2,-20} {3}", i, datos.nombre, datos.direccion, datos.NoLibros);

            }
            
            Console.WriteLine("\nElige un repositorio: ");
            int opc22 = int.Parse(Console.ReadLine());

            return opc22;
        }



        public static void menuPrincipal()
        {
            Console.WriteLine("Gestor Biblioteca (menú Principal)");
            Console.WriteLine("**********************************");
            Console.WriteLine("1.- M. Administración");
            Console.WriteLine("2.- Consulta de Libros");
            Console.WriteLine("3.- Prestamo de Libros");
            Console.WriteLine("4.- Devolución de Libros");
            Console.WriteLine("0.- Salir");
            Console.Write("\nElige una opción: ");
        }
        public static void MenuAdministracion()
        {
            Console.WriteLine("Gestor Biblioteca (Menú Administración)");
            Console.WriteLine("**********************************");
            Console.WriteLine("1.- Cargar Repositorio");
            Console.WriteLine("2.- Guardar Repositorio");
            Console.WriteLine("3.- Nuevo libro");
            Console.WriteLine("4.- Comprar libros");
            Console.WriteLine("5.- Retirar libros");
            Console.WriteLine("6.- Ordenar libros");
            Console.WriteLine("7.- Buscar libros");
            Console.WriteLine("8.- Listar libros");
            Console.WriteLine("0.- Salir");
            Console.Write("\nElige una opción: ");
        }

        static void Main(string[] args)
        {

            List<string> Lista = new List<string>();
            ChannelServices.RegisterChannel(new TcpChannel(), false);
            GestorBibliotecario_inf biblioteca = (GestorBibliotecario_inf)Activator.GetObject(typeof(GestorBibliotecario_inf), "tcp://localhost:8085/GestorBiblioteca");





            // TODO code application logic here


            int idAdmin = -10;
            TLibro l;
            try
            {

                int opc1;
                do
                {
                    menuPrincipal();
                    opc1 = int.Parse(Console.ReadLine());

                    switch (opc1)
                    {
                        case 1:
                            //5634989
                            Console.Write("Indique la contraseña de acceso: ");
                            String contra = Console.ReadLine();

                            idAdmin = biblioteca.Conexion(contra);
                            if (idAdmin > 0)
                            {
                                int opc2;
                                do
                                {
                                    MenuAdministracion();
                                    opc2 = int.Parse(Console.ReadLine());

                                    switch (opc2)
                                    {
                                        case 1:
                                            Console.Write("¿Que repositorio quieres cargar? (Indique el nombre)");
                                            String rep = Console.ReadLine();
                                            int result1 = biblioteca.AbrirRepositorio(idAdmin, rep);
                                            switch (result1)
                                            {
                                                case -1:
                                                    Console.WriteLine("Ya hay un administrador conectado o el ida no coincide");
                                                    break;
                                                case -2:
                                                    Console.WriteLine("Ya esta cargado ese repositorio");
                                                    break;
                                                case 0:
                                                    Console.WriteLine("no se ha podido abrir el repositorio");
                                                    break;
                                                case 1:
                                                    Console.WriteLine("El repositorio se ha abierto correctamente");
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;

                                        case 2:
                                            int opc22 = mostrarRepositorio(biblioteca, idAdmin);

                                            int guardar = biblioteca.GuardarRepositorio(idAdmin, --opc22);

                                            switch (guardar)
                                            {
                                                case -1:
                                                    Console.WriteLine("Ya hay un administrador conectado o el ida no coincide");
                                                    break;
                                                case -2:
                                                    Console.WriteLine("El repositorio no existe");
                                                    break;
                                                case 0:
                                                    Console.WriteLine("no se ha podido guardar el/los repositorio");
                                                    break;
                                                case 1:
                                                    Console.WriteLine("El repositorio/s se ha/n guardado correctamente");
                                                    break;
                                                default:
                                                    break;
                                            }
                                            break;
                                        case 3:
                                            l = new TLibro();

                                            Console.Write("Introduce el ISBN: ");
                                            l.Isbn = Console.ReadLine();

                                            Console.Write("Introduce el Autor: ");
                                            l.Autor = Console.ReadLine();

                                            Console.Write("Introduce el Titulo: ");
                                            l.Titulo = Console.ReadLine();

                                            Console.Write("Introduce el Año: ");
                                            l.anio = int.Parse(Console.ReadLine());

                                            Console.Write("Introduce el Pais: ");
                                            l.Pais = Console.ReadLine();

                                            Console.Write("Introduce el Idioma: ");
                                            l.Idioma = Console.ReadLine();

                                            Console.Write("Introduce el Nº de Libros inicial: ");
                                            l.NoLibros = int.Parse(Console.ReadLine());

                                            int opc3 = mostrarRepositorio2(biblioteca, idAdmin);
                                            int resultado = biblioteca.NuevoLibro(idAdmin, l, --opc3);
                                            switch (resultado)
                                            {
                                                case -2:
                                                    Console.WriteLine("El repositorio indicado por el parámetro ‘Repo’ no existe o es -1.");
                                                    break;
                                                case -1:
                                                    Console.WriteLine("No hay un usuario identificado como administrador o el ‘Ida’ no coincide con el almacenado en el servidor");
                                                    break;
                                                case 0:
                                                    Console.WriteLine("Hay un libro en algún repositorio de la biblioteca que tiene el mismo Isbn.");
                                                    break;
                                                case 1:
                                                    Console.WriteLine("Se ha añadido el nuevo libro al repositorio indicado por Repo.");
                                                    break;
                                            }
                                            break;
                                        case 4:

                                            Console.WriteLine("Introduce el ISBN a buscar: ");
                                            String Isbn = Console.ReadLine();

                                            int pos = biblioteca.Buscar(idAdmin, Isbn);
                                            switch (pos)
                                            {
                                                case -1:
                                                    Console.WriteLine("No hay un usuario identificado como administrador o el ‘Ida’ no coincide con el almacenado en el servidor");
                                                    break;
                                                case -2:
                                                    Console.WriteLine("No se ha encontrado ningún Libro con el Isbn indicado por parámetro");
                                                    break;
                                            }
                                            if (pos >= 0)
                                            {
                                                l = biblioteca.Descargar(idAdmin, -1, pos);
                                                Mostrar(pos, true, l);
                                                Console.Write("¿Este es el libro del que deseas comprar mas unidades? (s/n)");
                                                String sn4 = Console.ReadLine();

                                                if (sn4.ToLower().Equals("s"))
                                                {
                                                    Console.Write("Introduce el numero de libros a comprar: ");
                                                    int cant = int.Parse(Console.ReadLine());
                                                    int result = biblioteca.Comprar(idAdmin, l.Isbn, cant);
                                                    switch (result)
                                                    {
                                                        case -1:
                                                            Console.WriteLine("No hay un usuario identificado como administrador o el ‘Ida’ no coincide con el almacenado en el Servidor");
                                                            break;
                                                        case 0:
                                                            Console.WriteLine("No se ha encontrado ningún Libro con el Isbn indicado por parámetro");
                                                            break;
                                                        case 1:
                                                            Console.WriteLine("Se han agregado los nuevos ejemplares del libro y el repositorio se ha ordenado.");
                                                            break;
                                                    }
                                                }
                                                else if (sn4.ToLower().Equals("n"))
                                                {
                                                    Console.WriteLine("¡Vuelva pronto!");
                                                }
                                            }
                                            break;
                                        case 5:
                                            Console.Write("Introduce el ISBN a buscar: ");
                                            String Isbn5 = Console.ReadLine();

                                            int pos5 = biblioteca.Buscar(idAdmin, Isbn5);
                                            switch (pos5)
                                            {
                                                case -1:
                                                    Console.WriteLine("No hay un usuario identificado como administrador o el ‘Ida’ no coincide con el almacenado en el servidor");
                                                    break;
                                                case -2:
                                                    Console.WriteLine("No se ha encontrado ningún Libro con el Isbn indicado por parámetro");
                                                    break;
                                            }
                                            if (pos5 >= 0)
                                            {
                                                l = biblioteca.Descargar(idAdmin, -1, pos5);
                                                Mostrar(pos5, true, l);
                                                Console.Write("¿Este es el libro del que deseas retirar unidades? (s/n)");
                                                String sn5 = Console.ReadLine();

                                                if (sn5.ToLower().Equals("s"))
                                                {
                                                    Console.Write("Introduce el numero de libros a retirar: ");
                                                    int cant = int.Parse(Console.ReadLine());
                                                    int result = biblioteca.Retirar(idAdmin, l.Isbn, cant);
                                                    switch (result)
                                                    {
                                                        case -1:
                                                            Console.WriteLine("No hay un usuario identificado como administrador o el ‘Ida’ no coincide con el almacenado en el Servidor");
                                                            break;
                                                        case 0:
                                                            Console.WriteLine("No se ha encontrado ningún Libro con el Isbn indicado por parámetro");
                                                            break;
                                                        case 1:
                                                            Console.WriteLine("Se han retiradp los ejemplares indicados del libro y el repositorio se ha ordenado.");
                                                            break;
                                                    }
                                                }
                                                else if (sn5.ToLower().Equals("n"))
                                                {
                                                    Console.WriteLine("¡Vuelva pronto!");
                                                }
                                            }
                                            break;
                                        case 6:
                                            int codi;
                                            do
                                            {
                                                Console.WriteLine("Código de ordenación \n0.- Por Isbn \n1.- Por Título \n2.- Por Autor \n3.- Por Año \n4.- Por País \n5.- Por Idioma \n6.- Por nº de libros Disponibles \n7.- Por nº de libros Prestados \n8.- Por nº de libros en espera ");
                                                Console.Write("Introduce el codigo");
                                                codi = int.Parse(Console.ReadLine());
                                            } while (codi < 0 || codi > 8);

                                            if (biblioteca.Ordenar(idAdmin, codi))
                                            {
                                                Console.WriteLine("Se ha ordenado correctamente");
                                            }
                                            else
                                            {
                                                Console.WriteLine("No hay un usuario identificado como administrador o el ‘Ida’ no coincide con el servidor. ");
                                            }
                                            break;
                                        case 7:
                                            Console.Write("Introduce el texto a buscar: ");
                                            String buscar7 = Console.ReadLine();

                                            Console.WriteLine("Código de búsqueda \nI.- Por Isbn \nT.- Por Título \nA.- Por Autor \nP.- Por País \nD.- Por Idioma \n*.- Por todos los campos.");
                                            Console.Write("Introduce el codigo: ");
                                            String cod7 = Console.ReadLine();

                                            int opc7 = mostrarRepositorio(biblioteca, idAdmin);
                                            bool cabe7 = true;
                                            int nlib7 = biblioteca.NLibros(--opc7);
                                            for (int i = 0; i < nlib7; i++)
                                            {
                                                l = biblioteca.Descargar(idAdmin, opc7, i);
                                                if (comprobar(l, buscar7, cod7))
                                                {
                                                    Mostrar(i, cabe7, l);
                                                    cabe7 = false;
                                                }
                                            }
                                            break;
                                        case 8:
                                            int nLibros = biblioteca.NLibros(-1);
                                            bool cab8 = true;
                                            for (int i = 0; i < nLibros; i++)
                                            {
                                                l = biblioteca.Descargar(idAdmin, -1, i);
                                                Mostrar(i, cab8, l);
                                                cab8 = false;
                                            }
                                            

                                            break;
                                        case 0:
                                            bool salir0 = biblioteca.Desconexion(idAdmin);
                                            if (salir0)
                                            {
                                                idAdmin = -1;
                                                Console.WriteLine("Saliendo de la aplicacion...");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Error al desconectarse (Ida incorrecto)");
                                            }
                                            break;

                                    }

                                } while (opc2 != 0);

                            }
                            else if (idAdmin == -1)
                            {
                                Console.WriteLine("ya hay un admin conectado");
                            }
                            else if (idAdmin == -2)
                            {
                                Console.WriteLine("la contraseña no coincide");
                            }

                            break;

                        case 2:
                            Console.Write("Introduce el texto a buscar: ");
                            String buscar = Console.ReadLine();

                            Console.WriteLine("Código de búsqueda \nI.- Por Isbn \nT.- Por Título \nA.- Por Autor \nP.- Por País \nD.- Por Idioma \n*.- Por todos los campos.");
                            Console.Write("Introduce el codigo: ");
                            String cod = Console.ReadLine();

                            bool cabe = true;
                            int nlib = biblioteca.NLibros(-1);
                            for (int i = 0; i < nlib; i++)
                            {
                                l = biblioteca.Descargar(idAdmin, -1, i);
                                if (comprobar(l, buscar, cod))
                                {
                                    Mostrar(i, cabe, l);
                                    cabe = false;
                                }
                            }

                            break;
                        case 3:
                            Console.Write("Introduce el texto a buscar: ");
                            String buscar3 = Console.ReadLine();

                            Console.WriteLine("Código de búsqueda \nI.- Por Isbn \nT.- Por Título \nA.- Por Autor \nP.- Por País \nD.- Por Idioma \n*.- Por todos los campos.");
                            Console.Write("Introduce el codigo: ");
                            String cod3 = Console.ReadLine();

                            bool cabe3 = true;
                            int nlib3 = biblioteca.NLibros(-1);
                            for (int i = 0; i < nlib3; i++)
                            {
                                l = biblioteca.Descargar(idAdmin, -1, i);
                                if (comprobar(l, buscar3, cod3))
                                {
                                    Mostrar(i, cabe3, l);
                                    cabe3 = false;
                                }
                            }
                            Console.Write("¿Quieres sacar algun libro de la biblioteca? (s/n)");
                            String sn = Console.ReadLine();

                            if (sn.ToLower().Equals("s"))
                            {
                                Console.Write("Introduce la posicion del libro: ");
                                int pos3 = int.Parse(Console.ReadLine());

                                int result = biblioteca.Prestar(--pos3);
                                switch (result)
                                {
                                    case -1:
                                        Console.WriteLine("La posición indicada no está dentro de los límites del repositorio mezclado y ordenado.");
                                        break;
                                    case 0:
                                        Console.WriteLine("No ay libros disponibles, usted se encuentra en lista de espera ¡Disculpe las molestias!");
                                        break;
                                    case 1:
                                        Console.WriteLine("El libro se ha solicitado correctamente, pase a recogerlo");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("¡Vuelva pronto!");
                            }
                            break;
                        case 4:
                            Console.Write("Introduce el Isbn a buscar: ");
                            String isbn = Console.ReadLine();

                            bool cabe4 = true;
                            int nlib4 = biblioteca.NLibros(-1);
                            for (int i = 0; i < nlib4; i++)
                            {
                                l = biblioteca.Descargar(idAdmin, -1, i);
                                if (comprobar(l, isbn, "i"))
                                {
                                    Mostrar(i, cabe4, l);
                                    cabe4 = false;
                                }
                            }

                            Console.Write("¿Quieres devolver algun libro a la biblioteca? (s/n)");
                            sn = Console.ReadLine();
                            if (sn.ToLower().Equals("s"))
                            {
                                Console.Write("Introduce la posicion del libro a devolver: ");
                                int pos4 = int.Parse(Console.ReadLine());
                                int result = biblioteca.Devolver(--pos4);
                                switch (result)
                                {
                                    case -1:
                                        Console.WriteLine("La posición indicada no está dentro de los límites del repositorio mezclado y ordenado.");
                                        break;
                                    case 0:
                                        Console.WriteLine("Se ha devuelto el libro reduciendo el número de usuarios en espera");
                                        break;
                                    case 1:
                                        Console.WriteLine("Se ha devuelto aumentando el número de libros disponibles");
                                        break;
                                    case 2:
                                        Console.WriteLine(" El libro no se puede devolver, porque no hay ni usuarios en lista de espera ni libros prestados.");
                                        break;
                                    default:
                                        Console.WriteLine("Error inesperado");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("El tiempo apremia moroso.");
                            }
                            break;
                        case 0:
                            bool salir = biblioteca.Desconexion(idAdmin);
                            if (salir)
                            {
                                Console.WriteLine("Saliendo de la aplicacion...");
                            }
                            else
                            {
                                Console.WriteLine("Error al desconectarse (Ida incorrecto)");
                            }
                            break;
                    }
                } while (opc1 != 0);

            }
            catch
            {
                Console.WriteLine("Error");

            }




        }



    }
}

