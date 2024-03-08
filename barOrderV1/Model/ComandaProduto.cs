using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Model
{
    public class ComandaProduto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int ComandaId { get; set; }

        public int ProdutoId { get; set; }

        public int QuantidadeDeProduto { get; set; }

    }
}
