using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.Loja.Testes.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //GravarUsandoAdoNet();
            //GravarUsandoEntity();

            //RecuperarProdutosUsandoEntity();
            //RecuperarProdutoUsandoEntity();

            //ExcluirTodosOsProdutosUsandoEntity();

            AtualizandoUsandoEntity();
        }

        private static void AtualizandoUsandoEntity()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                RecuperarProdutosUsandoEntity();
                Produto primeiro = contexto.Produtos().First();
                primeiro.Nome =  "2019 ano velho";

                contexto.Atualizar(primeiro);

                RecuperarProdutosUsandoEntity();
            }
        }

        private static void ExcluirTodosOsProdutosUsandoEntity()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> pLista = contexto.Produtos();

                foreach (var item in pLista)
                {
                    contexto.Remover(item);
                }
            }
        }

        private static void RecuperarProdutoUsandoEntity()
        {
            using (var contexto = new LojaContex())
            {
                IList<Produto> pLista = contexto.Produtos.Where(p => p.Preco > 21).ToList();

                foreach (var p in pLista)
                {
                    Console.WriteLine("RecuperarProdutoUsandoEntity - "+p.Nome+" - "+p.Preco);
                }
            }
        }

        private static void RecuperarProdutosUsandoEntity()
        {
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> plista = contexto.Produtos();

                if(plista.Count == 0)
                {
                    Console.WriteLine("Não tem livros!!!");
                    return;
                }

                foreach (var p in plista)
                {
                    Console.WriteLine("RecuperarProdutosUsandoEntity - " + p.Nome + " - " + p.Preco);
                }
            }
        }

        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "50 Tons de Cinza";
            p.Categoria = "Livros";
            p.Preco = 32.69;

            using (var contexto = new ProdutoDAOEntity())
            {
                contexto.Adicionar(p);
            }
        }
         
        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }
    }
}
