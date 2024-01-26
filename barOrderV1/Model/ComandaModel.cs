using barOrderV1.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Model
{
    internal class ComandaModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public List<ProdutoModel> Produtos { get; set; } = new List<ProdutoModel>();
        public double Total { get; set; }
        public FormaPagamento FormaDePagamento { get; set; }
        public Dictionary<ProdutoModel, DateTime> ProdutosAdicionados { get; set; } = new Dictionary<ProdutoModel, DateTime>();
    }
}
