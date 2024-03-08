using barOrderV1.Model.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Model
{
    [Table("Comanda")]
    public class ComandaModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), NotNull]
        public string? Nome { get; set; }

        [MaxLength(10), NotNull]
        public DateTime DataAbertura { get; set; }

        [MaxLength(10)]
        public DateTime? DataFechamento { get; set; }

        [NotNull]
        public double Total { get; set; }

        public FormaPagamento? FormaDePagamento { get; set; }

        [NotNull]
        public ComandaStatus Status { get; set; }

        
    }
}
