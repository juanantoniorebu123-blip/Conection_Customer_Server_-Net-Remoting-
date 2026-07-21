
using System;

namespace Practica_3
{
    public interface GestorBibliotecario_inf
    {
        int Conexion(String Passwd);
        bool Desconexion(int Ida);
        int AbrirRepositorio(int Ida, String NomFichero);
        int GuardarRepositorio(int Ida, int Repo);
        int NRepositorios(int Ida);
        TDatosRepositorio DatosRepositorio(int Ida, int Repo);
        int NuevoLibro(int Ida, TLibro Libro, int Repo);
        int Comprar(int Ida, String Isbn, int NoLibros);
        int Retirar(int Ida, String Isbn, int NoLibros);
        bool Ordenar(int Ida, int Campo);
        int NLibros(int Repo);
        int Buscar(int Ida, String Isbn);
        TLibro Descargar(int Ida, int Repo, int Pos);
        int Prestar(int Pos);
        int Devolver(int Pos);
    }

}