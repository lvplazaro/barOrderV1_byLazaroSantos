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
    public class ComandaService : IComandaService
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
                await _dbConnection.CreateTableAsync<ComandaModel>();
            }
        }

        public async Task<List<ComandaModel>> GetComandas()
        {
            return await _dbConnection.Table<ComandaModel>().ToListAsync();
        }

        public async Task<int> AddComanda(ComandaModel comanda)
        {
            return await _dbConnection.InsertAsync(comanda);
        }

        public async Task<int> DeleteComanda(ComandaModel comanda)
        {
            return await _dbConnection.DeleteAsync(comanda);
        }

        public async Task<int> UpdateComanda(ComandaModel comanda)
        {
            return await _dbConnection.UpdateAsync(comanda);
        }
    }
}
