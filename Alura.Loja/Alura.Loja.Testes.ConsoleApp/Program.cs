using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            RecuperandoRelacionamento_1_1();
        }

        static void RecuperandoRelacionamento_1_1()
        {
            using (var contexto = new LojaContex())
            {
                var cliente = contexto
                    .Clientes
                    .Include(c => c.EnderecoDeEntrega)
                    .FirstOrDefault();

                Console.WriteLine($"Endereço de entrega: {cliente.EnderecoDeEntrega.Logradouro}");

                /*var produto = contexto
                    .Produtos
                    .Include(p => p.Compras)        //Após criado relacionamento com compras, agora é possível acessar as compras.
                    .Where(p => p.Id == 1)
                    .FirstOrDefault();
                */

                var produto = contexto
                                .Produtos
                                .Where(p => p.Id == 1)
                                .FirstOrDefault();

                contexto.Entry(produto)
                    .Collection(p => p.Compras)
                    .Query()
                    .Where(c => c.Preco > 1)
                    .Load();

                Console.WriteLine($"Mostrando as compras do produto {produto.Nome}");
                foreach (var item in produto.Compras)
                {
                    Console.WriteLine(item.Preco);
                }
            }
        }

        static void RecuperandoRelacionamento_N_N()
        {
            /*using (var contexto = new LojaContex())
            {
                var promocao = new Promocao();
                promocao.Descricao = "Queima Tudo Ano Novo";
                promocao.DataInicio = new DateTime(2020, 1, 1);
                promocao.DataTermino = new DateTime(2020, 1, 31);

                var produtos = contexto
                    .Produtos
                    .Where(p => p.Categoria == "Bebidas")
                    .ToList();

                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }

                contexto.Promocoes.Add(promocao);
                contexto.SaveChanges();
            }*/

            using (var contexto2 = new LojaContex())
            {
                var promocao = contexto2
                                .Promocoes
                                .Include(p => p.Produtos)
                                .ThenInclude(pp => pp.produto)
                                .FirstOrDefault();

                Console.WriteLine("\nLista de Produtos...");
                foreach (var item in promocao.Produtos)
                {
                    Console.WriteLine(item.produto);
                }
            }
        }

        static void Relacionamento_1_1()
        {
            //Exemplo 1:1

            var fulano = new Cliente();
            fulano.Nome = "Riedo Teste";

            fulano.EnderecoDeEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua São",
                Complemento = "Casa Grande",
                Bairro = "Centro",
                Cidade = "São Paulo"
            };

            using (var contexto = new LojaContex())
            {
                contexto.Clientes.Add(fulano);
                contexto.SaveChanges();
            }
        }

        static void Relacionamento_1_N()
        {
            //Exemplo 1:N

            var paoFrances = new Produto();
            paoFrances.Nome = "Pão Francês";
            paoFrances.PrecoUnitario = 0.40;
            paoFrances.Unidade = "Unidade";
            paoFrances.Categoria = "Padaria";

            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = paoFrances;
            compra.Preco = paoFrances.PrecoUnitario * compra.Quantidade;

            using (var contexto = new LojaContex())
            {
                contexto.Compras.Add(compra);

                contexto.SaveChanges();
            }
        }

        static void Relacionamento_N_N()
        {
            //Exemplo N:N

            var p1 = new Produto() { Nome = "Suco de Laranja", Categoria = "Bebidas", PrecoUnitario = 8.79, Unidade = "Litros" };
            var p2 = new Produto() { Nome = "Café", Categoria = "Bebidas", PrecoUnitario = 12.45, Unidade = "Gramas" };
            var p3 = new Produto() { Nome = "Macarrão", Categoria = "Alimentos", PrecoUnitario = 4.23, Unidade = "Gramas" };

            var promocaoDePascoa = new Promocao();
            promocaoDePascoa.Descricao = "Páscoa Feliz";
            promocaoDePascoa.DataInicio = DateTime.Now;
            promocaoDePascoa.DataTermino = DateTime.Now.AddMonths(3);

            promocaoDePascoa.IncluiProduto(p1);
            promocaoDePascoa.IncluiProduto(p2);
            promocaoDePascoa.IncluiProduto(p3);

            using (var contexto = new LojaContex())
            {
                var promocao = contexto.Promocoes.Find(1);

                contexto.Promocoes.Remove(promocao);

                //contexto.Promocoes.Add(promocaoDePascoa);
                contexto.SaveChanges();
            }
        }
    }
}
