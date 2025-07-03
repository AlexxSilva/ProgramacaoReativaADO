using ProgramacaoReativaADO.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoReativaADO.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        List<Pedido> ObterNovosPedidos();
        void AtualizarStatusPedido(List<int> pedidoId, string status);
        void ExibirPedidos(List<Pedido> pedidos);
    }
}
