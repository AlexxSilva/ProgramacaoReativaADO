using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoReativaADO.Domain.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public string Produto { get; set; }
        public double Quantidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Lote { get; set; }
    }
}
