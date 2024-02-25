using barOrderV1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Services
{
    public interface IProdutoService
    {
        Task InitializeAsync();

        Task<List<ProdutoModel>> GetProdutos();
        Task<int> AddProduto(ProdutoModel produto);
        Task<int> UpdateProduto(ProdutoModel produto);
        Task<int> DeleteProduto(ProdutoModel produto);
    }
}
