using barOrderV1.Model;
using barOrderV1.Model.Enums;
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
        Task<List<ProdutoModel>> GetProdutosPorCategoria(CategoriaProduto CategoriaSelecionada);

        Task<ProdutoModel> GetProdutoPorId(int ProdutoId);

        Task<int> AddProduto(ProdutoModel produto);
        Task<int> UpdateProduto(ProdutoModel produto);
        Task<int> DeleteProduto(ProdutoModel produto);

    }
}
