using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;

namespace Practica_3
{
    internal class MainServidor
    {
        static void Main(string[] args)
        {
            try
            {
                TcpChannel canal = new TcpChannel(8085);
                ChannelServices.RegisterChannel(canal, false);

                RemotingConfiguration.RegisterWellKnownServiceType(
                    typeof(GestorBibliotecario),   
                    "GestorBiblioteca",
                    WellKnownObjectMode.Singleton
                );
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());


            }

            Console.WriteLine("Servidor listo.");
            Console.ReadLine();

        }
    }
}
