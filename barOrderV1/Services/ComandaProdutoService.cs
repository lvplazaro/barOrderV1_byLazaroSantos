using barOrderV1.Model;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Services
{
    public class ComandaProdutoService : IComandaProdutoService
    {
        private SQLiteAsyncConnection _dbConnection;

        private readonly IComandaService _comandaService;
        private readonly IProdutoService _produtoService;



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
                await _dbConnection.CreateTableAsync<ComandaProduto>();
            }
        }

        public ComandaProdutoService(IComandaService comandaService, IProdutoService produtoService)
        {
            _comandaService = comandaService;
            _produtoService = produtoService;
        }

        public async Task<List<ComandaModel>> GetComandas()
        {
            return await _comandaService.GetComandas();
        }

        public async Task<List<ProdutoModel>> GetProdutos()
        {
            return await _produtoService.GetProdutos();
        }

        public async Task<int> AdicionarProdutoAComanda(int comandaId, int produtoId)
        {         
                var comandaProduto = new ComandaProduto
                {
                    ComandaId = comandaId,
                    ProdutoId = produtoId,
                };

                return await _dbConnection.InsertAsync(comandaProduto);           
        }

        public async Task<int> RemoverProdutoDeComanda(int comandaId, int produtoId)
        {
            var comandaProduto = new ComandaProduto
            {
                ComandaId = comandaId,
                ProdutoId = produtoId,
            };

            return await _dbConnection.DeleteAsync(comandaProduto);
        }

        public async Task<List<ComandaProduto>> GetComandaProdutos()
        {
            return await _dbConnection.Table<ComandaProduto>().ToListAsync();
        }
    }
}
