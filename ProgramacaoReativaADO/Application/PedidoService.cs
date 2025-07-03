using ProgramacaoReativaADO.Domain.Interfaces;
using ProgramacaoReativaADO.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoReativaADO.Domain.Application
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public List<Pedido> BuscarNovosPedidos()
        {
            return _repository.ObterNovosPedidos();
        }

        public void ExibirPedidos(List<Pedido> pedidos)
        {
            _repository.ExibirPedidos(pedidos);
        }
    }
}
