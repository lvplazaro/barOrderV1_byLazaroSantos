using barOrderV1.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Model
{
    internal class ProdutoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Preco { get; set; }
        public CategoriaProduto Categoria { get; set; }
        public int QuantidadeEstoque { get; set; }
        public int QuantidadeCritica { get; set; }
        public DateTime HoraAdicao { get; set; }
    }
}
