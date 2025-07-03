using ProgramacaoReativaADO.Domain.Interfaces;
using ProgramacaoReativaADO.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramacaoReativaADO.Infraestruture
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly string _connectionString = "data Source=localhost;Initial Catalog=AppTestes;User ID=sa;Password=123mudar@@; Timeout=10;";


        public List<Pedido> ObterNovosPedidos()
        {
            // ------------------obter pedidos do banco de dados------------------

            var pedidos = new List<Pedido>();
            string query = @"
            SELECT Id, Produto, Quantidade, DataCriacao, Lote
            FROM Pedidos 
            WHERE status = 'NP'";
            DateTime ultimaVerificacao = DateTime.Now.AddSeconds(-15);

            var pedidosIds = new List<int>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UltimaVerificacao", ultimaVerificacao);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var pedido = new Pedido
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Produto = reader["Produto"].ToString(),
                                Quantidade = Convert.ToDouble(reader["Quantidade"]),
                                DataCriacao = Convert.ToDateTime(reader["DataCriacao"]),
                                Lote = reader["Lote"].ToString(),
                            };

                            pedidos.Add(pedido);
                            pedidosIds.Add(pedido.Id);
                        }

                        reader.Close();
                    }


                    if (pedidosIds.Any())
                    {
                        AtualizarStatusPedido(pedidosIds, "P");
                    }

                    return pedidos;

                }
            }
        }

        public void AtualizarStatusPedido(List<int> pedidosIds, string status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                // Cria a lista de parâmetros
                var parameters = pedidosIds
                    .Select((id, index) => new SqlParameter($"@Id{index}", id))
                    .ToArray();

                // ------------------atualizar status dos pedidos------------------

                // Cria a cláusula IN com os parâmetros
                string updateQuery = "UPDATE Pedidos SET Status = @Status WHERE Id IN ("
                                     + string.Join(",", parameters.Select(p => p.ParameterName)) + ")";

                using (var updateCommand = new SqlCommand(updateQuery, connection))
                {
                    // Adiciona o parâmetro de status
                    updateCommand.Parameters.AddWithValue("@Status", "P");

                    // Adiciona os parâmetros dos IDs
                    updateCommand.Parameters.AddRange(parameters);

                    updateCommand.ExecuteNonQuery(); // Executa o UPDATE em lote
                }
            }
        }

        public void ExibirPedidos(List<Pedido> pedidos)
        {
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"Novo Pedido: {pedido.Id}, Produto: {pedido.Produto}, Quantidade: {pedido.Quantidade}, Data: {pedido.DataCriacao}");
            }
        }
    }
}
