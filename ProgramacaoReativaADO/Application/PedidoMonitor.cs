using ProgramacaoReativaADO.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoReativaADO.Domain.Application
{
    public class PedidoMonitor
    {
        private readonly IPedidoService _pedidoService;
        public PedidoMonitor(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        public void Iniciar()
        {
            Observable.Interval(TimeSpan.FromSeconds(30))
                .Select(_ => _pedidoService.BuscarNovosPedidos())
                .Where(pedidos => pedidos != null && pedidos.Count > 0)
                .Do(pedidos => _pedidoService.ExibirPedidos(pedidos))
                .Subscribe(
                    _ => { },
                    ex => Console.WriteLine($"Erro: {ex.Message}"),
                    () => Console.WriteLine("Monitoramento concluído.")
                );

            Console.WriteLine("Monitorando novos pedidos...");
        }
    }
}
