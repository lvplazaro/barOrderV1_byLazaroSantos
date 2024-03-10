using barOrderV1.Model;
using Microsoft.Maui.Controls;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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

        public async Task<int> AdicionarProdutoAComanda(int comandaId, int produtoId, int QuantidadeDeProduto)
        {
            var comandaProduto = new ComandaProduto
            {
                ComandaId = comandaId,
                ProdutoId = produtoId,
                QuantidadeDeProduto = 1,
            };

            return await _dbConnection.InsertAsync(comandaProduto);
        }

        public async Task<int> RemoverProdutoDeComanda(int comandaId, int produtoId)
        {
            return await _dbConnection.ExecuteAsync("DELETE FROM ComandaProduto WHERE ComandaId = ? AND ProdutoId = ?", comandaId, produtoId);
        }


        public async Task<List<ComandaProduto>> GetComandaProdutos()
        {
            return await _dbConnection.Table<ComandaProduto>().ToListAsync();
        }

        public async Task<int> AdicionarQuantidadeProdutoAComanda(int comandaId, int produtoId, int quantidadeAdicional)
        {
            try
            {
                var comandaProduto = await _dbConnection.Table<ComandaProduto>()
                    .Where(cp => cp.ComandaId == comandaId && cp.ProdutoId == produtoId)
                    .FirstOrDefaultAsync();

                if (comandaProduto != null)
                {
                    comandaProduto.QuantidadeDeProduto += quantidadeAdicional;
                    return await _dbConnection.UpdateAsync(comandaProduto);
                }
                else
                {
                    throw new InvalidOperationException("O ComandaProduto especificado não foi encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar quantidade de produto à comanda: {ex.Message}");
                throw; 
            }
        }

        public async Task<int> DiminuirQuantidadeProdutoAComanda(int comandaId, int produtoId, int quantidadeAdicional)
        {
            try
            {
                var comandaProduto = await _dbConnection.Table<ComandaProduto>()
                    .Where(cp => cp.ComandaId == comandaId && cp.ProdutoId == produtoId)
                    .FirstOrDefaultAsync();

                if (comandaProduto != null)
                {
                    comandaProduto.QuantidadeDeProduto -= quantidadeAdicional;
                    return await _dbConnection.UpdateAsync(comandaProduto);
                }
                else
                {
                    throw new InvalidOperationException("O ComandaProduto especificado não foi encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar quantidade de produto à comanda: {ex.Message}");
                throw; 
            }
        }

        public async Task<int> DeletarComanda(int comandaId)
        {
            return await _dbConnection.ExecuteAsync("DELETE FROM ComandaProduto WHERE ComandaId = ?", comandaId);
        }
    }
}

