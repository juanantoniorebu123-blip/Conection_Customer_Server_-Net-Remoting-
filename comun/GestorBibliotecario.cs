using Practica_3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;


namespace Practica_3
{

    public class GestorBibliotecario : MarshalByRefObject, GestorBibliotecario_inf
    {
        private int idAdmin = -1;
        private List<TLibro> Biblioteca;
        private string path;
        private int nLibros = 0;
        private List<TRepositorio> repositorio;
        private int campoOrdenacion;

        public GestorBibliotecario() { 
            try
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), "..\\..");
                path += "\\";
                Biblioteca = new List<TLibro>();
                repositorio = new List<TRepositorio>();
               

            }
            catch (IOException ex)
            {
                Console.Write(ex.ToString());
            }
         }


        public int AbrirRepositorio(int Ida, string NomFichero)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {


                bool esta = false;
                int pos = 0;
                while (!esta && pos < repositorio.Count())
                {
                    if (NomFichero.Equals(repositorio[pos].nombreFich))
                    {
                        esta = true;
                        break;
                    }
                    else
                    {
                        pos++;
                    }
                }

                if (esta)
                {
                    return -2;
                }

                try
                {

                    string aux = Path.Combine(path, NomFichero);
                    using (FileStream fs = new FileStream(aux, FileMode.OpenOrCreate, FileAccess.Read))
                    using (BinaryReader fich = new BinaryReader(fs))
                    {

                        if (fs.Length == 0)
                        {
                            return 0;
                        }

                        int librosFich = fich.ReadInt32();
                        nLibros += librosFich;

                        TRepositorio tr = new TRepositorio();
                        tr.nombreFich = NomFichero;
                        TDatosRepositorio rep = new TDatosRepositorio();

                        

                        rep.NoLibros = librosFich;
                        rep.nombre = fich.ReadString();
                        rep.direccion = fich.ReadString();

                        tr.repositorio = rep;         

                        for (int i = 0; i < librosFich; i++)
                        {
                            TLibro libro = new TLibro();
                            libro.Isbn = fich.ReadString();
                            libro.Titulo = fich.ReadString();
                            libro.Autor = fich.ReadString();
                            libro.anio = fich.ReadInt32();
                            libro.Pais = fich.ReadString();
                            libro.Idioma = fich.ReadString();
                            libro.NoLibros = fich.ReadInt32();
                            libro.NoPrestados = fich.ReadInt32();
                            libro.NoListaEspera = fich.ReadInt32();
                            tr.libros.Add(libro);
                            Biblioteca.Add(libro);
                        }
                        repositorio.Add(tr);
                        Ordenar(Ida, campoOrdenacion);
                        fich.Close();
                        return 1;

                    }
                }
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                } catch (IOException ex) {
                    Console.WriteLine(ex.Message);
                }
            }
            return -1;
        }

        public int Buscar(int Ida, string Isbn)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                int i = 0, j = 0;
                bool iguales = false;
                while (iguales == false && i < Biblioteca.Count())
                {
                    if (Isbn.Equals(Biblioteca[i].Isbn))
                    {
                        iguales = true;
                    }
                    else
                    {
                        i++;
                    }
                }
                if (iguales == false)
                {
                    return -2;
                }
                else
                {
                    return i;
                }
            }
            else
            {
                return -1;
            }
        }

        public int Comprar(int Ida, string Isbn, int NoLibros)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                int i = 0, j = 0;
                bool iguales = false;
                while (iguales == false && i < repositorio.Count())
                {
                    j = 0;
                    while (iguales == false && j < repositorio[i].libros.Count())
                    {
                        if (repositorio[i].libros[j].Isbn.Equals(Isbn))
                        {
                            iguales = true;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    if (!iguales)
                    {
                        i++;
                    }
                }

                if (iguales == false)
                {
                    return 0;
                }
                else
                {
                    repositorio[i].libros[j].NoLibros += NoLibros;
                    while (repositorio[i].libros[j].NoListaEspera > 0 && repositorio[i].libros[j].NoLibros > 0)
                    {
                        repositorio[i].libros[j].NoListaEspera--;
                        repositorio[i].libros[j].NoLibros--;
                    }
                    Ordenar(Ida, campoOrdenacion);
                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }

        public int Conexion(string Passwd)
        {
            if (idAdmin != -1)
            {
                return -1;
            }
            if (Passwd.Equals("5634989"))
            {
                Random r = new Random();
                idAdmin = r.Next(1, 1000001);
                return idAdmin;
            }
            else
            {
                return -2;
            }
        }

        public TDatosRepositorio DatosRepositorio(int Ida, int Repo)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                if (Repo < 0 || Repo >= repositorio.Count())
                {
                    TDatosRepositorio noExiste = new TDatosRepositorio();

                    noExiste.NoLibros = 0;
                    noExiste.direccion = "";
                    noExiste.nombre = "???";

                    return noExiste;

                }
                else
                {
                    return repositorio[Repo].repositorio;
                }
            }
            else
            {
                return null;
            }
        }

        public TLibro Descargar(int Ida, int Repo, int Pos)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                if (Repo >= 0 && Repo < repositorio.Count())
                {
                    if (Pos < repositorio[Repo].libros.Count() && Pos >= 0)
                    {
                        return repositorio[Repo].libros[Pos];
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (Repo == -1 && Pos < Biblioteca.Count() && Pos >= 0)
                    {
                        return Biblioteca[Pos];
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            else
            {//El ida no esta en el servidor

                if (Repo >= 0 && Repo < repositorio.Count())
                {
                    if (Pos < repositorio[Repo].libros.Count() && Pos >= 0)
                    {
                        TLibro libro = new TLibro(repositorio[Repo].libros[Pos]);
                        libro.NoListaEspera = 0;
                        libro.NoPrestados = 0;
                        return libro;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    if (Repo == -1 && Pos < Biblioteca.Count() && Pos >= 0)
                    {
                        TLibro libro = new TLibro(Biblioteca[Pos]);
                        libro.NoListaEspera = 0;
                        libro.NoPrestados = 0;
                        return libro;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        public bool Desconexion(int Ida)
        {
            if (Ida == idAdmin)
            {
                idAdmin = -1;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int Devolver(int Pos)
        {
           

            if (Biblioteca.Count() > Pos && Pos >= 0)
            {
                if (Biblioteca[Pos].NoPrestados == 0)
                {
                    return 2;
                }
                else if (Biblioteca[Pos].NoListaEspera > 0)
                {
                    Biblioteca[Pos].NoListaEspera--;
                    Ordenar(idAdmin, campoOrdenacion);

                    return 0;
                }
                else
                {
                    Biblioteca[Pos].NoLibros++;
                    Biblioteca[Pos].NoPrestados--;
                    Ordenar(idAdmin, campoOrdenacion);

                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }

        public int GuardarRepositorio(int Ida, int Repo)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                if (Repo == -1)
                {
                    for (int i = 0; i < repositorio.Count(); i++)
                    {
                        try
                        {
                            guardarRepositorio(repositorio[i]);
                        }
                        catch (Exception ex){ 
                            Console.WriteLine(ex.Message);
                        }
                    }
                    return 1;
                } else if (Repo < 0 || Repo >= repositorio.Count())
                {
                    return -2;
                }
                else
                { //El repositorio existe (es uno de los 3)
                    try
                    {
                        bool guarda = guardarRepositorio(repositorio[Repo]);
                        if (guarda)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    } catch (FileNotFoundException ex) {
                        Console.WriteLine(ex.Message);
                    } catch (IOException ex) {
                        Console.WriteLine(ex.Message);
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            return -1;
        }

        private bool guardarRepositorio(TRepositorio rep)
        {
            string aux = Path.Combine(path, rep.nombreFich);
            using (FileStream fs = new FileStream(aux, FileMode.OpenOrCreate, FileAccess.Write))
            using (BinaryWriter fich = new BinaryWriter(fs))
            {

                if (fs.Length == 0)
                {
                    return false;
                }
                fich.Write(rep.libros.Count());
                fich.Write(rep.repositorio.nombre);
                fich.Write(rep.repositorio.direccion);

                for (int j = 0; j < rep.libros.Count(); j++)
                {
                    fich.Write(rep.libros[j].Isbn);
                    fich.Write(rep.libros[j].Titulo);
                    fich.Write(rep.libros[j].Autor);
                    fich.Write(rep.libros[j].anio);
                    fich.Write(rep.libros[j].Pais);
                    fich.Write(rep.libros[j].Idioma);
                    fich.Write(rep.libros[j].NoLibros);
                    fich.Write(rep.libros[j].NoPrestados);
                    fich.Write(rep.libros[j].NoListaEspera);

                }
                fich.Close();
                return true;
            }
        }

    public int NLibros(int Repo)
        {
            if (Repo >= 0 && Repo < repositorio.Count())
            {
                return repositorio[Repo].repositorio.NoLibros;
            }
            else if (Repo == -1)
            {
                return Biblioteca.Count();
            }
            else
            {
                return -1;
            }
        }

        public int NRepositorios(int Ida)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                return repositorio.Count();
            }
            else
            {
                return -1;
            }
        }

        public int NuevoLibro(int Ida, TLibro Libro, int Repo)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                if (Repo >= 0 && Repo < repositorio.Count())
                {
                    bool iguales = false;
                    int i = 0;
                    while (!iguales && i < Biblioteca.Count())
                    {
                        if (Libro.Isbn.Trim().Equals(Biblioteca[i].Isbn.Trim()))
                        {
                            iguales = true;
                        }
                        else
                        {
                            i++;
                        }
                    }

                    if (iguales == true)
                    {
                        return 0;
                    }
                    else
                    {
                        repositorio[Repo].libros.Add(Libro);
                        repositorio[Repo].repositorio.NoLibros++;
                        Biblioteca.Add(Libro);
                        Ordenar(Ida, campoOrdenacion);
                        return 1;
                    }
                }
                else
                {
                    return -2;
                }

            }
            else
            {
                return -1;
            }
        }


        public bool Ordenar(int Ida, int Campo)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                campoOrdenacion = Campo;

                for (int i = 0; i < repositorio.Count(); i++)
                {
                    for (int j = 0; j < repositorio[i].libros.Count() - 1; j++)
                    { 
                        for (int k = 0; k < repositorio[i].libros.Count() - 1 - j; k++)
                        {
                            if (libroPorCampo(Campo, i, k, k + 1))
                            {
                                TLibro temp = repositorio[i].libros[k];
                                repositorio[i].libros[k] = repositorio[i].libros[k + 1];
                                repositorio[i].libros[k + 1] = temp;
                            }
                        }
                    }
                }
                for (int j = 0; j < Biblioteca.Count() - 1; j++)
                { 
                    for (int k = 0; k < Biblioteca.Count() - 1 - j; k++)
                    {
                        if (libroPorCampo(Campo, k, k + 1))
                        {
                            TLibro temp = Biblioteca[k];
                            Biblioteca[k] = Biblioteca[k + 1];
                            Biblioteca[k + 1] = temp;
                        }
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool libroPorCampo(int Campo, int rep, int l2, int l1)
        { // compara los campos de los libros
            bool salida = false;
            TLibro L1 = repositorio[rep].libros[l1];
            TLibro L2 = repositorio[rep].libros[l2];
            switch (Campo)
            {
                case 0:
                    if (L1.Isbn.CompareTo(L2.Isbn) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 1:
                    if (L1.Titulo.CompareTo(L2.Titulo) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 2:
                    if (L1.Autor.CompareTo(L2.Autor) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 3:
                    if (L1.anio < L2.anio)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 4:
                    if (L1.Pais.CompareTo(L2.Pais) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 5:
                    if (L1.Idioma.CompareTo(L2.Idioma) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 6:
                    if (L1.NoLibros < L2.NoLibros)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 7:
                    if (L1.NoPrestados < L2.NoPrestados)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 8:
                    if (L1.NoListaEspera < L2.NoListaEspera)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
            }
            return salida;
        }

        public bool libroPorCampo(int Campo, int l2, int l1)
        { // compara los campos de los libros
            bool salida = false;
            TLibro L1 = Biblioteca[l1];
            TLibro L2 = Biblioteca[l2];
            switch (Campo)
            {
                case 0:
                    if (L1.Isbn.CompareTo(L2.Isbn) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 1:
                    if (L1.Titulo.CompareTo(L2.Titulo) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 2:
                    if (L1.Autor.CompareTo(L2.Autor) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 3:
                    if (L1.anio < L2.anio)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 4:
                    if (L1.Pais.CompareTo(L2.Pais) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 5:
                    if (L1.Idioma.CompareTo(L2.Idioma) < 0)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 6:
                    if (L1.NoLibros < L2.NoLibros)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 7:
                    if (L1.NoPrestados < L2.NoPrestados)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
                case 8:
                    if (L1.NoListaEspera < L2.NoListaEspera)
                    {
                        salida = true;
                    }
                    else
                    {
                        salida = false;
                    }
                    break;
            }
            return salida;
        }

        public int Prestar(int Pos)
        {
            int i = 0, j = 0, k = 0;
            bool iguales = false;

            if (Biblioteca.Count() > Pos && Pos >= 0)
            {

                if (Biblioteca[Pos].NoLibros == 0)
                {
                    //esto es con repositorios, con biblioteca seria Biblioteca.get(Pos).NoListaEspera++; y llamamos al ordenar
                    while (i < repositorio.Count() && !iguales)
                    { //miramos si la pos de biblioteca coincide con algun isbn de algun libro de un repositorio (trabajamos con repositorio ya que al hacer ++ automaticamente se hace en biblioteca pq son el mismo objeto libro)
                        j = 0;
                        while (j < repositorio[i].libros.Count() - 1 && !iguales)
                        { // Bubble Sort
                            k = 0;
                            while (k < repositorio[i].libros.Count() - 1 - j && !iguales)
                            {
                                if (Biblioteca[Pos].Isbn.Equals(repositorio[i].libros[k].Isbn))
                                {
                                    iguales = true;
                                    repositorio[i].libros[k].NoListaEspera++;
                                    Ordenar(idAdmin, campoOrdenacion);
                                }
                                else
                                {
                                    k++;
                                }
                            }
                            j++;
                        }
                        i++;
                    }

                    return 0;
                }
                else
                {
                    while (i < repositorio.Count() && !iguales)
                    { //igual q arriba
                        j = 0;
                        while (j < repositorio[i].libros.Count()  && !iguales)
                        {
                            k = 0;
                            while (k < repositorio[i].libros.Count() && !iguales)
                            {
                                if (Biblioteca[Pos].Isbn.Equals(repositorio[i].libros[k].Isbn))
                                {
                                    iguales = true;
                                    repositorio[i].libros[k].NoLibros--;
                                    repositorio[i].libros[k].NoPrestados++;
                                    Ordenar(idAdmin, campoOrdenacion);
                                }
                                else
                                {
                                    k++;
                                }
                            }
                            j++;
                        }
                        i++;
                    }

                    return 1;
                }
            }
            else
            {
                return -1;
            }
        }

        public int Retirar(int Ida, string Isbn, int NoLibros)
        {
            if (Ida == idAdmin && idAdmin > 0)
            {
                int i = 0, j = 0;
                bool iguales = false;
                while (iguales == false && i < repositorio.Count())
                {
                    j = 0;
                    while (iguales == false && j < repositorio[i].libros.Count())
                    {
                        if (repositorio[i].libros[j].Isbn.Equals(Isbn))
                        {
                            iguales = true;
                        }
                        else
                        {
                            j++;
                        }
                    }
                    if (!iguales)
                    {
                        i++;
                    }
                }

                if (iguales == false)
                {
                    return 0;
                }
                else
                {
                    if (repositorio[i].libros[j].NoLibros >= NoLibros)
                    {
                        repositorio[i].libros[j].NoLibros -= NoLibros;

                        Ordenar(Ida, campoOrdenacion);
                        return 1;
                    }
                    else
                    {
                        return 2;
                    }

                }
            }
            else
            {
                return -1;
            }
        }


    }
}
