using ProgramacaoReativaADO.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoReativaADO.Domain.Interfaces
{
    public interface IPedidoService
    {
        List<Pedido> BuscarNovosPedidos();
         void ExibirPedidos(List<Pedido> pedidos);
    }
}
