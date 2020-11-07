using CafeVirtual.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CafeVirtual.Repositorios
{
    public interface IProdutoRepository
    {
        List<Produto> GetProdutos();
        Produto FindProduto(int id);

        bool InicialAdd(string nome);
        void AddProduto(Produto produto);
    }
}
