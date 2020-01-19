namespace Alura.Loja.Testes.ConsoleApp
{
    internal class Produto
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }
        public double Preco { get; internal set; }

        public override string ToString()
        {
            return  string.Format("Produto: {0}\n" +
                                  "Preço:   {1}\n\n",this.Nome,this.Preco);
        }
    }
}