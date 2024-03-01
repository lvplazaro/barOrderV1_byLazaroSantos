using barOrderV1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Services
{
    public interface IComandaService
    {
        Task InitializeAsync();

        Task<List<ComandaModel>> GetComandas();
        Task<int> AddComanda(ComandaModel comanda);
        Task<int> UpdateComanda(ComandaModel comanda);
        Task<int> DeleteComanda(ComandaModel comanda);
    }
}
