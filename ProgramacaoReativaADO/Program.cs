using ProgramacaoReativaADO.Models;
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
            var observable = Observable
            .Interval(TimeSpan.FromSeconds(10)) // Verifica a cada 10 segundos
            .Select(_ => ObterNovosPedidos()) // Consulta os dados
            .Where(pedidos => pedidos != null && pedidos.Count > 0) // Processa apenas se houver novos pedidos
            .Do(pedidos => ExibirPedidos(pedidos)) // Exibe os resultados
            .Subscribe(
                onNext: _ => { }, // Sem ações adicionais para cada evento
                onError: ex => Console.WriteLine($"Erro: {ex.Message}"),
                onCompleted: () => Console.WriteLine("Monitoramento concluído.")
            );


            //Console.WriteLine("Monitorando novos pedidos...");
            Console.ReadLine(); // Evita que o programa encerre
        }

        static void ExibirPedidos(List<Pedido> pedidos)
        {
            foreach (var pedido in pedidos)
            {
                Console.WriteLine($"Novo Pedido: {pedido.Id}, Produto: {pedido.Produto}, Quantidade: {pedido.Quantidade}, Data: {pedido.DataCriacao}");
            }
        }

        static List<Pedido> ObterNovosPedidos()
        {
            var pedidos = new List<Pedido>();
            string connectionString = "data Source=localhost;Initial Catalog=AppTestes;User ID=sa;Password=sa; Timeout=10;";
            string query = @"
            SELECT Id, Produto, Quantidade, DataCriacao, Lote
            FROM Pedidos 
            WHERE status = 'NP'";
            DateTime ultimaVerificacao = DateTime.Now.AddSeconds(-15);

            var pedidosIds = new List<int>();

            using (var connection = new SqlConnection(connectionString))
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
                        // Cria a lista de parâmetros
                        var parameters = pedidosIds
                            .Select((id, index) => new SqlParameter($"@Id{index}", id))
                            .ToArray();

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


                    Console.WriteLine("Monitorando novos pedidos...");

                }
            }

            return pedidos;
        }
    }
}
