using barOrderV1.Model;
using barOrderV1.Model.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Services
{
    public class ProdutoService : IProdutoService
    {
        private SQLiteAsyncConnection _dbConnection;

        public async Task InitializeAsync()
        {
            await SetUpDb();
        }

        private async Task SetUpDb()
        {
            if (_dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.
                    GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "barOrderApp.db");

                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<ProdutoModel>();
            }
        }

        public async Task<List<ProdutoModel>> GetProdutos()
        {
            return await _dbConnection.Table<ProdutoModel>().ToListAsync();
        }

        public async Task<int> AddProduto(ProdutoModel produto)
        {
            return await _dbConnection.InsertAsync(produto);
        }

        public async Task<int> DeleteProduto(ProdutoModel produto)
        {
            return await _dbConnection.DeleteAsync(produto);
        }

        public async Task<int> UpdateProduto(ProdutoModel produto)
        {
            return await _dbConnection.UpdateAsync(produto);
        }

        public async Task<List<ProdutoModel>> GetProdutosPorCategoria(CategoriaProduto categoriaSelecionada)
        {
            try
            {
                var produtosFiltrados = await _dbConnection.Table<ProdutoModel>()
                                                         .Where(produto => produto.Categoria == categoriaSelecionada)
                                                         .ToListAsync();
                return produtosFiltrados;
            }
            catch (Exception ex)
            {
                // Trate a exceção conforme necessário
                Console.WriteLine($"Erro ao recuperar produtos por categoria: {ex.Message}");
                return new List<ProdutoModel>(); // Retorna uma lista vazia em caso de erro
            }
        }

        public async Task<ProdutoModel> GetProdutoPorId(int produtoId)
        {
            try
            {
                // Consulta o banco de dados para encontrar o produto com o ID fornecido
                var produto = await _dbConnection.Table<ProdutoModel>()
                                                .Where(p => p.Id == produtoId)
                                                .FirstOrDefaultAsync();

                // Retorna o produto encontrado (ou null se não encontrado)
                return produto;
            }
            catch (Exception ex)
            {
                // Trate qualquer exceção que possa ocorrer durante a consulta ao banco de dados
                Console.WriteLine($"Erro ao obter produto por ID: {ex.Message}");
                return null; // Retorna null em caso de erro
            }
        }

    }
}
