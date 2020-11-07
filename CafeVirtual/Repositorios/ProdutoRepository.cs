using CafeVirtual.data;
using CafeVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeVirtual.Repositorios
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationDbContext context;

        public ProdutoRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<Produto> GetProdutos()
        {
            return context.Produtos.ToList();
        }

        public Produto FindProduto(int id)
        {
            return context.Produtos.Find(id);
        }

        public void AddProduto(Produto produto)
        {
            context.Produtos.Add(produto);
            context.SaveChanges();
        }

        public bool InicialAdd(string nome)
        {
            return context.Produtos.ToList().Where(x => x.Nome == nome).Any();
        }
    }
}
