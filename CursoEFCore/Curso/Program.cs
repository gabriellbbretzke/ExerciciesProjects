using System;
using System.Runtime.InteropServices;
using CursoEFCore.Domain;
using CursoEFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio");

            using var db = new Data.ApplicationContext();

            //db.Database.Migrate();
            //var existe = db.Database.GetPendingMigrations().Any();
            //if (existe)
            //{
            //    //
            //}
            //InserirDados();
            //InserirDadosEmMassa();
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultaPedidoCarregamentoAdiantado();
            //AtualizarDados();
            RemoverRegistro();

            Console.WriteLine("Fim");
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            //var cliente = db.Clientes.Find(2);
            var cliente = new Cliente { Id = 3 };
            //db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(3);
            var clienteDesconectado = new
            {
                Nome = "Cliente desconectado",
                Telefone = "7966669999"
            };

            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);

            //db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void ConsultaPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                    //.ThenInclude(p=> p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };

            db.Pedidos.Add(pedido);

            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id>0 select c).ToList();
            var consultaPorMetodo = db.Clientes
                .Where(p=>p.Id <100)
                .OrderByDescending(p=>p.Id)
                .ToList();

            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");
                //db.Clientes.Find(cliente.Id);
                db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
            }
        }

        private static void InserirDadosEmMassa()
        {
            List<Produto> produtos = new List<Produto>();
            List<Cliente> clientes = new List<Cliente>();

            using var db = new Data.ApplicationContext();

            for (int i = 0; i < 10000; i++)
            {
                produtos.Add(new Produto
                {
                    Descricao = $"Produto {i}",
                    CodigoBarras = "1234567"+i,
                    Valor = 10m,
                    TipoProduto = TipoProduto.MercadoriaParaRevenda,
                    Ativo = true
                });

                clientes.Add(new Cliente
                {
                    Nome = $"Rafael {i}",
                    CEP = "999"+i,
                    Cidade = "Itabaiana",
                    Estado = "SE",
                    Telefone = "99000"+i,
                });

                db.AddRange(produtos[i], clientes[i]);
            }

            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = ValueObjects.TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            //db.Produtos.Add(produto);
            db.Add(produto);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");
        }
    }
}
