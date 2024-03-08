using barOrderV1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Services
{
    public interface IComandaProdutoService
    {
        Task InitializeAsync();


        Task<List<ComandaModel>> GetComandas();
        Task<List<ProdutoModel>> GetProdutos();

        Task<List<ComandaProduto>> GetComandaProdutos();
        


        Task<int> AdicionarProdutoAComanda(int comandaId, int produtoId, int quantidadeDeProduto);
        Task<int> AdicionarQuantidadeProdutoAComanda(int comandaId, int produtoId, int quantidadeDeProduto);


        Task<int> RemoverProdutoDeComanda(int comandaId, int produtoId);
        Task<int> DiminuirQuantidadeProdutoAComanda(int comandaId, int produtoId, int quantidadeDeProduto);

    }
}
