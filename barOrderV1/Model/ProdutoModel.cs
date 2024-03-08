using barOrderV1.Model.Enums;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barOrderV1.Model
{
    [Table("Produtos")]
    public class ProdutoModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), NotNull]
        public string Nome { get; set; }

        [NotNull]
        public double Preco { get; set; }

        [NotNull]
        public CategoriaProduto Categoria { get; set; }

        [NotNull]
        public int QuantidadeEstoque { get; set; }

        [NotNull]
        public int QuantidadeCritica { get; set; }

        [Ignore]
        public int QuantidadeDeProduto { get; set; }


    }


}
