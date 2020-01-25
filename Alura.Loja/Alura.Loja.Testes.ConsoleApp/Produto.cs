﻿using System.Collections.Generic;

namespace Alura.Loja.Testes.ConsoleApp
{
    public class Produto
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public double PrecoUnitario { get; internal set; }
        public string Unidade { get; internal set; }
        public IList<PromocaoProduto> Promocoes{ get; internal set; }

        public override string ToString()
        {
            return  string.Format("Produto: {0}\n" +
                                  "Preço:   {1}\n\n",this.Nome,this.PrecoUnitario);
        }
    }
}