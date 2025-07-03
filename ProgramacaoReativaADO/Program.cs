using ProgramacaoReativaADO.Domain.Application;
using ProgramacaoReativaADO.Domain.Interfaces;
using ProgramacaoReativaADO.Domain.Models;
using ProgramacaoReativaADO.Infraestruture;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoReativaADO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPedidoRepository repository = new PedidoRepository();
            IPedidoService service = new PedidoService(repository);
            

            var monitor = new PedidoMonitor(service);
            monitor.Iniciar();


            //Console.WriteLine("Monitorando novos pedidos...");
            Console.ReadLine(); // Evita que o programa encerre
        }
    }
}
