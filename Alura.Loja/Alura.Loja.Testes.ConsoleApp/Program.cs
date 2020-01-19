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
            using (var contexto = new ProdutoDAOEntity())
            {
                IList<Produto> listaProduto = contexto.Produtos().ToList();

                foreach (var p in listaProduto)
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }
    }
}
